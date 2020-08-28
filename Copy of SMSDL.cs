using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using RBBE;
using RBDL;

namespace RBDL
{
   public class SMSDL
    {
       CommonDL _objCommonDL = new CommonDL();
       public class SmsConfigurationDL
       {
           CommonDL _objComDL = new CommonDL();
           SMSBE.SmsConfigurationBE _objSmsBE = new SMSBE.SmsConfigurationBE();

           public long InsertUpdateSmsConfigDetails(SMSBE.SmsConfigurationBE _objSmsBE)
           {
               try
               {
                   long returnValue = 0;

                   SqlParameter ParamAutoID = new SqlParameter("@AutoID", SqlDbType.Int);
                   ParamAutoID.Value = _objSmsBE.AutoID;

                   SqlParameter ParameServerID = new SqlParameter("@sC_ServerDetail", SqlDbType.NVarChar);
                   ParameServerID.Value = _objSmsBE.sC_ServerDetail;

                   SqlParameter ParamUserName = new SqlParameter("@sC_UserName", SqlDbType.NVarChar);
                   ParamUserName.Value = _objSmsBE.sC_UserName;

                   SqlParameter ParamPassword = new SqlParameter("@sC_Password", SqlDbType.NVarChar);
                   ParamPassword.Value = _objSmsBE.sC_Password;

                   SqlParameter ParamClientID = new SqlParameter("@sC_ClientID", SqlDbType.Int);
                   ParamClientID.Value = _objSmsBE.sC_ClientID;

                   SqlParameter ParamCompanyId = new SqlParameter("@CompanyId", SqlDbType.Int);
                   ParamCompanyId.Value = _objSmsBE.CompanyId;

                   SqlParameter ParamLocationId = new SqlParameter("@LocationId", SqlDbType.Int);
                   ParamLocationId.Value = _objSmsBE.LocationId;

                   SqlParameter ParamisActive = new SqlParameter("@sC_isActive", SqlDbType.Bit);
                   ParamisActive.Value = _objSmsBE.sC_isActive;

                   SqlParameter ParamisDefault = new SqlParameter("@sC_isDefault", SqlDbType.Bit);
                   ParamisDefault.Value = _objSmsBE.sC_isDefault;

                   SqlParameter ParamAddeddBy = new SqlParameter("@AddedBy", SqlDbType.Int);
                   ParamAddeddBy.Value = _objSmsBE.AddeddBy;

                   SqlParameter ParamModifiedBy = new SqlParameter("@LastModifiedBy", SqlDbType.Int);
                   ParamModifiedBy.Value = _objSmsBE.ModifiedBy;

                   SqlParameter ParamAddeddOn = new SqlParameter("@AddedOn", SqlDbType.DateTime);
                   ParamAddeddOn.Value = _objSmsBE.AddeddOn;

                   SqlParameter ParamModifiedOn = new SqlParameter("@LastModifiedOn", SqlDbType.DateTime);
                   ParamModifiedOn.Value = _objSmsBE.ModifiedOn;

                   SqlParameter paramsC_status = new SqlParameter("@sC_status", SqlDbType.Bit);
                   paramsC_status.Value = 1;

                   SqlParameter paramSuccess = new SqlParameter("@success", SqlDbType.Bit);
                   paramSuccess.Value = 0;


                   paramSuccess.Direction = ParameterDirection.Output;
                   SqlParameter[] _parameter = { ParamAutoID,
                                        ParameServerID,
                                        ParamUserName,
                                        ParamPassword,
                                        ParamClientID,
                                        ParamCompanyId,
                                        ParamLocationId,
                                        ParamisActive,
                                        ParamisDefault,
                                        ParamAddeddBy,
                                        ParamModifiedBy,
                                        ParamAddeddOn,
                                        ParamModifiedOn,
                                        paramsC_status,
                                        paramSuccess };
                   returnValue = _objComDL.executeReturnLongNonSelectQuery("[EINS_RBMS_SMS].[dbo].Isp_SMSConfiguration", _parameter);
                   return returnValue;
               }
               catch
               {
                   return 0;

               }

           }

