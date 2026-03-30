using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AssessorDesk
{
    public partial class AttachedFile : Form
    {
        public AttachedFile()
        {
            InitializeComponent();
            WireLocalHandlers();
        }

        // Convenience ctor so callers can set the header/title immediately
        public AttachedFile(string title) : this()
        {
            SetFileName(title);
        }

        private void WireLocalHandlers()
        {
            // Safe guards if designer didn't generate the controls for some reason
            if (ExitFileForm != null)
            {
                ExitFileForm.Click -= ExitFileForm_Click;
                ExitFileForm.Click += ExitFileForm_Click;
            }

            if (DownloadFile != null)
            {
                DownloadFile.Click -= DownloadFile_Click;
                DownloadFile.Click += DownloadFile_Click;
            }
        }

        // Public helper to set the label shown on the form
        public void SetFileName(string title)
        {
            if (FileName == null) return;
            FileName.Text = title ?? "File name";
        }

        // Close the form
        private void ExitFileForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Tries to download the currently selected attachment in the grid.
        // Supports:
        // - DataGridView cell containing an Image
        // - DataGridView cell containing byte[] (image bytes)
        // - DataGridView cell containing a file path (string) - copies the file
        private void DownloadFile_Click(object sender, EventArgs e)
        {
            if (FileAttachment == null)
            {
                MessageBox.Show("No attachments available.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = FileAttachment.CurrentRow;
            if (row == null)
            {
                MessageBox.Show("Select an attachment row first.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Find first non-null cell value to attempt to download from
            object value = null;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell?.Value != null)
                {
                    value = cell.Value;
                    break;
                }
            }

            if (value == null)
            {
                MessageBox.Show("Selected row contains no downloadable data.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save dialog
            using var sfd = new SaveFileDialog();
            sfd.Title = "Save attachment";
            sfd.OverwritePrompt = true;

            try
            {
                // If value is Image
                if (value is Image img)
                {
                    sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap|*.bmp";
                    sfd.FileName = $"{FileName?.Text ?? "attachment"}.png";
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;

                    var ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                    ImageFormat fmt = ImageFormat.Png;
                    if (ext == ".jpg" || ext == ".jpeg") fmt = ImageFormat.Jpeg;
                    else if (ext == ".bmp") fmt = ImageFormat.Bmp;

                    using var fs = File.OpenWrite(sfd.FileName);
                    img.Save(fs, fmt);
                    MessageBox.Show("Saved.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // If value is byte[] (image bytes)
                if (value is byte[] bytes)
                {
                    // Try to detect image format by creating Image from stream
                    using var ms = new MemoryStream(bytes);
                    using var fromStream = Image.FromStream(ms);
                    sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap|*.bmp";
                    sfd.FileName = $"{FileName?.Text ?? "attachment"}.png";
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;

                    var ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                    ImageFormat fmt = ImageFormat.Png;
                    if (ext == ".jpg" || ext == ".jpeg") fmt = ImageFormat.Jpeg;
                    else if (ext == ".bmp") fmt = ImageFormat.Bmp;

                    using var fs = File.OpenWrite(sfd.FileName);
                    fromStream.Save(fs, fmt);
                    MessageBox.Show("Saved.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // If value is string -> treat as path or base64
                if (value is string s)
                {
                    // If file exists on disk, copy it
                    if (File.Exists(s))
                    {
                        sfd.FileName = Path.GetFileName(s);
                        if (sfd.ShowDialog(this) != DialogResult.OK) return;
                        File.Copy(s, sfd.FileName, true);
                        MessageBox.Show("Saved.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Try treat as base64
                    try
                    {
                        var bytesFromBase64 = Convert.FromBase64String(s);
                        using var ms = new MemoryStream(bytesFromBase64);
                        using var fromBase64 = Image.FromStream(ms);
                        sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap|*.bmp";
                        sfd.FileName = $"{FileName?.Text ?? "attachment"}.png";
                        if (sfd.ShowDialog(this) != DialogResult.OK) return;

                        var ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        ImageFormat fmt = ImageFormat.Png;
                        if (ext == ".jpg" || ext == ".jpeg") fmt = ImageFormat.Jpeg;
                        else if (ext == ".bmp") fmt = ImageFormat.Bmp;

                        using var fs = File.OpenWrite(sfd.FileName);
                        fromBase64.Save(fs, fmt);
                        MessageBox.Show("Saved.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    catch
                    {
                        // not base64
                    }
                }

                // If value is a Stream-like object
                if (value is Stream streamVal)
                {
                    sfd.FileName = $"{FileName?.Text ?? "attachment"}.bin";
                    if (sfd.ShowDialog(this) != DialogResult.OK) return;
                    using var outFs = File.OpenWrite(sfd.FileName);
                    streamVal.Seek(0, SeekOrigin.Begin);
                    streamVal.CopyTo(outFs);
                    MessageBox.Show("Saved.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Selected cell type is not supported for download.", "Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to download: {ex.Message}", "Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
