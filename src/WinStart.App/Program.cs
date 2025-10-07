using WinStart.App.Managers;

namespace WinStart.App;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        // 初始化配置管理器
        ConfigManager.Initialize();

        // 检查版本升级
        ConfigManager.MigrateConfigVersion("1.0.0");
        Application.Run(new LauncherForm());
    }    
}