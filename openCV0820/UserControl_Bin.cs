using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static openCV0820.UserControl_Bin;

namespace openCV0820
{
    public partial class UserControl_Bin : UserControl
    {
        public delegate void trackBar1delegate();
        public event trackBar1delegate trackBar1event;

        public delegate void button1delegate();
        public event button1delegate button1event;

        public delegate void endBTNdelegate();
        public event endBTNdelegate endBTNevent;
        public UserControl_Bin()
        {
            InitializeComponent();
        }

        public TrackBar TrackBar 
        { 
            get { return trackBar1; } 
            set { trackBar1 = value; }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1event.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1event.Invoke();
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            endBTNevent.Invoke();
        }
    }
}
