using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;

namespace MySpaceLoader
{
    
    class Helper
    {
        private static int version = 5;

        
       

        public static string GetHtmlSourceNewMySpace(string url, ref CookieContainer cookieCon)
        {
            //if (cookieCon == null || !LoggedInToNewMySpace())
            if (!LoggedInToNewMySpace())
            {
            
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://new.myspace.com/signin");
                if (cookieCon == null)
                    cookieCon = new CookieContainer();
                request.CookieContainer = cookieCon;
                //Get the response from the server and save the cookies from the first request..
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();                
                StreamReader srHash = new StreamReader(response.GetResponseStream());
                string hash = srHash.ReadToEnd();
                srHash.Close();
                response.Close();
                if (hash.Contains("hashMashter"))
                {
                    hash = hash.Substring(hash.IndexOf("hashMashter") + "hashMashter".Length + 3);
                    hash = hash.Substring(0,hash.IndexOf("\""));
                }

                /*
                request = (HttpWebRequest)WebRequest.Create("https://new.myspace.com/signin");                
                request.CookieContainer = cookieCon;
                //Get the response from the server and save the cookies from the first request..
                response = (HttpWebResponse)request.GetResponse();
               */


                string getUrl = "https://new.myspace.com/ajax/account/signin";
                string postData = String.Format("email={0}&password={1}&rememberMe=on", "info%40code-bude.net", "01603543657");
                HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(getUrl);
                getRequest.CookieContainer = cookieCon;
                getRequest.Method = WebRequestMethods.Http.Post;
                getRequest.Accept = "*/*";
                getRequest.Headers.Add("Accept-Language", "de-de,de;q=0.8,en-us;q=0.5,en;q=0.3");
                getRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                getRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
                getRequest.Headers.Add("Hash", hash);

                getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
                getRequest.AllowWriteStreamBuffering = true;
                getRequest.ProtocolVersion = HttpVersion.Version11;
                getRequest.AllowAutoRedirect = true;
                getRequest.ContentType = "Content-Type: application/x-www-form-urlencoded; charset=UTF-8";
                getRequest.KeepAlive = true;
                getRequest.Referer = "https://new.myspace.com/signin";
                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                getRequest.ContentLength = byteArray.Length;
                Stream newStream = getRequest.GetRequestStream(); //open connection
                newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
                newStream.Close();

                HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
                StreamReader sr = new StreamReader(getResponse.GetResponseStream());
                string sourceCode = sr.ReadToEnd();
                sr.Close();
                getResponse.Close();
            }
            else
            {

            }
            bool loggedIn = LoggedInToNewMySpace();
            return "1";
        }

        private static bool LoggedInToNewMySpace()
        {
            string html = GetHtmlSource("https://new.myspace.com/signin");
            if (html.Contains("<a href=\"/home\">Home</a>"))
                return true;
            else
                return false;
        }

        public static string GetHtmlSource(string url, int? timeoutSeconds = null)
        {
            string html;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            if (timeoutSeconds != null)
                req.Timeout = (int)timeoutSeconds;
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                html = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                res.Close();
            }
            return html;
        }

        /// <summary>
        /// encrypt and decrypt strings
        /// </summary>
        public class Encryption
        {
            /// <summary>
            /// Encrypts the string.
            /// </summary>
            /// <param name="clearText">The clear text.</param>
            /// <param name="Key">The key.</param>
            /// <param name="IV">The IV.</param>
            /// <returns></returns>
            private static byte[] EncryptString(byte[] clearText, byte[] Key, byte[] IV)
            {
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;
                CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearText, 0, clearText.Length);
                cs.Close();
                byte[] encryptedData = ms.ToArray();
                return encryptedData;
            }

