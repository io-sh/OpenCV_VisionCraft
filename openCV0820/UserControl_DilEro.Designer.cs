namespace openCV0820
{
    partial class UserControl_DilEro
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.applyBTN = new System.Windows.Forms.Button();
            this.dilTBar = new System.Windows.Forms.TrackBar();
            this.eroTBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.dilTBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eroTBar)).BeginInit();
            this.SuspendLayout();
            // 
            // endBTN
            // 
            this.endBTN.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endBTN.Location = new System.Drawing.Point(321, 21);
            this.endBTN.Name = "endBTN";
            this.endBTN.Size = new System.Drawing.Size(47, 43);
            this.endBTN.TabIndex = 39;
            this.endBTN.Text = "X";
            this.endBTN.UseVisualStyleBackColor = true;
            this.endBTN.Click += new System.EventHandler(this.endBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(54, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 54);
            this.label1.TabIndex = 40;
            this.label1.Text = "모폴로지 변환";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 41;
            this.label2.Text = "팽창";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 42;
            this.label3.Text = "침식";
            // 
            // applyBTN
            // 
            this.applyBTN.Location = new System.Drawing.Point(136, 431);
            this.applyBTN.Name = "applyBTN";
            this.applyBTN.Size = new System.Drawing.Size(110, 44);
            this.applyBTN.TabIndex = 43;
            this.applyBTN.Text = "적용";
            this.applyBTN.UseVisualStyleBackColor = true;
            this.applyBTN.Click += new System.EventHandler(this.applyBTN_Click);
            // 
            // dilTBar
            // 
            this.dilTBar.Location = new System.Drawing.Point(34, 215);
            this.dilTBar.Name = "dilTBar";
            this.dilTBar.Size = new System.Drawing.Size(310, 69);
            this.dilTBar.TabIndex = 44;
            this.dilTBar.Scroll += new System.EventHandler(this.dilTBar_Scroll);
            // 
            // eroTBar
            // 
            this.eroTBar.Location = new System.Drawing.Point(34, 343);
            this.eroTBar.Name = "eroTBar";
            this.eroTBar.Size = new System.Drawing.Size(310, 69);
            this.eroTBar.TabIndex = 45;
            this.eroTBar.Scroll += new System.EventHandler(this.eroTBar_Scroll);
            // 
            // UserControl_DilEro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eroTBar);
            this.Controls.Add(this.dilTBar);
            this.Controls.Add(this.applyBTN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endBTN);
            this.Name = "UserControl_DilEro";
            this.Size = new System.Drawing.Size(391, 506);
            ((System.ComponentModel.ISupportInitialize)(this.dilTBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eroTBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button applyBTN;
        private System.Windows.Forms.TrackBar dilTBar;
        private System.Windows.Forms.TrackBar eroTBar;
    }
}
