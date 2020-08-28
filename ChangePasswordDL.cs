using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;

namespace RBDL
{

  public  class ChangePasswordDL
    {
        EINS_RBMSEntities DB = new EINS_RBMSEntities();


        public List<LoginBE.LoginDetails> getUserData()
        {
            var result = from s in DB.UserMasters
                         where s.Status == true
                         select new LoginBE.LoginDetails
                         {
                             ID = s.ID,
                             EmployeeManualID = s.EmployeeManualID,
                             Name = s.Name,
                             Designation = s.Designation.Value,
                             Email = s.EmailID,
                             Mobile = s.Mobile,
                             ExtensionNo = s.ExtensionNo,
                             UserType = s.UserType,
                             ActivationStatus = s.ActivationStatus,

                             LoginName = s.LoginName,
                             Password = s.Password,
                             Status = s.Status.Value,
                          //   Locked = s.Locked.Value,
                             AddedBy = s.AddedBy.Value,
                             AddedOn = s.AddedOn,
                             LastModifiedBy = s.LastModifiedBy.Value,
                             LastModifiedOn = s.LastModifiedOn


                         };
            return result.ToList();

        }
        public long changePssword(long loginId,string Password)
        {
            var result = DB.UserMasters.FirstOrDefault(s => s.ID == loginId);
            result.Password = Password;
            DB.SaveChanges();
            return result.ID;
        }
    }
}
