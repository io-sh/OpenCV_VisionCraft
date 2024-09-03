using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openCV0820
{
    public partial class UserControl_color : UserControl
    {
        public delegate void colorBtnDelegate();
        public event colorBtnDelegate colorBtnEvent;

        public delegate void applyBtnDelegate();
        public event applyBtnDelegate applyBtnEvent;

        public delegate void endBtnDelegate();
        public event endBtnDelegate endBtnEvent;

        string colorText;
        public string ColorText
        {
            get {  return colorText; }
            set {  colorText = value; }
        }
        public UserControl_color()
        {
            InitializeComponent();
        }
        private void colorBTN_Click(object sender, EventArgs e)
        {
            Button colorBTN = (Button)sender;
            colorText = colorBTN.Text.ToLower();
            colorBtnEvent.Invoke();

        }
        private void applyBTN_Click(object sender, EventArgs e)
        {
            applyBtnEvent.Invoke();
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            endBtnEvent.Invoke();
        }
    }
}
