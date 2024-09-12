namespace openCV0820
{
    partial class userControl_barcode
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.endBTN = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UC_camBTN = new System.Windows.Forms.Button();
            this.UC_barcodeBTN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.UC_select = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // endBTN
            // 
            this.endBTN.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endBTN.Location = new System.Drawing.Point(792, 22);
            this.endBTN.Name = "endBTN";
            this.endBTN.Size = new System.Drawing.Size(47, 43);
            this.endBTN.TabIndex = 41;
            this.endBTN.Text = "X";
            this.endBTN.UseVisualStyleBackColor = true;
            this.endBTN.Click += new System.EventHandler(this.endBTN_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // UC_camBTN
            // 
            this.UC_camBTN.Location = new System.Drawing.Point(352, 17);
            this.UC_camBTN.Name = "UC_camBTN";
            this.UC_camBTN.Size = new System.Drawing.Size(126, 58);
            this.UC_camBTN.TabIndex = 49;
            this.UC_camBTN.Text = "카메라 연결";
            this.UC_camBTN.UseVisualStyleBackColor = true;
            this.UC_camBTN.Click += new System.EventHandler(this.UC_camBTN_Click);
            // 
            // UC_barcodeBTN
            // 
            this.UC_barcodeBTN.Location = new System.Drawing.Point(498, 17);
            this.UC_barcodeBTN.Name = "UC_barcodeBTN";
            this.UC_barcodeBTN.Size = new System.Drawing.Size(126, 58);
            this.UC_barcodeBTN.TabIndex = 50;
            this.UC_barcodeBTN.Text = "바코드 인식";
            this.UC_barcodeBTN.UseVisualStyleBackColor = true;
            this.UC_barcodeBTN.Click += new System.EventHandler(this.UC_barcodeBTN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(18, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 237);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(358, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 18);
            this.label1.TabIndex = 52;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 273);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(821, 235);
            this.dataGridView1.TabIndex = 53;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(639, 230);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 54;
            // 
            // UC_select
            // 
            this.UC_select.Location = new System.Drawing.Point(758, 230);
            this.UC_select.Name = "UC_select";
            this.UC_select.Size = new System.Drawing.Size(81, 24);
            this.UC_select.TabIndex = 55;
            this.UC_select.Text = "검색";
            this.UC_select.UseVisualStyleBackColor = true;
            this.UC_select.Click += new System.EventHandler(this.UC_select_Click);
            // 
            // userControl_barcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UC_select);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.UC_barcodeBTN);
            this.Controls.Add(this.UC_camBTN);
            this.Controls.Add(this.endBTN);
            this.Name = "userControl_barcode";
            this.Size = new System.Drawing.Size(863, 530);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button UC_camBTN;
        private System.Windows.Forms.Button UC_barcodeBTN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button UC_select;
    }
}
