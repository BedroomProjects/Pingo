namespace WatanyaPingTester
{
    partial class StartScreen
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
            this.sokhnaBtn = new System.Windows.Forms.Button();
            this.alexBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sokhnaBtn
            // 
            this.sokhnaBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sokhnaBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.sokhnaBtn.Location = new System.Drawing.Point(35, 111);
            this.sokhnaBtn.Name = "sokhnaBtn";
            this.sokhnaBtn.Size = new System.Drawing.Size(196, 52);
            this.sokhnaBtn.TabIndex = 0;
            this.sokhnaBtn.Text = "القاهرة - السخنة";
            this.sokhnaBtn.UseVisualStyleBackColor = true;
            this.sokhnaBtn.Click += new System.EventHandler(this.sokhnaBtn_Click);
            // 
            // alexBtn
            // 
            this.alexBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.alexBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.alexBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.alexBtn.Location = new System.Drawing.Point(35, 169);
            this.alexBtn.Name = "alexBtn";
            this.alexBtn.Size = new System.Drawing.Size(196, 52);
            this.alexBtn.TabIndex = 1;
            this.alexBtn.Text = "القاهرة - الاسكندرية";
            this.alexBtn.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20F);
            this.label1.Location = new System.Drawing.Point(65, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "اختر الطريق";
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(265, 269);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.alexBtn);
            this.Controls.Add(this.sokhnaBtn);
            this.Name = "StartScreen";
            this.Text = "Watanya Pingo";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sokhnaBtn;
        private System.Windows.Forms.Button alexBtn;
        private System.Windows.Forms.Label label1;


    }
}