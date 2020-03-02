namespace ClientApp
{
    partial class ClientWindow
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
            this.btnConnet = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnPassBall = new System.Windows.Forms.Button();
            this.listActiveClient = new System.Windows.Forms.ListBox();
            this.lstMonitor = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblActiveClient = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnet
            // 
            this.btnConnet.Location = new System.Drawing.Point(19, 502);
            this.btnConnet.Name = "btnConnet";
            this.btnConnet.Size = new System.Drawing.Size(139, 46);
            this.btnConnet.TabIndex = 0;
            this.btnConnet.Text = "Connect";
            this.btnConnet.UseVisualStyleBackColor = true;
            this.btnConnet.Click += new System.EventHandler(this.btnConnet_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(178, 502);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(139, 46);
            this.btnDisconnect.TabIndex = 0;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnPassBall
            // 
            this.btnPassBall.Location = new System.Drawing.Point(498, 502);
            this.btnPassBall.Name = "btnPassBall";
            this.btnPassBall.Size = new System.Drawing.Size(195, 46);
            this.btnPassBall.TabIndex = 0;
            this.btnPassBall.Text = "Pass Ball";
            this.btnPassBall.UseVisualStyleBackColor = true;
            this.btnPassBall.Click += new System.EventHandler(this.btnPassBall_Click);
            // 
            // listActiveClient
            // 
            this.listActiveClient.FormattingEnabled = true;
            this.listActiveClient.ItemHeight = 16;
            this.listActiveClient.Location = new System.Drawing.Point(498, 53);
            this.listActiveClient.Name = "listActiveClient";
            this.listActiveClient.Size = new System.Drawing.Size(195, 436);
            this.listActiveClient.TabIndex = 1;
            this.listActiveClient.SelectedIndexChanged += new System.EventHandler(this.listActiveClient_SelectedIndexChanged);
            // 
            // lstMonitor
            // 
            this.lstMonitor.BackColor = System.Drawing.SystemColors.InfoText;
            this.lstMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMonitor.ForeColor = System.Drawing.Color.LimeGreen;
            this.lstMonitor.FormattingEnabled = true;
            this.lstMonitor.ItemHeight = 25;
            this.lstMonitor.Location = new System.Drawing.Point(19, 53);
            this.lstMonitor.Name = "lstMonitor";
            this.lstMonitor.ScrollAlwaysVisible = true;
            this.lstMonitor.Size = new System.Drawing.Size(465, 429);
            this.lstMonitor.TabIndex = 2;
            this.lstMonitor.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(16, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(62, 17);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Monitor";
            // 
            // lblActiveClient
            // 
            this.lblActiveClient.AutoSize = true;
            this.lblActiveClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveClient.Location = new System.Drawing.Point(495, 24);
            this.lblActiveClient.Name = "lblActiveClient";
            this.lblActiveClient.Size = new System.Drawing.Size(106, 17);
            this.lblActiveClient.TabIndex = 3;
            this.lblActiveClient.Text = "Active Clients";
            // 
            // ClientWindow
            // 
            this.ClientSize = new System.Drawing.Size(714, 560);
            this.Controls.Add(this.lblActiveClient);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstMonitor);
            this.Controls.Add(this.listActiveClient);
            this.Controls.Add(this.btnPassBall);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ClientWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ClientWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstActiveClients;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.ListView lstActivity;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox liveListBox;
        private System.Windows.Forms.Button btnPasstoClient;
        private System.Windows.Forms.Button btnConnet;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnPassBall;
        private System.Windows.Forms.ListBox listActiveClient;
        private System.Windows.Forms.ListBox lstMonitor;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblActiveClient;
    }
}

