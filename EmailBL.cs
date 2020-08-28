using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;
using System.Data;
using System.Data.SqlClient;

namespace RBBL
{
   public class EmailBL
    {
       public class EmailConfigurationBL
       {
           EmailBE.EmailConfigurationBE _objEmailBE = new EmailBE.EmailConfigurationBE();
           EmailDL.EmailConfigurationDL _objEmailDL = new EmailDL.EmailConfigurationDL();

           public long Insert_EmailConfiguration(EmailBE.EmailConfigurationBE _objEmailBE)
           {
               return _objEmailDL.Insert_EmailConfiguration(_objEmailBE);
           }
           public long Update_EmailConfiguration(EmailBE.EmailConfigurationBE _objEmailBE)
           {
               return _objEmailDL.Update_EmailConfiguration(_objEmailBE);
           }
           public long DeleteEmailConfiguration(int emialId)
           {
               return _objEmailDL.DeleteEmailConfiguration(emialId);
           }

           public DataTable getEmailDetails()
           {
               return _objEmailDL.getEmailDetails();

           }

           public DataTable getEditEmailDetail(int emialId)
           {
               return _objEmailDL.getEditEmailDetail(emialId);

           }

           public int chkAssociat_exist(int CompanyId, int LocationId)
           {
               return _objEmailDL.chkAssociat_exist(CompanyId, LocationId);
           }

       }
       public class EmailEventManagerBL
       {
           EmailDL.EmailEventManagerDL _objEmail = new EmailDL.EmailEventManagerDL();
           public DataTable getmoduleDetails()
           {
               return _objEmail.getmoduleDetails();
           }

           public DataTable getModulDetais()
           {
               return _objEmail.getModulDetais();

           }
           public DataTable getFormsDetais(int ModuleID)
           {
               return _objEmail.getFormsDetais(ModuleID);
           }
           public DataTable getEventsDetais(int formId)
           {
               return _objEmail.getEventsDetais(formId);
           }







           public DataTable getSectionDetais(int modulId)
           {
               return _objEmail.getSectionDetais(modulId);

           }

           public DataTable getFormDetais(int SectionID)
           {
               return _objEmail.getFormDetais(SectionID);
           }

           //public long Insert_EmailManagerDetails(EmailBE.EmailEventManagerBE _objEmailBE)
           //{
           //    return _objEmail.Insert_EmailManagerDetails(_objEmailBE);
           //}
           //public long Update_EmailManagerDetails(EmailBE.EmailEventManagerBE _objEmailBE)
           //{
           //    return _objEmail.Update_EmailManagerDetails(_objEmailBE);
           //}



           #region AFR

           public long Insert_EmailEventManagerDetails(EmailBE.EMLEventManagerBE _objEmailBE)
           {
               return _objEmail.Insert_EmailEventManagerDetails(_objEmailBE);
           }
           public long Update_EmailEventManagerDetails(EmailBE.EMLEventManagerBE _objEmailBE)
           {
               return _objEmail.Update_EmailEventManagerDetails(_objEmailBE);
           }

           #endregion



           public DataTable getEventmanagerDetails()
           {
               return _objEmail.getEventmanagerDetails();
           }

           public DataTable geteventmanagementsearchdetailbyID(int Event_id)
           {
               return _objEmail.geteventmanagementsearchdetailbyID(Event_id);
           }

           public int deleteEmailEventDetailbyId(int emialId)
           {
               return _objEmail.deleteEmailEventDetailbyId(emialId);
           }

           public DataTable getEventDetais(int formId)
           {
               return _objEmail.getEventDetais(formId);
           }

           public DataTable getcolumnDetails(int SectionId, int FormId)
           {
               return _objEmail.getcolumnDetails(SectionId, FormId);
           }

           public DataTable getSubModule(int mainmoduleId)
           {
               return _objEmail.getSubModule(mainmoduleId);
           }
           public DataTable ShowCompLocwisedata(int CompanyID, int LocationID)
           {
               return _objEmail.ShowCompLocwisedata(CompanyID, LocationID);
           }
           public DataTable showeventwisedata(int Company_ID, int Location_ID, string EventName)
           {
               return _objEmail.showeventwisedata(Company_ID, Location_ID, EventName);
           }
           public DataTable Showallcompwise(int Company_ID)
           {
               return _objEmail.Showallcompwise(Company_ID);
           }
           public DataTable Showalllocwise(int Location_ID)
           {
               return _objEmail.Showalllocwise(Location_ID);
           }
           public DataTable ShowAlldata()
           {
               return _objEmail.ShowAlldata();
           }

       }
  
    }
}
