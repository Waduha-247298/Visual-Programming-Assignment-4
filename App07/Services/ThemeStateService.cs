using System;

namespace App07.Services
{
    public class ThemeStateService
    {
        public bool IsDarkMode { get; private set; } = false;
        public event Action? OnThemeChanged;

        public void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            OnThemeChanged?.Invoke();
        }
    }
}
