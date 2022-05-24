using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace sql_neural
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName($"{System.IO.Path.GetFileName(Application.ExecutablePath).Replace(".exe", "")}").Length == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form main = new MainForm();
                main.Text = "Простая игра";
                Application.Run(main);
            }
            else
            {
                MessageBox.Show("Данная программа уже открыта");
            }
        }
    }
}
