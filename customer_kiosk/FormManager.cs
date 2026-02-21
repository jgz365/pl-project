using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace customer_kiosk
{
    /// <summary>
    /// Manages form navigation to prevent disposed object exceptions
    /// </summary>
    public static class FormManager
    {
        private static Stack<Form> formStack = new Stack<Form>();
        private static Form? currentForm = null;

        /// <summary>
        /// Navigate to a new form and hide the current one
        /// </summary>
        public static void NavigateTo(Form newForm)
        {
            if (currentForm != null && !currentForm.IsDisposed)
            {
                formStack.Push(currentForm);
                currentForm.Hide();
            }

            currentForm = newForm;
            currentForm.ShowDialog();

            // After dialog closes, go back to previous form
            if (formStack.Count > 0)
            {
                currentForm = formStack.Pop();
                if (currentForm != null && !currentForm.IsDisposed)
                {
                    currentForm.Show();
                }
            }
        }

        /// <summary>
        /// Go back to the previous form
        /// </summary>
        public static void GoBack()
        {
            if (currentForm != null && !currentForm.IsDisposed)
            {
                currentForm.Close();
            }

            if (formStack.Count > 0)
            {
                currentForm = formStack.Pop();
                if (currentForm != null && !currentForm.IsDisposed)
                {
                    currentForm.Show();
                }
            }
        }

        /// <summary>
        /// Get the current active form
        /// </summary>
        public static Form? GetCurrentForm()
        {
            return currentForm;
        }

        /// <summary>
        /// Clear the form stack (use when exiting application)
        /// </summary>
        public static void ClearStack()
        {
            while (formStack.Count > 0)
            {
                var form = formStack.Pop();
                if (!form.IsDisposed)
                {
                    form.Dispose();
                }
            }
        }
    }
}
