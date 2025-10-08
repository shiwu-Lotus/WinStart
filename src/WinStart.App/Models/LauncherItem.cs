namespace WinStart.App.Models
{
    /// <summary>
    /// 启动器中单个项目的配置项
    /// </summary>
    public class LauncherItem
    {
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Path { get; set; } = "";
    }
}
