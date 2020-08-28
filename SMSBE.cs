using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
   public class SMSBE
    {
       public class SmsConfigurationBE
       {
           public int AutoID { get; set; }
           public string sC_ServerDetail { get; set; }
           public string sC_UserName { get; set; }
           public string sC_Password { get; set; }
           public int sC_ClientID { get; set; }
           public int CompanyId { get; set; }
           public int LocationId { get; set; }
           public bool sC_isActive { get; set; }
           public bool sC_isDefault { get; set; }
           public long AddeddBy { get; set; }
           public long ModifiedBy { get; set; }
           public DateTime AddeddOn { get; set; }
           public DateTime ModifiedOn { get; set; }

       }
       public class SmsEventManagerBE
       {
           public int Event_Id { get; set; }
           public string EventName { get; set; }
           public int eM_ModuleId { get; set; }
           public int eM_FormId { get; set; }
           public int eM_Event_Id { get; set; }
           public int eM_CompanyId { get; set; }
           public int eM_LocationId { get; set; }
           public string CommonSMSBody { get; set; }
           public string HostSMSBody { get; set; }
           public string VisitorSMSBody { get; set; }
           public string EmployeeContact { get; set; }
           public string AdditionalContact { get; set; }
           public string GroupIds { get; set; }
           public bool CheckForHost { get; set; }
           public bool CheckForVisitor { get; set; }
           
           public long AddedBy { get; set; }
           public long LastModifiedBy { get; set; }
           public DateTime AddedOn { get; set; }
           public DateTime LastModifiedOn { get; set; }
       }

       public class AdditionalContactsBE
       {
           public int AdditionalContId { get; set; }
           public string ContactName { get; set; }
           public string ContactNo { get; set; }
           public long AddedBy { get; set; }
           public long LastModifiedBy { get; set; }
           public DateTime AddedOn { get; set; }
           public DateTime LastModifiedOn { get; set; }
       }

       public class Group_DetailsBE
       {
           public int GroupID { get; set; }
           public string GroupName { get; set; }
           public string GroupDescription { get; set; }
           public string Grp_Contacts_Emp { get; set; }
           public string Grp_Contacts_Add { get; set; }
           public int CompanyId { get; set; }
           public int LocationId { get; set; }
           public bool status { get; set; }
           public long AddedBy { get; set; }
           public long LastModifiedBy { get; set; }
           public DateTime AddedOn { get; set; }
           public DateTime LastModifiedOn { get; set; }
       }
    }
}
