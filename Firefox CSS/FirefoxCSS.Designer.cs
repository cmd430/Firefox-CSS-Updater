namespace Firefox_CSS
{
    partial class FirefoxCSS
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirefoxCSS));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_currentVersion = new System.Windows.Forms.TextBox();
            this.textBox_latestVersion = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel_userPrefs = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.panel_loading = new System.Windows.Forms.Panel();
            this.label_loading = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.panel_loading.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Latest Version: ";
            // 
            // textBox_currentVersion
            // 
            this.textBox_currentVersion.Enabled = false;
            this.textBox_currentVersion.Location = new System.Drawing.Point(100, 6);
            this.textBox_currentVersion.Name = "textBox_currentVersion";
            this.textBox_currentVersion.ReadOnly = true;
            this.textBox_currentVersion.Size = new System.Drawing.Size(262, 20);
            this.textBox_currentVersion.TabIndex = 3;
            // 
            // textBox_latestVersion
            // 
            this.textBox_latestVersion.Enabled = false;
            this.textBox_latestVersion.Location = new System.Drawing.Point(100, 32);
            this.textBox_latestVersion.Name = "textBox_latestVersion";
            this.textBox_latestVersion.ReadOnly = true;
            this.textBox_latestVersion.Size = new System.Drawing.Size(262, 20);
            this.textBox_latestVersion.TabIndex = 4;
            // 
            // flowLayoutPanel_userPrefs
            // 
            this.flowLayoutPanel_userPrefs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_userPrefs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_userPrefs.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel_userPrefs.Name = "flowLayoutPanel_userPrefs";
            this.flowLayoutPanel_userPrefs.Size = new System.Drawing.Size(341, 352);
            this.flowLayoutPanel_userPrefs.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_user);
            this.groupBox1.Controls.Add(this.flowLayoutPanel_userPrefs);
            this.groupBox1.Location = new System.Drawing.Point(15, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 371);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(6, 236);
            this.textBox_user.Multiline = true;
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(335, 130);
            this.textBox_user.TabIndex = 0;
            this.textBox_user.Visible = false;
            // 
            // panel_loading
            // 
            this.panel_loading.Controls.Add(this.label_loading);
            this.panel_loading.Controls.Add(this.progressBar1);
            this.panel_loading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_loading.Location = new System.Drawing.Point(0, 0);
            this.panel_loading.Name = "panel_loading";
            this.panel_loading.Size = new System.Drawing.Size(374, 450);
            this.panel_loading.TabIndex = 7;
            // 
            // label_loading
            // 
            this.label_loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_loading.Location = new System.Drawing.Point(12, 141);
            this.label_loading.Name = "label_loading";
            this.label_loading.Size = new System.Drawing.Size(350, 34);
            this.label_loading.TabIndex = 1;
            this.label_loading.Text = "Loading ...";
            this.label_loading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(97, 178);
            this.progressBar1.MarqueeAnimationSpeed = 25;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(181, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FirefoxCSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 450);
            this.Controls.Add(this.panel_loading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_latestVersion);
            this.Controls.Add(this.textBox_currentVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirefoxCSS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Firefox CSS";
            this.Shown += new System.EventHandler(this.FirefoxCSS_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_loading.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_currentVersion;
        private System.Windows.Forms.TextBox textBox_latestVersion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_userPrefs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.Panel panel_loading;
        private System.Windows.Forms.Label label_loading;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
    }
}

