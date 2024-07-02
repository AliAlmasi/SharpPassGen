using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace SharpPassGen {
    public partial class Form1: Form {
        static RNGCryptoServiceProvider entropyProvider = new RNGCryptoServiceProvider();
        public Form1() {
            InitializeComponent();

            ToolTip tp = new ToolTip();
            tp.SetToolTip(this.label1, "About this program");
            tp.SetToolTip(this.textBox1, "Click to copy");
            tp.SetToolTip(this.checkBox1, "From \"a\" to \"z\"\n(Enabled by default)");
            tp.SetToolTip(this.checkBox2, "From \"A\" to \"Z\"");
            tp.SetToolTip(this.checkBox3, "From \"0\" to \"9\"");
            tp.SetToolTip(this.checkBox4, "All special characters like !@#$%&");
            tp.SetToolTip(this.button1, "Click to generate password");
            tp.SetToolTip(this.numericUpDown1, "How many characters do you want in your password?");
            tp.SetToolTip(this.label2, "How many characters do you want in your password?\n(Default is 8)");
            tp.SetToolTip(this.button2, "Good luck, mate");
        }

        private void Form1_Load(object sender, EventArgs e) {
            try {
                numericUpDown1.Value = Properties.Settings.Default.PasswordLength;
            } catch (Exception) {
                numericUpDown1.Value = 8;
            }
        }

        private bool small_char = true;
        private bool capital_char = false;
        private bool number_char = false;
        private bool special_char = false;
        private int length = 8;

        private string PasswordChars() {
            string small = "abcdefghijklmnopqrstuvwxyz";
            string capital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string special = "!@#$%^&*()_+/*-+.\\<>[]{}:;\"'";
            string collection = "\x0";

            try {
                if (small_char) {
                    if (capital_char) {
                        if (number_char) {
                            if (special_char) collection = small + capital + numbers + special;
                            else collection = small + capital + numbers;
                        } else collection = small + capital;
                    } else collection = small;
                    if (number_char) {
                        if (special_char) collection = small + numbers + special;
                        else collection = small + numbers;
                    }
                    if (special_char) collection = small + special;
                } else if (capital_char) {
                    if (number_char) {
                        if (special_char) collection = capital + numbers + special;
                        else collection = capital + numbers;
                    } else collection = capital;
                    if (special_char) collection = capital + special;
                } else if (number_char) {
                    if (special_char) collection = numbers + special;
                    else collection = numbers;
                } else if (special_char) collection = special;
            } catch (Exception ex) {
                Program.showError("Error while checking what type of characters you want in your password.", ex.Message);
            }

            return collection;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Cursor = Cursors.SizeAll;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e) {
            try {
                if (textBox1.Text.Length == 0 || textBox1.Text == "Password") {
                    Program.showError("Generate a password first.");
                } else {
                    Clipboard.SetText(textBox1.Text);
                    MessageBox.Show("Password copied.", "SharpPassGen");
                }
            } catch (Exception ex) {
                Program.showError("Error occured while copying the generated password.", ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            small_char = !small_char;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            capital_char = !capital_char;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            number_char = !number_char;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            special_char = !special_char;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (!small_char && !capital_char && !number_char && !special_char) {
                Program.showError("Cannot create a password with no letters or characters in it.", "Generating a password with the default 8 characters with small letters.");
                checkBox1.Checked = true;
                small_char = true;
                numericUpDown1.Value = 8;
                Properties.Settings.Default.PasswordLength = 8;
                Properties.Settings.Default.Save();
            }
            textBox1.Visible = true;
            if (timer1.Enabled) {
                timer1.Enabled = false;
                timer1.Enabled = true;
            } else timer1.Enabled = true;
            length = (int) (numericUpDown1.Value);
            if (length > 128) Program.showError("Wow! that's a lot of characters in a password.");
            if (length == 0) Program.showError("Cannot create a password with zero length.");

            StringBuilder password = new StringBuilder();
            for (int n = 0; n < length; n++)
                password = password.Append(GenerateChar(PasswordChars()));

            try {
                Program.history += password.ToString() + Environment.NewLine + "~~~~~" + Environment.NewLine;
                textBox1.Text = password.ToString();
            } catch (Exception ex) {
                Program.showError("Error while showing the generated password.", ex.Message);
            }
        }

        private static char GenerateChar(string availableChars) {
            byte[] byteArray = new byte[1];
            char c;
            do {
                entropyProvider.GetBytes(byteArray);
                c = (char) byteArray[0];
            } while (!availableChars.Any(x => x == c));
            return c;
        }

        private void button2_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Clipboard.Clear();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e) {
            new Form3().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            Properties.Settings.Default.PasswordLength = numericUpDown1.Value;
            Properties.Settings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            textBox1.Visible = false;
            timer1.Enabled = false;
        }

        private void showhistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            new Form2().ShowDialog();
        }
    }
}
