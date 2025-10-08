using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinStart.App.Managers
{
    public class LauncherItemManager
    {
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "launcher_items.json"
        );

        public class LauncherItem
        {
            public string Name { get; set; } = "";
            public string IconPath { get; set; } = "";
            public string LaunchPath { get; set; } = "";
        }

        public static List<LauncherItem> LoadItems()
        {
            if (!File.Exists(ConfigFilePath)) return new List<LauncherItem>();

            try
            {
                string json = File.ReadAllText(ConfigFilePath);
                return JsonSerializer.Deserialize<List<LauncherItem>>(json)
                    ?? new List<LauncherItem>();
            }
            catch
            {
                return new List<LauncherItem>();
            }
        }

        public static void SaveItems(List<LauncherItem> items)
        {
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
