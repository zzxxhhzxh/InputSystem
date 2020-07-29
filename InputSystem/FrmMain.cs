using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputSystem
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InputMonitor.KeyDown += OnKeyDown;
            InputMonitor.KeyUp += OnKeyUp;
            InputMonitor.MouseWheel += OnMouseWheel;
            InputMonitor.MouseDown += OnMouseDown;
            InputMonitor.MouseUp += OnMouseUp;
            icnNote.ContextMenuStrip = new ContextMenuStrip();
            icnNote.ContextMenuStrip.Items.Add("Start", null, TsmiStart_Click);
            icnNote.ContextMenuStrip.Items.Add("Stop", null, TsmiStop_Click);
            icnNote.ContextMenuStrip.Items.Add("Exit", null, TsmiExit_Click);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            InputMonitor.SubscribeGlobalEvents();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            InputMonitor.UnsubscribeGlobalEvents();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void BtnSimulate_Click(object sender, EventArgs e)
        {
            InputSimulate.MouseMove(new Point(200, 200));
            InputSimulate.MouseDown(MouseButtons.Left);
            InputSimulate.MouseUp(MouseButtons.Left);
            InputSimulate.KeyDown(Keys.A);
            InputSimulate.KeyUp(Keys.A);
            InputSimulate.KeyDown(Keys.Enter);
            InputSimulate.KeyUp(Keys.Enter);
            InputSimulate.Delay(2000);
            InputSimulate.KeyType("HELLO world");
            InputSimulate.KeyModifierPress(Keys.A, new Keys[] { Keys.ControlKey });
        }

        private void TsmiStart_Click(object sender, EventArgs e)
        {
            InputMonitor.SubscribeGlobalEvents();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void TsmiStop_Click(object sender, EventArgs e)
        {
            InputMonitor.UnsubscribeGlobalEvents();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void TsmiExit_Click(object sender, EventArgs e)
        {
            icnNote.Dispose();
            Application.Exit();
        }

        private void IcnNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                icnNote.ContextMenuStrip.Show();
            else if (e.Button == MouseButtons.Left)
                Show();
        }

        private void ChkKeyInput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKeyInput.Checked)
            {
                InputMonitor.KeyDown += OnKeyDown;
                InputMonitor.KeyUp += OnKeyUp;
            }
            else
            {
                InputMonitor.KeyDown -= OnKeyDown;
                InputMonitor.KeyUp -= OnKeyUp;
            }
        }

        private void ChkMouseInput_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMouseInput.Checked)
            {
                InputMonitor.MouseWheel += OnMouseWheel;
                InputMonitor.MouseDown += OnMouseDown;
                InputMonitor.MouseUp += OnMouseUp;
            }
            else
            {
                InputMonitor.MouseWheel -= OnMouseWheel;
                InputMonitor.MouseDown -= OnMouseDown;
                InputMonitor.MouseUp -= OnMouseUp;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [KeyDown] [{e.KeyCode}]\n");
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [KeyUp] [{e.KeyCode}]\n");
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [MouseWheel] [{e.Delta:000}]\n");
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [MouseMoveTo] [({e.X:0000}, {e.Y:0000})]\n");
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [MouseDown] [{e.Button}]\n");
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [MouseMoveTo] [({e.X:0000}, {e.Y:0000})]\n");
            rtxLog.AppendText($"{DateTime.Now:hh:mm:ss.fff}: [MouseUp] [{e.Button}]\n");
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}