            /// <summary>
            /// Encrypts the string.
            /// </summary>
            /// <param name="clearText">The clear text.</param>
            /// <param name="Password">The password.</param>
            /// <returns></returns>
            public static string EncryptString(string clearText, string Password)
            {
                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                byte[] encryptedData = EncryptString(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return Convert.ToBase64String(encryptedData);
            }

            /// <summary>
            /// Decrypts the string.
            /// </summary>
            /// <param name="cipherData">The cipher data.</param>
            /// <param name="Key">The key.</param>
            /// <param name="IV">The IV.</param>
            /// <returns></returns>
            private static byte[] DecryptString(byte[] cipherData, byte[] Key, byte[] IV)
            {
                MemoryStream ms = new MemoryStream();
                Rijndael alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;
                CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
                byte[] decryptedData = ms.ToArray();
                return decryptedData;
            }

            /// <summary>
            /// Decrypts the string.
            /// </summary>
            /// <param name="cipherText">The cipher text.</param>
            /// <param name="Password">The password.</param>
            /// <returns></returns>
            public static string DecryptString(string cipherText, string Password)
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                byte[] decryptedData = DecryptString(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
        }

        public class Debug
        {
            bool debug;
            public Debug(bool debugOn)
            {
                this.debug = debugOn;
            }

            public void WriteDebug(Exception e)
            {
                WriteDebug("=> Fehler passiert\r\nMessage: " + e.Message + "\r\nInnerException: " + e.InnerException);
            }

            public bool ExportDebug()
            {
                if (File.Exists(Application.StartupPath + "\\error_log.txt"))
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\error_log.txt");
                    string log = sr.ReadToEnd();
                    sr.Close();
                    log = Encryption.DecryptString(log, "Goslar!\"§$%&");
                    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\error_log_encoded.txt", false);
                    sw.Write(log);
                    sw.Close();
                    return true;
                }
                else
                    return false;
            }

            public void WriteDebug(string str)
            {
                if (debug)
                {
                    try
                    {
                        string existentLog = string.Empty;
                        if (File.Exists(Application.StartupPath + "\\error_log.txt"))
                        {                       
                            StreamReader sr = new StreamReader(Application.StartupPath + "\\error_log.txt");
                            existentLog = sr.ReadToEnd();
                            sr.Close();                        
                        }
                        if (!string.IsNullOrEmpty(existentLog))
                            existentLog = Encryption.DecryptString(existentLog, "Goslar!\"§$%&");
                        existentLog += "\r\n== " + DateTime.Now.ToLongTimeString() + " ==";
                        existentLog += "\r\n" + str + "\r\n\r\n";
                        StreamWriter sw = new StreamWriter(Application.StartupPath + "\\error_log.txt", false);
                        sw.Write(Encryption.EncryptString(existentLog, "Goslar!\"§$%&"));
                        sw.Close();
                    }
                    catch{}
                }
            }
        }

        public static bool UpdateCheck()
        {
            try
            {
                int onlineversion = Convert.ToInt32(GetHtmlSource("http://www.code-bude.net/downloads/myspace/myspaceversion.txt", 10000));
                if (onlineversion > version)
                {
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }        

        public static string GetAppSetting(string key)
        {
            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(
                                    System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Zurückgeben der dem Key zugehörigen Value
            return config.AppSettings.Settings[key] != null ? config.AppSettings.Settings[key].Value : null;
        }

        public static void SetAppSetting(string key, string value)
        {
            //Laden der AppSettings
            Configuration config = ConfigurationManager.OpenExeConfiguration(
                                    System.Reflection.Assembly.GetExecutingAssembly().Location);
            //Überprüfen ob Key existiert
            if (config.AppSettings.Settings[key] != null)
            {
                //Key existiert. Löschen des Keys zum "überschreiben"
                config.AppSettings.Settings.Remove(key);
            }
            //Anlegen eines neuen KeyValue-Paars
            config.AppSettings.Settings.Add(key, value);
            //Speichern der aktualisierten AppSettings
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static bool CheckWriteRight(string directory)
        {
            bool canWrite = false;
            try
            {
                StreamWriter sw = new StreamWriter(directory + "\\write.chk");
                sw.Write("a");
                sw.Close();
                canWrite = true;
            }
            catch (Exception fail)
            {
                if (fail.GetType() == typeof(UnauthorizedAccessException))
                    canWrite = false;
                else
                    canWrite = true;
            }
            finally
            {
                try
                {
                    if (File.Exists(directory + "\\write.chk"))
                    {
                        File.Delete(directory + "\\write.chk");
                    }
                }
                catch { }
            }
            return canWrite;
        }

        public static void ClearCache()
        {
           
            foreach (string file in Directory.GetFiles(Application.StartupPath + "\\Utils"))
            {
                if (!file.Contains("scon") && !file.Contains("sload"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        try
                        {
                            //Workaround von Stackoverflow.com um dem Explorer Zeit zu geben das Handle freizugeben 
                            Thread.Sleep(0);
                            File.Delete(file);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
