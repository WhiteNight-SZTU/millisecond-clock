namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string name = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--name")
                {
                    name = args[i + 1];
                }
            }
            Application.Run(new Form1(name));
        }
    }
}