using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpPassGen {
    public partial class Form2: Form {
        public Form2() {
            InitializeComponent();

            new ToolTip().SetToolTip(this.button2, "Close this window");
        }

        private void Form2_Load(object sender, EventArgs e) {
            textBox1.Text = Program.history;
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            Program.history = "";
            textBox1.Text = "";
        }
    }
}
