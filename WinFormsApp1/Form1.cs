namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private WinEventDelegate _winEventDelegate;
        public Form1(string name)
        {
            InitializeComponent(name);
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

            _winEventDelegate = new WinEventDelegate(WinEventProc);
            SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _winEventDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
