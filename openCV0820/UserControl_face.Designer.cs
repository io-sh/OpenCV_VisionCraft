namespace openCV0820
{
    partial class UserControl_face
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
            this.camBTN = new System.Windows.Forms.Button();
            this.endBTN = new System.Windows.Forms.Button();
            this.decorationBTN1 = new System.Windows.Forms.Button();
            this.decorationBTN2 = new System.Windows.Forms.Button();
            this.girlHairBTN = new System.Windows.Forms.Button();
            this.baldHeadBTN = new System.Windows.Forms.Button();
            this.sunglassesBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // camBTN
            // 
            this.camBTN.Location = new System.Drawing.Point(14, 13);
            this.camBTN.Name = "camBTN";
            this.camBTN.Size = new System.Drawing.Size(124, 54);
            this.camBTN.TabIndex = 0;
            this.camBTN.Text = "카메라 연결";
            this.camBTN.UseVisualStyleBackColor = true;
            this.camBTN.Click += new System.EventHandler(this.camBTN_Click);
            // 
            // endBTN
            // 
            this.endBTN.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.endBTN.Location = new System.Drawing.Point(154, 13);
            this.endBTN.Name = "endBTN";
            this.endBTN.Size = new System.Drawing.Size(47, 43);
            this.endBTN.TabIndex = 42;
            this.endBTN.Text = "X";
            this.endBTN.UseVisualStyleBackColor = true;
            this.endBTN.Click += new System.EventHandler(this.endBTN_Click);
            // 
            // decorationBTN1
            // 
            this.decorationBTN1.Location = new System.Drawing.Point(14, 83);
            this.decorationBTN1.Name = "decorationBTN1";
            this.decorationBTN1.Size = new System.Drawing.Size(124, 54);
            this.decorationBTN1.TabIndex = 43;
            this.decorationBTN1.Text = "얼굴감지";
            this.decorationBTN1.UseVisualStyleBackColor = true;
            this.decorationBTN1.Click += new System.EventHandler(this.decorationBTN1_Click);
            // 
            // decorationBTN2
            // 
            this.decorationBTN2.Location = new System.Drawing.Point(14, 158);
            this.decorationBTN2.Name = "decorationBTN2";
            this.decorationBTN2.Size = new System.Drawing.Size(124, 54);
            this.decorationBTN2.TabIndex = 44;
            this.decorationBTN2.Text = "토끼귀";
            this.decorationBTN2.UseVisualStyleBackColor = true;
            this.decorationBTN2.Click += new System.EventHandler(this.decorationBTN2_Click);
            // 
            // girlHairBTN
            // 
            this.girlHairBTN.Location = new System.Drawing.Point(14, 235);
            this.girlHairBTN.Name = "girlHairBTN";
            this.girlHairBTN.Size = new System.Drawing.Size(124, 54);
            this.girlHairBTN.TabIndex = 45;
            this.girlHairBTN.Text = "긴머리";
            this.girlHairBTN.UseVisualStyleBackColor = true;
            this.girlHairBTN.Click += new System.EventHandler(this.girlHairBTN_Click);
            // 
            // baldHeadBTN
            // 
            this.baldHeadBTN.Location = new System.Drawing.Point(14, 311);
            this.baldHeadBTN.Name = "baldHeadBTN";
            this.baldHeadBTN.Size = new System.Drawing.Size(124, 54);
            this.baldHeadBTN.TabIndex = 46;
            this.baldHeadBTN.Text = "대머리";
            this.baldHeadBTN.UseVisualStyleBackColor = true;
            this.baldHeadBTN.Click += new System.EventHandler(this.baldHeadBTN_Click);
            // 
            // sunglassesBTN
            // 
            this.sunglassesBTN.Location = new System.Drawing.Point(14, 387);
            this.sunglassesBTN.Name = "sunglassesBTN";
            this.sunglassesBTN.Size = new System.Drawing.Size(124, 54);
            this.sunglassesBTN.TabIndex = 47;
            this.sunglassesBTN.Text = "선글라스";
            this.sunglassesBTN.UseVisualStyleBackColor = true;
            this.sunglassesBTN.Click += new System.EventHandler(this.sunglassesBTN_Click);
            // 
            // UserControl_face
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sunglassesBTN);
            this.Controls.Add(this.baldHeadBTN);
            this.Controls.Add(this.girlHairBTN);
            this.Controls.Add(this.decorationBTN2);
            this.Controls.Add(this.decorationBTN1);
            this.Controls.Add(this.endBTN);
            this.Controls.Add(this.camBTN);
            this.Name = "UserControl_face";
            this.Size = new System.Drawing.Size(219, 460);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button camBTN;
        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.Button decorationBTN1;
        private System.Windows.Forms.Button decorationBTN2;
        private System.Windows.Forms.Button girlHairBTN;
        private System.Windows.Forms.Button baldHeadBTN;
        private System.Windows.Forms.Button sunglassesBTN;
    }
}