           public DataTable getSmsDetails()
           {
               string str = "select AutoID,C.CompanyName as Company,L.LocationName as Location,sC_ServerDetail,sC_UserName,sC_Password,sC_ClientID,S.CompanyId,S.LocationId,sC_isActive,sC_isDefault from EINS_RBMS_SMS.[dbo].SMSRoute_Configuration S join [EINS_RBMS].dbo.CompanyMaster C on(C.[Status]=1 and s.CompanyId=C.CompanyId)join [EINS_RBMS].dbo.LocationMaster L on (L.[LocationStatus]=1 and s.LocationId=L.LocationId) where sC_status=1";
               DataTable dt = _objComDL.executeSelectQuery(str);
               return dt;
           }

           public DataTable getEditSmsDetail(int smsId)
           {
               string strEdit = "select AutoID,C.CompanyName as Company,L.LocationName as Location,sC_ServerDetail,sC_UserName,sC_Password,sC_ClientID,S.CompanyId,S.LocationId,sC_isActive,sC_isDefault from EINS_RBMS_SMS.[dbo]. SMSRoute_Configuration S join [EINS_RBMS].dbo.CompanyMaster C on(C.[Status]=1 and s.CompanyId=c.CompanyId)join [EINS_RBMS].dbo.LocationMaster L on (L.[LocationStatus]=1 and s.LocationId=l.LocationId) where sC_status=1 and AutoID='" + smsId + "'";
               DataTable dt = _objComDL.executeSelectQuery(strEdit);
               return dt;
           }

           public int deleteSmsDetailbyId(int smsId)
           {
               string strdelete = "update EINS_RBMS_SMS.[dbo].SMSRoute_Configuration set sC_status=0 where AutoID='" + smsId + "'";
               int result = _objComDL.executeScalarQuery(strdelete);
               return result;
           }

           public int chkAssociat_exist(int CompanyId, int LocationId)
           {
               string chkassociation = "Select count(1) from EINS_RBMS_SMS.[dbo].SMSRoute_Configuration where CompanyId='" + CompanyId + "' and LocationId='" + LocationId + "' and sC_status='true'";
               int result = _objComDL.executeScalarQuery(chkassociation);
               return result;

           }

       }

       public class SMSEventManagerDL
       {
           CommonFunction _objComm = new CommonFunction();
           CommonDL _objCommDL = new CommonDL();
           EINS_RBMS_SMSEntities _objSmsEntity = new EINS_RBMS_SMSEntities();

