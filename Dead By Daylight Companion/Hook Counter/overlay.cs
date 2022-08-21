﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dead_By_Daylight_Companion.Hook_Counter {
    public partial class overlay : Form {
        public overlay() {
            InitializeComponent();
        }
        public static bool bHasDrawn = false;
        public static List<float> sList = new List<float>();
        public static List<float> _2List = new List<float>();
        public static Graphics G; 
        [DllImport("user32.dll", SetLastError = true)] public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void overlay_Load(object sender, EventArgs e) {
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = Screen.PrimaryScreen.Bounds.Width / 5;
            this.ShowInTaskbar = false;
            this.Location = new Point(0, 0);
            this.TopMost = true;
            int initStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initStyle | 0x8000 | 0x20);

            DrawTimer.Start();
        }

        private void DrawTimer_Tick(object sender, EventArgs e) {
            if (!bHasDrawn) {
                G = this.CreateGraphics();
                for (int i = 0; i < Hook_Counter.hCount.Count; i++) {
                    float f = float.Parse(Hook_Counter.hCount[i]);
                    if (!sList.Contains(f)) {
                        sList.Add(f);
                        G.DrawString(Hook_Counter.HookText, new Font(Hook_Counter.CurFontName, Hook_Counter.CurFontSize), new SolidBrush(Color.White), 170.0F, f);
                    }
                }
                if (Hook_Counter.HookText == "I") {
                    for (int j = 0; j < Hook_Counter._2stage.Count; j++) {
                        float fl = float.Parse(Hook_Counter._2stage[j]);
                        if (!_2List.Contains(fl)) {
                            _2List.Add(fl);
                            G.DrawString("I", new Font(Hook_Counter.CurFontName, Hook_Counter.CurFontSize), new SolidBrush(Color.White), 178.0F, fl - 47.0F);
                        }
                    }
                }
                bHasDrawn = true;
            }
        }
    }
}