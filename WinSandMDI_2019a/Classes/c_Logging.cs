using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;

namespace WinSandMDI_2019a.Classes
{
    class c_Logging
    {
    }
    public class c_log
    {
 #region region_Log_To_File

        static int iMaxLogLength = 20000;
        static int iTrimmedLogLength = -2000;

        /// <summary>
        /// Should not be used any in Production
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="iLogLevel"></param>
        static public void writeToFile(string strMessage, string strLogFileDirectory, int iLogLevel)
        {
            string strFile = strLogFileDirectory + "FaxDashboard_log.log";
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(strFile, true))
                {
                    writer.WriteLine(iLogLevel.ToString() + "-" + strMessage);
                }
            }
            catch (Exception ex)
            {
                ; // Nothing to do...
                //writeEvent("writeToFile() Failed to write to logfile : " + ex.Message + "... Log Message: " + strMessage + "...Log File:" + strFile + "...", 5);
            }
        }

        static public void writeToFile2(string strMessage, string strLogFileDirectory, int iLogLevel)
        {
            string strFile = strLogFileDirectory + "FaxDashboard_log.log";

            try
            {
                FileInfo fi = new FileInfo(strFile);

                Byte[] bytesRead = null;

                if (fi.Length > iMaxLogLength)
                {
                    using (BinaryReader br = new BinaryReader(File.Open(strFile, FileMode.Open)))
                    {

                        // 3. Seek to our required position.
                        br.BaseStream.Seek(iTrimmedLogLength, SeekOrigin.End);

                        // 4. Read what you want.
                        bytesRead = br.ReadBytes((-1 * iTrimmedLogLength));
                    }
                }

                byte[] newLine = System.Text.ASCIIEncoding.ASCII.GetBytes(Environment.NewLine);

                FileStream fs = null;
                if (fi.Length < iMaxLogLength)
                    fs = new FileStream(strFile, FileMode.Append, FileAccess.Write, FileShare.Read);
                else
                    fs = new FileStream(strFile, FileMode.Create, FileAccess.Write, FileShare.Read);

                using (fs)
                {
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(strMessage);
                    fs.Write(sendBytes, 0, sendBytes.Length);
                    fs.Write(newLine, 0, newLine.Length);
                    if (bytesRead != null)
                    {
                        Byte[] lineBreak = Encoding.ASCII.GetBytes("### *** *** *** Old Log Start Position *** *** *** *** ###");
                        fs.Write(lineBreak, 0, lineBreak.Length);
                        fs.Write(newLine, 0, newLine.Length);
                        fs.Write(bytesRead, 0, bytesRead.Length);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                ; // Nothing to do...
                //writeEvent("writeToFile() Failed to write to logfile : " + ex.Message + "... Log Message: " + strMessage + "...Log File:" + strFile + "...", 5);
            }
        }
        #endregion //region_Log_To_File


//#if THIS_DOES_NOT_LOG_TO_EVENT_VIEWER_OR_TO_EMAIL___ONLY_TO_FILE
#region region_Event_Viewer
        /// <summary>
        /// If it is supposed to go to Event Viewer, this does it.
        /// This would be internal, but there are times to force a message to the Event Viewer with no routing
        /// </summary>
        /// <param name="strMessage">Message to write</param>
        /// <param name="iLogLevel">Severity, 1 - 5</param>
        public static void writeEvent(string strMessage, int iLogLevel)
        {
            EventLogEntryType EvType = new EventLogEntryType();

            if (iLogLevel == 1)
                EvType = EventLogEntryType.Information;
            else if (iLogLevel == 5)
                EvType = EventLogEntryType.Error;
            else if (iLogLevel == 4)
                EvType = EventLogEntryType.Warning;
            else if (iLogLevel == 3)
                EvType = EventLogEntryType.FailureAudit;
            else if (iLogLevel == 2)
                EvType = EventLogEntryType.SuccessAudit;
            else
                EvType = EventLogEntryType.Error;

            string sSource = "FaxDashboard";
            string sLog = "Application";
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);
            EventLog.WriteEntry(sSource, strMessage, EvType, iLogLevel);
        }

#endregion // region_Event_Viewer

#region region_Email
        public static void sendEmail(string subcat
                             , string sTo
                             , string sFrom
                             , string sUserName
                             , string sPassword
                             , string sEmailServer // sDomain
                             , string sSubject
                             , string sBody
                             , out string strError
                             , string sTo2 = "")
        {
            strError = String.Empty;
            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo = new System.Net.NetworkCredential();
            SmtpClient objSmtpClient; // = new SmtpClient();  //exchange or smtp server goes here. 

            try
            {
                objMailMessage.From = new MailAddress(sFrom);
                objMailMessage.To.Add(new MailAddress(sTo));

                if (sTo2 != String.Empty)
                    objMailMessage.To.Add(new MailAddress(sTo2));

                objMailMessage.Subject = "(" + subcat + ")-Msg from CorrFax Service.";  //strSubject;
                if (sSubject != String.Empty)
                    objMailMessage.Subject = "(" + subcat + ")" + sSubject + "- Msg from CorrFax Service.";
                objMailMessage.Body = strMakeHTMLMessage_Completion(sBody); // sBody;
                objMailMessage.IsBodyHtml = true;

                objSmtpClient = new SmtpClient(sEmailServer); // Exchange or SMTP Server IP
                //objSMTPUserInfo = new System.Net.NetworkCredential("User name", "Password", "Domain");
                sPassword = "Heptagon;555";
                objSMTPUserInfo = new System.Net.NetworkCredential(sUserName, sPassword, sEmailServer);
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // Seems to be for Exchange

                objSmtpClient.Send(objMailMessage);

            }
            catch (Exception ex)
            { strError = ex.Message + "..." + ex.InnerException + "."; }

            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }
        }
        public static string strMakeHTMLMessage_Completion(string strBody)
        {
            DateTime dt = DateTime.Now;

            return "<html><head><style>"
                   + ".msgtext { FONT-SIZE: 11pt; COLOR: black; FONT-FAMILY: Tahoma; BACKGROUND-COLOR: white;TEXT-ALIGN: left;}</style>"
                   + "</head><body><font class='msgtext'>"
                   + strBody
                   + "<br><br><font style=\"FONT-SIZE: 8pt; COLOR: blue\">This email message was generated by the FaxDashboard application running on \\\\"
                   + System.Environment.MachineName
                   + "</font>&nbsp;-&nbsp;<font style=\"FONT-SIZE: 8pt; COLOR: black\">"
                   + dt.ToLocalTime() // "%02d/%02d/%04d  %02d:%02d:%02d", tm.wMonth, tm.wDay, tm.wYear, tm.wHour, tm.wMinute, tm.wSecond
                // + csInstance // m_pLog->m_pSettings->InstanceName()) ...???
                   + "</font></font></body></html>";
        }
#endregion // End region_Email
//endif

    } // End    public class c_log    

}
