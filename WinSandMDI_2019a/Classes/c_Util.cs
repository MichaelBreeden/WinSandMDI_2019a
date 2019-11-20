using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Data;
//using OfficeAutomationDLL;

namespace WinSandMDI_2019a.Classes
{
    class c_Util
    {
        /// <summary>
        /// A blank or NULL string of an integer may not mean an error
        /// </summary>
        /// <param name="strValue">string to convert to integer if possible</param>
        /// <returns>integer value returned</returns>
        public static int iReturn_0_ForNonIntegerString(string strValue)
        {
            if (strValue == null)
                return 0;
            int ii = 0;
            if (Int32.TryParse(strValue, out ii) == true)
                return ii;
            return 0;
        }

        /// <summary>
        /// Verifies that there are strings in an address for City, State and Zip. strErrorReason is unused right now.
        /// </summary>
        /// <param name="addrString"></param>
        /// <param name="strErrorReason">Unused</param>
        /// <returns></returns>
        // Chatsworth, CA 91311 - Good
        // Chatsworth CA 91311 - Good
        // Chatsworth Park, CA 91311 - Good
        // Chatsworth Park 91311 - Bad
        public static bool bHasCityStateZip(string addrString, out string strErrorReason)
        {
            strErrorReason = String.Empty;

            string sADDR_CITY = String.Empty;
            string sADDR_STATE = String.Empty;
            string sADDR_ZIP = String.Empty;
            string strErr = String.Empty;

            addrString = addrString.Replace(".", "").Replace(",", ", ").Replace("  ", " ");
            System.Text.RegularExpressions.Regex addressPattern =
                new System.Text.RegularExpressions.Regex(@"(?<city>[A-Za-z',.\s]+) (?<state>([A-Za-z]{2}|[A-Za-z]{2},))\s*(?<zip>\d{5}(-\d{4})|\d{5})");

            System.Text.RegularExpressions.MatchCollection matches4 = addressPattern.Matches(addrString);

            for (int mc = 0; mc < matches4.Count; mc++)
            {
                sADDR_CITY = matches4[mc].Groups["city"].Value;
                sADDR_STATE = matches4[mc].Groups["state"].Value;
                sADDR_ZIP = matches4[mc].Groups["zip"].Value;
            }

            string ss = sADDR_STATE.Trim();
            if (ss == String.Empty)
                return false;

            string s = sADDR_CITY.Trim();
            if ((s.Length < 1) ||
               (s[0] == ',') ||
               (s[0] == '.') ||
               (s[0] == ';')
              )
                return false;

            return true;
            /*

            string strComma = String.Empty;
            if (sADDR_CITY.Trim() == String.Empty)
            {
                strErr = "City";
                strComma = ", ";
            }
            if (sADDR_STATE.Trim() == String.Empty)
            {
                strErr += strComma + "State";
                strComma = ", ";
            }
            if (sADDR_ZIP.Trim() == String.Empty)
            {
                strErr += strComma + "Zip";
            }

            if (strErr == String.Empty)
            {
                return false;
            }
            strErrorReason = strErr;
            return true;
            */
        } // End public static bool bHasCityStateZip

        /// <summary>
        ///  Test for valid IP address
        /// </summary>
        /// <param name="addrString"></param>
        /// <returns></returns>
        public static bool bIsIPAddressValid(string addrString)
        {
            System.Net.IPAddress address;
            return System.Net.IPAddress.TryParse(addrString, out address);
        }

        /// <summary>
        /// Binary file copy. This is very fast
        /// </summary>
        /// <param name="srcfilename"></param>
        /// <param name="destfilename"></param>
        public static void CopyFileBinary(string srcfilename, string destfilename)
        {
            System.IO.Stream s1 = System.IO.File.Open(srcfilename, System.IO.FileMode.Open);
            System.IO.Stream s2 = System.IO.File.Open(destfilename, System.IO.FileMode.Create);
            System.IO.BinaryReader f1 = new System.IO.BinaryReader(s1);
            System.IO.BinaryWriter f2 = new System.IO.BinaryWriter(s2);

            while (true)
            {
                byte[] buf = new byte[1024];
                int sz = f1.Read(buf, 0, 1024);
                if (sz <= 0)
                    break;

                f2.Write(buf, 0, sz);
                if (sz < 10240)
                    break; // eof reached
            }

            f1.Close();
            f2.Close();
        }

