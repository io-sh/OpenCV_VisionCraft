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
    public partial class UserControl_DilEro : UserControl
    {
        public delegate void applyBTNdelegate();
        public event applyBTNdelegate applyBTNevent;

        public delegate void dilTBardelegate();
        public event dilTBardelegate dilTBarevent;

        public delegate void eroTBardelegate();
        public event eroTBardelegate eroTBarevent;

        public delegate void endBTNdelegate();
        public event endBTNdelegate endBTNevent;
        public UserControl_DilEro()
        {
            InitializeComponent();
        }
        public TrackBar DilTBar
        {
            get { return dilTBar; }
            set { dilTBar = value; }
        }
        public TrackBar EroTBar
        {
            get { return eroTBar; }
            set { eroTBar = value; }
        }
        private void applyBTN_Click(object sender, EventArgs e)
        {
            applyBTNevent.Invoke();
        }

        private void dilTBar_Scroll(object sender, EventArgs e)
        {
            dilTBarevent.Invoke();
        }

        private void eroTBar_Scroll(object sender, EventArgs e)
        {
            eroTBarevent.Invoke();
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            endBTNevent.Invoke();
        }
    }
}
