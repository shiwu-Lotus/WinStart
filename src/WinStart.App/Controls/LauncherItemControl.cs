using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinStart.App.Services;

namespace WinStart.App.Controls
{
    public class LauncherItemControl : Panel
    {
        private readonly PictureBox icon;
        private readonly Label label;
        private readonly string launchPath;

        public LauncherItemControl(string name, string? iconPath, string launchPath)
        {
            this.launchPath = launchPath;

            Width = 100;
            Height = 120;
            Margin = new Padding(10);
            BackColor = Color.FromArgb(40, 40, 40);
            BorderStyle = BorderStyle.FixedSingle;
            Cursor = Cursors.Hand;

            Image? img = null;

            try
            {
                if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath))
                {
                    img = Image.FromFile(iconPath);
                }
                else if (File.Exists(launchPath) && Path.GetExtension(launchPath).Equals(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    Icon extracted = Icon.ExtractAssociatedIcon(launchPath);
                    img = extracted?.ToBitmap();

                    // 缓存到 assets
                    string assetsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets");
                    Directory.CreateDirectory(assetsDir);
                    string iconFile = Path.Combine(assetsDir, $"{Path.GetFileNameWithoutExtension(launchPath)}.png");
                    img?.Save(iconFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图标提取失败: {launchPath} ({ex.Message})");
            }

            icon = new PictureBox
            {
                Image = img != null ? new Bitmap(img, new Size(64, 64)) : null,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 64,
                Height = 64,
                Top = 10,
                Left = (Width - 64) / 2,
                BackColor = img == null ? Color.DimGray : Color.Transparent
            };
            Controls.Add(icon);

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

            Click += (s, e) => LaunchService.Launch(launchPath);
            icon.Click += (s, e) => LaunchService.Launch(launchPath);
            label.Click += (s, e) => LaunchService.Launch(launchPath);

            MouseEnter += (s, e) => BackColor = Color.FromArgb(60, 60, 60);
            MouseLeave += (s, e) => BackColor = Color.FromArgb(40, 40, 40);
        }
    }
}
