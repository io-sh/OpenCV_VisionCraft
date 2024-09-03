namespace openCV0820
{
    partial class UserControl_color
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
            this.label1 = new System.Windows.Forms.Label();
            this.redBTN = new System.Windows.Forms.Button();
            this.orangeBTN = new System.Windows.Forms.Button();
            this.yellowBTN = new System.Windows.Forms.Button();
            this.blueBTN = new System.Windows.Forms.Button();
            this.greenBTN = new System.Windows.Forms.Button();
            this.cyanBTN = new System.Windows.Forms.Button();
            this.pinkBTN = new System.Windows.Forms.Button();
            this.magentaBTN = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.applyBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(87, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 54);
            this.label1.TabIndex = 41;
            this.label1.Text = "색상 검출";
            // 
            // redBTN
            // 
            this.redBTN.Location = new System.Drawing.Point(26, 182);
            this.redBTN.Name = "redBTN";
            this.redBTN.Size = new System.Drawing.Size(98, 45);
            this.redBTN.TabIndex = 42;
            this.redBTN.Text = "Red";
            this.redBTN.UseVisualStyleBackColor = true;
            this.redBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // orangeBTN
            // 
            this.orangeBTN.Location = new System.Drawing.Point(26, 253);
            this.orangeBTN.Name = "orangeBTN";
            this.orangeBTN.Size = new System.Drawing.Size(98, 45);
            this.orangeBTN.TabIndex = 43;
            this.orangeBTN.Text = "Orange";
            this.orangeBTN.UseVisualStyleBackColor = true;
            this.orangeBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // yellowBTN
            // 
            this.yellowBTN.Location = new System.Drawing.Point(26, 327);
            this.yellowBTN.Name = "yellowBTN";
            this.yellowBTN.Size = new System.Drawing.Size(98, 45);
            this.yellowBTN.TabIndex = 44;
            this.yellowBTN.Text = "Yellow";
            this.yellowBTN.UseVisualStyleBackColor = true;
            this.yellowBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // blueBTN
            // 
            this.blueBTN.Location = new System.Drawing.Point(150, 327);
            this.blueBTN.Name = "blueBTN";
            this.blueBTN.Size = new System.Drawing.Size(98, 45);
            this.blueBTN.TabIndex = 45;
            this.blueBTN.Text = "Blue";
            this.blueBTN.UseVisualStyleBackColor = true;
            this.blueBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // greenBTN
            // 
            this.greenBTN.Location = new System.Drawing.Point(150, 182);
            this.greenBTN.Name = "greenBTN";
            this.greenBTN.Size = new System.Drawing.Size(98, 45);
            this.greenBTN.TabIndex = 46;
            this.greenBTN.Text = "Green";
            this.greenBTN.UseVisualStyleBackColor = true;
            this.greenBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // cyanBTN
            // 
            this.cyanBTN.Location = new System.Drawing.Point(150, 253);
            this.cyanBTN.Name = "cyanBTN";
            this.cyanBTN.Size = new System.Drawing.Size(98, 45);
            this.cyanBTN.TabIndex = 47;
            this.cyanBTN.Text = "Cyan";
            this.cyanBTN.UseVisualStyleBackColor = true;
            this.cyanBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // pinkBTN
            // 
            this.pinkBTN.Location = new System.Drawing.Point(270, 253);
            this.pinkBTN.Name = "pinkBTN";
            this.pinkBTN.Size = new System.Drawing.Size(98, 45);
            this.pinkBTN.TabIndex = 48;
            this.pinkBTN.Text = "Pink";
            this.pinkBTN.UseVisualStyleBackColor = true;
            this.pinkBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // magentaBTN
            // 
            this.magentaBTN.Location = new System.Drawing.Point(269, 182);
            this.magentaBTN.Name = "magentaBTN";
            this.magentaBTN.Size = new System.Drawing.Size(98, 45);
            this.magentaBTN.TabIndex = 50;
            this.magentaBTN.Text = "Magenta";
            this.magentaBTN.UseVisualStyleBackColor = true;
            this.magentaBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // applyBTN
            // 
            this.applyBTN.Location = new System.Drawing.Point(141, 411);
            this.applyBTN.Name = "applyBTN";
            this.applyBTN.Size = new System.Drawing.Size(124, 45);
            this.applyBTN.TabIndex = 51;
            this.applyBTN.Text = "적용하기";
            this.applyBTN.UseVisualStyleBackColor = true;
            this.applyBTN.Click += new System.EventHandler(this.applyBTN_Click);
            // 
            // UserControl_color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.applyBTN);
            this.Controls.Add(this.magentaBTN);
            this.Controls.Add(this.pinkBTN);
            this.Controls.Add(this.cyanBTN);
            this.Controls.Add(this.greenBTN);
            this.Controls.Add(this.blueBTN);
            this.Controls.Add(this.yellowBTN);
            this.Controls.Add(this.orangeBTN);
            this.Controls.Add(this.redBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endBTN);
            this.Name = "UserControl_color";
            this.Size = new System.Drawing.Size(391, 506);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button endBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button redBTN;
        private System.Windows.Forms.Button orangeBTN;
        private System.Windows.Forms.Button yellowBTN;
        private System.Windows.Forms.Button blueBTN;
        private System.Windows.Forms.Button greenBTN;
        private System.Windows.Forms.Button cyanBTN;
        private System.Windows.Forms.Button pinkBTN;
        private System.Windows.Forms.Button magentaBTN;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button applyBTN;
    }
}
