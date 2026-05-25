using System;

namespace App06.Models
{
    public class NotificationConfig
    {
        public int DefaultNumberOfNotifications { get; set; } = 3;
        public string NotificationStyle { get; set; } = "Compact"; // Options: "Compact" or "Detailed"

        // Event to notify display components when settings are updated
        public event Action? OnConfigChanged;

        public void NotifyChanged() => OnConfigChanged?.Invoke();
    }
}