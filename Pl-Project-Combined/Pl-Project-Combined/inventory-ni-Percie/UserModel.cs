// ═══════════════════════════════════════
// FILE: UserModel.cs
// ═══════════════════════════════════════
using System;

namespace inventory_ni_Percie
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}