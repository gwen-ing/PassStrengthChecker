using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Checker
{
    public partial class Form1 : Form
    {
        public const int nclbuttondown = 0x00A1;
        public const int caption = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, nclbuttondown, caption, 0);
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Mini_Click(object sender, EventArgs e)
        {
        if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
          else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Multi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int points = 0;
            string check = textBox1.Text;

            var tuple = (lowercase: 0, uppercase: 0, digits: 0, symbols: 0, repeat: 1);

            foreach (var o in check)
            {
                if (char.IsLower(o))
                {
                    tuple.lowercase++;
                }
                if (char.IsUpper(o))
                {
                    tuple.uppercase++;
                }
                if (char.IsDigit(o))
                {
                    tuple.digits++;
                }
                if (!char.IsLetterOrDigit(o))
                {
                    tuple.symbols++;
                }
            }

            if (tuple.lowercase >= 1)
            {
                points++;
            }

            if (tuple.uppercase >= 1)
            {
                points++;
            }

            if (tuple.digits >= 3)
            {
                points++;
            }

            if (tuple.symbols >= 1)
            {
                points++;
            }

            if (check.Length >= 8)
            {
                points++;
            }

            if (check.Length >= 12)
            {
                points++;
            }

            bool hasrepeat = false;

            for (int i = 1; i < check.Length; i++)
            {
                if (check[i] == check[i - 1])
                {
                    tuple.repeat++;
                    if (tuple.repeat >= 6)
                    {
                        hasrepeat = true;
                    }
                }
                else
                {
                    tuple.repeat = 1;
                }
            }

            bool lowerisin = tuple.lowercase > 0;
            bool upperisin = tuple.uppercase > 0;
            bool digitsisin = tuple.digits > 0;
            bool symbolsisin = tuple.symbols > 0;

            double charsetsizing = 0;

            if (tuple.lowercase > 0) 
            {
                charsetsizing += 26;
            }
            if (tuple.uppercase > 0) 
            {
                charsetsizing += 26;
            }
            if (tuple.digits > 0) 
            {
                charsetsizing += 10;
            }
            if (tuple.symbols > 0) 
            {
                charsetsizing += 32;
            }

            int entropepoints = 0;
            double entropy = check.Length * Math.Log(charsetsizing) / Math.Log(2);

            if (entropy < 26){
                entropepoints = 0;
            }

            else if (entropy < 36)
            {
                entropepoints = 1;
            }

            else if (entropy < 60)
            {
                entropepoints = 2;
            }

            else if (entropy < 128)
            {
                entropepoints = 3;
            }
            else
            {
                entropepoints = 4;
            }

            points += entropepoints;


            if (points <= 1)
            {
                checker1.Text = "very weak";
            }
            else if (points == 2)
            {
                checker1.Text = "weak";
            }
            else if (points == 3)
            {
                checker1.Text = "medium";
            }
            else if (points == 4)
            {
                checker1.Text = "kinda strong";
            }
            else if (points == 5)
            {
                checker1.Text = "strong";
            }
            else if (points == 6)
            {
                checker1.Text = "very strong";
            }

            if (check.Length == 0)
            {
                checker1.Text = "checker";
                return;
            }

            if (hasrepeat)
            {
                checker1.Text = "words are repeating";
            }

            string[] explicity = {"bitch", "asshole", "shit", "fuck", "cunt"};

            string lowerpass = check.ToLower();

            foreach (string passlowered in explicity)
            {
                if (lowerpass.Contains(passlowered))
                {
                    checker1.Text = "No bad words";
                }
            }
            

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.UseSystemPasswordChar == false)
            {
                textBox1.UseSystemPasswordChar = true;
            }
            else
            {
                textBox1.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
