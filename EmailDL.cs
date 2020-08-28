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
  public class EmailDL
    {
      public class EmailConfigurationDL
      {
          
          CommonDL _objComDL = new CommonDL();
          EmailBE.EmailConfigurationBE _objEmailBE = new EmailBE.EmailConfigurationBE();
          
          EINS_RBMS_EMAILEntities _ObjEmailEntity = new EINS_RBMS_EMAILEntities();

          public long InsertUpdateEmailConfigDetails(EmailBE.EmailConfigurationBE _objEmailBE)
          {
              try
              {
                  long returnValue = 0;

                  SqlParameter ParamEmailId = new SqlParameter("@eC_Id", SqlDbType.Int);
                  ParamEmailId.Value = _objEmailBE.eC_Id;

                  SqlParameter ParameSMTPServerName = new SqlParameter("@eC_SMTPServerName", SqlDbType.NVarChar);
                  ParameSMTPServerName.Value = _objEmailBE.eC_SMTPServerName;

                  SqlParameter ParamUserName = new SqlParameter("@eC_UserName", SqlDbType.NVarChar);
                  ParamUserName.Value = _objEmailBE.eC_UserName;

                  SqlParameter ParamPassword = new SqlParameter("@eC_Password", SqlDbType.NVarChar);
                  ParamPassword.Value = _objEmailBE.eC_Password;

                  SqlParameter ParamSMTPPort = new SqlParameter("@eC_SMTPPort", SqlDbType.Int);
                  ParamSMTPPort.Value = _objEmailBE.eC_SMTPPort;

                  SqlParameter ParamIsSSLEnabled = new SqlParameter("@eC_IsSSLEnabled", SqlDbType.Bit);
                  ParamIsSSLEnabled.Value = _objEmailBE.eC_IsSSLEnabled;

                  SqlParameter ParamCompanyId = new SqlParameter("@CompanyId", SqlDbType.Int);
                  ParamCompanyId.Value = _objEmailBE.CompanyId;

                  SqlParameter ParamLocationId = new SqlParameter("@LocationId", SqlDbType.Int);
                  ParamLocationId.Value = _objEmailBE.LocationId;

                  SqlParameter ParamIsDefault = new SqlParameter("@eC_IsDefault", SqlDbType.Bit);
                  ParamIsDefault.Value = _objEmailBE.eC_IsDefault;

                  SqlParameter ParamIsActive = new SqlParameter("@eC_IsActive", SqlDbType.Bit);
                  ParamIsActive.Value = _objEmailBE.eC_IsActive;

                  SqlParameter ParamDisplayName = new SqlParameter("@DisplayName", SqlDbType.NVarChar);
                  ParamDisplayName.Value = _objEmailBE.DisplayName;

                  SqlParameter ParamPop3Server = new SqlParameter("@eC_Pop3Server", SqlDbType.NVarChar);
                  ParamPop3Server.Value = _objEmailBE.eC_Pop3Server;

                  SqlParameter ParamAddedBy = new SqlParameter("@AddedBy", SqlDbType.NVarChar);
                  ParamAddedBy.Value = _objEmailBE.AddedBy;

                  SqlParameter ParamModifyBy = new SqlParameter("@LastModifiedBy", SqlDbType.NVarChar);
                  ParamModifyBy.Value = _objEmailBE.ModifyBy;

                  SqlParameter ParamPop3Portno = new SqlParameter("@ec_Pop3Portno", SqlDbType.Int);
                  ParamPop3Portno.Value = _objEmailBE.ec_Pop3Portno;

                  SqlParameter paramSuccess = new SqlParameter("@success", SqlDbType.Bit);
                  paramSuccess.Value = 0;

                  paramSuccess.Direction = ParameterDirection.Output;
                  SqlParameter[] _parameter = { ParamEmailId,
                                        ParameSMTPServerName,
                                        ParamUserName,
                                        ParamPassword,
                                        ParamSMTPPort,
                                        ParamIsSSLEnabled,
                                        ParamCompanyId,
                                        ParamLocationId,
                                        ParamIsDefault,
                                        ParamIsActive,
                                        ParamDisplayName,
                                        ParamPop3Server,
                                        ParamAddedBy,
                                        ParamModifyBy,
                                        ParamPop3Portno,
                                        paramSuccess };
                  returnValue = _objComDL.executeReturnLongNonSelectQuery("EINSPRO_EMAIL.[dbo].Isp_EmailConfiguration", _parameter);
                  return returnValue;
              }
              catch
              {
                  return 0;

              }

          }
          //bind emaul details
          public DataTable getEmailDetails()
          {
              string str = "select eC_Id, C.CompanyName as Company ,L.LocationName as Location,eC_SMTPServerName,eC_SMTPPort,eC_Pop3Server,ec_Pop3Portno,eC_UserName,eC_Password,DisplayName from EINS_RBMS_EMAIL.[dbo].[EmailConfiguration] E join  EINS_RBMS.[dbo].[CompanyMaster] C on (C.[Status]=1 and E.CompanyId=C.CompanyId)join EINS_RBMS.[dbo].[LocationMaster] L on (L.[LocationStatus]=1 and E.LocationId=L.LocationId) where eC_status=1 order by [eC_Id] desc";
              DataTable dt = _objComDL.executeSelectQuery(str);
              return dt;
          }


          public DataTable getEditEmailDetail(int emialId)
          {
              string strEdit = "select eC_Id, C.CompanyName as Company,L.LocationName as Location,E.CompanyId,L.LocationId, eC_SMTPServerName,eC_SMTPPort,eC_Pop3Server,ec_Pop3Portno,eC_UserName,eC_Password,DisplayName, eC_IsSSLEnabled,eC_IsDefault,eC_IsActive from EINS_RBMS_EMAIL.[dbo].[EmailConfiguration] E join EINS_RBMS.[dbo].[CompanyMaster] C on (E.CompanyId=C.CompanyId)join EINS_RBMS.[dbo].[LocationMaster] L on(E.LocationId=L.LocationId) where eC_Id='" + emialId + "' order by [eC_Id] desc";
              DataTable dt = _objComDL.executeSelectQuery(strEdit);
              return dt;
          }

          public int chkAssociat_exist(int CompanyId, int LocationId)
          {
              string chkassociation = "Select count(1) from EINSPRO_EMAIL.[dbo].[EmailConfiguration] where CompanyId='" + CompanyId + "' and LocationId='" + LocationId + "' and eC_status='true'";
              int result = _objComDL.executeScalarQuery(chkassociation);
              return result;

          }
          public int Insert_EmailConfiguration(EmailBE.EmailConfigurationBE _objBE)
          {
              try
              {
                  
                  EmailConfiguration _ObjEmail = new EmailConfiguration();
                  _ObjEmail.eC_SMTPServerName = _objBE.eC_SMTPServerName;
                  _ObjEmail.eC_UserName = _objBE.eC_UserName;
                  _ObjEmail.eC_Password = _objBE.eC_Password;
                  _ObjEmail.eC_SMTPPort = _objBE.eC_SMTPPort;
                  _ObjEmail.eC_IsSSLEnabled = _objBE.eC_IsSSLEnabled;
                  _ObjEmail.CompanyId = _objBE.CompanyId;
                  _ObjEmail.LocationId = _objBE.LocationId;
                  _ObjEmail.eC_status = true;
                  _ObjEmail.eC_IsDefault = _objBE.eC_IsDefault;
                  _ObjEmail.eC_IsActive = _objBE.eC_IsActive;
                  _ObjEmail.eC_signature = _objBE.eC_signature;
                  _ObjEmail.eC_Pop3Server = _objBE.eC_Pop3Server;
                  _ObjEmail.ec_Pop3Portno = _objBE.ec_Pop3Portno;
                  _ObjEmail.DisplayName = _objBE.DisplayName;
                  _ObjEmail.AddedBy = _objBE.AddedBy;
                  _ObjEmail.AddedOn = DateTime.Now;
                  _ObjEmail.eC_signature = "";
                  _ObjEmail.LastModifiedBy = null;
                  _ObjEmail.LastModifiedOn = null;

                  _ObjEmailEntity.AddToEmailConfigurations(_ObjEmail);
                  _ObjEmailEntity.SaveChanges();
                  return _ObjEmail.eC_Id;
              }
              catch (Exception ex)
              {
                  return 0;
              }

          }
          public int Update_EmailConfiguration(EmailBE.EmailConfigurationBE _objBE)
          {
              EmailConfiguration _ObjEmail = _ObjEmailEntity.EmailConfigurations.Where(x => x.eC_Id == _objBE.eC_Id).FirstOrDefault();
              _ObjEmail.eC_SMTPServerName = _objBE.eC_SMTPServerName;
              _ObjEmail.eC_UserName = _objBE.eC_UserName;
              _ObjEmail.eC_Password = _objBE.eC_Password;
              _ObjEmail.eC_SMTPPort = _objBE.eC_SMTPPort;
              _ObjEmail.eC_IsSSLEnabled = _objBE.eC_IsSSLEnabled;
              _ObjEmail.CompanyId = _objBE.CompanyId;
              _ObjEmail.LocationId = _objBE.LocationId;
              _ObjEmail.eC_status = true;
              _ObjEmail.eC_IsDefault = _objBE.eC_IsDefault;
              _ObjEmail.eC_IsActive = _objBE.eC_IsActive;
              _ObjEmail.eC_signature = _objBE.eC_signature;
              _ObjEmail.eC_Pop3Server = _objBE.eC_Pop3Server;
              _ObjEmail.ec_Pop3Portno = _objBE.ec_Pop3Portno;
              _ObjEmail.DisplayName = _objBE.DisplayName;

              _ObjEmail.LastModifiedBy = _objBE.ModifyBy;
              _ObjEmail.LastModifiedOn = _objBE.LastModifiedOn;
              _ObjEmailEntity.SaveChanges();
              return _ObjEmail.eC_Id;

          }
          public int DeleteEmailConfiguration(int Id)
          {
              EmailConfiguration obj = _ObjEmailEntity.EmailConfigurations.Where(x => x.eC_Id == Id).FirstOrDefault();
              obj.eC_status = false;
              _ObjEmailEntity.SaveChanges();
              return obj.eC_Id;
          }
      }
      public class EmailEventManagerDL
      {
          CommonFunction _objComm = new CommonFunction();
          CommonDL _objCommDL = new CommonDL();
          EINS_RBMS_EMAILEntities objEmailEntity = new EINS_RBMS_EMAILEntities();


          //public long Insert_EmailManagerDetails(EmailBE.EmailEventManagerBE _objEmailBE)
          //{
          //    try
          //    {
          //        Event_Manager objEvM = new Event_Manager();
          //        objEvM.EventName = _objEmailBE.EventName;
          //        objEvM.eM_ModuleId = _objEmailBE.eM_ModuleId;
          //        objEvM.eM_SectionID = _objEmailBE.eM_SectionID;
          //        objEvM.eM_MainModuleId = _objEmailBE.eM_MainModuleId;
          //        objEvM.eM_FormId = _objEmailBE.eM_FormId;
          //        objEvM.eM_Event_Id = _objEmailBE.eM_Event_Id;
          //        objEvM.eM_CompanyId = _objEmailBE.eM_CompanyId;
          //        objEvM.eM_LocationId = _objEmailBE.eM_LocationId;
          //        objEvM.EmployeeBody = _objEmailBE.EmployeeBody;
          //        objEvM.AdminBody = _objEmailBE.AdminBody;
          //        objEvM.ReportingToBody = _objEmailBE.ReportingToBody;
          //        objEvM.RuleName = _objEmailBE.RuleName;
          //        objEvM.AddedBy = Convert.ToInt32(_objEmailBE.AddedBy);
          //        objEvM.AddedOn = DateTime.Now;
          //        objEvM.status = true;
          //        objEvM.LastModifiedOn = DateTime.Now;
          //        objEvM.LastModifiedBy = Convert.ToInt32(_objEmailBE.LastModifiedBy);
          //        objEmailEntity.AddToEvent_Manager(objEvM);
          //        objEmailEntity.SaveChanges();

          //        return objEvM.Event_Id;
          //    }
          //    catch (Exception ex)
          //    {
          //        return 0;
          //    }
          //}


          #region AFR

          public long Insert_EmailEventManagerDetails(EmailBE.EMLEventManagerBE _objEmailEvntBE)
          {
              try
              {
                  EventManager objEvM = new EventManager();
                  objEvM.eM_Id = _objEmailEvntBE.eM_Id;
                  objEvM.eM_ModuleId = _objEmailEvntBE.eM_ModuleId;
                  objEvM.eM_FormId = _objEmailEvntBE.eM_FormId;
                  objEvM.eM_Events_Id =_objEmailEvntBE.eM_Events_Id;
                  objEvM.eM_CompanyId = _objEmailEvntBE.eM_CompanyId;
                  objEvM.eM_LocationId = _objEmailEvntBE.eM_LocationId;
                  objEvM.eM_ConfigId = _objEmailEvntBE.eM_ConfigId;
                  objEvM.eM_RuleName = _objEmailEvntBE.eM_RuleName;
                  objEvM.eM_Subject = _objEmailEvntBE.eM_Subject;
                  objEvM.eM_CommonEmailBody = _objEmailEvntBE.eM_CommonEmailBody;
                  objEvM.eM_HostEmailBody = _objEmailEvntBE.eM_HostEmailBody;
                  objEvM.eM_VisitorEmailBody = _objEmailEvntBE.eM_VisitorEmailBody;
                  objEvM.eM_Priority = _objEmailEvntBE.eM_Priority;
                  objEvM.eM_Reciepients = _objEmailEvntBE.eM_Reciepients;
                  objEvM.AddedBy = Convert.ToInt32(_objEmailEvntBE.AddedBy);
                  objEvM.AddedOn = DateTime.Now;
                  objEvM.eM_Reciepients = _objEmailEvntBE.eM_Reciepients;
                  objEvM.LastModifiedOn = DateTime.Now;
                  objEvM.LastModifiedBy = Convert.ToInt32(_objEmailEvntBE.LastModifiedBy);

                  objEmailEntity.AddToEventManagers(objEvM);
                  objEmailEntity.SaveChanges();

                  return objEvM.eM_Id;
              }
              catch (Exception ex)
              {
                  return 0;
              }
          }

          public long Update_EmailEventManagerDetails(EmailBE.EMLEventManagerBE _objEmailBE)
          {
              try
              {
                  EventManager objEvM = objEmailEntity.EventManagers.Where(x => x.eM_Id == _objEmailBE.eM_Id).FirstOrDefault();

                  objEvM.eM_Id = _objEmailBE.eM_Id;
                  objEvM.eM_ModuleId = _objEmailBE.eM_ModuleId;
                  objEvM.eM_FormId = _objEmailBE.eM_FormId;
                  objEvM.eM_Events_Id = _objEmailBE.eM_Events_Id;
                  objEvM.eM_CompanyId = _objEmailBE.eM_CompanyId;
                  objEvM.eM_LocationId = _objEmailBE.eM_LocationId;
                  objEvM.eM_ConfigId = _objEmailBE.eM_ConfigId;
                  objEvM.eM_RuleName = _objEmailBE.eM_RuleName;
                  objEvM.eM_Subject = _objEmailBE.eM_Subject;
                  objEvM.eM_CommonEmailBody = _objEmailBE.eM_CommonEmailBody;
                  objEvM.eM_HostEmailBody = _objEmailBE.eM_HostEmailBody;
                  objEvM.eM_VisitorEmailBody = _objEmailBE.eM_VisitorEmailBody;
                  objEvM.eM_Priority = _objEmailBE.eM_Priority;
                  objEvM.eM_Reciepients = _objEmailBE.eM_Reciepients;
                  objEvM.AddedBy = Convert.ToInt32(_objEmailBE.AddedBy);
                  objEvM.AddedOn = DateTime.Now;

                  objEvM.LastModifiedOn = DateTime.Now;
                  objEvM.LastModifiedBy = Convert.ToInt32(_objEmailBE.LastModifiedBy);
                  objEmailEntity.SaveChanges();

                  return objEvM.eM_Id;
              }
              catch
              {
                  return 0;
              }
          }

          #endregion


          //public long Update_EmailManagerDetails(EmailBE.EmailEventManagerBE _objEmailBE)
          //{
          //    try
          //    {
          //        Event_Manager objEvM = objEmailEntity.Event_Manager.Where(x => x.Event_Id == _objEmailBE.Event_Id).FirstOrDefault();
          //        objEvM.EventName = _objEmailBE.EventName;
          //        objEvM.eM_ModuleId = _objEmailBE.eM_ModuleId;
          //        objEvM.eM_SectionID = _objEmailBE.eM_SectionID;
          //        objEvM.eM_MainModuleId = _objEmailBE.eM_MainModuleId;
          //        objEvM.eM_FormId = _objEmailBE.eM_FormId;
          //        objEvM.eM_Event_Id = _objEmailBE.eM_Event_Id;
          //        objEvM.eM_CompanyId = _objEmailBE.eM_CompanyId;
          //        objEvM.eM_LocationId = _objEmailBE.eM_LocationId;
          //        objEvM.EmployeeBody = _objEmailBE.EmployeeBody;
          //        objEvM.AdminBody = _objEmailBE.AdminBody;
          //        objEvM.ReportingToBody = _objEmailBE.ReportingToBody;
          //        objEvM.RuleName = _objEmailBE.RuleName;
          //        objEvM.LastModifiedBy = Convert.ToInt32(_objEmailBE.LastModifiedBy);
          //        objEvM.LastModifiedOn = DateTime.Now;
          //        objEmailEntity.SaveChanges();
          //        return objEvM.Event_Id;
          //    }
          //    catch
          //    {
          //        return 0;
          //    }
          //}

          // event manager  Sub module filling

          public DataTable getmoduleDetails()
          {
              string module_Detail = "select Id, ModuleName from [EINS_RBMS].[dbo].[ModuleDetails] where status=1";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }

          // event manager MAin Module filling
          public DataTable getSubModule(int mainmoduleId)
          {
              string str = " select SectionID,[SectionName] from [EINS_RBMS].[dbo].[SectionMaster] where SectionHeadId='" + mainmoduleId + "' and EmailSmsStatus=1";
              DataTable dt = _objCommDL.executeSelectQuery(str);
              return dt;
          }

          // event manager Section filling

          public DataTable getSectionDetais(int moduleId)
          {
              string module_Detail = "select SectionID,[SectionName] from EINS_RBMS.[dbo].[SectionMaster] where SectionHeadId='" + moduleId + "' and EmailSmsStatus=1";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }
          // Get Module Details.................!!!!
          public DataTable getModulDetais()
          {
              string module_Detail = "Select ModuleID,ModuleName from [EINS_RBMS_EMAIL].[dbo].[Modules] where [status]=1";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
             return dt;
          }
          //Get Form Details.....................!!!
          public DataTable getFormsDetais(int ModuleID)
          {
              string module_Detail = "select FormName,FormID from [EINS_RBMS_EMAIL].[dbo].[Forms] where [ModuleID]='" + ModuleID + "'";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }
          // Get Events Details..................!!!
          public DataTable getEventsDetais(int formId)
          {
              //string module_Detail = "select [EventsID],[EventsName] from EINSPRO_EMAIL.[dbo].[Events]  where (FormID like '6,%' or FormID like '6%,' or FormID like '%,6,%' or  FormID ='6')";
              string module_Detail = "select [EventsID],[EventsName] from [EINS_RBMS_EMAIL].[dbo].[Events] where (FormID like '" + formId + ",%' or FormID like '%," + formId + "' or FormID like '%," + formId + ",%' or  FormID ='" + formId + "')";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }






          // event manager Forms filling

          public DataTable getFormDetais(int SectionID)
          {
              string module_Detail = "select SubsectionName,SubsectionID from EINS_RBMS.[dbo].[SubSectionMaster] where EmailSmsStatus=1 and SectionID='" + SectionID + "'";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }

          // event manager grid filling
          public DataTable getEventmanagerDetails()
          {
              string EventManager_Detail = "select E.Event_Id,E.EventName,C.CompanyName as Company ,L.LocationName as Location from EINS_RBMS_EMAIL.[dbo].[Event_Manager] E join EINS_RBMS.[dbo].[CompanyMaster] C on (E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (E.eM_LocationId=L.LocationId) where E.status=1";
              DataTable dt = _objCommDL.executeSelectQuery(EventManager_Detail);
              return dt;
          }

          public DataTable getSearchEventmanagerDetails()
          {
              string EventManager_Detail = "select E.Event_Id,E.EventName,C.CompanyName as Company ,L.LocationName as Location from EINS_RBMS_EMAIL.[dbo].[Event_Manager] E join EINS_RBMS.[dbo].[CompanyMaster] C on (E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[Locationmaster] L on (E.eM_LocationId=L.LocationId)";
              DataTable dt = _objCommDL.executeSelectQuery(EventManager_Detail);
              return dt;
          }

          public DataTable geteventmanagementsearchdetailbyID(int Event_id)
          {
              //string eventmanagersearchdetailbyid = "select  EventName,eM_ModuleId,eM_SectionID,eM_FormId,eM_Event_Id,eM_CompanyId,eM_LocationId,EmployeeBody,ReportingToBody,AdminBody,RuleName from EINSPRO_EMAIL.[dbo].[EventManager] where Event_Id='" + Event_id + "' ";

              string eventmanagersearchdetailbyid1 = "select eM_CompanyId,eM_LocationId,[eM_ModuleId],[eM_Reciepients],[eM_Subject],[eM_FormId], [eM_Events_Id],[eM_RuleName],[eM_CommonEmailBody],[eM_HostEmailBody],[eM_VisitorEmailBody],eM_Priority from EINS_RBMS_EMAIL.[dbo].[EventManager] where [eM_Id]='" + Event_id + "'";
              DataTable dt = _objCommDL.executeSelectQuery(eventmanagersearchdetailbyid1);
              return dt;
          }

          public int deleteEmailEventDetailbyId(int emialId)
          {
              string strdelete = "delete from EINS_RBMS_EMAIL.[dbo].[EventManager] where [eM_Id]='" + emialId + "'";
              int result = _objCommDL.executeScalarQuery(strdelete);
              return result;
          }


          //event id on form
          public DataTable getEventDetais(int formId)
          {
              //string module_Detail = "select [EventsID],[EventsName] from EINSPRO_EMAIL.[dbo].[Events]  where (FormID like '6,%' or FormID like '6%,' or FormID like '%,6,%' or  FormID ='6')";
              string module_Detail = "select [EventsID],[EventsName] from EINS_RBMS_EMAIL.[dbo].[Events] where (FormID like '" + formId + ",%' or FormID like '%," + formId + "' or FormID like '%," + formId + ",%' or  FormID ='" + formId + "')";
              DataTable dt = _objCommDL.executeSelectQuery(module_Detail);
              return dt;
          }

          public DataTable getcolumnDetails(int SectionId, int FormId)
          {
              string strgetcolumn = "select [ColumnID],[ColumnName] from EINS_RBMS.[dbo].[Sectionmaster] S join EINS_RBMS.[dbo].[SubSectionMaster] F on (f.SectionId=s.SectionID)join EINS_RBMS_EMAIL.[dbo].[Columns] C on (S.SectionID= c.Sectionwise_column)  where S.SectionID='" + SectionId + "' and C.FormID='" + FormId + "'";
              DataTable dt = _objCommDL.executeSelectQuery(strgetcolumn);
              return dt;
          }

          public DataTable ShowCompLocwisedata(int CompanyID, int LocationID)
          {
              string strshowdata = "select E.[eM_Id] as Event_Id,C.CompanyName as Company ,L.LocationName as Location,E.[eM_Subject] As EventName from [EINS_RBMS_EMAIL].[dbo].[EventManager] E " +
                                 " join EINS_RBMS.[dbo].[CompanyMaster] C on (c.Status=1 and E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (L.LocationStatus=1 and E.eM_LocationId=L.LocationId) " +
                                 " where E.eM_CompanyId=" + CompanyID + " and E.eM_LocationId=" + LocationID + "";
              DataTable dtshowdata = _objCommDL.executeSelectQuery(strshowdata);
              return dtshowdata;
          }

          public DataTable showeventwisedata(int Company_ID, int Location_ID, string EventName)
          {
              //string strdata = "select  E.Event_Id,C.CompanyName as Company ,L.LocationName as Location,E.EventName As EventName from  [EINS_RBMS_EMAIL].[dbo].[Event_Manager] E join EINS_RBMS.[dbo].[CompanyMaster] C on (c.Status=1 and E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (L.LocationStatus=1 and E.eM_LocationId=L.LocationId)" +
              //        " where E.eM_CompanyId=" + Company_ID + " and E.eM_LocationId=" + Location_ID + " and E.EventName like '%" + EventName + "%' and E.status=1";

              string strdata = "select E.[eM_Id] as Event_Id ,C.CompanyName as Company ,L.LocationName as Location,E.[eM_Subject] As EventName from [EINS_RBMS_EMAIL].[dbo].[EventManager] E join EINS_RBMS.[dbo].[CompanyMaster] C on (c.Status=1 and E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (L.LocationStatus=1 and E.eM_LocationId=L.LocationId) where E.eM_CompanyId=" + Company_ID + " and E.eM_LocationId=" + Location_ID + " and E.[eM_Subject] like '%" + EventName + "%' ";
              DataTable dt = _objCommDL.executeSelectQuery(strdata);
              return dt;
          }

          public DataTable ShowAlldata()
          {
              string showall = "  select  E.eM_Id as  Event_Id,C.CompanyName as Company ,L.LocationName as Location,E.eM_Subject As EventName from [EINS_RBMS_EMAIL].[dbo].[EventManager] E  join EINS_RBMS .[dbo].[CompanyMaster] C on (c.Status=1 and E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (L.LocationStatus=1 and E.eM_LocationId=L.LocationId) ";
              DataTable dtall = _objCommDL.executeSelectQuery(showall);
              return dtall;
          }

          public DataTable Showallcompwise(int Company_ID)
          {
              string strcomp = "  select  E.Event_Id,C.CompanyName as Company ,L.LocationName as Location,E.EventName As EventName from  EINS_RBMS_EMAIL.[dbo].[Event_Manager] E " +
                             "  join EINS_RBMS.[dbo].[CompanyMaster] C on (E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (E.eM_LocationId=L.LocationId) " +
                             "  where E.eM_CompanyId=" + Company_ID + " and E.status=1";
              DataTable dtcomp = _objCommDL.executeSelectQuery(strcomp);
              return dtcomp;
          }
          public DataTable Showalllocwise(int Location_ID)
          {
              string strloc = " select  E.Event_Id,C.CompanyName as Company ,L.LocationName as Location,E.EventName As EventName from EINS_RBMS_EMAIL.[dbo].[Event_Manager] E " +
                             " join EINS_RBMS .[dbo].[CompanyMaster] C on (E.eM_CompanyId=c.CompanyId) join EINS_RBMS.[dbo].[LocationMaster] L on (E.eM_LocationId=L.LocationId) " +
                             " where E.eM_LocationId=" + Location_ID + " and E.status=1";
              DataTable dtloc = _objCommDL.executeSelectQuery(strloc);
              return dtloc;
          }
          //public DataTable getFieldDetailsOnId(int formId,int sectionId)
          //{
          //    string fieldDetails = "select [ColumnName] from EINSPRO_EMAIL.[dbo].[Columns] where [FormID] ='"++"' and  [SectionID]='"++"'";

          //}

      }
      public class MailStatusReportDL
      {
          CommonDL _objComDL = new CommonDL();
          EmailBE.EmailConfigurationBE _objEmailBE = new EmailBE.EmailConfigurationBE();
          EINS_RBMS_EMAILEntities _ObjEmailEntity = new EINS_RBMS_EMAILEntities();
          #region
          public DataTable getdtSend_EmailDetails(DateTime Fromdate,DateTime ToDate)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CONVERT(DATE,E.[AddedOn])>=CONVERT(DATE,'" + Fromdate + "') AND CONVERT(DATE,E.[AddedOn])<=CONVERT(DATE,'" + ToDate + "') order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsWithTime(DateTime Fromdate, DateTime ToDate, DateTime fromTime, DateTime ToTime)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CONVERT(DATE,E.[AddedOn])>=CONVERT(DATE,'" + Fromdate + "') AND CONVERT(DATE,E.[AddedOn])<=CONVERT(DATE,'" + ToDate + "') AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsWithTimeWithStatus(DateTime Fromdate, DateTime ToDate, DateTime fromTime, DateTime ToTime, bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CONVERT(DATE,E.[AddedOn])>=CONVERT(DATE,'" + Fromdate + "') AND CONVERT(DATE,E.[AddedOn])<=CONVERT(DATE,'" + ToDate + "') AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' AND E.bE_EmailStatus='" + status + "' order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsWithstatus(DateTime Fromdate, DateTime ToDate, bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CONVERT(DATE,E.[AddedOn])>=CONVERT(DATE,'" + Fromdate + "') AND CONVERT(DATE,E.[AddedOn])<=CONVERT(DATE,'" + ToDate + "') AND E.bE_EmailStatus='" + status + "' order by [bE_To],bE_Id DESC");
              return dt;
          }
          #endregion
          #region
          public DataTable getdtSend_EmailDetailsByMonth(int Month, int year)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND MONTH(E.[AddedOn])='" + Month + "' AND YEAR(E.[AddedOn])='" + year + "' order by [bE_To],bE_Id DESC ");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsByMonthWihTime(int Month, int year,DateTime fromTime,DateTime ToTime)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND MONTH(E.[AddedOn])='" + Month + "' AND YEAR(E.[AddedOn])='" + year + "' AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsByMonthWihTimeWithStatus(int Month, int year, DateTime fromTime, DateTime ToTime, bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND MONTH(E.[AddedOn])='" + Month + "' AND YEAR(E.[AddedOn])='" + year + "' AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' AND E.bE_EmailStatus='" + status + "'  order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsByMonthWithstatus(int Month, int year,bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND MONTH(E.[AddedOn])='" + Month + "' AND YEAR(E.[AddedOn])='" + year + "' AND E.bE_EmailStatus='" + status + "'  order by [bE_To],bE_Id DESC");
              return dt;
          }
          #endregion
         
          public DataTable getdtSend_EmailDetailsWihTimeWithStatusALL( DateTime fromTime, DateTime ToTime, bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' AND E.bE_EmailStatus='" + status + "'  order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsWithstatusALL( bool status)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND E.bE_EmailStatus='" + status + "'  order by [bE_To],bE_Id DESC");
              return dt;
          }
          public DataTable getdtSend_EmailDetailsWihTimeALL(DateTime fromTime, DateTime ToTime)
          {
              DataTable dt = new DataTable();
              dt = _objComDL.executeSelectQuery("  SELECT [bE_Id],[bE_To],[bE_EmailStatus],[bE_Subject],Convert(DATE,E.AddedOn)AS AddedOn,CONVERT(varchar, CAST(E.[AddedOn] as time(0)), 100) as Time ,[eC_UserName] FROM [EINS_RBMS_EMAIL].[dbo].[BroadcastEmail] E INNER  JOIN [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] EL ON E.[bE_From]=EL.[eC_Id] WHERE [bE_Isdeleted]=0 AND CAST(E.[AddedOn] as time)>='" + fromTime + "' AND CAST(E.[AddedOn] as time)<='" + ToTime + "' order by [bE_To],bE_Id DESC");
              return dt;
          }
      }
    }
}
