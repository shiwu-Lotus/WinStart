using System;
using System.Drawing;
using System.Windows.Forms;
using WinStart.App.Controls;
using WinStart.App.Managers;

namespace WinStart.App
{
    /// <summary>
    /// 启动器主窗体（类似 Java Swing 的 JFrame）
    /// </summary>
    public partial class LauncherForm : Form
    {
        private readonly FlowLayoutPanel panel;

        public LauncherForm()
        {
            // 从配置加载窗口参数
            Width = ConfigManager.UI.WindowWidth;
            Height = ConfigManager.UI.WindowHeight;
            Text = "WinStart 启动器";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // 设置背景样式
            BackColor = ColorTranslator.FromHtml(ConfigManager.Style.BackgroundColor);
            Opacity = ConfigManager.Style.Opacity;

            // 添加容器面板（FlowLayout 类似 Java 的 FlowLayoutPanel）
            panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(20)
            };
            Controls.Add(panel);

            // 加载启动项
            LoadLauncherItems();
        }

        /// <summary>
        /// 加载启动项（目前使用示例数据）
        /// </summary>
        private void LoadLauncherItems()
        {
            // 程序运行目录（bin\Debug\net9.0-windows）
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // assets 目录在项目根目录（往上三级）
            string assetDir = Path.Combine(baseDir, "..", "..", "..", "assets");

            var items = new[]
            {
                new { Name = "记事本", Icon = Path.Combine(assetDir, "notepad.png"), Path = "notepad.exe" },
                new { Name = "计算器", Icon = Path.Combine(assetDir, "calc.png"), Path = "calc.exe" },
                new { Name = "浏览器", Icon = Path.Combine(assetDir, "browser.png"), Path = "https://www.bing.com" }
            };

            foreach (var item in items)
            {
                try
                {
                    var control = new LauncherItemControl(item.Name, item.Icon, item.Path);
                    panel.Controls.Add(control);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载启动项 {item.Name} 失败: {ex.Message}");
                }
            }
        }
    }
}

