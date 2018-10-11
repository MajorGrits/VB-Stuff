namespace TestPrint
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbTemplate = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCompany = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(12, 139);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(117, 23);
			this.btnPrint.TabIndex = 2;
			this.btnPrint.Text = "Print label";
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(163, 139);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(117, 23);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "Template";
			// 
			// cmbTemplate
			// 
			this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTemplate.FormattingEnabled = true;
			this.cmbTemplate.Items.AddRange(new object[] {
            "Simple",
            "Decoration Frame"});
			this.cmbTemplate.Location = new System.Drawing.Point(93, 10);
			this.cmbTemplate.Name = "cmbTemplate";
			this.cmbTemplate.Size = new System.Drawing.Size(174, 20);
			this.cmbTemplate.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "Company";
			// 
			// txtCompany
			// 
			this.txtCompany.Location = new System.Drawing.Point(93, 53);
			this.txtCompany.Name = "txtCompany";
			this.txtCompany.Size = new System.Drawing.Size(174, 19);
			this.txtCompany.TabIndex = 8;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(93, 97);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 19);
			this.txtName.TabIndex = 9;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 174);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtCompany);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbTemplate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnPrint);
			this.Name = "Form1";
			this.Text = "TestPrint";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbTemplate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCompany;
		private System.Windows.Forms.TextBox txtName;
    }
}

