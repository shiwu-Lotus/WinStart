using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinStart.App.Services
{
    /// <summary>
    /// 启动器核心服务：负责启动本地应用、打开网址或文件夹。
    /// 类似 Java 中的工具类（Utility Class）
    /// </summary>
    public static class LaunchService
    {
        /// <summary>
        /// 启动指定路径或网址。
        /// </summary>
        public static void Launch(string target)
        {
            try
            {
                // 判断是否为网址（http 或 https 开头）
                if (target.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = target,
                        UseShellExecute = true
                    });
                }
                else
                {
                    // 视为本地应用或文件夹
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = target,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动：{target}\n错误：{ex.Message}", "启动失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
