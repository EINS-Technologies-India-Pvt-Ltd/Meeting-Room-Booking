using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;
using System.Data.SqlClient;
using System.Data;


namespace RBDL
{
    public class LoginDL
    {
        EINS_RBMSEntities _ObjEINS_RBMSEntities = new EINS_RBMSEntities();
        CommonDL _ObjCommonDL = new CommonDL();
        public bool IsAuthorisedLogin(string UserID, string Password)
        {
            int Count = _ObjCommonDL.executeScalarQuery("SELECT COUNT(*) FROM [EINS_RBMS].[dbo].[UserMaster] WHERE [LoginName] = '" + UserID + "' AND [Password] = '" + Password + "' collate SQL_Latin1_General_CP1_CS_AS");

            if (Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsActiveUser(string UserID, string Password)
        {
            try
            {
                bool res = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.LoginName == UserID && x.Password == Password && x.Status == true && x.ActivationStatus == "Active").Any();
                return res;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void InsertUpdateWrongAttempts(string UserID)
        {
            if (_ObjEINS_RBMSEntities.UserMasters.Any(x => x.LoginName == UserID && (x.WrongAttempt == null || x.WrongAttempt == 0)))
            {
                var lst = _ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.LoginName == UserID);

                lst.WrongAttempt = 1;
                _ObjEINS_RBMSEntities.SaveChanges();
            }
            else if (_ObjEINS_RBMSEntities.UserMasters.Any(x => x.LoginName == UserID && (x.WrongAttempt != null || x.WrongAttempt != 0)))
            {
                int Attempts = _ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.LoginName == UserID).WrongAttempt.Value;

                var lst = _ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.LoginName == UserID);
                lst.WrongAttempt = Attempts + 1;
                _ObjEINS_RBMSEntities.SaveChanges();

                if (_ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.LoginName == UserID).WrongAttempt.Value == 3)
                {
                    var lstLock = _ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.LoginName == UserID);
                    lst.Locked = true;
                    _ObjEINS_RBMSEntities.SaveChanges();
                }
            }
        }

        public bool IsUserLoginLocked(string UserID)
        {
            return _ObjEINS_RBMSEntities.UserMasters.Any(x => x.LoginName == UserID && x.Locked == true);
        }

        public List<LoginBE.LoginDetails> GetLoginDetails(string UserID,string strPassword)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.UserMasters
                      where _Obj.LoginName == UserID && _Obj.Password == strPassword && _Obj.Status==true 
                      select new LoginBE.LoginDetails
                      {
                          Password=_Obj.Password,
                          ID = _Obj.ID,
                          EmployeeManualID = _Obj.EmployeeManualID,
                          Name = _Obj.Name,
                          Designation = _Obj.Designation,
                          Email = _Obj.EmailID,
                          Mobile = _Obj.Mobile,
                          ExtensionNo = _Obj.ExtensionNo,
                          UserType = _Obj.UserType,
                          ActivationStatus = _Obj.ActivationStatus,
                          Company = _Obj.Company.Value,
                          Location = _Obj.Location.Value,
                          Department = _Obj.Department.Value,
                          LoginName = _Obj.LoginName,
                          AddedOn = _Obj.AddedOn,
                          Photo = _Obj.Photo
                      };

            return lst.ToList();
        }
     
        public int getUserCount()
        {
            return _ObjCommonDL.executeScalarQuery("select count(*) from [EINS_RBMS].[dbo].[UserMaster] where [Status]=1");
        }
        public long ChangePassword(long ID, string NewPassword)
        {
            var lst = _ObjEINS_RBMSEntities.UserMasters.FirstOrDefault(x => x.ID == ID);
            lst.Password = NewPassword;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.ID;
        }

        // Code For Forget Password
        #region Forget Password Details
        public int ResetPassword(string emailID , string password)
        {
            string EmailID1 = emailID;
            string Query = "update [EINS_RBMS].[dbo].[UserMaster] set [Password]='" + password + "' where EmailID='" + EmailID1 + "' and status=1";
            int result = _ObjCommonDL.executeScalarQuery(Query);
            return result;
        }
        public int ResetPasswordsms(string mblno, string password)
        {
            string EmailID1 = mblno;
            string Query = "update [EINS_RBMS].[dbo].[UserMaster] set [Password]='" + password + "' where [Mobile]='" + EmailID1 + "' and status=1";
            int result = _ObjCommonDL.executeScalarQuery(Query);
            return result;
        }

        public bool GetForgetPassword(string EmailID, string MobileNumber, int Flag)
        {
            try
            {
                if (Flag == 0)
                {
                    string EmailID1 = EmailID;
                    int Count = _ObjCommonDL.executeScalarQuery("select count(*) from [EINS_RBMS].[dbo].[UserMaster] where EmailID='" + EmailID1 + "' and status=1");

                    if (Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                if (Flag == 1)
                {
                    string MobileNumber1 = MobileNumber;
                    int Count = _ObjCommonDL.executeScalarQuery("select count(*) from [EINS_RBMS].[dbo].[UserMaster] where Mobile='" + MobileNumber1 + "' and status=1");

                    if (Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            { }
            return false;
        }
        public bool UpdateNewPassword(string EmailID, string MobileNumber, int Flag, string strNewPass)
        {
            try
            {
                if (Flag == 0)
                {
                    string EmailID1 = EmailID;
                    int Count = _ObjCommonDL.executeScalarQuery("select count(*) from [EINS_RBMS].[dbo].[UserMaster] where EmailID='" + EmailID1 + "' and status=1");

                    if (Count > 0)
                    {
                        long empid = _ObjCommonDL.executeScalarQuery("select top 1 ID from [EINS_RBMS].[dbo].[UserMaster] where EmailID='" + EmailID1 + "' and status=1");

                        UserMaster objEm = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.ID == empid).FirstOrDefault();
                        objEm.Password = strNewPass;
                        _ObjEINS_RBMSEntities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (Flag == 1)
                {
                    string MobileNumber1 = MobileNumber;
                    int Count = _ObjCommonDL.executeScalarQuery("select count(*) from [EINS_RBMS].[dbo].[UserMaster] where Mobile='" + MobileNumber1 + "' and status=1");

                    if (Count > 0)
                    {
                        long empid = _ObjCommonDL.executeScalarQuery("select top 1 ID from [EINS_RBMS].[dbo].[UserMaster] where Mobile='" + MobileNumber1 + "' and status=1");

                        UserMaster objEm = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.ID == empid).FirstOrDefault();
                        objEm.Password = strNewPass;
                        _ObjEINS_RBMSEntities.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            { }
            return false;
        }

        #endregion
        public IEnumerable<LoginBE.Employee_WeekOffBE> getEmp_WeekOffDetails_ForSearch(int EmpID, string Keyword)
        {
            SqlParameter[] sparam = new SqlParameter[2];

            sparam[0] = new SqlParameter("@EmpID", SqlDbType.Int);
            sparam[0].Value = EmpID;

            sparam[1] = new SqlParameter("@Keyword", SqlDbType.VarChar);
            sparam[1].Value = Keyword;

            IEnumerable<LoginBE.Employee_WeekOffBE> lstEmp_WeekOffDetails = _ObjEINS_RBMSEntities.ExecuteStoreQuery<LoginBE.Employee_WeekOffBE>("EXEC [[EINS_RBMS]].[dbo].[get_Sp_EmployeeWeekOffDetails_ByID] @EmpID,@Keyword", sparam);
            return lstEmp_WeekOffDetails.AsEnumerable();

        }

        public int IsLicenceActive()
        {
            return _ObjCommonDL.executeScalarQuery("Select Case When Convert(date,GetDate()) Between Convert(Date,RegistrationDate) and dATEaDD(dd,15,Convert(date,RegistrationExpDate)) then 1 else 0 end as Licencebit from [EINS_RBMS].[dbo].[RegistrationDetails]");
        }
    }
}
