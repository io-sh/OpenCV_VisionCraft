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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace openCV0820
{
    public partial class userControl_barcode : UserControl
    {
        public delegate void delegate_UC_camBTN_Click();
        public event delegate_UC_camBTN_Click event_UC_camBTN_Click;

        public delegate void delegate_UC_barcodeBTN_Click();
        public event delegate_UC_barcodeBTN_Click event_UC_barcodeBTN_Click;

        public delegate void delegate_UC_select_Click();
        public event delegate_UC_select_Click event_UC_select_Click;

        public delegate void delegate_endBTN_Click();
        public event delegate_endBTN_Click event_endBTN_Click;

        public PictureBox PictureBox1
        {
            get { return pictureBox1; }
            set { pictureBox1 = value; }
        }
        public Label Label1
        {
            get { return label1; }
            set { label1 = value; }
        }
        public DataGridView DataGridView1
        {
            get { return dataGridView1; }
            set { dataGridView1 = value; }
        }
        public System.Windows.Forms.TextBox TextBox1
        {
            get { return textBox1; }
            set { textBox1 = value; }
        }
        public userControl_barcode()
        {
            InitializeComponent();
        }

        private void UC_camBTN_Click(object sender, EventArgs e)
        {
            event_UC_camBTN_Click.Invoke();
        }

        private void UC_barcodeBTN_Click(object sender, EventArgs e)
        {
            event_UC_barcodeBTN_Click.Invoke();
        }

        private void UC_select_Click(object sender, EventArgs e)
        {
            event_UC_select_Click.Invoke();
        }

        private void endBTN_Click(object sender, EventArgs e)
        {
            event_endBTN_Click.Invoke();
        }
    }
}
