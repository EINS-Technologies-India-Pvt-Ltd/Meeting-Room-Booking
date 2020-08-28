using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RBBE;
using RBBL;
using RBDL;

namespace RBBL
{
   public class SMSBL
    {
       SMSDL.SmsConfigurationDL _objSmcDL = new SMSDL.SmsConfigurationDL();
       public class SmsConfigurationBL
       {
           SMSDL.SmsConfigurationDL _objSmcDL = new SMSDL.SmsConfigurationDL();
           public long InsertUpdateSmsConfigDetails(SMSBE.SmsConfigurationBE _objSmsBE)
           {
               return _objSmcDL.InsertUpdateSmsConfigDetails(_objSmsBE);
           }

           public DataTable getSmsDetails()
           {
               DataTable dt = _objSmcDL.getSmsDetails();
               return dt;
           }

           public DataTable getEditSmsDetail(int smsId)
           {
               return _objSmcDL.getEditSmsDetail(smsId);
           }

           public int deleteSmsDetailbyId(int smsId)
           {
               return _objSmcDL.deleteSmsDetailbyId(smsId);
           }

           public int chkAssociat_exist(int CompanyId, int LocationId)
           {
               return _objSmcDL.chkAssociat_exist(CompanyId, LocationId);
           }

           
       }

       public class SmsEventManagerBL
       {
           SMSDL.SMSEventManagerDL _objSmsDL = new SMSDL.SMSEventManagerDL();


           public long Insert_SmsManagerDetails(SMSBE.SmsEventManagerBE _objSMsBE)
           {
               return _objSmsDL.Insert_SmsManagerDetails(_objSMsBE);
           }
           public long Update_SmsManagerDetails(SMSBE.SmsEventManagerBE _objSMsBE)
           {
               return _objSmsDL.Update_SmsManagerDetails(_objSMsBE);
           }
           //public DataTable getmoduleDetails()
           //{
           //    return _objSmsDL.getmoduleDetails();
           //}

           public DataTable getSectionDetais(int modulId)
           {
               return _objSmsDL.getSectionDetais(modulId);

           }

           public DataTable getFormDetais(int SectionID)
           {
               return _objSmsDL.getFormDetais(SectionID);
           }

           public DataTable getEventmanagerDetails()
           {
               return _objSmsDL.getEventmanagerDetails();
           }

           public DataTable geteventmanagementsearchdetailbyID(int Event_id)
           {
               return _objSmsDL.geteventmanagementsearchdetailbyID(Event_id);
           }

           public DataTable getEventDetais(int formId)
           {
               return _objSmsDL.getEventDetais(formId);
           }

           public DataTable getSmsColumnName(int sectionId, int formId)
           {
               return _objSmsDL.getSmsColumnName(sectionId, formId);
           }

           public DataTable getSubModule(int mainmoduleId)
           {
               return _objSmsDL.getSubModule(mainmoduleId);
           }

           public int Delete_SMSEvent(int SMSEventID)
           {
               return _objSmsDL.Delete_SMSEvent(SMSEventID);
           }

           public bool Delete_SMS(Int64 SMSId)
           {
               return _objSmsDL.Delete_SMS(SMSId);
           }

           public DataTable getmoduleDetails()
           {
               return _objSmsDL.getmoduleDetails();
           }

           public DataTable getModulDetais()
           {
               return _objSmsDL.getModulDetais();

           }
           public DataTable getFormsDetais(int ModuleID)
           {
               return _objSmsDL.getFormsDetais(ModuleID);
           }
           public DataTable getEventsDetais(int formId)
           {
               return _objSmsDL.getEventsDetais(formId);
           }



       }

      
     
    }
}
