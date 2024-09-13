using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAN0837_BP.Helper
{
    public static class MessageBoxHelper
    {
        public static bool errorMessageBoxShown;
        public static bool exceptionMessageBoxShown;

        public static void ShowErrorMessageBox(object sender, EventArgs e)
        {
            if (!errorMessageBoxShown)
            {
                errorMessageBoxShown = true;

                var stackTrace = new StackTrace(true);
                var frame = stackTrace.GetFrame(0);
                var file = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                string title = "Error MessageBox";
                string message;

                //MessageBox
                MessageBox.Show($"Error: Message\nFile: {file}\nLine: {line}\n", title,
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void ShowExceptionMessageBox(object sender, EventArgs e)
        {
            if (!exceptionMessageBoxShown)
            {
                exceptionMessageBoxShown = true;

                var stackTrace = new StackTrace(true);
                var frame = stackTrace.GetFrame(0);
                var file = frame.GetFileName();
                var line = frame.GetFileLineNumber();
                string title = "Exception MessageBox";
                string message;

                //MessageBox
                MessageBox.Show($"Error: Message\nFile: {file}\nLine: {line}\n", 
                    title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
