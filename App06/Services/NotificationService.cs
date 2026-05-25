using System.Collections.Generic;
using System.Threading.Tasks;
using App06.Models;

namespace App06.Services
{
    public class NotificationService
    {
        private readonly NotificationConfig _config;

        // Constructor Injection of our configuration singleton dependency
        public NotificationService(NotificationConfig config)
        {
            _config = config;
        }

        public string GetCurrentStyle() => _config.NotificationStyle;

        public async Task<List<string>> GetNotificationsAsync(int? numberOfNotifications = null)
        {
            // Simulate asynchronous database/API fetching logic delay
            await Task.Delay(100);

            // Use user-provided override count, otherwise fall back to DI injected configuration default
            int count = numberOfNotifications ?? _config.DefaultNumberOfNotifications;
            if (count < 0) count = 0;

            var list = new List<string>();
            string styleSuffix = _config.NotificationStyle == "Detailed"
                ? " [Logged from System Core Node Delta at " + System.DateTime.Now.ToString("hh:mm tt") + "]"
                : "";

            for (int i = 1; i <= count; i++)
            {
                list.Add("System Alert Alert-0" + i + ": Internal data pipeline synchronized successfully." + styleSuffix);
            }

            return list;
        }
    }
}