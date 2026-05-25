using System;

namespace App05.Services
{
    public class AuthenticationStateService
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string CurrentUser { get; private set; } = string.Empty;
        public event Action? OnStateChange;

        public void LogIn(string username)
        {
            IsAuthenticated = true;
            CurrentUser = username;
            NotifyStateChanged();
        }
        public void LogOut()
        {
            IsAuthenticated = false;
            CurrentUser = string.Empty;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