        /// <summary>
        /// Uses Regex to get count of pages in PDF Document
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int getNumberOfPdfPages(string fileName)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(sr.ReadToEnd());
                return matches.Count;
            }
        }

        /// <summary>
        /// Return count of pages in a PostScript File
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="strFound"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public static int iGetPagesCountFromPostscriptFile(string strFileName, out string strFound, out string strError)
        {
            strError = String.Empty;
            strFound = String.Empty;
            int iMaxLinesToRead = 15;
            int iLineCounter = 0;
            int iPagesCount = -1;
            const Int32 BufferSize = 512;
            using (var fileStream = File.OpenRead(strFileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                while ((strFound = streamReader.ReadLine()) != null)
                {
                    if (strFound.Contains("%%Pages: ") == true)
                    {
                        if (strFound.Contains("%%Pages: (atend)") == true)
                            iMaxLinesToRead = Int32.MaxValue;

                        else if (Int32.TryParse(strFound.Replace("%%Pages: ", ""), out iPagesCount) == false)
                        {
                            strError = "Error, could not parse value of (" + strFound.Replace("%%Pages: ", "") + ")...";
                            return -3; // 
                        }
                        else
                            return iPagesCount;
                    }
                    iLineCounter++;
                    if (iLineCounter > iMaxLinesToRead)
                    {
                        strError = "Error, could not find '%%Pages: ' after (" + iLineCounter.ToString() + ") lines...";
                        return -5; // 
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Remove unprintable chracters from string using Regex
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string strRemoveSpecialCharsReg(string strInput)
        {
            string str = String.Empty;
            try
            {
                str = System.Text.RegularExpressions.Regex.Replace(strInput, @"[^ -~]+", string.Empty); // printable characters lie between space (" ") and "~" [GOOD] // str;
            }
            catch (Exception ereg)
            {
                return str;
            }
            return str;
        }

        /// <summary>
        /// Return milliseconds between two DateTimes
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double dTaskTime(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan elapsed = dtEnd - dtStart;
            return elapsed.TotalMilliseconds;
        }

        //public static long iReturn_0_ForNonLongString(string strValue)
        //{
        //    if (strValue == null)
        //        return 0;
        //    long ii = 0;
        //    if (Int64.TryParse(strValue, out ii) == true)
        //        return ii;
        //    return 0;
        //}

        /// <summary>
        /// Tests if string is valid DateTime
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool bIsDate(string strDate)
        {
            try
            {
                DateTime dt = DateTime.Parse(strDate);
            }
            catch
            { return false; }
            return true;
        }

        /// <summary>
        /// Returns two integers froma comma separated string. If invalid, returns 0 and 0. Does not check for negative numbers.
        /// </summary>
        /// <param name="strTimes"></param>
        /// <returns></returns>
        public static Tuple<int, int, bool> TwoTimeValuesFromString(string strTimes)
        {
            bool bStillGood1 = true;
            bool bStillGood2 = true;

            int iOne = 0;
            int iTwo = 0;

            if (String.IsNullOrWhiteSpace(strTimes) == true)
                bStillGood1 = false;

            if (bStillGood1 == true)
            {
                string[] strarr = strTimes.Split(',');
                if (strarr.Length > 1)
                {
                    // Be sure to try to parese both values
                    bStillGood1 = Int32.TryParse(strarr[0], out iOne);
                    bStillGood2 = Int32.TryParse(strarr[1], out iTwo);
                }
            }

            return new Tuple<int, int, bool>(iOne, iTwo, bStillGood1 & bStillGood2);
        }

        /// <summary>
        /// Returns false if DateTime.Now.ToString("HHmmss") as Int between int iStart, int iEnd. Negative values return false.
        /// </summary>
        /// <param name="iStart"></param>
        /// <param name="iEnd"></param>
        /// <returns></returns>
        public static bool bIs_NowTime_BetweenTheseTwoNumbers(int iStart, int iEnd)
        {
            if ((iStart == iEnd) || // off 
                (iStart < 0) ||     // error
                (iEnd < 0)          // error
               )
                return false; // keep processing

            string strTime = DateTime.Now.ToString("HHmmss");
            int iTime = 0;
            bool bStillGood1 = Int32.TryParse(strTime, out iTime);

            if (bStillGood1 == false) // iTime == 0)
                return false; // Keep processing, almost certainly an error.

            if ((iTime > iStart) &&
                (iTime < iEnd)
                )
                return true; // Only true returned

            return false; // keep processing
        }

        /// <summary>
        /// Is this in Time Period HH:mm, minutes [0=Off, must be under 60]
        /// </summary>
        /// <param name="strTime_Minutes"></param>
        /// <returns></returns>
        public static bool bIs_NowTime_ToBePaused(string strTime_Minutes)
        {
            bool bIs_NowTime_ToBePaused = true;
            string[] strarr = strTime_Minutes.Split(',');
            if (strarr.Length < 2)
                return false; // error whatever
            int iMinutesOffset = 0;
            bool bStillGood1 = Int32.TryParse(strarr[1], out iMinutesOffset);
            if ((bStillGood1 == false) ||
                (iMinutesOffset < 1) ||   // Turned off
                (iMinutesOffset > 60)
              )
                return false; // invalid values or turned off
            string[] strarrt = strarr[0].Trim().Split(':');
            if (strarrt.Length != 2)
                return false; // error whatever

            int iHours = 0;
            int iMinutes = 0;
            bStillGood1 = Int32.TryParse(strarrt[0], out iHours);
            if ((bStillGood1 == false) ||
                (iHours < 1) ||   // Turned off
                (iHours > 23)
               )
                return false; // invalid values
            bStillGood1 = Int32.TryParse(strarrt[1], out iMinutes);
            if ((bStillGood1 == false) ||
                (iMinutes < 0) ||   
                (iMinutes > 59)
               )
                return false; // invalid values

            TimeSpan tsOffset = new TimeSpan(0, iMinutesOffset, 0);

            DateTime dtNow = DateTime.Now;
            DateTime dtSetting = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, iHours, iMinutes, 0);
            DateTime dtWithOffset = dtSetting + tsOffset;
            if ((dtNow > dtSetting) &&
                (dtNow < dtWithOffset)
              )
                return true;

            return false;
        }

#if THIS_IS_ALL_DUPLICATED
        public static void CopyFileBinary(string srcfilename, string destfilename)
        {
            System.IO.Stream s1 = System.IO.File.Open(srcfilename, System.IO.FileMode.Open);
            System.IO.Stream s2 = System.IO.File.Open(destfilename, System.IO.FileMode.Create);
            System.IO.BinaryReader f1 = new System.IO.BinaryReader(s1);
            System.IO.BinaryWriter f2 = new System.IO.BinaryWriter(s2);

            while (true)
            {
                byte[] buf = new byte[1024];
                int sz = f1.Read(buf, 0, 1024);
                if (sz <= 0)
                    break;

                f2.Write(buf, 0, sz);
                if (sz < 10240)
                    break; // eof reached
            }

            f1.Close();
            f2.Close();
        }

        public static int getNumberOfPdfPages(string fileName)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(sr.ReadToEnd());
                return matches.Count;
            }
        }

        public static int iGetPagesCountFromPostscriptFile(string strFileName, out string strFound, out string strError)
        {
            strError = String.Empty;
            strFound = String.Empty;
            int iMaxLinesToRead = 15;
            int iLineCounter = 0;
            int iPagesCount = -1;
            const Int32 BufferSize = 512;
            using (var fileStream = File.OpenRead(strFileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                while ((strFound = streamReader.ReadLine()) != null)
                {
                    if (strFound.Contains("%%Pages: ") == true)
                    {
                        if (strFound.Contains("%%Pages: (atend)") == true)
                            iMaxLinesToRead = Int32.MaxValue;

                        else if (Int32.TryParse(strFound.Replace("%%Pages: ", ""), out iPagesCount) == false)
                        {
                            strError = "Error, could not parse value of (" + strFound.Replace("%%Pages: ", "") + ")...";
                            return -3; // 
                        }
                        else
                            return iPagesCount;
                    }
                    iLineCounter++;
                    if (iLineCounter > iMaxLinesToRead)
                    {
                        strError = "Error, could not find '%%Pages: ' after (" + iLineCounter.ToString() + ") lines...";
                        return -5; // 
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// For Statistics. Adds a pair of integers as a JSON string to the StringBuilder
        /// It will allow the calculation of timespans between processing events. 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="eLoc"></param>
        //public static void makeStatisticsPairString(StringBuilder sb, e_Locs eLoc)
        //{
        //    string strComma = "";
        //    if (sb.ToString() != String.Empty)
        //        strComma = ",";
        //    string str = DateTime.Now.Ticks.ToString();
        //    str = str.Substring(0, str.Length - 4);
        //    //sb.Append(strComma + "{\"" + eLoc.ToString() + "\":\"" + DateTime.Now.Ticks.ToString() + "\"}");
        //    //int ii = (int)eLoc; // Use this in production
        //    //sb.Append(strComma + "\"" + ii.ToString() + "\":\"" + DateTime.Now.Ticks.ToString() + "\"");
        //    //sb.Append(strComma + "{\"" + eLoc.ToString() + "\":\"" + DateTime.Now.Ticks.ToString() + "\"}");
        //    //sb.Append(strComma + "\"" + eLoc.ToString() + "~" + DateTime.Now.Ticks.ToString() + "\""); // MAB122314
        //    // The last 4 characters of the Time.Tic are invalid
        //    sb.Append(strComma + "\"" + eLoc.ToString() + "~" + str + "\""); // MAB122314
        //}

        /// <summary>
        /// Only used for getting the start and end values from c_Bookmark Aggregated Queries
        /// </summary>
        /// <param name="sb">Accumulates statistics pairs</param>
        /// <param name="eLoc">Location in code of time ticks</param>
        /// <param name="dt">time of timestamp</param>
        //public static void makeStatisticsPairString(StringBuilder sb, e_Locs eLoc, DateTime dt)
        //{
        //    string strComma = "";
        //    if (sb.ToString() != String.Empty)
        //        strComma = ",";
        //    string str = dt.ToString();
        //    str = str.Substring(0, str.Length - 4);
        //    //sb.Append(strComma + "{\"" + eLoc.ToString() + "\":\"" + DateTime.Now.Ticks.ToString() + "\"}");
        //    //int ii = (int)eLoc; // Use this in production
        //    //sb.Append(strComma + "\"" + ii.ToString() + "\":\"" + dt.Ticks.ToString() + "\"");
        //    //sb.Append(strComma + "\"" + eLoc.ToString() + "\":\"" + dt.Ticks.ToString() + "\"");
        //    sb.Append(strComma + "\"" + eLoc.ToString() + "\":\"" + str + "\"");
        //}

        //public static string makeStatisticsString(string strPairs, string strName)
        //{
        //    return "{\"stats\":{\"" + strName + "\":{" + strPairs + "\"}}";
        //}




        /// <summary>
        /// Verifies that there are strings in an address for City, State and Zip. strErrorReason is unused right now.
        /// </summary>
        /// <param name="addrString"></param>
        /// <param name="strErrorReason">Unused</param>
        /// <returns></returns>
        // Chatsworth, CA 91311 - Good
        // Chatsworth CA 91311 - Good
        // Chatsworth Park, CA 91311 - Good
        // Chatsworth Park 91311 - Bad
        public static bool bHasCityStateZip(string addrString, out string strErrorReason)
        {
            strErrorReason = String.Empty;

            string sADDR_CITY = String.Empty;
            string sADDR_STATE = String.Empty;
            string sADDR_ZIP = String.Empty;
            string strErr = String.Empty;

            addrString = addrString.Replace(".", "").Replace(",", ", ").Replace("  ", " ");
            System.Text.RegularExpressions.Regex addressPattern =
                new System.Text.RegularExpressions.Regex(@"(?<city>[A-Za-z',.\s]+) (?<state>([A-Za-z]{2}|[A-Za-z]{2},))\s*(?<zip>\d{5}(-\d{4})|\d{5})");

            System.Text.RegularExpressions.MatchCollection matches4 = addressPattern.Matches(addrString);

            for (int mc = 0; mc < matches4.Count; mc++)
            {
                sADDR_CITY = matches4[mc].Groups["city"].Value;
                sADDR_STATE = matches4[mc].Groups["state"].Value;
                sADDR_ZIP = matches4[mc].Groups["zip"].Value;
            }

            string ss = sADDR_STATE.Trim();
            if (ss == String.Empty)
                return false;

            string s = sADDR_CITY.Trim();
            if ((s.Length < 1) ||
               (s[0] == ',') ||
               (s[0] == '.') ||
               (s[0] == ';')
              )
                return false;

            return true;
            /*

            string strComma = String.Empty;
            if (sADDR_CITY.Trim() == String.Empty)
            {
                strErr = "City";
                strComma = ", ";
            }
            if (sADDR_STATE.Trim() == String.Empty)
            {
                strErr += strComma + "State";
                strComma = ", ";
            }
            if (sADDR_ZIP.Trim() == String.Empty)
            {
                strErr += strComma + "Zip";
            }

            if (strErr == String.Empty)
            {
                return false;
            }
            strErrorReason = strErr;
            return true;
            */
        } // End public static bool bHasCityStateZip

        public static bool bIsIPAddressValid(string addrString)
        {
            System.Net.IPAddress address;
            return System.Net.IPAddress.TryParse(addrString, out address);
        }

        //private bool ConvertDocToDocx(string oldName, string newName)
        //{
        //    WordAutomation c = new WordAutomation(true);
        //    if (!c.ConvertToDocx(oldName, newName))
        //    {
        //        //MessageBox.Show(this, "ConvertToDocx failed");
        //        // Handle error there
        //        return false;
        //    }
        //    return true;
        //}

        /// <summary>
        /// A blank or NULL string of an integer may not mean an error
        /// </summary>
        /// <param name="strValue">string to convert to integer if possible</param>
        /// <returns>integer value returned</returns>
        public static int iReturn_0_ForNonIntegerString(string strValue)
        {
            if (strValue == null)
                return 0;
            int ii = 0;
            if (Int32.TryParse(strValue, out ii) == true)
                return ii;
            return 0;
        }

        //public static long iReturn_0_ForNonLongString(string strValue)
        //{
        //    if (strValue == null)
        //        return 0;
        //    long ii = 0;
        //    if (Int64.TryParse(strValue, out ii) == true)
        //        return ii;
        //    return 0;
        //}

        //public static bool bIsDate(string strDate)
        //{
        //    try
        //    {
        //        DateTime dt = DateTime.Parse(strDate);
        //    }
        //    catch
        //    { return false; }
        //    return true;
        //}

        /// <summary>
        /// Is this in Time Period HH:mm, minutes [0=Off, must be under 60]
        /// </summary>
        /// <param name="strTime_Minutes"></param>
        /// <returns></returns>
        public static bool bIs_NowTime_TimeToBePaused(string strTime_Minutes)
        {
            bool bIs_NowTime_ToBePaused = true;
            string[] strarr = strTime_Minutes.Split(',');
            if (strarr.Length < 2)
                return false; // error whatever
            int iMinutesOffset = 0;
            bool bStillGood1 = Int32.TryParse(strarr[1], out iMinutesOffset);
            if ((bStillGood1 == false) ||
                (iMinutesOffset < 1) ||   // Turned off
                (iMinutesOffset > 60)
              )
                return false; // invalid values or turned off
            string[] strarrt = strarr[0].Trim().Split(':');
            if (strarrt.Length != 2)
                return false; // error whatever

            int iHours = 0;
            int iMinutes = 0;
            bStillGood1 = Int32.TryParse(strarrt[0], out iHours);
            if ((bStillGood1 == false) ||
                (iHours < 1) ||   // Turned off
                (iHours > 23)
               )
                return false; // invalid values
            bStillGood1 = Int32.TryParse(strarrt[1], out iMinutes);
            if ((bStillGood1 == false) ||
                (iMinutes < 0) ||
                (iMinutes > 59)
               )
                return false; // invalid values

            TimeSpan tsOffset = new TimeSpan(0, iMinutesOffset, 0);

            DateTime dtNow = DateTime.Now;
            DateTime dtSetting = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, iHours, iMinutes, 0);
            DateTime dtWithOffset = dtSetting + tsOffset;
            if ((dtNow > dtSetting) &&
                (dtNow < dtWithOffset)
              )
                return true;

            return false;
        }
#endif

#region region_Fix_WhiteSpace_In_Tables
        /// <summary>
        /// Test1. OpenXML removes leading spaces from Table Cells, but maybe not Tabs. 
        /// Integrated Products (IP) Denial
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="iSpacesEquelATab">Replace this many spaces with a Tab character</param>
        /// <param name="strError"></param>
        //public static void convertSpacesToTabsInTableColumns(System.Data.DataSet ds, int iSpacesEquelATab, out string strError)
        public static void convertSpacesToTabsInTableColumns(System.Data.DataTable dt, int iSpacesEquelATab, out string strError)
        {
            strError = String.Empty;

            if (dt.Columns.Count < 1)
            {
                strError = "Error(MWSIT2):There are " + dt.Columns.Count.ToString() + "Columns in DataSet.Tables[0].";
                return;
            }

            // DataColumn dc = dt.Columns[0];

            //if (dt.Columns[0].GetType() != typeof(String))
            //{
            //    strError = "Error(MWSIT3):Column[0] is not typeof(String).";
            //    return;
            //}
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                string strColumn1 = dr.ItemArray[0].ToString();
                if (String.IsNullOrWhiteSpace(strColumn1) == true)
                    continue;
                int iColumn1Length = strColumn1.Length;
                int iCharCounter = 0;
                int iTabCounter = 0;
                int iLeadingSpaceCounter = 0;
                // Are there leading spaces?
                while ((strColumn1[iCharCounter] == '*') && (iCharCounter < iColumn1Length - 1))
                {
                    iLeadingSpaceCounter++;
                    iCharCounter++;
                }
                if (iLeadingSpaceCounter < iSpacesEquelATab)
                    continue; // Nothing to do, but iLeadingSpaceCounter % iSpacesEquelATab should == 0
                iTabCounter = iLeadingSpaceCounter / iSpacesEquelATab; // Should be no Mod value.

                StringBuilder sbColumn1WithLeadingSapcesRemoved = new StringBuilder();
                for (int ii = 0; ii < iTabCounter; ii++)
                    sbColumn1WithLeadingSapcesRemoved.Append('\t');

                sbColumn1WithLeadingSapcesRemoved.Append(strColumn1.Substring(iLeadingSpaceCounter - 1));
                dr.SetField(0, sbColumn1WithLeadingSapcesRemoved.ToString());
            }
        }

        /// <summary>
        /// For CorrPub, move leading WhiteSpace from one table column to another. 
        /// ... No good, bookmark data is in columns in two different single column tables.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <param name="iColumn1"></param>
        /// <param name="iColumn2"></param>
        public static void moveWhiteSpaceInTableBetweenColumns(DataTable dt1, DataTable dt2, int iSpacesEquelATab, out string strError)
        {
            string strToReplace = "     ";
            strError = String.Empty;

            if ((dt1.Rows.Count < 1) || (dt2.Rows.Count < 1))
            {
                //strError = ("Error(MWSIT1):No Rows in DataSet");
                return;
            }

            //foreach (DataRow dr in dt2.Rows)
            //{
            //    //string strColumn1 = dr.ItemArray[iColumn1].ToString();
            //    string strColumn1 = dr[0].ToString();
            //    strColumn1 = strColumn1.Replace("*****", "     ");
            //    dr.SetField(0, strColumn1);
            //}
            int iRowCounter = 0;
            /* foreach (DataRow dr in dt1.Rows)
            {
                //string strColumn1 = dr.ItemArray[iColumn1].ToString();
                // Append the first column of the second table to the first table
                string strColumn1 = dr[0].ToString();
                if (strColumn1.Trim() == String.Empty)
                    dr.SetField(0, "." + dt2.Rows[iRowCounter][0].ToString()); // Add a '.' where the 'X' isn't so that the leading whitespace is not Trimmed
                else
                    dr.SetField(0, strColumn1 + dt2.Rows[iRowCounter][0].ToString());

                dt2.Rows[iRowCounter].SetField(0, ""); // This column was moved to first table, so blank this.
                iRowCounter++;
            } */
            foreach (DataRow dr in dt2.Rows)
            {
                //string strColumn1 = dr.ItemArray[iColumn1].ToString();
                // Append the first column of the second table to the first table
                string strColumn1 = dt1.Rows[iRowCounter][0].ToString();
                if (strColumn1.Trim() == String.Empty)
                    dt2.Rows[iRowCounter].SetField(0, ".   " + dt2.Rows[iRowCounter][0].ToString()); // Add a '.' where the 'X' isn't so that the leading whitespace is not Trimmed
                else
                    dt2.Rows[iRowCounter].SetField(0, strColumn1 + "   " + dt2.Rows[iRowCounter][0].ToString());

                dt1.Rows[iRowCounter].SetField(0, ""); // This column was moved to first table, so blank this.
                iRowCounter++;
            }
        }

#endregion // end region_Fix_WhiteSpace_In_Tables

#region region_Bar_String_Deserialization
        /// <summary>
        /// <param name="strInBarSeperator">'|' separated string of ':' separated string pairs</param>
        /// <param name="strValueToFind">First member of string pair</param>
        /// <param name="strValueFound">Second member of string pair</param>
        /// <param name="iValueFound">Integer value of Second member of string pair</param>
        /// <returns>integer value returned. -1 is error. Cannot work .</returns>
        /// </summary>
        public static int FromBarString_GetValue(string strInBarSeperator, string strValueToFind,
                                                 out string strValueFound, out int iValueFound, bool bIsInt = false)
        {
            strValueFound = String.Empty;
            iValueFound = 0;

            if ((strValueToFind == null) || (strInBarSeperator == null))
            {
                iValueFound = -1;
                return -1;
            }

            string[] strarrBar = strInBarSeperator.Split('|');
            if (strarrBar.Length < 1)
            {
                iValueFound = -1;
                return -1;
            }

            int iPositionInString = 0;
            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                if (strarrBar[iPositionInString].Contains(strValueToFind) == true)
                    break;
            }

            if (iPositionInString == strarrBar.Length)
            {
                iValueFound = -1;
                return -1;
            }

            string[] strarrColon = strarrBar[iPositionInString].Split(':');
            if (strarrColon.Length < 2)
            {
                iValueFound = -1;
                return -1;
            }

            strValueFound = strarrColon[1];

            if (bIsInt == true)
            {
                int ii = 0;
                if (Int32.TryParse(strValueFound, out ii) == true)
                    iValueFound = ii;
                else
                    iValueFound = -1;
            }
            return 0; // not an error
        }
        /// <summary>
        /// Returns second value of bar  "|" separated pair ie. Key1:Value1|Key2:Value2
        /// </summary>
        /// <param name="strInBarSeperator"></param>
        /// <param name="strValueToFind"></param>
        /// <param name="strValueFound">Will have error value, starting with "Error(" if is there is an Error</param>
        public static void FromBarString_GetValue(string strInBarSeperator, string strValueToFind, out string strValueFound)
        {
            strValueFound = String.Empty;

            if ((strValueToFind == null) || (strInBarSeperator == null))
            {
                strValueFound = "Error(220)-Null Values";
                return;
            }

            string[] strarrBar = strInBarSeperator.Split('|');
            if (strarrBar.Length < 1)
            {
                strValueFound = "Error(227)-Empty Values";
                return;
            }

            int iPositionInString = 0;
            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                if (strarrBar[iPositionInString].Contains(strValueToFind) == true)
                    break;
            }

            if (iPositionInString == strarrBar.Length)
            {
                strValueFound = "Error(241)-Value Not Found";
                return;
            }

            string[] strarrColon = strarrBar[iPositionInString].Split(':');
            if (strarrColon.Length < 2)
            {
                strValueFound = "Error(248)-Value Not Found(:)";
                return;
            }

            strValueFound = strarrColon[1];

            return;
        }

        public static int FromBarString_GetStrings(string strInBarSeperator, string strValueToFind,
                                                   out string strError, out int iValueFound, bool bIsInt = false)
        {
            strError = String.Empty;
            iValueFound = 0;

            if ((strValueToFind == null) || (strInBarSeperator == null))
            {
                iValueFound = -1;
                return -1;
            }

            string[] strarrBar = strInBarSeperator.Split('|');
            if (strarrBar.Length < 1)
            {
                iValueFound = -1;
                return -1;
            }

            int iPositionInString = 0;
            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                if (strarrBar[iPositionInString].Contains(":") == false)
                {
                    strError = strarrBar[iPositionInString];
                    iValueFound = -1;
                    return -1;
                }
            }

            if (iPositionInString == strarrBar.Length)
            {
                iValueFound = -1;
                return -1;
            }

            string[] strarrColon = strarrBar[iPositionInString].Split(':');
            if (strarrColon.Length < 2)
            {
                iValueFound = -1;
                return -1;
            }

            strError = strarrColon[1];

            if (bIsInt == true)
            {
                int ii = 0;
                if (Int32.TryParse(strError, out ii) == true)
                    iValueFound = ii;
                else
                    iValueFound = -1;
            }
            return iValueFound;
        }

        /// <summary>
        /// Get a List of the second values of ':' separated pairs in a '|' separated string
        /// </summary>
        /// <param name="strInBarSeperator"></param>
        /// <param name="lststr"></param>
        /// <param name="strError"></param>
        /// <param name="iValueFound"></param>
        /// <returns></returns>
        public static void FromBarString_GetStrings(string strInBarSeperator, List<string> lststr, out string strError)
        {
            strError = String.Empty;

            if (String.IsNullOrWhiteSpace(strInBarSeperator) == true)
            {
                strError = "Invalid-Empty String";
                return;
            }

            string[] strarrBar = strInBarSeperator.Split('|');
            if (strarrBar.Length < 1)
            {
                strError = "Invalid-No '|'s to split";
                return;
            }

            int iPositionInString = 0;
            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                if (strarrBar[iPositionInString].Contains(":") == false)
                {
                    strError = "Invalid1-" + strarrBar[iPositionInString];
                    return;
                }
            }

            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                string[] strarrColon = strarrBar[iPositionInString].Split(':');
                if (strarrColon.Length < 2) // unlikely
                {
                    strError = "Invalid2-" + strarrBar[iPositionInString];
                    return;
                }
                lststr.Add(strarrColon[1]);
            }
        }

        /// <summary>
        /// Find the bool value of a value memeber of a pair where key is strValueToFind
        /// </summary>
        /// <param name="strWithBarSeperator"></param>
        /// <param name="strValueToFind"></param>
        /// <param name="strError"></param>
        /// <param name="bValueFound"></param>
        /// <param name="strStatus">Filled in if strValueToFind is not found - may or may not be an error</param>
        public static void BarString_findBoolValueForString(string strWithBarSeperator, string strValueToFind, out string strError, out bool bValueFound, out string strStatus)
        {
            strStatus = String.Empty;
            strError = String.Empty;
            bValueFound = false;

            if ((strValueToFind == null) || (strWithBarSeperator == null))
            {
                strError = "Invalid1-Empty String";
                return;
            }

            string[] strarrBar = strWithBarSeperator.Split('|');
            if (strarrBar.Length < 1)
            {
                strError = "Invalid2-No '|'s to split";
                return;
            }

            int iPositionInString = 0;

            //for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            //{
            //    if (strarrBar[iPositionInString].Contains(":") == false)
            //    {
            //        strError = "Invalid1-" + strarrBar[iPositionInString];
            //        return;
            //    }
            //}

            for (iPositionInString = 0; iPositionInString < strarrBar.Length; iPositionInString++)
            {
                string[] strarrColon = strarrBar[iPositionInString].Split(':');
                if (strarrColon.Length < 2)
                {
                    strError = "Invalid3-" + strarrBar[iPositionInString];
                    continue;
                }

                if (strarrColon[0] == strValueToFind)
                {
                    string strValueFound = strarrColon[1];

                    if (strValueFound.ToLower() == "true")
                        bValueFound = true;
                    else if (strValueFound.ToLower() == "false")
                        bValueFound = false;
                    else
                        strError = "Invalid4-" + strValueToFind + ":" + strValueFound + "- is not a boolean value";
                    return;
                }
            }
            strStatus = "Invalid5-" + strValueToFind + "- was not found";
        }

