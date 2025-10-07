using System;
using System.IO;
using System.Text.Json;
using WinStart.App.Models;

namespace WinStart.App.Managers
{
	public static class ConfigManager
	{
		private static readonly string ConfigDir = Path.Combine(AppContext.BaseDirectory, "Config");
		private static readonly string AppConfigPath = Path.Combine(ConfigDir, "appdata.json");
		private static readonly string UIConfigPath = Path.Combine(ConfigDir, "ui.json");
		private static readonly string StyleConfigPath = Path.Combine(ConfigDir, "style.json");

        public static AppConfig App { get; private set; } = null!;
        public static UIConfig UI { get; private set; } = null!;
        public static StyleConfig Style { get; private set; } = null!;


		public static void Initialize()
		{
			if (!Directory.Exists(ConfigDir))
				Directory.CreateDirectory(ConfigDir);

			App = LoadOrCreateConfig(AppConfigPath, new AppConfig());
			UI = LoadOrCreateConfig(UIConfigPath, new UIConfig());
			Style = LoadOrCreateConfig(StyleConfigPath, new StyleConfig());
		}

		private static T LoadOrCreateConfig<T>(string path, T defaultConfig)
		{
			try
			{
				if (File.Exists(path))
				{
					string json = File.ReadAllText(path);
					var config = JsonSerializer.Deserialize<T>(json);
					if (config != null) return config;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"º”‘ÿ≈‰÷√ ß∞‹£∫{path}\n{ex.Message}");
			}

			SaveConfig(path, defaultConfig);
			return defaultConfig;
		}

		public static void SaveAll()
		{
			SaveConfig(AppConfigPath, App);
			SaveConfig(UIConfigPath, UI);
			SaveConfig(StyleConfigPath, Style);
		}

		private static void SaveConfig<T>(string path, T config)
		{
			try
			{
				string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(path, json);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"±£¥Ê≈‰÷√ ß∞‹£∫{path}\n{ex.Message}");
			}
		}

		public static void MigrateConfigVersion(string newVersion)
		{
			if (App.Version != newVersion)
			{
				Console.WriteLine($"ºÏ≤‚µΩ∞Ê±æ±‰ªØ£∫{App.Version} °˙ {newVersion}");
				App.Version = newVersion;
				App.LastUpdate = DateTime.Now;
				SaveConfig(AppConfigPath, App);
			}
		}
	}
}
