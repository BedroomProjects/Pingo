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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.sokhnaBtn = new System.Windows.Forms.Button();
            this.alexBtn = new System.Windows.Forms.Button();
            this.allRoadsBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sokhnaBtn
            // 
            this.sokhnaBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sokhnaBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.sokhnaBtn.Location = new System.Drawing.Point(196, 204);
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
            this.alexBtn.Location = new System.Drawing.Point(196, 262);
            this.alexBtn.Name = "alexBtn";
            this.alexBtn.Size = new System.Drawing.Size(196, 52);
            this.alexBtn.TabIndex = 1;
            this.alexBtn.Text = "القاهرة - الاسكندرية";
            this.alexBtn.UseVisualStyleBackColor = false;
            this.alexBtn.Click += new System.EventHandler(this.alexBtn_Click);
            // 
            // allRoadsBtn
            // 
            this.allRoadsBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.allRoadsBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.allRoadsBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.allRoadsBtn.Location = new System.Drawing.Point(196, 320);
            this.allRoadsBtn.Name = "allRoadsBtn";
            this.allRoadsBtn.Size = new System.Drawing.Size(196, 52);
            this.allRoadsBtn.TabIndex = 3;
            this.allRoadsBtn.Text = "IPs قائمة ال";
            this.allRoadsBtn.UseVisualStyleBackColor = false;
            this.allRoadsBtn.Click += new System.EventHandler(this.allRoadsBtn_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(207, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "اختر الطريق";
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(587, 488);
            this.Controls.Add(this.allRoadsBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.alexBtn);
            this.Controls.Add(this.sokhnaBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StartScreen";
            this.Text = "Watanya Pingo";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sokhnaBtn;
        private System.Windows.Forms.Button alexBtn;
        private System.Windows.Forms.Button allRoadsBtn;
        private System.Windows.Forms.Label label1;


    }
}