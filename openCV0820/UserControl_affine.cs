using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;

namespace openCV0820
{
    public partial class UserControl_affine : UserControl
    {
        public delegate void confirmBTN_delegate();
        public event confirmBTN_delegate confirmBTN_event;

        public delegate void applyBTN_delegate();
        public event applyBTN_delegate applyBTN_event;

        public delegate void returnBTN_delegate();
        public event returnBTN_delegate returnBTN_event;

        public delegate void endBTN_delegate();
        public event endBTN_delegate endBTN_event;
        public UserControl_affine()
        {
            InitializeComponent();
        }
        public TextBox LuxTB
        {
            get { return luxTB; }
            set { luxTB = value; }
        }
        public TextBox LdxTB
        {
            get { return ldxTB; }
            set { ldxTB = value; }
        }
        public TextBox RuxTB
        {
            get { return ruxTB; }
            set { ruxTB = value; }
        }
        public TextBox RdxTB
        {
            get { return rdxTB; }
            set { rdxTB = value; }
        }
        public TextBox LuyTB
        {
            get { return luyTB; }
            set { luyTB = value; }
        }
        public TextBox LdyTB
        {
            get { return ldyTB; }
            set { ldyTB = value; }
        }
        public TextBox RuyTB
        {
            get { return ruyTB; }
            set { ruyTB = value; }
        }
        public TextBox RdyTB
        {
            get { return rdyTB; }
            set { rdyTB = value; }
        }

        private void confirmBTN_Click(object sender, EventArgs e)
        {
            confirmBTN_event.Invoke();
        }
        private void applyBTN_Click(object sender, EventArgs e)
        {
            applyBTN_event.Invoke();
        }

        private void returnBTN_Click(object sender, EventArgs e)
        {
            returnBTN_event.Invoke(); 
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            endBTN_event.Invoke();
        }
    }   
}
