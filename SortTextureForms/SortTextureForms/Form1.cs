using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortTextureForms
{
    public partial class Form1 : Form
    {
        public static float imgWidth = 512f;
        public static float imgHeight = 512f;
        public static string[] patterns;
        public Form1()
        {
            ShowIcon = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            patterns = textBox1.Text.Split(';');
            float f1;
            if (!float.TryParse(width.Text, out f1))
            {
                Console.WriteLine("宽度输入不正确，请重新输入！");
                return;
            }
            imgWidth = f1;
            float f2;
            if (!float.TryParse(height.Text, out f2))
            {
                Console.WriteLine("高度输入不正确，请重新输入！");
                return;
            }
            imgHeight = f2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowser.OpenFolderBrowserDialog();
            Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float f2;
            if (!float.TryParse(textBox.Text, out f2))
            {
                Console.WriteLine("宽度输入不正确，请重新输入！");
                return;
            }
            imgWidth = f2;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            float f2;
            if (!float.TryParse(textBox.Text, out f2))
            {
                Console.WriteLine("高度输入不正确，请重新输入！");
                return;
            }
            imgHeight = f2;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            patterns = textBox.Text.Split(';');

        }
    }
}
