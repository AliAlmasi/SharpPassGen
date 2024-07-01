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
