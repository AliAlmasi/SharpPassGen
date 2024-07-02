using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpPassGen {
    public partial class Form3: Form {
        public Form3() {
            InitializeComponent();

            ToolTip tp = new ToolTip();
            tp.SetToolTip(this.button2, "Close this window");
            tp.SetToolTip(this.button1, "https://github.com/AliAlmasi/SharpPassGen");
            tp.SetToolTip(this.button3, "https://github.com/AliAlmasi");
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/AliAlmasi");
        }

        private void button1_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/AliAlmasi/SharpPassGen");
        }
    }
}
