using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static openCV0820.userControl_barcode;

namespace openCV0820
{
    public partial class UserControl_face : UserControl
    {
        public delegate void delegate_endBTN();
        public event delegate_endBTN event_endBTN;

        public delegate void delegate_camBTN();
        public event delegate_camBTN event_camBTN;

        public delegate void delegate_decorationBTN1();
        public event delegate_decorationBTN1 event_decorationBTN1;

        public delegate void delegate_decorationBTN2();
        public event delegate_decorationBTN2 event_decorationBTN2;

        public delegate void delegate_girlHairBTN();
        public event delegate_girlHairBTN event_girlHairBTN;

        public delegate void delegate_baldHeadBTN();
        public event delegate_baldHeadBTN event_baldHeadBTN;

        public delegate void delegate_sunglassesBTN();
        public event delegate_sunglassesBTN event_sunglassesBTN;

        public UserControl_face()
        {
            InitializeComponent();
        }

        
        private void camBTN_Click(object sender, EventArgs e)
        {
            event_camBTN.Invoke();  
        }
        private void endBTN_Click(object sender, EventArgs e)
        {
            event_endBTN.Invoke();
        }
        private void decorationBTN1_Click(object sender, EventArgs e)
        {
            event_decorationBTN1.Invoke();
        }

        private void decorationBTN2_Click(object sender, EventArgs e)
        {
            event_decorationBTN2.Invoke();
        }

        private void girlHairBTN_Click(object sender, EventArgs e)
        {
            event_girlHairBTN.Invoke();
        }

        private void baldHeadBTN_Click(object sender, EventArgs e)
        {
            event_baldHeadBTN.Invoke();
        }

        private void sunglassesBTN_Click(object sender, EventArgs e)
        {
            event_sunglassesBTN.Invoke();
        }
    }
}
