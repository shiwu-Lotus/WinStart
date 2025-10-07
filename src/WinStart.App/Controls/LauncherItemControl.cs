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

            try
            {
                using (var img = Image.FromFile(iconPath))
                {
                    icon = new PictureBox
                    {
                        Image = new Bitmap(img, new Size(64, 64)), // 强制缩放为 64×64
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = 64,
                        Height = 64,
                        Top = 10,
                        Left = (Width - 64) / 2
                    };
                }
            }
            catch (Exception ex)
            {
                // 如果图片加载失败，显示占位符
                icon = new PictureBox
                {
                    BackColor = Color.DimGray,
                    Size = new Size(64, 64),
                    Top = 10,
                    Left = (Width - 64) / 2
                };

                Label err = new Label
                {
                    Text = "无图",
                    ForeColor = Color.Red,
                    Dock = DockStyle.Bottom,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(err);

                Console.WriteLine($"图标加载失败: {iconPath} ({ex.Message})");
            }



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
