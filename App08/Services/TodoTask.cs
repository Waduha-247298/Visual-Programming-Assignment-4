using System;

namespace App08.Services
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

