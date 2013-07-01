using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Superstar.Html.Linq;
using System.Diagnostics;
using ID3TagLib;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.ComponentModel;

namespace MySpaceLoader
{
    class HelperMySpace
    {
        public static class Find
        {
            public static List<HelperObjects.Song> SongsFromHtmlSongpage(string profileName, Helper.Debug debugHelper)
            {
                List<HelperObjects.Song> songs = new List<HelperObjects.Song>();
                List < HelperObjects.Song > tempSongs;
                int startId = 1;

                while ((tempSongs = Find.SongFromHtmlPagewise(profileName, startId, debugHelper)).Count > 0)
                {
                    songs.AddRange(tempSongs.ToArray());
                    tempSongs.Clear();
                    startId += 20;
                }
                return songs;
            }

            private static List<HelperObjects.Song> SongFromHtmlPagewise(string profileName, int songStartId, Helper.Debug debugHelper)
            {
                List<HelperObjects.Song> songs = new List<HelperObjects.Song>();
                try
                {
                    //Erstellen eines Webrequest und auslesen des Quelltexts der
                    //MySpace Songpage in den String "html"
                    string html = Helper.GetHtmlSource("http://www.myspace.com/" + profileName + "/music/songs?pageNo=2&typeid=12021&filter=alphabetical&nextStartId=" + songStartId.ToString() + "&pages=1-1");
                    debugHelper.WriteDebug("Songpage HTML erhalten. Length: " + html.Length.ToString());

                    //Herraussuchen der für den nächsten Schritt benötigten
                    //Variablen aus dem Quelltext  

                    debugHelper.WriteDebug("Suche Songs mit neuer Methode");
                    HDocument document = HDocument.Parse(html);
                    HElement songTab = document.Descendants("section").Where(x => x.Attribute("id") != null && x.Attribute("id").Value == "col1_0").Single()
                                               .Descendants("section").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "content moduleBody").Single();
                    List<HElement> songContainer = songTab.Descendants("li").Where(x => x.Attribute("class") != null && x.Attribute("class").Value.StartsWith("song")).ToList();
                    foreach (HElement hEl in songContainer)
                    {

                        songs.Add(new HelperObjects.Song()
                        {
                            Title = hEl.Descendants("div").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "songDetails").Single()
                                         .Descendants("span").Where(x => x.Attribute("itemprop") != null && x.Attribute("itemprop").Value == "name")
                                         .Single().Value,
                            Artist = hEl.Descendants("div").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "songDetails").Single()
                                         .Descendants("div").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "artistName").Single()
                                         .Descendants("a").Single().Value,
                            Id = hEl.Descendants("div").Where(x => x.Attribute("class") != null && x.Attribute("class").Value.Contains("playAction")).Single().Attribute("data-songid").Value
                        });
                    }
                    return songs;
                }
                catch (Exception ee)
                {
                    debugHelper.WriteDebug(ee);
                    return songs;
                }
            }

            public static List<HelperObjects.Song> SongsFromPlayerPlaylistFeed(string profileName, Helper.Debug debugHelper)
            {
                List<HelperObjects.Song> songs = new List<HelperObjects.Song>();
                try
                {
                    //Erstellen eines Webrequest und auslesen des Quelltexts des 
                    //MySpace Profils in den String "html"
                    string html = Helper.GetHtmlSource("http://www.myspace.com/" + profileName);
                    string profid = string.Empty;
                    debugHelper.WriteDebug("Profil HTML erhalten. Length: " + html.Length.ToString());

                    //Herraussuchen der für den nächsten Schritt benötigten
                    //Variablen aus dem Quelltext                
                    try
                    {
                        debugHelper.WriteDebug("Suche ProfID Method 1");
                        HDocument document = HDocument.Parse(html);
                        HElement userid = document.Descendants((HName)"ol").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "horizontalMenu group").Descendants((HName)"li").Descendants((HName)"a").FirstOrDefault();
                        profid = userid.Attribute("href") == null ? string.Empty : userid.Attribute("href").Value;
                        if (!string.IsNullOrEmpty(profid))
                        {
                            profid = profid.Trim('/');
                            profid = profid.Substring(0, profid.IndexOf('/'));
                        }
                    }
                    catch (Exception ee)
                    {
                        debugHelper.WriteDebug(ee);
                    }

                    if (string.IsNullOrEmpty(profid))
                    {
                        Regex reg = new Regex(@"(profid=)+[0-9]+");
                        profid = reg.Match(html).Value.Replace("profid=", "");
                    }
                    if (string.IsNullOrEmpty(profid))
                    {
                        Regex reg = new Regex(@"(prid"":"")+[0-9]+");
                        profid = reg.Match(html).Value.Replace("prid\":\"", "");
                    }
                    debugHelper.WriteDebug("Suche ProfID Ende. ProfID: " + profid);


                    //Wenn das angegebene MySpaceProfil gültig war, dann
                    //forde die nächste Seite (XML) an.
                    if (!string.IsNullOrEmpty(profid))
                    {

                        //Einlesen über XML-Feed (alt)

                        XDocument xdoc = XDocument.Load("http://www.myspace.com/music/services/player?action=getArtistPlaylist&artistUserId=" + profid + "&artistId=" + profid);
                        XNamespace ns = xdoc.Root.Attribute("xmlns").Value;

                        songs = (from song
                                    in xdoc.Document.Descendants(ns + "track")
                                                          select new HelperObjects.Song
                                                          {
                                                              Title = song.Descendants(ns + "title").First().Value,
                                                              Artist = song.Descendants(ns + "artist").First().Attribute("name").Value,
                                                              Id = song.Descendants(ns + "song").First().Attribute("songId").Value
                                                          }).ToList();

                        debugHelper.WriteDebug("Songs gefunden. SongList-Length: " + songs.Count.ToString());

                      

                    }

                    return songs;

                }
                catch
                {
                    return songs;
                }
                
            }
        }

        public static class Download
        {
            public static bool DownloadSingleSong(HelperObjects.Song song, BackgroundWorker thisBgw, ref string consoleMessage, ref Timer getOutput, Helper.Debug debugHelper)
            {
                
                //SDK-Wrapper (Version) aus Player-Seite auslesen
                string sdkWrapper = GetPlayerSDK(song);          
                               
                //Lösche temporäres Downloadverzeichnis
                Helper.ClearCache();

                //Aktualisiere GUI (Window Title, Progressbar auf aktuellen Song)
                thisBgw.ReportProgress(1, song);

                try
                {
                  

                    //Starte sload-Download
                    debugHelper.WriteDebug("Starte Song: " + song.Title);
                    RunSload(ref song, sdkWrapper, ref consoleMessage, thisBgw, debugHelper);
                    try
                    {
                        FileInfo fi = new FileInfo(Application.StartupPath + "\\Utils\\temp.flv");
                        debugHelper.WriteDebug("Ende sload. File-Size: " + fi.Length.ToString());
                    }
                    catch (Exception eeSload) { debugHelper.WriteDebug("Fehler bei sload."); debugHelper.WriteDebug(eeSload); }

                    //Setze Konsolentimer-Modus auf "convert"
                    getOutput.Tag = "convert";

                    //Parse unter Windows verbotene Zeichen aus zukünftigem Dateinamen
                    foreach (char chr in Path.GetInvalidFileNameChars())
                    {
                        song.Title = song.Title.Replace(chr.ToString(), "");
                        song.Artist = song.Artist.Replace(chr.ToString(), "");
                    }
                    song.Title = HttpUtility.HtmlDecode(song.Title);
                    song.Artist = HttpUtility.HtmlDecode(song.Artist);

                    //Setze temporären und finalen Downloadpfad
                    string filePath = song.DlPath + "\\" + song.Artist + " - " + song.Title + song.Format;
                    string filePathSimple = Application.StartupPath + "\\Utils\\" + DateTime.Now.Ticks.ToString() + song.Format;

                    if (song.Codec != "flv")
                    {
                        #region Dateikonvertierung

                        //Starte scon-Konvertierung
                        debugHelper.WriteDebug("Begin convert.\r\nfilePath: " + filePath + "\r\nfilePathSimple: " + filePathSimple);
                        RunSCon(song, filePathSimple, ref consoleMessage, thisBgw, debugHelper);
                        debugHelper.WriteDebug("Conversion ended.");
                        try
                        {
                            FileInfo fi = new FileInfo(filePathSimple);
                            debugHelper.WriteDebug("Ende scon. File-Size: " + fi.Length.ToString());
                        }
                        catch (Exception eeSload) { debugHelper.WriteDebug("Fehler bei scon."); debugHelper.WriteDebug(eeSload); }

                        //Benenne & verschiebe mp3 von temporär zu final
                        try
                        {
                            File.Move(filePathSimple, filePath);
                        }
                        catch (Exception ee)
                        {
                            debugHelper.WriteDebug("File Move Error =>");
                            debugHelper.WriteDebug(ee);
                        }

                        //Setze ID3-Tags
                        debugHelper.WriteDebug("ID3-Tag start");
                        bool success = SetID3Tag(filePath, song);
                        debugHelper.WriteDebug("ID3-Tag ende. Tags " + (success ? "erfolgreich" : "mit Problem") + " erstellt. Song fertig.");

                        #endregion
                    }
                    else
                    {
                        try
                        {
                            File.Move(Application.StartupPath + "\\Utils\\temp.flv", filePath);
                        }
                        catch (Exception ee)
                        {
                            debugHelper.WriteDebug("File Move Error =>");
                            debugHelper.WriteDebug(ee);
                        }
                    }

                    //Stoppe Konsolentimer (Modus: "convert")
                    thisBgw.ReportProgress(5);
                    return true;
                }
                catch (Exception ee)
                {
                    debugHelper.WriteDebug("Genereller Fehler beim Download");
                    debugHelper.WriteDebug(ee);
                    return false;
                }
            }

            private static bool SetID3Tag(string filePath, HelperObjects.Song song)
            {
                try
                {
                    ID3File songID3 = new ID3File(filePath);
                    ID3v1Tag v1Tag = songID3.ID3v1Tag;
                    if (v1Tag == null)
                    {
                        v1Tag = new ID3v1Tag();
                        songID3.ID3v1Tag = v1Tag;
                    }
                    ID3v2Tag v2Tag = songID3.ID3v2Tag;
                    if (v2Tag == null)
                    {
                        v2Tag = new ID3v2Tag();
                        songID3.ID3v2Tag = v2Tag;
                    }
                    // Frame mit der ID "TALB"(=FrameFactory.AlbumFrameId) im Tag suchen.
                    // Liefert null, wenn kein Frame mit der ID "TALB" vorhanden ist.
                    TextFrame f = v2Tag.Frames[FrameFactory.TitleFrameId] as TextFrame;
                    if (f == null)
                    {
                        // neuen Frame anlegen. FrameFactory.GetFrame liefert den passenden
                        // Frame zur ID. Also für z.B. "TALB" einen TextFrame, for "APIC"
                        // einen PictureFrame.
                        f = FrameFactory.GetFrame(FrameFactory.TitleFrameId) as TextFrame;
                        v2Tag.Frames.Add(f);
                    }
                    f.Text = song.Title;

                    TextFrame g = v2Tag.Frames[FrameFactory.AlbumFrameId] as TextFrame;
                    if (g == null)
                    {
                        // neuen Frame anlegen. FrameFactory.GetFrame liefert den passenden
                        // Frame zur ID. Also für z.B. "TALB" einen TextFrame, for "APIC"
                        // einen PictureFrame.
                        g = FrameFactory.GetFrame(FrameFactory.AlbumFrameId) as TextFrame;
                        v2Tag.Frames.Add(g);
                    }
                    g.Text = "";

                    TextFrame h = v2Tag.Frames[FrameFactory.OriginalArtistFrameId] as TextFrame;
                    if (h == null)
                    {
                        // neuen Frame anlegen. FrameFactory.GetFrame liefert den passenden
                        // Frame zur ID. Also für z.B. "TALB" einen TextFrame, for "APIC"
                        // einen PictureFrame.
                        h = FrameFactory.GetFrame(FrameFactory.OriginalArtistFrameId) as TextFrame;
                        v2Tag.Frames.Add(h);
                    }
                    h.Text = song.Artist;


                    v1Tag.Title = song.Title;
                    v1Tag.Album = "";
                    v1Tag.Artist = song.Artist;

                    songID3.Save(filePath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private static string GetPlayerSDK(HelperObjects.Song song)
            {
                try
                {
                    string playerHtml = Helper.GetHtmlSource("http://www.myspace.com/music/player?sid=" + song.Id);
                    playerHtml = playerHtml.Substring(playerHtml.IndexOf("sdkwrapper/SDKWrapper"));
                    playerHtml = playerHtml.Replace("sdkwrapper/", "");
                    playerHtml = playerHtml.Substring(0, playerHtml.IndexOf(".swf") + 4);
                    string sdkWrapper = playerHtml;
                    if (!sdkWrapper.Contains("SDKWrapper") || sdkWrapper.Substring(sdkWrapper.Length - 4) != ".swf")
                    {
                        //SDK Wrapper Fallback. Falscher SDK-Wrapper kann Download verhindern!
                        if (string.IsNullOrEmpty(sdkWrapper))
                            sdkWrapper = "SDKWrapper.2.2.6.swf";
                    } 
                    return sdkWrapper;
                }
                catch { return string.Empty; }
            }

            private static void RunSload(ref HelperObjects.Song song, string sdkWrapper, ref string consoleMessage, BackgroundWorker thisBgw, Helper.Debug debugHelper)
            {
                //Lade Song-Informationen                    
                    XDocument xdoc = XDocument.Load("http://www.myspace.com/music/services/player?songId=" + song.Id + "&action=getSong&sample=0&ptype=4");
                    XNamespace ns = xdoc.Root.Attribute("xmlns").Value;
                    song.rtmpe = (from songdata
                                   in xdoc.Document.Descendants(ns + "rtmp")
                                  select songdata.Value).First();

                    //Fix für RTMP Dump
                    song.rtmpe = song.rtmpe.Replace("rtmpte", "rtmpe");

                    //Macht aus 30sek. Samples die vollen Streams
                    song.rtmpe = song.rtmpe.Replace("clip", "std");

                    //Konfiguriere Timer zum auslesen von gehookter Konsolenanwendung. Setze Timermodus auf "download".
                    thisBgw.ReportProgress(3);

                    //Starte Timer zum auslesen der Konsolenanwendung (sload)
                    thisBgw.ReportProgress(4);

                    debugHelper.WriteDebug("RTMPE-Url: " + song.rtmpe);

                    //Setze sload-Pfad (RTMPDump) zusammen
                    string r = song.rtmpe.Substring(0, song.rtmpe.IndexOf(".com/") + 5);
                    string y = string.Empty;
                    if (song.rtmpe.Contains(".m4a"))
                        y = "mp4:" + song.rtmpe.Substring(song.rtmpe.IndexOf(".com/") + 5);
                    else
                    {
                        y = "mp3:" + song.rtmpe.Substring(song.rtmpe.IndexOf(".com/") + 5);
                        y = y.Substring(0, y.Length - 4);
                    }
                    string w = "http://lads.myspacecdn.com/music/sdkwrapper/" + sdkWrapper;
                    string p = "http://www.myspace.com/music/player?sid=" + song.Id + "&ac=now";
                    string rtmpdumpParams = " -r \"" + r + "\" -W \"" + w + "\" -p \"" + p + "\" -y \"" + y + "\" -o temp.flv";

                    //Konfiguriere sload-Prozess
                    ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\Utils\\sload.exe", rtmpdumpParams);
                    psi.RedirectStandardError = true;
                    psi.RedirectStandardOutput = true;
                    psi.WindowStyle = ProcessWindowStyle.Hidden;
                    psi.CreateNoWindow = true;
                    psi.WorkingDirectory = Application.StartupPath + "\\Utils";
                    psi.UseShellExecute = false;

                    //Starte sload
                    Process ps = new Process();
                    ps.StartInfo = psi;
                    ps.Start();
                    debugHelper.WriteDebug("Start sload. Params: " + rtmpdumpParams);

                    while (!ps.HasExited)
                    {
                        string line = ps.StandardError.ReadLine();
                        if (line != consoleMessage)
                            consoleMessage = line;
                    }

                    //Stoppe Timer zum auslesen der Konsolenanwendung
                    thisBgw.ReportProgress(5);
            }

            private static void RunSCon(HelperObjects.Song song, string filePathSimple, ref string consoleMessage, BackgroundWorker thisBgw, Helper.Debug debugHelper)
            {

                //Konfiguriere scon (ffmpeg) Prozess
                ProcessStartInfo psi = new ProcessStartInfo(Application.StartupPath + "\\Utils\\scon.exe", "-vn -i \"" + Application.StartupPath + "\\Utils\\temp.flv\" -ab 192kb -acodec " + song.Codec + " \"" + filePathSimple + "\"");
                psi.RedirectStandardError = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.CreateNoWindow = true;
                psi.WorkingDirectory = Application.StartupPath + "\\Utils";
                psi.UseShellExecute = false;
                debugHelper.WriteDebug("Convert Params: " + psi.Arguments);

                //Starte scon
                Process ps = new Process();
                ps.StartInfo = psi;
                ps.Start();

                //Starte Konsolentimer
                thisBgw.ReportProgress(4);

                while (!ps.HasExited)
                {
                    string line = ps.StandardError.ReadLine();
                    if (line != consoleMessage)
                        consoleMessage = line;
                }

            }
        }
    }
}
