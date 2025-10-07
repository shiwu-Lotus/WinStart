using System;

namespace WinStart.App.Models
{
    public class AppConfig
    {
        public string Version { get; set; } = "1.0.0";
        public string Theme { get; set; } = "light";
        public string Language { get; set; } = "zh-CN";
        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