#endregion

#region region_Processor_Characteristics

#if PROBABLY_SHOULD_TEST_THIS
From:http://stackoverflow.com/questions/5235613/tpl-difference-between-maxdegreeofparallelism-and-maximumconcurrencylevel?rq=1

internal int EffectiveMaxConcurrencyLevel
{
    get
    {
        int maxDegreeOfParallelism = this.MaxDegreeOfParallelism;
        int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
        if ((maximumConcurrencyLevel > 0) && (maximumConcurrencyLevel != 0x7fffffff))
        {
            maxDegreeOfParallelism = (maxDegreeOfParallelism == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, maxDegreeOfParallelism);
        }
        return maxDegreeOfParallelism;
    }
} 
#endif
        public static double dTaskTimeSpan_Milliseconds(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan elapsed = dtEnd - dtStart;
            return elapsed.TotalMilliseconds;
        }

        //public static int iNumberOfPhysicalProcessors()
        //{
        //    int ii = 0;
        //    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
        //    {
        //        //Console.WriteLine("Number Of Physical Processors: {0} ", item["NumberOfProcessors"]);
        //        var jj = item["NumberOfProcessors"];
        //        //                ii = (int)jj;
        //        ii = Convert.ToInt32(jj);
        //    }
        //    return ii;
        //}

        //public static int iNumberOfCores()
        //{
        //    int coreCount = 0;
        //    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
        //    {
        //        coreCount += int.Parse(item["NumberOfCores"].ToString());
        //    }
        //    //Console.WriteLine("Number Of Cores: {0}", coreCount);
        //    return coreCount;
        //}

        //public static int iLogicalProcessors()
        //{
        //    //var coreCount;// = 0;
        //    int ii = 0;
        //    foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
        //    {
        //        //Console.WriteLine("Number Of Logical Processors: {0}", item["NumberOfLogicalProcessors"]);
        //        var coreCount = item["NumberOfLogicalProcessors"];
        //        //                ii = (int)coreCount;
        //        ii = Convert.ToInt32(coreCount);
        //    }
        //    //Console.WriteLine("Number Of Cores: {0}", coreCount);
        //    //return coreCount;
        //    return ii;
        //}

        public static int iLogicalProcessors2()
        {
            return Environment.ProcessorCount;
        }

        /*private static class NativeMethods
        //{
        //    [StructLayout(LayoutKind.Sequential)]
        //    internal struct SYSTEM_INFO
        //    {
        //        public ushort wProcessorArchitecture;
        //        public ushort wReserved;
        //        public uint dwPageSize;
        //        public IntPtr lpMinimumApplicationAddress;
        //        public IntPtr lpMaximumApplicationAddress;
        //        public UIntPtr dwActiveProcessorMask;
        //        public uint dwNumberOfProcessors;
        //        public uint dwProcessorType;
        //        public uint dwAllocationGranularity;
        //        public ushort wProcessorLevel;
        //        public ushort wProcessorRevision;
        //    }

        //    [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //    internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);
        //}

        //public static int ProcessorCount
        //{
        //    get
        //    {
        //        NativeMethods.SYSTEM_INFO lpSystemInfo = new NativeMethods.SYSTEM_INFO();
        //        NativeMethods.GetNativeSystemInfo(ref lpSystemInfo);
        //        return (int)lpSystemInfo.dwNumberOfProcessors;
        //    }
        //}
        */
        //}
        /// <summary>
        /// Provides a single property which gets the number of processor threads
        /// available to the currently executing process.
        /// </summary>
        // internal static class ProcessInfo
        // {
        /// <summary>
        /// Gets the number of processors.
        /// </summary>
        /// <value>The number of processors.</value>
        internal static uint NumberOfProcessorThreads
        {
            get
            {
                uint processAffinityMask;

                using (var currentProcess = System.Diagnostics.Process.GetCurrentProcess())
                {
                    processAffinityMask = (uint)currentProcess.ProcessorAffinity;
                }

                const uint BitsPerByte = 8;
                var loop = BitsPerByte * sizeof(uint);
                uint result = 0;

                while (--loop > 0)
                {
                    result += processAffinityMask & 1;
                    processAffinityMask >>= 1;
                }

                return (result == 0) ? 1 : result;
            }
        }
#endregion
    }

} // End namespace WinSandMDI_2019a.Classes
