using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SharpPassGen {
    static class Program {
        public static string history = "";
        private static Mutex mutex = null;
        [STAThread]
        static void Main() {
            bool createdNew;
            mutex = new Mutex(true, Application.ProductName, out createdNew);
            if (!createdNew) {
                showError("Only one instance of this app is allowed.");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void showError(string text = "Runtime error\n\nYou can Inform the developer by submitting an issue in GitHub repo.", string exceptionMessage = "") {
            if (exceptionMessage.Length == 0) MessageBox.Show(text, "SharpPassGen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show(text + Environment.NewLine + Environment.NewLine + exceptionMessage, "SharpPassGen", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
