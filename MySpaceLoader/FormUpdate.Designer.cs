namespace MySpaceLoader
{
    partial class FormUpdate
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
            this.webBrowserUpdate = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserUpdate
            // 
            this.webBrowserUpdate.AllowNavigation = false;
            this.webBrowserUpdate.AllowWebBrowserDrop = false;
            this.webBrowserUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserUpdate.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserUpdate.Location = new System.Drawing.Point(0, 0);
            this.webBrowserUpdate.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpdate.Name = "webBrowserUpdate";
            this.webBrowserUpdate.ScriptErrorsSuppressed = true;
            this.webBrowserUpdate.Size = new System.Drawing.Size(581, 299);
            this.webBrowserUpdate.TabIndex = 2;
            this.webBrowserUpdate.WebBrowserShortcutsEnabled = false;
            this.webBrowserUpdate.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowserUpdate_NewWindow);
            // 
            // FormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 299);
            this.Controls.Add(this.webBrowserUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormUpdate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormUpdate_FormClosed);
            this.Load += new System.EventHandler(this.FormUpdate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserUpdate;
    }
}