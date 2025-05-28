namespace AdministratorComander
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.chcb = new System.Windows.Forms.ComboBox();
            this.enterbt = new System.Windows.Forms.Button();
            this.chooselb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chcb
            // 
            this.chcb.AutoCompleteCustomSource.AddRange(new string[] {
            "Admin"});
            this.chcb.FormattingEnabled = true;
            this.chcb.Items.AddRange(new object[] {
            "Admin",
            "User"});
            this.chcb.Location = new System.Drawing.Point(346, 208);
            this.chcb.Name = "chcb";
            this.chcb.Size = new System.Drawing.Size(111, 21);
            this.chcb.TabIndex = 5;
            // 
            // enterbt
            // 
            this.enterbt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(242)))), ((int)(((byte)(206)))));
            this.enterbt.Location = new System.Drawing.Point(360, 235);
            this.enterbt.Name = "enterbt";
            this.enterbt.Size = new System.Drawing.Size(75, 23);
            this.enterbt.TabIndex = 4;
            this.enterbt.Text = "Вход";
            this.enterbt.UseVisualStyleBackColor = false;
            this.enterbt.Click += new System.EventHandler(this.enterbt_Click);
            // 
            // chooselb
            // 
            this.chooselb.AutoSize = true;
            this.chooselb.Location = new System.Drawing.Point(343, 192);
            this.chooselb.Name = "chooselb";
            this.chooselb.Size = new System.Drawing.Size(114, 13);
            this.chooselb.TabIndex = 3;
            this.chooselb.Text = "Выбор пользователя";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(253)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chcb);
            this.Controls.Add(this.enterbt);
            this.Controls.Add(this.chooselb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox chcb;
        private System.Windows.Forms.Button enterbt;
        private System.Windows.Forms.Label chooselb;
    }
}