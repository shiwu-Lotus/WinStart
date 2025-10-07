using System;
using System.Drawing;
using System.Windows.Forms;
using WinStart.App.Services;

namespace WinStart.App.Controls
{
    /// <summary>
    /// 启动器的单个项目组件（如“浏览器”、“笔记本”等）
    /// </summary>
    public class LauncherItemControl : Panel
    {
        private readonly PictureBox icon;
        private readonly Label label;
        private string launchPath;

        public LauncherItemControl(string name, string iconPath, string launchPath)
        {
            this.launchPath = launchPath;

            // 设置整体样式
            Width = 100;
            Height = 120;
            Margin = new Padding(10);
            BackColor = Color.FromArgb(40, 40, 40);
            BorderStyle = BorderStyle.FixedSingle;
            Cursor = Cursors.Hand;

            // 图标
            icon = new PictureBox
            {
                Image = Image.FromFile(iconPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 64,
                Height = 64,
                Top = 10,
                Left = (Width - 64) / 2
            };
            Controls.Add(icon);

            // 名称
            label = new Label
            {
                Text = name,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                Height = 30
            };
            Controls.Add(label);

            // 点击事件（打开程序/网址）
            Click += (s, e) => LaunchService.Launch(launchPath);
            icon.Click += (s, e) => LaunchService.Launch(launchPath);
            label.Click += (s, e) => LaunchService.Launch(launchPath);

            // 鼠标悬停效果
            MouseEnter += (s, e) => BackColor = Color.FromArgb(60, 60, 60);
            MouseLeave += (s, e) => BackColor = Color.FromArgb(40, 40, 40);
        }
    }
}