           public long Insert_SmsManagerDetails(SMSBE.SmsEventManagerBE  _objSmsBE)
           {
               try
               {
                   EventsManager objEvm = new EventsManager();

                   objEvm.EventName = _objSmsBE.EventName;
                   objEvm.eM_ModuleId = _objSmsBE.eM_ModuleId;
                   objEvm.eM_FormId = _objSmsBE.eM_FormId;
                   objEvm.eM_Event_Id = _objSmsBE.eM_Event_Id;
                   objEvm.eM_CompanyId = _objSmsBE.eM_CompanyId;
                   objEvm.eM_LocationId = _objSmsBE.eM_LocationId;
                   objEvm.CommonSMSBody = _objSmsBE.CommonSMSBody;
                   objEvm.HostSMSBody = _objSmsBE.HostSMSBody;
                   objEvm.VisitorSMSBody = _objSmsBE.VisitorSMSBody;
                   objEvm.EmployeeContact = _objSmsBE.EmployeeContact;
                   objEvm.AdditionalContact = _objSmsBE.AdditionalContact;
                   //objEvm.GroupIds = _objSmsBE.GroupIds;
                   objEvm.CheckForHost = _objSmsBE.CheckForHost;
                   objEvm.CheckForVisitor = _objSmsBE.CheckForVisitor;
                   objEvm.AddedBy = Convert.ToInt32(_objSmsBE.AddedBy);
                   objEvm.AddedOn = DateTime.Now;
                   objEvm.LastModifiedBy = null;
                   objEvm.LastModifiedOn = null;
                   _objSmsEntity.AddToEventsManagers(objEvm);
                   _objSmsEntity.SaveChanges();

                   return objEvm.Event_Id;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }
           public long Update_SmsManagerDetails(SMSBE.SmsEventManagerBE  _objSmsBE)
           {
               try
               {
                   EventsManager objEvm = _objSmsEntity.EventsManagers.Where(x => x.Event_Id == _objSmsBE.Event_Id).FirstOrDefault();

                   objEvm.EventName = _objSmsBE.EventName;
                   objEvm.eM_ModuleId = _objSmsBE.eM_ModuleId;
                   objEvm.eM_FormId = _objSmsBE.eM_FormId;
                   objEvm.eM_Event_Id = _objSmsBE.eM_Event_Id;
                   objEvm.eM_CompanyId = _objSmsBE.eM_CompanyId;
                   objEvm.eM_LocationId = _objSmsBE.eM_LocationId;
                   objEvm.CommonSMSBody = _objSmsBE.CommonSMSBody;
                   objEvm.HostSMSBody = _objSmsBE.HostSMSBody;
                   objEvm.VisitorSMSBody = _objSmsBE.VisitorSMSBody;
                   objEvm.EmployeeContact = _objSmsBE.EmployeeContact;
                   objEvm.AdditionalContact = _objSmsBE.AdditionalContact;
                   //objEvm.GroupIds = _objSmsBE.GroupIds;
                   objEvm.CheckForHost = _objSmsBE.CheckForHost;
                   objEvm.CheckForVisitor = _objSmsBE.CheckForVisitor;
                   objEvm.LastModifiedBy = Convert.ToInt32(_objSmsBE.LastModifiedBy);
                   objEvm.LastModifiedOn = DateTime.Now;
                   _objSmsEntity.SaveChanges();

                   return objEvm.Event_Id;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }
           public DataTable getmoduleDetails()
           {
               string module_Detail = "select ModuleId, ModuleName from [EINS_RBMS_SMS].[dbo].[Modules] where status=1";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }

           // event manager Section filling
           public DataTable getSectionDetais(int modulId)
           {
               string module_Detail = "select SectionName,SectionID from [EINS_RBMS].[dbo].[SectionMaster] where [EmailSmsStatus]=1 and [SectionHeadId]='" + modulId + "'";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }

           // event manager Forms filling
           public DataTable getFormDetais(int SectionID)
           {
               string module_Detail = "select [SubsectionName] ,SubsectionID from [EINS_RBMS].[dbo].[SubSectionMaster] F join [EINS_RBMS].[dbo].[Sectionmaster] S on (F.SectionId=S.SectionID )where S.SectionID='" + SectionID + "'";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }
           // get event details
           public DataTable getEventmanagerDetails()
           {
               string EventManager_Detail = "select S.Event_Id,S.EventName,C.CompanyName as Company ,L.LocationName as Location from [EINS_RBMS_SMS].[dbo].[EventManager] S join EINS_RBMS .[dbo].[CompanyMaster] C on (C.[Status]=1 and S.eM_CompanyId=C.CompanyId) join [EINS_RBMS].[dbo].[LocationMaster] L on (L.[LocationStatus]=1 and S.eM_LocationId=L.LocationId)";
               DataTable dt = _objCommDL.executeSelectQuery(EventManager_Detail);
               return dt;
           }
           // serach by Id
           public DataTable geteventmanagementsearchdetailbyID(int Event_id)
           {
               //string eventmanagersearchdetailbyid = "select  EventName,eM_ModuleId,eM_SectionID,eM_FormId,eM_Event_Id,eM_CompanyId,eM_LocationId,EmployeeBody,ReportingToBody,AdminBody,RuleName from EINSPRO_EMAIL.[dbo].[EventManager] where Event_Id='" + Event_id + "' ";

               string eventmanagersearchdetailbyid1 = "select eM_CompanyId,eM_LocationId,EventName,eM_ModuleId,eM_FormId,eM_Event_Id,HostSMSBody,CommonSMSBody,VisitorSMSBody,EmployeeContact,AdditionalContact,CheckForHost,CheckForVisitor from EINS_RBMS_SMS.[dbo].[EventManager] where Event_Id='" + Event_id + "'";
               DataTable dt = _objCommDL.executeSelectQuery(eventmanagersearchdetailbyid1);
               return dt;
           }


           public DataTable getEventDetais(int formId)
           {
               //string module_Detail = "select [EventsID],[EventsName] from EINSPRO_EMAIL.[dbo].[Events]  where (FormID like '6,%' or FormID like '6%,' or FormID like '%,6,%' or  FormID ='6')";
               string module_Detail = "select [EventsID],[EventsName] from EINS_RBMS_SMS.[dbo].[Events] where (FormID like '" + formId + ",%' or FormID like '%," + formId + "' or FormID like '%," + formId + ",%' or  FormID ='" + formId + "')";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }

           public DataTable getSmsColumnName(int sectionId, int formId)
           {
               string strsmscolumn = "select ColumnID,[ColumnName] from EINS_RBMS_SMS.[dbo].[Section_Details] S join EINS_RBMS_SMS.[dbo].[Forms] F on (f.SectionId=s.Section_Id)  join EINS_RBMS_SMS.[dbo].[Columns] C on (S.SectionID= c.Sectionwise_column)  where S.SectionID='" + sectionId + "' and C.FormID='" + formId + "'";
               DataTable dt = _objCommDL.executeSelectQuery(strsmscolumn);
               return dt;

           }

           public DataTable getSubModule(int mainmoduleId)
           {
               string str = " select ModuleId,[ModuleName] from  EINS_RBMS_SMS.[dbo].Modules where MainModuleId='" + mainmoduleId + "'";
               DataTable dt = _objCommDL.executeSelectQuery(str);
               return dt;

           }

           public int Delete_SMSEvent(int SMSEventID)
           {
               int count = 0;
               count = _objCommDL.ExecuteInsertUpdateQuery("delete from EINS_RBMS_SMS.[dbo].[EventManager]  where Event_Id=" + SMSEventID + "");
               return count;
           }
           public bool Delete_SMS(Int64 SMSID)
           {
               try
               {
                   Sent objSms = _objSmsEntity.Sents.FirstOrDefault(x => x.SentId == SMSID);
                   _objSmsEntity.DeleteObject(objSms);
                   _objSmsEntity.SaveChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   return false;
               }
           }

           // Get Module Details.................!!!!
           public DataTable getModulDetais()
           {
               string module_Detail = "Select ModuleID,ModuleName from [EINS_RBMS_SMS].[dbo].[Modules] where [status]=1";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }
           //Get Form Details.....................!!!
           public DataTable getFormsDetais(int ModuleID)
           {
               string module_Detail = "select FormName,FormID from [EINS_RBMS_SMS].[dbo].[Forms] where [ModuleID]='" + ModuleID + "'";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }
           // Get Events Details..................!!!
           public DataTable getEventsDetais(int formId)
           {
               //string module_Detail = "select [EventsID],[EventsName] from EINSPRO_EMAIL.[dbo].[Events]  where (FormID like '6,%' or FormID like '6%,' or FormID like '%,6,%' or  FormID ='6')";
               string module_Detail = "select [EventsID],[EventsName] from [EINS_RBMS_SMS].[dbo].[Events] where (FormID like '" + formId + ",%' or FormID like '%," + formId + "' or FormID like '%," + formId + ",%' or  FormID ='" + formId + "')";
               DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
               return dt;
           }


}

       public class AdditionalContactsDL
       {
           EINS_RBMS_SMSEntities _objSmsEntity = new EINS_RBMS_SMSEntities();

           public long Insert_AdditionalContacts(SMSBE.AdditionalContactsBE _objAddiBE)
           {
               try
               {
                   AdditionalContact objAdditionalContact = new AdditionalContact();

                   objAdditionalContact.ContactName = _objAddiBE.ContactName;
                   objAdditionalContact.ContactNo = _objAddiBE.ContactNo;
                   objAdditionalContact.AddedBy = _objAddiBE.AddedBy;
                   objAdditionalContact.AddedOn = DateTime.Now;
                   objAdditionalContact.LastModifiedBy = null;
                   objAdditionalContact.LastModifiedOn = null;
                   _objSmsEntity.AddToAdditionalContacts(objAdditionalContact);
                   _objSmsEntity.SaveChanges();

                   return objAdditionalContact.AdditionalContId;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }
           public long Update_AdditionalContacts(SMSBE.AdditionalContactsBE _objAddiBE)
           {

               try
               {
                   AdditionalContact objAdditionalContact = _objSmsEntity.AdditionalContacts.Where(x => x.AdditionalContId == _objAddiBE.AdditionalContId).FirstOrDefault();

                   objAdditionalContact.ContactName = _objAddiBE.ContactName;
                   objAdditionalContact.ContactNo = _objAddiBE.ContactNo;
                   objAdditionalContact.LastModifiedBy = Convert.ToInt32(_objAddiBE.LastModifiedBy);
                   objAdditionalContact.LastModifiedOn = DateTime.Now;
                   _objSmsEntity.SaveChanges();

                   return objAdditionalContact.AdditionalContId;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }

           public bool Delete_AdditionalContacts(int AddContactID)
           {
               try
               {
                   AdditionalContact objSms = _objSmsEntity.AdditionalContacts.FirstOrDefault(x => x.AdditionalContId == AddContactID);
                   _objSmsEntity.DeleteObject(objSms);
                   _objSmsEntity.SaveChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   return false;
               }
           }

           public List<SMSBE.AdditionalContactsBE> Get_SmsAdditionalContacts(int AddContactID)
           {
               try
               {
                   var lst = from _Obj in _objSmsEntity.AdditionalContacts
                             where _Obj.AdditionalContId == AddContactID
                             select new SMSBE.AdditionalContactsBE
                             {
                                 AdditionalContId = _Obj.AdditionalContId,
                                 ContactName = _Obj.ContactName,
                                 ContactNo = _Obj.ContactNo
                             };
                   return lst.ToList();
               }
               catch (Exception ex)
               {
                   return null;
               }
           }
       }

       public class Group_DetailsDL
       {
           EINS_RBMS_SMSEntities _objSmsEntity = new EINS_RBMS_SMSEntities();

           public long Insert_Group_Details(SMSBE.Group_DetailsBE _objAddiBE)
           {
               try
               {
                   Group_Details objGroupContact = new Group_Details();

                   objGroupContact.GroupName = _objAddiBE.GroupName;
                   objGroupContact.GroupDescription = _objAddiBE.GroupDescription;
                   objGroupContact.Grp_Contacts_Emp = _objAddiBE.Grp_Contacts_Emp;
                   objGroupContact.Grp_Contacts_Add = _objAddiBE.Grp_Contacts_Add;
                   objGroupContact.status = _objAddiBE.status;
                   objGroupContact.AddedBy = _objAddiBE.AddedBy;
                   objGroupContact.AddedOn = DateTime.Now;
                   objGroupContact.LastModifiedBy = null;
                   objGroupContact.LastModifiedOn = null;
                   _objSmsEntity.AddToGroup_Details(objGroupContact);
                   _objSmsEntity.SaveChanges();

                   return objGroupContact.GroupID;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }

           public long Update_Group_Details(SMSBE.Group_DetailsBE _objAddiBE)
           {
               try
               {
                   Group_Details objGroupContact = _objSmsEntity.Group_Details.Where(x => x.GroupID == _objAddiBE.GroupID).FirstOrDefault();

                   objGroupContact.GroupName = _objAddiBE.GroupName;
                   objGroupContact.GroupDescription = _objAddiBE.GroupDescription;
                   objGroupContact.Grp_Contacts_Emp = _objAddiBE.Grp_Contacts_Emp;
                   objGroupContact.Grp_Contacts_Add = _objAddiBE.Grp_Contacts_Add;
                   objGroupContact.status = _objAddiBE.status;
                   objGroupContact.LastModifiedBy = Convert.ToInt32(_objAddiBE.LastModifiedBy);
                   objGroupContact.LastModifiedOn = DateTime.Now;
                   _objSmsEntity.SaveChanges();

                   return objGroupContact.GroupID;
               }
               catch (Exception ex)
               {
                   return (0);
               }
           }

           public bool Delete_Group_Details(int GroupID)
           {
               try
               {
                   Group_Details objGroupContact = _objSmsEntity.Group_Details.Where(x => x.GroupID == GroupID).FirstOrDefault();

                   objGroupContact.status = false;
                   _objSmsEntity.SaveChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   return false;
               }
           }

           public List<SMSBE.Group_DetailsBE> Get_Group_Details(int GroupID)
           {
               try
               {
                   var lst = from _Obj in _objSmsEntity.Group_Details
                             where _Obj.GroupID == GroupID && _Obj.status == true
                             select new SMSBE.Group_DetailsBE
                             {
                                 GroupID = _Obj.GroupID,
                                 GroupName = _Obj.GroupName,
                                 GroupDescription = _Obj.GroupDescription,
                                 Grp_Contacts_Emp = _Obj.Grp_Contacts_Emp,
                                 Grp_Contacts_Add = _Obj.Grp_Contacts_Add
                             };
                   return lst.ToList();
               }
               catch (Exception ex)
               {
                   return null;
               }
           }


       }
    }
}
