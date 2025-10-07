using System;
using System.Drawing;
using System.Windows.Forms;
using WinStart.App.Controls;
using WinStart.App.Managers;

namespace WinStart.App
{
    /// <summary>
    /// �����������壨���� Java Swing �� JFrame��
    /// </summary>
    public partial class LauncherForm : Form
    {
        private readonly FlowLayoutPanel panel;

        public LauncherForm()
        {
            // �����ü��ش��ڲ���
            Width = ConfigManager.UI.WindowWidth;
            Height = ConfigManager.UI.WindowHeight;
            Text = "WinStart ������";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // ���ñ�����ʽ
            BackColor = ColorTranslator.FromHtml(ConfigManager.Style.BackgroundColor);
            Opacity = ConfigManager.Style.Opacity;

            // ���������壨FlowLayout ���� Java �� FlowLayoutPanel��
            panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Padding = new Padding(20)
            };
            Controls.Add(panel);

            // ����������
            LoadLauncherItems();
        }

        /// <summary>
        /// ���������Ŀǰʹ��ʾ�����ݣ�
        /// </summary>
        private void LoadLauncherItems()
        {
            // ��������Ŀ¼��bin\Debug\net9.0-windows��
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // assets Ŀ¼����Ŀ��Ŀ¼������������
            string assetDir = Path.Combine(baseDir, "..", "..", "..", "assets");

            var items = new[]
            {
                new { Name = "���±�", Icon = Path.Combine(assetDir, "notepad.png"), Path = "notepad.exe" },
                new { Name = "������", Icon = Path.Combine(assetDir, "calc.png"), Path = "calc.exe" },
                new { Name = "�����", Icon = Path.Combine(assetDir, "browser.png"), Path = "https://www.bing.com" }
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
                    MessageBox.Show($"���������� {item.Name} ʧ��: {ex.Message}");
                }
            }
        }
    }
}

