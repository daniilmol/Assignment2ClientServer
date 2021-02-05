using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    static class Program
    {
        public static int PORT = 49452;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("before 1");
            Application.EnableVisualStyles();
            Console.WriteLine("before2");
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("before 3");
            Application.Run(new Form1());
        }
    }
}
