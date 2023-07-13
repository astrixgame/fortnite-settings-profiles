using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortniteSettingsProfile
{
    public partial class Form1 : Form
    {
        public string profilesPath = @"C:\Users\"+Environment.UserName+@"\AppData\Local\FortniteProfiles";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(Directory.Exists(profilesPath))
            {
                string[] dirs = Directory.GetDirectories(profilesPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(dir.Replace(profilesPath+@"\",""));
                }
            } else
            {
                Directory.CreateDirectory(profilesPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newProfileName = Prompt.ShowDialog("Profile name...", "Enter profile name...");
            Directory.CreateDirectory(profilesPath+@"\"+newProfileName);
            File.Copy(@"C:\Users\"+Environment.UserName+@"\AppData\Local\FortniteGame\Saved\Config\WindowsClient\GameUserSettings.ini",profilesPath+@"\"+newProfileName+@"\GameUserSettings.ini");
            listBox1.Items.Clear();
            string[] dirs = Directory.GetDirectories(profilesPath, "*", SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                listBox1.Items.Add(dir.Replace(profilesPath+@"\",""));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItems.Count>0)
            {
                File.Delete(profilesPath + @"\" + listBox1.SelectedItem.ToString() + @"\GameUserSettings.ini");
                Directory.Delete(profilesPath + @"\" + listBox1.SelectedItem.ToString());
                listBox1.Items.Clear();
                string[] dirs = Directory.GetDirectories(profilesPath, "*", SearchOption.TopDirectoryOnly);
                foreach (string dir in dirs)
                {
                    listBox1.Items.Add(dir.Replace(profilesPath + @"\", ""));
                }
            } else
            {
                MessageBox.Show("You don't have selected profile!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\FortniteGame\Saved\Config\WindowsClient\GameUserSettings.ini");
                File.Copy(profilesPath + @"\" + listBox1.SelectedItem.ToString() + @"\GameUserSettings.ini", @"C:\Users\" + Environment.UserName + @"\AppData\Local\FortniteGame\Saved\Config\WindowsClient\GameUserSettings.ini");
            } else
            {
                MessageBox.Show("You don't have selected profile!");
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
