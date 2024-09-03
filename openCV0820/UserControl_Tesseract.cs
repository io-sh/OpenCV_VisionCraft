using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace openCV0820
{
    public partial class UserControl_Tesseract : UserControl
    {
        public delegate void TesseractDelegate();
        public event TesseractDelegate TesseractEvent;

        public delegate void endBTNDelegate();
        public event endBTNDelegate endBTNtEvent;

        public UserControl_Tesseract()
        {
            InitializeComponent();
        }
        public Label ResultLB
        {
            get { return resultLB; }
            set { resultLB = value; }
        }

        private void TesseractBTN_Click(object sender, EventArgs e)
        {
            TesseractEvent.Invoke();
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            endBTNtEvent.Invoke();
        }
    }
}
