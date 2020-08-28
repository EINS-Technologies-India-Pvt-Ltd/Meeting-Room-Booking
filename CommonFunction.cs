using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using RBBE;

namespace RBDL
{
    public class CommonFunction
    {
        CommonDL _objCommDl = new CommonDL();
        EINS_RBMSEntities _objRBMSEntity = new EINS_RBMSEntities();
        #region For All Pages

        public const string EINSEncryptionKey = "eins@2014";

        public string Encrypt(string EncryptCode, string Key)
        {
            string EncryptionKey = Key;
            byte[] clearBytes = Encoding.Unicode.GetBytes(EncryptCode);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    EncryptCode = Convert.ToBase64String(ms.ToArray());
                }
            }
            return EncryptCode;
        }

        public string Decrypt(string DecryptCode, string Key)
        {
            string EncryptionKey = Key;
            DecryptCode = DecryptCode.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(DecryptCode);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    DecryptCode = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return DecryptCode;
        }

        public void Eventlog(string ServiceName, string Logtext, string LoginDetails)
        {
            try
            {
                _objCommDl.ExecuteInsertUpdateQuery("INSERT INTO [EINS2013_Masters].[dbo].[WebLog] ([CoreLog],[DateTime],[LoginDetails]) VALUES ('MBPT_Web - " + ServiceName + "  " + "Error_Message :- " + Logtext + "','" + DateTime.Now + "','" + LoginDetails + "')");
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteDocumentlog(long VehID, string Doctype, long EmployeeID, string RegmanualID)
        {
            try
            {
                _objCommDl.ExecuteInsertUpdateQuery("INSERT INTO [EINS_ParkingSystem].[dbo].[Vehicle_Document_Deleted_Details_Logs] ([VehID],[Doctype],[EmployeeID],[RegmanualID]) VALUES(" + VehID + ",'" + Doctype + "', " + EmployeeID + ",'" + RegmanualID + "')");
            }
            catch (Exception ex)
            {

            }
        }



        public byte[] ImagetoByte(string imagefilePath)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagefilePath);
            byte[] imageByte = ImageToByteArraybyMemoryStream(image);
            return imageByte;
        }
        private static byte[] ImageToByteArraybyMemoryStream(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        public Image byteToImage(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream(imageByte);
            Image image = Image.FromStream(ms);
            return image;
        }
        //public void InsertLog(CommonBE.LogBE _objBE)
        //{
        //    EINS2013_MastersEntities1 _ObjMasterEnty = new EINS2013_MastersEntities1();
        //    CoreLog _ObjLog = new CoreLog();
        //    _ObjLog.CoreLog1 = _objBE.LogText;
        //    _ObjLog.DateTime = _objBE.EventDateTime;
        //    _ObjMasterEnty.AddToCoreLogs(_ObjLog);
        //    _ObjMasterEnty.SaveChanges();
        //}
        #endregion

        #region All Pages Names
        public const string LoginPage = "LoginPage";
        public const string CompanyMaster = "Company Master";
        public const string UserMaster = "User Master";
        public const string ResourceMaster = "Resource Master";
        public const string RoomMaster = "Room Master";
        public const string RoomBookingApplication = "Room Booking Application";
        public const string RoomBookingSearch = "RoomBooking Search";
        public const string BookingApproverReject = "Booking Approver Reject";
        public const string BookingStatusView = "Booking Status View";
        public const string RequestDetails = "Request Details";
        public const string RoomAvailability = "Room Availability";
        public const string EmailConfiguration = "Email Configuration";
        public const string EmailEventManager = "Email Event Manager";
        public const string EmailEventSearch = "Email Event Search";
        public const string EmailSentPage = "Email Sent Page";
        public const string SMSConfiguration = "SMS Configuration";
        public const string SMSEventManager = "SMS Event Manager";
        public const string SMSEventSearch = "SMS Event Search";
        public const string SMSSentPage = "SMS Sent Page";
        public const string DatabaseManagement = "Database Management";
        public const string BasicSetting = "Basic Setting";
        public const string Feedback = "Feedback";
        public const string ChangePassword = "Change Password";
        public const string HelpPage = "Help Page";
        public const string Support = "Support Page";
        #endregion

        //get Image not available
        public byte[] get_ImagenotAvailable()
        {
            byte[] image = null;
            DataTable dt = _objCommDl.executeSelectQuery("select ImageNotFound from [EINS_RBMS].[dbo].[IMAGES]");
            if (dt != null && dt.Rows.Count > 0)
            {
                image = (byte[])dt.Rows[0][0];
            }
            return image;
        }
        public string getAutoGeneratedPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public string getAutogenerateOTP(int otpLength)
        {
            string allowno = "0123456789";
            Random objRandom = new Random();
            char[] chars = new char[otpLength];
             int allowedCharCount = allowno.Length;
            for (int i = 0; i < otpLength; i++)
            {
                chars[i] = allowno[(int)((allowno.Length) * objRandom.NextDouble())];
            }
            return new string(chars);
        }




        //To Create Permit no
        public string CreatePermitNo(string regManualID, string CatCode, string PassDur, int Numb)
        {
            string PermitNo = regManualID.Remove(5) + CatCode.Remove(3) + PassDur.Remove(1) + Numb.ToString().Remove(5);
            return PermitNo;
        }
        //To get the image
        public byte[] fetch_Image(String Query)
        {
            byte[] image = null;
            DataTable dt = _objCommDl.executeSelectQuery(Query);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0] != DBNull.Value && dt.Rows[0][0].ToString() != "")
                {
                    image = (byte[])dt.Rows[0][0];
                }
            }
            return image;
        }

        //For Event Log 
        public void InsertEventLog(long EmployeeID, string strMachineName, DateTime EventDateTime, string FormName, string Actions, string Result, string Details, string BrowserName)
        {
            try
            {
                EventLog objEvent = new EventLog();
                objEvent.UserID = EmployeeID;
                objEvent.MachineName = strMachineName;
                objEvent.EventDateTime = EventDateTime;
                objEvent.FormName = FormName;
                objEvent.Action = Actions;
                objEvent.Result = Result;
                objEvent.Details = Details;
                objEvent.BrowserName = BrowserName;

                _objRBMSEntity.AddToEventLogs(objEvent);
                _objRBMSEntity.SaveChanges();
            }
            catch (Exception ex)
            { }
        }

        public void RBMSErrorLogs(long EmployeeID, string FormName, string MethodEvent, string Error)
        {
            _objCommDl.ExecuteInsertUpdateQuery("INSERT INTO [EINS_RBMS].[dbo].[ErrorLog] ([EmployeeID],[EventDateTime],[FormName],[Method/Event],[Error]) VALUES ('" + EmployeeID + "','" + DateTime.Now + "','" + FormName + "','" + MethodEvent + "','" + Error + "')");
        }
        public List<CommonBE.CountryMasterBE> getCountryData()
        {
            var Result = from country in _objRBMSEntity.CountryMasters
                         select new CommonBE.CountryMasterBE
                             {
                                 CountryId = country.CountryId,
                                 Country = country.Country
                             };
            return Result.ToList();
        }
        public List<CommonBE.StatesMasterBE> getStateData(long CountryID)
        {
            var result = from state in _objRBMSEntity.StateMasters
                         where state.CountryId == CountryID
                         select new CommonBE.StatesMasterBE
                         {
                             ID=state.ID,
                             States=state.States,
                             CountryId=state.CountryId.Value
                         };
            return result.ToList();
        }
       
    }
}
