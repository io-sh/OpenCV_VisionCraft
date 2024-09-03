namespace openCV0820
{
    partial class UserControl_Tesseract
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
            this.label1 = new System.Windows.Forms.Label();
            this.endBTN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.resultLB = new System.Windows.Forms.Label();
            this.TesseractBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(88, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 54);
            this.label1.TabIndex = 21;
            this.label1.Text = "글자 판독";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 40;
            this.label2.Text = "판독 결과 :";
            // 
            // resultLB
            // 
            this.resultLB.AutoSize = true;
            this.resultLB.Location = new System.Drawing.Point(33, 299);
            this.resultLB.Name = "resultLB";
            this.resultLB.Size = new System.Drawing.Size(26, 18);
            this.resultLB.TabIndex = 41;
            this.resultLB.Text = "...";
            // 
            // TesseractBTN
            // 
            this.TesseractBTN.Location = new System.Drawing.Point(132, 175);
            this.TesseractBTN.Name = "TesseractBTN";
            this.TesseractBTN.Size = new System.Drawing.Size(95, 41);
            this.TesseractBTN.TabIndex = 42;
            this.TesseractBTN.Text = "실행";
            this.TesseractBTN.UseVisualStyleBackColor = true;
            this.TesseractBTN.Click += new System.EventHandler(this.TesseractBTN_Click);
            // 
            // UserControl_Tesseract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.TesseractBTN);
            this.Controls.Add(this.resultLB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endBTN);
            this.Controls.Add(this.label1);
            this.Name = "UserControl_Tesseract";
            this.Size = new System.Drawing.Size(389, 504);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label resultLB;
        private System.Windows.Forms.Button TesseractBTN;
    }
}
