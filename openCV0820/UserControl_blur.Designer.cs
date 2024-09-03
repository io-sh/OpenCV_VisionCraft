namespace openCV0820
{
    partial class UserControl_blur
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
            this.endBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.blur_trackBar = new System.Windows.Forms.TrackBar();
            this.blurRB = new System.Windows.Forms.RadioButton();
            this.gaussianRB = new System.Windows.Forms.RadioButton();
            this.gaussian_trackBar = new System.Windows.Forms.TrackBar();
            this.applyBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.blur_trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaussian_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // endBTN
            // 
            this.endBTN.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endBTN.Location = new System.Drawing.Point(321, 21);
            this.endBTN.Name = "endBTN";
            this.endBTN.Size = new System.Drawing.Size(47, 43);
            this.endBTN.TabIndex = 40;
            this.endBTN.Text = "X";
            this.endBTN.UseVisualStyleBackColor = true;
            this.endBTN.Click += new System.EventHandler(this.endBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(127, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 54);
            this.label1.TabIndex = 41;
            this.label1.Text = "블러";
            // 
            // blur_trackBar
            // 
            this.blur_trackBar.Enabled = false;
            this.blur_trackBar.Location = new System.Drawing.Point(34, 209);
            this.blur_trackBar.Name = "blur_trackBar";
            this.blur_trackBar.Size = new System.Drawing.Size(314, 69);
            this.blur_trackBar.TabIndex = 42;
            this.blur_trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // blurRB
            // 
            this.blurRB.AutoSize = true;
            this.blurRB.Location = new System.Drawing.Point(34, 162);
            this.blurRB.Name = "blurRB";
            this.blurRB.Size = new System.Drawing.Size(105, 22);
            this.blurRB.TabIndex = 44;
            this.blurRB.TabStop = true;
            this.blurRB.Text = "단순블러";
            this.blurRB.UseVisualStyleBackColor = true;
            this.blurRB.CheckedChanged += new System.EventHandler(this.RB_CheckedChanged);
            // 
            // gaussianRB
            // 
            this.gaussianRB.AutoSize = true;
            this.gaussianRB.Location = new System.Drawing.Point(34, 276);
            this.gaussianRB.Name = "gaussianRB";
            this.gaussianRB.Size = new System.Drawing.Size(141, 22);
            this.gaussianRB.TabIndex = 46;
            this.gaussianRB.TabStop = true;
            this.gaussianRB.Text = "가우시안블러";
            this.gaussianRB.UseVisualStyleBackColor = true;
            this.gaussianRB.CheckedChanged += new System.EventHandler(this.RB_CheckedChanged);
            // 
            // gaussian_trackBar
            // 
            this.gaussian_trackBar.Enabled = false;
            this.gaussian_trackBar.Location = new System.Drawing.Point(34, 323);
            this.gaussian_trackBar.Name = "gaussian_trackBar";
            this.gaussian_trackBar.Size = new System.Drawing.Size(314, 69);
            this.gaussian_trackBar.TabIndex = 45;
            this.gaussian_trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // applyBTN
            // 
            this.applyBTN.Location = new System.Drawing.Point(136, 413);
            this.applyBTN.Name = "applyBTN";
            this.applyBTN.Size = new System.Drawing.Size(110, 48);
            this.applyBTN.TabIndex = 47;
            this.applyBTN.Text = "적용하기";
            this.applyBTN.UseVisualStyleBackColor = true;
            this.applyBTN.Click += new System.EventHandler(this.applyBTN_Click);
            // 
            // UserControl_blur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.applyBTN);
            this.Controls.Add(this.gaussianRB);
            this.Controls.Add(this.gaussian_trackBar);
            this.Controls.Add(this.blurRB);
            this.Controls.Add(this.blur_trackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endBTN);
            this.Name = "UserControl_blur";
            this.Size = new System.Drawing.Size(391, 506);
            ((System.ComponentModel.ISupportInitialize)(this.blur_trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gaussian_trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar blur_trackBar;
        private System.Windows.Forms.RadioButton blurRB;
        private System.Windows.Forms.RadioButton gaussianRB;
        private System.Windows.Forms.TrackBar gaussian_trackBar;
        private System.Windows.Forms.Button applyBTN;
    }
}
