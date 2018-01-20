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
            this.SuspendLayout();
            // 
            // sokhnaBtn
            // 
            this.sokhnaBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sokhnaBtn.BackgroundImage = global::WatanyaPingTester.Properties.Resources.btn1;
            this.sokhnaBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sokhnaBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.sokhnaBtn.Location = new System.Drawing.Point(48, 149);
            this.sokhnaBtn.Name = "sokhnaBtn";
            this.sokhnaBtn.Size = new System.Drawing.Size(196, 52);
            this.sokhnaBtn.TabIndex = 0;
            this.sokhnaBtn.UseVisualStyleBackColor = true;
            this.sokhnaBtn.Click += new System.EventHandler(this.sokhnaBtn_Click);
            // 
            // alexBtn
            // 
            this.alexBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.alexBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.alexBtn.BackgroundImage = global::WatanyaPingTester.Properties.Resources.btn2;
            this.alexBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.alexBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.alexBtn.Location = new System.Drawing.Point(48, 207);
            this.alexBtn.Name = "alexBtn";
            this.alexBtn.Size = new System.Drawing.Size(196, 52);
            this.alexBtn.TabIndex = 1;
            this.alexBtn.UseVisualStyleBackColor = false;
            this.alexBtn.Click += new System.EventHandler(this.alexBtn_Click);
            // 
            // allRoadsBtn
            // 
            this.allRoadsBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.allRoadsBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.allRoadsBtn.BackgroundImage = global::WatanyaPingTester.Properties.Resources.btn3;
            this.allRoadsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.allRoadsBtn.Font = new System.Drawing.Font("Arial", 15F);
            this.allRoadsBtn.Location = new System.Drawing.Point(48, 265);
            this.allRoadsBtn.Name = "allRoadsBtn";
            this.allRoadsBtn.Size = new System.Drawing.Size(196, 52);
            this.allRoadsBtn.TabIndex = 3;
            this.allRoadsBtn.UseVisualStyleBackColor = false;
            this.allRoadsBtn.Click += new System.EventHandler(this.allRoadsBtn_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::WatanyaPingTester.Properties.Resources.introback;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(291, 379);
            this.Controls.Add(this.allRoadsBtn);
            this.Controls.Add(this.alexBtn);
            this.Controls.Add(this.sokhnaBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartScreen";
            this.Text = "Watanya Pinger";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sokhnaBtn;
        private System.Windows.Forms.Button alexBtn;
        private System.Windows.Forms.Button allRoadsBtn;


    }
}