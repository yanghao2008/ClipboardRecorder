using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Diagnostics;
using System.Collections;

 

namespace ClipboardRecorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clipboard.Clear();
            timer1.Enabled = checkBox1.Checked;
            //RegisterHotKey(Handle, 100, 0, Keys.F1);
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd,
        // handle to window 
        int id, // hot key identifier 
        KeyModifiers fsModifiers, // key-modifier options 
        Keys vk // virtual-key code 
        );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd,
        // handle to window 
        int id // hot key identifier 
        );


        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        private void ProcessHotkey()//���������
        {
            Clipboard.Clear();
            SendKeys.SendWait("^C");

        }
        protected override void WndProc(ref Message m)//ѭ������Windows��Ϣ
        {
            const int WM_HOTKEY = 0x0312;//����ݼ�
            switch (m.Msg)
            {
                case WM_HOTKEY:

                    ProcessHotkey();//�������������
                    break;
            }
            base.WndProc(ref m);
            
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UnregisterHotKey(Handle, 100);//ж�ؿ�ݼ�
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Clipboard.Clear();
            timer1.Enabled = checkBox1.Checked;
        }

        
        ArrayList buff = new ArrayList();
        string str="";
       private void timer1_Tick(object sender, EventArgs e)
        {

            string s = Clipboard.GetText();
            if (s == "") return;
            if (richTextBox1.Focused) return;
            if (checkBox4.Checked == false)
            {
                if (buff.IndexOf(s) < 0)
                {
                    buff.Add(s);
                    if (checkBox3.Checked == true)
                    {
                        richTextBox1.AppendText("#" + s + "\r\n");
                        str = s;
                        //���ı����ȡ����
                        //this.richTextBox1.Focus();
                        //���ù���λ�õ��ı�β
                        this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                        //�������ؼ���괦
                        this.richTextBox1.ScrollToCaret();

                    }
                    else
                    {
                        richTextBox1.AppendText(s + "\r\n");
                        str = s;
                        //���ù���λ�õ��ı�β
                        this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                        //�������ؼ���괦
                        this.richTextBox1.ScrollToCaret();

                    }
                }
            }
            else
            {
                if (str == s)
                { return; }
                else
                {
                    if (buff.IndexOf(s) < 0)
                    {
                        buff.Add(s);
                    }
                        if (checkBox3.Checked == true)
                    {
                        richTextBox1.AppendText("#" + s + "\r\n");
                        str = s;
                        //���ù���λ�õ��ı�β
                        this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                        //�������ؼ���괦
                        this.richTextBox1.ScrollToCaret();
                    }
                    else
                    {
                        richTextBox1.AppendText(s + "\r\n");
                        str = s;
                        //���ù���λ�õ��ı�β
                        this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                        //�������ؼ���괦
                        this.richTextBox1.ScrollToCaret();

                    }
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            { this.TopMost = false; }
            else
            { this.TopMost = true; }
            
        }


        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}