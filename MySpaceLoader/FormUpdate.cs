using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySpaceLoader
{
    public partial class FormUpdate : Form
    {
        public FormUpdate()
        {
            InitializeComponent();
        }

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            webBrowserUpdate.Navigate("http://adf.ly/1250914/myspaceupdatemessage");
            //webBrowserUpdate.Navigate("http://www.code-bude.net/downloads/myspace/updatepage/update_message.html");
        }

        private void webBrowserUpdate_NewWindow(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void FormUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
