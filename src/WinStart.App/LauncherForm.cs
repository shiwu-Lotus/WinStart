using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinStart.App.Controls;
using WinStart.App.Managers;

namespace WinStart.App
{
    public partial class LauncherForm : Form
    {
        private readonly FlowLayoutPanel panel;
        private readonly Button btnAddItem;
        private readonly List<LauncherItemManager.LauncherItem> items;

        public LauncherForm()
        {
            Width = ConfigManager.UI.WindowWidth;
            Height = ConfigManager.UI.WindowHeight;
            Text = "WinStart 启动器";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            BackColor = ColorTranslator.FromHtml(ConfigManager.Style.BackgroundColor);
            Opacity = ConfigManager.Style.Opacity;

            panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(20)
            };
            Controls.Add(panel);

            btnAddItem = new Button
            {
                Text = "? 新增启动项",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddItem.FlatAppearance.BorderSize = 0;
            btnAddItem.Click += BtnAddItem_Click;
            Controls.Add(btnAddItem);

            items = LauncherItemManager.LoadItems();

            if (items.Count == 0)
                LoadDefaultItems();

            LoadLauncherItems();
        }

        private void LoadLauncherItems()
        {
            panel.Controls.Clear();
            foreach (var item in items)
            {
                try
                {
                    var control = new LauncherItemControl(item.Name, item.IconPath, item.LaunchPath);
                    panel.Controls.Add(control);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载启动项 {item.Name} 失败: {ex.Message}");
                }
            }
        }

        private void LoadDefaultItems()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string assetDir = Path.Combine(baseDir, "..", "..", "..", "assets");

            items.AddRange(new[]
            {
                new LauncherItemManager.LauncherItem
                {
                    Name = "记事本",
                    IconPath = Path.Combine(assetDir, "notepad.png"),
                    LaunchPath = "notepad.exe"
                },
                new LauncherItemManager.LauncherItem
                {
                    Name = "计算器",
                    IconPath = Path.Combine(assetDir, "calc.png"),
                    LaunchPath = "calc.exe"
                },
                new LauncherItemManager.LauncherItem
                {
                    Name = "浏览器",
                    IconPath = Path.Combine(assetDir, "browser.png"),
                    LaunchPath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
                }
            });
        }

        private void BtnAddItem_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "可执行文件 (*.exe)|*.exe",
                Title = "选择启动项程序"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string exePath = dialog.FileName;
                string name = Path.GetFileNameWithoutExtension(exePath);
                string assetsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "assets");
                Directory.CreateDirectory(assetsDir);

                try
                {
                    // 添加并保存
                    var newItem = new LauncherItemManager.LauncherItem
                    {
                        Name = name,
                        IconPath = "",
                        LaunchPath = exePath
                    };
                    items.Add(newItem);

                    LauncherItemManager.SaveItems(items);
                    LoadLauncherItems();
                    MessageBox.Show($"已添加启动项：{name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"添加启动项失败: {ex.Message}");
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            LauncherItemManager.SaveItems(items);
        }
    }
}
