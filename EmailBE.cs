using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
   public class EmailBE
    {
       public class EmailConfigurationBE
       {
           public int eC_Id { get; set; }
           public string eC_SMTPServerName { get; set; }
           public string eC_UserName { get; set; }
           public string eC_Password { get; set; }
           public int eC_SMTPPort { get; set; }
           public bool eC_IsSSLEnabled { get; set; }
           public int CompanyId { get; set; }
           public int LocationId { get; set; }
           public bool eC_status { get; set; }
           public bool eC_IsDefault { get; set; }
           public string DisplayName { get; set; }
           public bool eC_IsActive { get; set; }
           public string eC_signature { get; set; }
           public string eC_Pop3Server { get; set; }
           public int ec_Pop3Portno { get; set; }
           public long? AddedBy { get; set; }
           public long? ModifyBy { get; set; }
           public DateTime? AddedOn { get; set; }
           public DateTime? LastModifiedOn { get; set; }
       }
       public class EmailEventManagerBE
       {
           public int Event_Id { get; set; }
           public string EventName { get; set; }
           public int eM_ModuleId { get; set; }
           public int eM_MainModuleId { get; set; }
           public int eM_SectionID { get; set; }
           public int eM_FormId { get; set; }
           public int eM_Event_Id { get; set; }
           public int eM_CompanyId { get; set; }
           public int eM_LocationId { get; set; }
           public string EmployeeBody { get; set; }
           public string ReportingToBody { get; set; }
           public string AdminBody { get; set; }
           public string RuleName { get; set; }
           public long AddedBy { get; set; }
           public long LastModifiedBy { get; set; }
       }
       #region Afr EMAIL
       public class EMLEventManagerBE
       {
           public int eM_Id { get; set; }
           public int eM_ModuleId { get; set; }
           public int eM_FormId { get; set; }
           public int eM_Events_Id { get; set; }
           public int eM_CompanyId { get; set; }
           public int eM_LocationId { get; set; }
           public int eM_ConfigId { get; set; }
           public string eM_RuleName { get; set; }
           public string eM_Subject { get; set; }
           public string eM_CommonEmailBody { get; set; }
           public string eM_HostEmailBody { get; set; }
           public string eM_VisitorEmailBody { get; set; }
           public string eM_Priority { get; set; }
           public string eM_Reciepients { get; set; }
           public int AddedBy { get; set; }
           public DateTime AddedOn { get; set; }
           public int LastModifiedBy { get; set; }
           public DateTime LastModifiedOn { get; set; }
           public int eM_Department { get; set; }
           public int eM_TemplateId { get; set; }
       }
       #endregion
    }
}
