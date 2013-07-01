using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace MySpaceLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string consoleMessage;
        private Timer getOutput = new Timer();
        Helper.Debug debugHelper = new Helper.Debug(false);
        CookieContainer cookieCon;

        #region Search-Button and Search-BGW
        private void SearchSongs_Click(object sender, EventArgs e)
        {
            if (textBoxUrl.Text == "/debug=1")
            {
                debugHelper = new Helper.Debug(true);
                MessageBox.Show("Debug mode switched: on.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                Version v = Environment.Version;
                string s = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
                debugHelper.WriteDebug("Environment.Version: " + v.ToString());
                debugHelper.WriteDebug("System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion(): " + s);
            }
            else if (textBoxUrl.Text == "/debug=0")
            {
                debugHelper = new Helper.Debug(false);
                MessageBox.Show("Debug mode switched: off.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBoxUrl.Text == "/exportdebug=1")
            {
                bool success = debugHelper.ExportDebug();
                MessageBox.Show(success ? "Exported debugfile." : "Debugfile not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                debugHelper.WriteDebug("Suche start. Suchbegriff: " + textBoxUrl.Text);

                //Wir blenden den Button aus damit die Suche
                //nicht 2 mal zur gleichen Zeit gestartet werden kann
                //SearchSongs.Enabled = false;
                groupBoxSearch.Enabled = false;
                searchActive = true;
                

                //Statuspanel einblenden & konfigurieren
                panelstatus.Visible = true;
                labelStatus.Text = statusSuche;
                panelstatus.Size = new Size(panelstatus.Size.Width, 70);
                progressBar1.Style = ProgressBarStyle.Marquee;

                //rework nur unselected löschen
                //dataGridViewSongs.Rows.Clear();
                for (int i = dataGridViewSongs.Rows.Count - 1; i >= 0; i--)
                {
                    if (dataGridViewSongs.Rows[i].Cells[0].Value == null || !(bool)dataGridViewSongs.Rows[i].Cells[0].Value)
                        dataGridViewSongs.Rows.RemoveAt(i);
                    
                }

                //Backgroundworker initalisieren, da ohne einen
                //Backgroundworker das GUI beim Suchen hängen würde
                BackgroundWorker bgwSearch = new BackgroundWorker();
                bgwSearch.WorkerReportsProgress = true;
                bgwSearch.ProgressChanged += new ProgressChangedEventHandler(bgwSearch_ProgressChanged);
                bgwSearch.DoWork += new DoWorkEventHandler(bgwSearch_DoWork);
                bgwSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwSearch_RunWorkerCompleted);
                bgwSearch.RunWorkerAsync();
            }
        }

        void bgwSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
                MessageBox.Show(errorMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (e.ProgressPercentage == 2)
            {
                List<HelperObjects.Song> songs = (e.UserState as List<HelperObjects.Song>);
                foreach (HelperObjects.Song song in songs)
                {
                    dataGridViewSongs.Rows.Add();
                    dataGridViewSongs.Rows[dataGridViewSongs.Rows.Count - 1].Cells[1].Value = HttpUtility.HtmlDecode(song.Title);
                    dataGridViewSongs.Rows[dataGridViewSongs.Rows.Count - 1].Cells[2].Value = HttpUtility.HtmlDecode(song.Artist);
                    dataGridViewSongs.Rows[dataGridViewSongs.Rows.Count - 1].Cells[1].Tag = song;
                }
            }
        }


               

        void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
           
            try
            {
                List<HelperObjects.Song> songs = HelperMySpace.Find.SongsFromHtmlSongpage(textBoxUrl.Text, debugHelper);
                    
                //Wenn neue Suche über Songpage keine Results bringt, versuche alte Methode über Feed
                if (songs.Count == 0)
                    songs = HelperMySpace.Find.SongsFromPlayerPlaylistFeed(textBoxUrl.Text, debugHelper);


                if (songs.Count > 0)
                    (sender as BackgroundWorker).ReportProgress(2, songs);
                else
                    throw new Exception();     
            }
            catch (Exception ee)
            {
                debugHelper.WriteDebug(ee);
                (sender as BackgroundWorker).ReportProgress(1);
            }
            
        }

        void bgwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Die Suche ist somit beendet und der SearchSong Button darf
            //wieder benutzt werden. Das Panel hingegen brauchen wir vorerst nicht
            //mehr und blenden es deshalb aus.
            searchActive = false;
            //SearchSongs.Enabled = true;
            groupBoxSearch.Enabled = true;
            panelstatus.Visible = false;
            debugHelper.WriteDebug("Suche beendet");
        }
        #endregion
        
        #region Download-Button and Download-BGW
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            debugHelper.WriteDebug("Download gestartet");

            //Panel vergrößern für zweite Progressbar
            panelstatus.Size = new Size(panelstatus.Size.Width, 132);

            //Überprüfen ob mindestens ein Song ausgewählt wurde    
            if (CountSongsChecked() == 0)
                MessageBox.Show(nosongMessage, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.SelectedPath = Helper.GetAppSetting("dlpath");
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    if (!Helper.CheckWriteRight(fbd.SelectedPath))
                    {
                        MessageBox.Show("You have no rights to write at this directory. Please choose another directory or start the MySpace Loader as administrator!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Such-Button und Song-Grid für Eingaben sperren
                    //SearchSongs.Enabled = false;
                    groupBoxSearch.Enabled = false;
                    dataGridViewSongs.Enabled = false;

                    //Da es wieder einen Vorgang gibt, auf den der User warten muss, blenden
                    //wir das Satuspanel wieder ein und geben dem Label auf dem Panel einen
                    //aktuellen Statustext.
                    panelstatus.Visible = true;
                    labelStatus.Text = statusInitializing;
                    
                    List<HelperObjects.Song> downloadSongs = new List<HelperObjects.Song>();
                    string cb = comboBoxFormat.SelectedItem.ToString();
                    foreach (DataGridViewRow drv in dataGridViewSongs.Rows)
                    {
                        if (drv.Cells[0].Value != null && (bool)drv.Cells[0].Value)
                        {
                            HelperObjects.Song tempSong = (drv.Cells[1].Tag as HelperObjects.Song);
                            tempSong.DlPath = fbd.SelectedPath;
                            tempSong.Format = cb;
                            tempSong.Codec = cb == ".wma" ? "wmav2" : 
                                                        cb == ".mp3" ? "libmp3lame" : 
                                                                cb == ".ogg" ? "libvorbis" :
                                                                    cb == ".aac" ? "aac" : "flv";
                            downloadSongs.Add(tempSong);
                        }
                    }

                    progressBarSong.Minimum = 0;
                    progressBarSong.Maximum = downloadSongs.Count;

                    BackgroundWorker bgwDownload = new BackgroundWorker();
                    bgwDownload.WorkerReportsProgress = true;
                    bgwDownload.ProgressChanged += new ProgressChangedEventHandler(bgwDownload_ProgressChanged);
                    bgwDownload.DoWork += new DoWorkEventHandler(bgwDownload_DoWork);
                    bgwDownload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwDownload_RunWorkerCompleted);
                    bgwDownload.RunWorkerAsync(downloadSongs);               
                }
            }
        }

        void bgwDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                progressBarSong.Value++;
                labelStatusSong.Text = progressBarSong.Value.ToString() + "/" + progressBarSong.Maximum.ToString();
                this.Text = "[" + (e.UserState as HelperObjects.Song).Title + "] - MySpace Downloader by http://www.code-bude.net";
            }
            else if (e.ProgressPercentage == 2)
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
            else if (e.ProgressPercentage == 3)
            {
                getOutput.Interval = 500;
                getOutput.Tick += new EventHandler(getOutput_Tick);
                getOutput.Tag = "download";
            }
            else if (e.ProgressPercentage == 4)
            {
                getOutput.Start();
            }
            else if (e.ProgressPercentage == 5)
            {
                getOutput.Stop();
            }
        }
            
        void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            debugHelper.WriteDebug("Download Songs BGW entered");

            BackgroundWorker thisBgw = (sender as BackgroundWorker);
            List<HelperObjects.Song> songs = (e.Argument as List<HelperObjects.Song>);

            foreach (HelperObjects.Song song in songs)
            {
                song.DownloadSuccessful = HelperMySpace.Download.DownloadSingleSong(song, thisBgw, ref consoleMessage, ref getOutput, debugHelper);                
            }
            e.Result = songs;
        }
                 
        void bgwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<HelperObjects.Song> downloadedSongs = (e.Result as List<HelperObjects.Song>);
            foreach (DataGridViewRow dgvr in dataGridViewSongs.Rows)
            {
                foreach (HelperObjects.Song song in downloadedSongs)
                {
                    if (song.Id == (dgvr.Cells[1].Tag as HelperObjects.Song).Id)
                        dgvr.DefaultCellStyle.BackColor = song.DownloadSuccessful ? Color.PaleGreen : Color.LightCoral;
                }
            }
            dataGridViewSongs.ClearSelection();
            dataGridViewSongs.CurrentCell = null;

            groupBoxSearch.Enabled = true;
            //SearchSongs.Enabled = true;
            dataGridViewSongs.Enabled = true;
            panelstatus.Visible = false;
            labelStatus.Text = statusSuche;
            progressBarSong.Value = 0;
            this.Text = "MySpace Downloader by www.code-bude.net";
            debugHelper.WriteDebug("Downloads abgeschlossen");
        }
        #endregion
        
        void getOutput_Tick(object sender, EventArgs e)
        {
            string consoleState = (sender as Timer).Tag.ToString();

            if (consoleState == "download" && consoleMessage != null && consoleMessage.Contains(" kB /"))
            {
                labelStatus.Text = consoleMessage.Substring(0, consoleMessage.IndexOf("kB") + 2).Remove(consoleMessage.IndexOf(".") + 3, 1).Replace(".", ",").Replace("kB", "KB") + " " + statusReceived;
            }
            else if (consoleState == "convert" && consoleMessage != null && consoleMessage.Contains("size="))                
            {
                string info = consoleMessage.Substring(consoleMessage.IndexOf("=") + 1);
                info = info.Substring(0, info.ToUpper().IndexOf("KB")+2).Replace(" ti", "").Trim();
                labelStatus.Text = statusGenerateMp3.Replace(".mp3", comboBoxFormat.SelectedItem.ToString()) + " (" + info.ToUpper() + ")";
            }
        }


        private void textBoxUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SearchSongs_Click(SearchSongs, new EventArgs());
        }
        
        private bool searchActive = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            //string html = Helper.GetHtmlSource("https://new.myspace.com/muse/music");
            //Helper.GetHtmlSourceNewMySpace("https://new.myspace.com/muse/music", ref cookieCon);

            Version v = Environment.Version;
            string s = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();

            this.MouseMove += new MouseEventHandler(this.FormMouseMove);
            this.MouseDown += new MouseEventHandler(this.FormMouseDown);
            transparentLabel1.MouseMove += new MouseEventHandler(this.FormMouseMove);
            transparentLabel1.MouseDown += new MouseEventHandler(this.FormMouseDown);
            pictureBoxLogo.MouseMove += new MouseEventHandler(this.FormMouseMove);
            pictureBoxLogo.MouseDown += new MouseEventHandler(this.FormMouseDown);
            label2.MouseMove += new MouseEventHandler(this.FormMouseMove);
            label2.MouseDown += new MouseEventHandler(this.FormMouseDown);

            if (Helper.GetAppSetting("lang") == null)
                Helper.SetAppSetting("lang", "en");
            if (Helper.GetAppSetting("dlpath") == null)
                Helper.SetAppSetting("dlpath", "C:\\");
            if (Helper.GetAppSetting("ms_system") == null)
                Helper.SetAppSetting("ms_system", "old");


            textBoxConfigSave.Text = Helper.GetAppSetting("dlpath");
            radioButtonSearchClassic.Checked = Helper.GetAppSetting("ms_system") == "old" ? true : false;
            radioButtonSearchNew.Checked = Helper.GetAppSetting("ms_system") == "new" ? true : false;
            LoadLanguage();
            Helper.ClearCache();
            comboBoxFormat.SelectedText = ".mp3";
            comboBoxFormat.SelectedItem = ".mp3";

            BackgroundWorker bgwUpdate = new BackgroundWorker();
            bgwUpdate.WorkerReportsProgress = true;
            bgwUpdate.ProgressChanged += new ProgressChangedEventHandler(bgwUpdate_ProgressChanged);
            bgwUpdate.DoWork += new DoWorkEventHandler(bgwUpdate_DoWork);
            bgwUpdate.RunWorkerAsync();

            searchActive = false;
        }

        void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                debugHelper.WriteDebug("Update start");
                if (Helper.UpdateCheck())
                    (sender as BackgroundWorker).ReportProgress(1);
                debugHelper.WriteDebug("Update ende");
            }
            catch (Exception ee)
            {
                debugHelper.WriteDebug(ee);
            }
        }

        void bgwUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FormUpdate frmUpd = new FormUpdate();
            frmUpd.ShowDialog();
        }

        #region Load Language

        private string statusSuche;
        private string statusGenerateMp3;
        private string statusReceived;
        private string ofdFileType;
        private string ofdTitle;
        private string statusInitializing;
        private string errorMessage;
        private string nosongMessage;
        private string errorTitle;

        private void LoadLanguage()
        {
            if (Helper.GetAppSetting("lang") == "en")
            {
                
                labelConfigSave.Text = "Standard download path";
                buttonConfigSave.Text = "open path";
                labelConfigTitle.Text = "Configuration";
                errorTitle = "Error";
                errorMessage = "Couldn't find any song. Maybe you entered a wrong artist-id.";
                nosongMessage = "Please select atleast one song to download.";
                statusInitializing = "initializing...";
                ofdTitle = "Save song";
                ofdFileType = "mp3-files (*.mp3)|*.mp3";
                statusReceived = "received";
                statusGenerateMp3 = "build .mp3-file";
                statusSuche = "searching...";
                SearchSongs.Text = "Search";
                labelStatus.Text = statusSuche;
                groupBox1.Text = "Help";
                label1.Text = @"You need help or want 
to know some tipps and tricks? 
Click on the question mark!";
                checkBoxSelectAll.Text = "Select / deselect all";

            }
            else if (Helper.GetAppSetting("lang") == "de") 
            {
                labelConfigSave.Text = "Standard Download-Ordner";
                buttonConfigSave.Text = "Ordner wählen";
                labelConfigTitle.Text = "Einstellungen";
                errorTitle = "Fehler";
                errorMessage = "Konnte keine Songs finden. Eventuell wurde eine ungültige Profil-ID angegeben.";
                nosongMessage = "Bitte wähle mindestens einen Song aus, der heruntergeladen werden soll.";
                statusInitializing = "Initalisiere...";
                ofdTitle = "Song speichern";
                ofdFileType = "Mp3-Datei (*.mp3)|*.mp3";
                statusReceived = "empfangen";
                statusGenerateMp3 = "Generiere .mp3-Datei";
                statusSuche = "Suche...";
                SearchSongs.Text = "Suchen";
                labelStatus.Text = statusSuche;
                groupBox1.Text = "Hilfe";
                label1.Text = @"Du brauchst Hilfe oder willst 
noch ein paar Tipps und Tricks 
kennenlernen? Dann drücke 
auf das Fragezeichen!";
                checkBoxSelectAll.Text = "Alle an- / abwählen";
            }
        }
        #endregion

        

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;                
        }


       

        #region Handler für das Verschieben der Form
        /// <summary>
        /// Maus-Position im Bildschirm
        /// </summary>
        private Point mousePosition;

        /// <summary>
        /// Verarbeitet das MouseDown-Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Mouse-Event Argumente</param>
        private void FormMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
        this.mousePosition = new Point(-e.X, -e.Y);
        }

        /// <summary>
        /// Verarbeitet das MouseMove-Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Mouse-Event Argumente</param>
        private void FormMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Wenn der Linke Button gedrückt ist
            if (e.Button == MouseButtons.Left)
            {
                // Maus-Position auf dem Control
                Point mousePos = Control.MousePosition;

                // Verschiebt den Punkt um den angegebenden Betrag
                mousePos.Offset(this.mousePosition.X, this.mousePosition.Y);

                // Neue Position setzen
                if (sender.GetType() == typeof(HelperObjects.TransparentLabel))
                {
                    this.Location = new Point(mousePos.X - (sender as Control).Location.X, mousePos.Y - (sender as Control).Location.Y);
                }
                else if (sender.GetType() == typeof(PictureBox))
                {
                    this.Location = new Point(mousePos.X - (sender as Control).Parent.Location.X - (sender as Control).Location.X, mousePos.Y - (sender as Control).Parent.Location.Y - (sender as Control).Location.Y);
                }
                else
                {
                    this.Location = mousePos;
                }

                
            }

            
        }
        #endregion

        

        private void pictureBoxEn_Click(object sender, EventArgs e)
        {
            Helper.SetAppSetting("lang", "en");
            LoadLanguage();
        }

        private void pictureBoxDe_Click(object sender, EventArgs e)
        {
            Helper.SetAppSetting("lang", "de");
            LoadLanguage();
        }

        

        private void dataGridViewSongs_Click(object sender, EventArgs e)
        {
            
            if (dataGridViewSongs.SelectedRows.Count > 0 && dataGridViewSongs.EditingControl == null)
            {
                if (dataGridViewSongs.SelectedRows[0].Cells[0].Value == null || !((bool)dataGridViewSongs.SelectedRows[0].Cells[0].Value))
                    dataGridViewSongs.SelectedRows[0].Cells[0].Value = true;
                else
                    dataGridViewSongs.SelectedRows[0].Cells[0].Value = false;

                int checkedSongs = CountSongsChecked();
                if (checkedSongs == 0)
                    checkBoxSelectAll.CheckState = CheckState.Unchecked;
                else if (checkedSongs == dataGridViewSongs.Rows.Count)
                    checkBoxSelectAll.CheckState = CheckState.Checked;
                else
                    checkBoxSelectAll.CheckState = CheckState.Indeterminate;
            }


        }

        private int CountSongsChecked()
        {
            int checkedSongs = 0;
            foreach (DataGridViewRow dgvr in dataGridViewSongs.Rows)
            {
                if (dgvr.Cells[0].Value != null && ((bool)dgvr.Cells[0].Value))
                    checkedSongs++;
            }
            return checkedSongs;
        }
        

        private void transparentLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxConfig_Click(object sender, EventArgs e)
        {
            if (panelConfig.Visible)
                panelConfig.Visible = false;
            else
            {
                panelConfig.Visible = true;
                panelConfig.BringToFront();
            }
        }

        private void buttonConfigSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxConfigSave.Text = fbd.SelectedPath;
                Helper.SetAppSetting("dlpath", fbd.SelectedPath);
            }
        }

        private void dataGridViewSongs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && !searchActive)
            {
                HelperObjects.Song song = (dataGridViewSongs.Rows[e.RowIndex].Cells[1].Tag as HelperObjects.Song);
                song.Artist = dataGridViewSongs.Rows[e.RowIndex].Cells[2].Value.ToString();
                dataGridViewSongs.Rows[e.RowIndex].Cells[1].Tag = song;
            }
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBoxSelectAll_Click(object sender, EventArgs e)
        {
            if (checkBoxSelectAll.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < dataGridViewSongs.Rows.Count; i++)
                    dataGridViewSongs.Rows[i].Cells[0].Value = false;
            }
            else if (checkBoxSelectAll.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < dataGridViewSongs.Rows.Count; i++)
                    dataGridViewSongs.Rows[i].Cells[0].Value = true;
            }

        }

        private void pictureBoxHelp_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            FormHelp frmHelp = new FormHelp();
            frmHelp.Shown += frmHelp_Shown;
            frmHelp.Show();
        }

        void frmHelp_Shown(object sender, EventArgs e)
        {
            UseWaitCursor = false;
        }

        private void radioButtonSearchClassic_CheckedChanged(object sender, EventArgs e)
        {
            Helper.SetAppSetting("ms_system", radioButtonSearchClassic.Checked ? "old" : "new");
            if (radioButtonSearchClassic.Checked)
            {
                textBoxBase.Text = "http://myspace.com";
                textBoxBase.Width -= 36;
                textBoxUrl.Width += 36;
                textBoxUrl.Location = new Point(textBoxUrl.Location.X - 36, textBoxUrl.Location.Y);
            }
            else
            {
                textBoxBase.Text = "https://new.myspace.com/";
                textBoxBase.Width += 36;
                textBoxUrl.Width -= 36;
                textBoxUrl.Location = new Point(textBoxUrl.Location.X + 36, textBoxUrl.Location.Y);
            }
     
            
        }

       

   
    }
}
