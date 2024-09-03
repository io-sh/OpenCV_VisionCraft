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
    public partial class UserControl_blur : UserControl
    {
        public delegate void blurTBarDelegate();
        public event blurTBarDelegate blurTBarEvent;

        public delegate void endBTNDelegate();
        public event endBTNDelegate endBTNEvent;

        public delegate void applyBTNDelegate();
        public event applyBTNDelegate applyBTNEvent;

        public RadioButton BlurRB
        {
            get { return blurRB; }
            set { blurRB = value; }
        }
        public RadioButton GaussianRB
        {
            get { return gaussianRB; }
            set { gaussianRB = value; }
        }
        [Category("Blur_TB_Scroll"), Description("trackBar_Scroll")]
        public TrackBar Blur_trackBar
        {
            get { return blur_trackBar; }
            set { blur_trackBar = value; }
        }
        [Category("Gaussian_TB_Scroll"), Description("trackBar_Scroll")]
        public TrackBar Gaussian_trackBar
        {
            get { return gaussian_trackBar; }
            set { gaussian_trackBar = value; }
        }
        public UserControl_blur()
        {
            InitializeComponent();
        }
        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            blur_trackBar.Enabled = blurRB.Checked;
            gaussian_trackBar.Enabled = gaussianRB.Checked;
        }
        private void applyBTN_Click(object sender, EventArgs e)
        {
            applyBTNEvent.Invoke();
        }
        private void endBTN_Click(object sender, EventArgs e)
        {
            endBTNEvent.Invoke();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            blurTBarEvent.Invoke();
        }
    }
}
