using System;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
namespace WinFormsApp1
{
    partial class Form1 : Form
    {
        private Label labelTime;
        private System.Timers.Timer timer;

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;

        const uint EVENT_SYSTEM_FOREGROUND = 0x0003;

        const uint WINEVENT_OUTOFCONTEXT = 0x0000;

        void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (!this.TopMost)
            {
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
        }

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private string displayName;
        private void InitializeComponent(string name)
        {
            this.ShowIcon = false;
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 100);
            this.Text = " ";
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2, 0);
            this.labelTime = new Label();
            this.labelTime.Size = new Size(800, 450);
            this.labelTime.TextAlign = ContentAlignment.MiddleCenter;
            this.displayName = name;
            int margin = 5;

            this.labelTime.Location = new Point(margin, margin);
            this.labelTime.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 2 * margin);

            float fontSize = Math.Max(15, Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 10);
            this.labelTime.Font = new Font("微软雅黑", fontSize, FontStyle.Bold);

            this.labelTime.AutoSize = false;

            this.Controls.Add(this.labelTime);

            this.timer = new System.Timers.Timer();
            this.timer.Interval = 1;
            this.timer.Elapsed += new ElapsedEventHandler(UpdateLabelTime);
            this.timer.Start();
            this.MinimumSize = new Size(20, 30);

            this.Resize += new EventHandler(UpdateLabelPosition);
            this.TopMost= true;
        }
        private string GetLocalMachineName()
        {
            return Environment.MachineName;
        }
        private void UpdateLabelTime(object sender, EventArgs e)
        {
            // 由于这个方法不是在UI线程执行的，所以需要使用Invoke来更新UI
            this.Invoke(new Action(() =>
            {
                string machineName = GetLocalMachineName();
                if (this.displayName != "" && this.displayName != machineName) 
                { 
                    this.labelTime.Text = this.displayName + ":" + DateTime.Now.ToString("HH:mm:ss:fff");
                }
                else
                {
                    this.labelTime.Text = GetLocalMachineName() + ":" + DateTime.Now.ToString("HH:mm:ss:fff");
                }
                this.TopMost = true;
            }));
        }

        private void UpdateLabelPosition(object sender, EventArgs e)
        {
            int margin = 5;

            this.labelTime.Location = new Point(margin, margin);
            this.labelTime.Size = new Size(this.ClientSize.Width - 2 * margin, this.ClientSize.Height - 2 * margin);

            // 根据窗口的大小动态调整字体大小，最小值为1
            float fontSize = Math.Max(15, Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 10);
            this.labelTime.Font = new Font("微软雅黑", fontSize, FontStyle.Bold);
        }
    }
}