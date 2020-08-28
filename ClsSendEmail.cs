using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace RBDL
{
    public class ClsSendEmail
    {
        CommonDL dbcon = new CommonDL();



        public long VisitorId = 0;
        /// <summary>
        ///  Email Sending Funtion  /// 
        /// </summary>
        public void SendMessage(long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname)
        {
            //if (CompanyID == 0 && LocationID == 0)
            //{
            //    DataTable dtGetAdmin = new DataTable();
            //    dtGetAdmin = dbcon.executeSelectQuery("select EmpCompanyid,EmpLocationid from [dbo].[EmployeeMaster] where EmpID=" + EINS_VMS_2013_COMMON.clsUserData.strEmployeeId + " and status=1");
            //    if (dtGetAdmin != null && dtGetAdmin.Rows.Count > 0)
            //    {
            //        CompanyID = Convert.ToInt64(dtGetAdmin.Rows[0][0]);
            //        LocationID = Convert.ToInt64(dtGetAdmin.Rows[0][1]);
            //    }
            //}
            DataTable dtCheckConfig = new DataTable();
            dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='" + LocationID + "' and [eC_IsActive]=1 ");
            if (dtCheckConfig == null || dtCheckConfig.Rows.Count == 0)
            {
                dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0' and [eC_IsActive]=1 ");
                if (dtCheckConfig == null || dtCheckConfig.Rows.Count == 0)
                {
                    dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0' and [eC_IsActive]=1 ");
                }
            }

            if (dtCheckConfig != null && dtCheckConfig.Rows.Count > 0)
            {
                string ToEmailId = string.Empty;
                string body = string.Empty;
                DataTable dtMessage = new DataTable();
                dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='" + LocationID + "'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                    if (dtMessage == null || dtMessage.Rows.Count == 0)
                    {
                        dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                    }
                }
                RichTextBox rtb = new RichTextBox();
                DataTable dtType = new DataTable();
                dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_EMAIL.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                if (dtType != null && dtType.Rows.Count > 0)
                {
                    if (dtType.Rows[0]["MessageBox1"].ToString() == "True")
                    {
                        if (dtMessage != null && dtMessage.Rows.Count > 0)
                        {
                            if (dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString() == string.Empty)
                            {
                                body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                ToEmailId = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                {
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (VisitorId == 0)
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                    }
                                    else
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");                           
                                    }
                                }
                            }
                            else
                            {
                                body = dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                ToEmailId = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                {
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    } if (VisitorId == 0)
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                    }
                                    else
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                    }
                                }
                            }
                        }
                    }
                    if (dtType.Rows[0]["MessageBox2"].ToString() == "True")
                    {
                        if (dtMessage != null && dtMessage.Rows.Count > 0)
                        {
                            if (dtMessage.Rows[0]["eM_HostEmailBody"].ToString() == string.Empty)
                            {
                                body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                ToEmailId = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                {
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (VisitorId == 0)
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                    }
                                    else
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                    }
                                }
                            }
                            else
                            {
                                body = dtMessage.Rows[0]["eM_HostEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                ToEmailId = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                {
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (VisitorId == 0)
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                    }
                                    else
                                    {
                                        if (body != string.Empty)
                                            dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                    }
                                }
                            }
                        }
                    }
                    if (dtType.Rows[0]["MessageBox3"].ToString() == "True")
                    {

                        if (dtMessage != null && dtMessage.Rows.Count > 0)
                        {
                            ToEmailId = dtMessage.Rows[0]["eM_Reciepients"].ToString();
                            string[] array = ToEmailId.Split(',');
                            string CommonsCond = "where " + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + " in " + "(" + ToEmailId + ")";
                            DataTable dt = new DataTable();
                            dt = dbcon.executeSelectQuery("select * from EINS_RBMS_EMAIL.[dbo].Columns where ColumnName='" + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + "' and FormID='" + FormId + "' ");
                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (array.Length != 0)
                                {
                                    if (dtMessage != null && dtMessage.Rows.Count > 0)
                                    {
                                        if (dtMessage.Rows[0]["eM_CommonEmailBody"].ToString() != string.Empty)
                                        {
                                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                                            ToEmailId = body.Split('`')[1].ToString();
                                            array = ToEmailId.Split(',');
                                            for (int i = 0; i < array.Length; i++)
                                            {
                                                DataTable dtGetEmpEmail = new DataTable();
                                                dtGetEmpEmail = dbcon.executeSelectQuery("select EmpEmail from [EINSVMS2013].dbo.EmployeeMaster where EmpID='" + array[i] + "'");
                                                if (dtGetEmpEmail != null && dtGetEmpEmail.Rows.Count > 0)
                                                {
                                                    array[i].Insert(i, dtGetEmpEmail.Rows[0]["EmpEmail"].ToString());
                                                }
                                            }
                                        }
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        for (int i = 0; i < array.Length; i++)
                                        {
                                            if (array[i] != string.Empty)
                                            {
                                                if (VisitorId == 0)
                                                {
                                                    if (body != string.Empty)
                                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                                }
                                                else
                                                {
                                                    if (body != string.Empty)
                                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {

                                body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                                //ToEmailId = body.Split('`')[1].ToString();
                                ////array = ToEmailId.Split(',');
                                body = body.Split('`')[0].ToString();
                                if (body.Contains("'"))
                                {
                                    body = body.Replace("'", "''");
                                }
                                for (int i = 0; i < array.Length; i++)
                                {
                                    string email = "";
                                    DataTable dtGetEmpEmail = new DataTable();
                                    dtGetEmpEmail = dbcon.executeSelectQuery("select Id,EmailID from [EINS_RBMS].[dbo].[UserMaster] where ID='" + array[i] + "'");
                                    if (dtGetEmpEmail != null && dtGetEmpEmail.Rows.Count > 0)
                                    {
                                        //array[i].Insert(i, dtGetEmpEmail.Rows[0]["EmpEmail"].ToString());
                                        email = dtGetEmpEmail.Rows[0]["EmailID"].ToString();
                                        if (email != "")
                                        {
                                            if (VisitorId == 0)
                                            {
                                                if (body != string.Empty)
                                                    dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + email + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                            }
                                            else
                                            {
                                                if (body != string.Empty)
                                                    dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + email + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                            }
                                        }
                                    }
                                }
                                //for (int i = 0; i < array.Length; i++)
                                //{
                                //    if (array[i] != string.Empty)
                                //    {
                                //        dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0)");
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Overload for visitors and host
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="FormId"></param>
        /// <param name="Event"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LocationID"></param>
        /// <param name="cond"></param>
        /// <param name="Visitor"></param>
        /// <param name="Host"></param>
        public void SendMessage(long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname, bool Visitor, bool Host)
        {
            DataTable dtCheckConfig = new DataTable();
            dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='" + LocationID + "' and [eC_IsActive]=1 ");
            if (dtCheckConfig == null || dtCheckConfig.Rows.Count == 0)
            {
                dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0' and [eC_IsActive]=1 ");
                if (dtCheckConfig == null || dtCheckConfig.Rows.Count == 0)
                {
                    dtCheckConfig = dbcon.executeSelectQuery("  select [eC_SMTPServerName],[eM_Id] from [EINS_RBMS_EMAIL].[dbo].[EventManager] em left join [EINS_RBMS_EMAIL].[dbo].[EmailConfiguration] ec on  em.[eM_ConfigId]=ec.[eC_Id] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0' and [eC_IsActive]=1 ");
                }
            }

            if (dtCheckConfig != null && dtCheckConfig.Rows.Count > 0)
            {
                string ToEmailId = string.Empty;
                string body = string.Empty;
                DataTable dtMessage = new DataTable();
                dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='" + LocationID + "'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                    if (dtMessage == null || dtMessage.Rows.Count == 0)
                    {
                        dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                    }
                }
                RichTextBox rtb = new RichTextBox();
                DataTable dtType = new DataTable();
                dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_EMAIL.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                if (dtType != null && dtType.Rows.Count > 0)
                {
                    if (dtType.Rows[0]["MessageBox1"].ToString() == "True")
                    {

                        if (Visitor == true)
                        {
                            if (dtMessage != null && dtMessage.Rows.Count > 0)
                            {
                                if (dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString() != string.Empty)
                                {
                                    body = dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                    ToEmailId = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        if (VisitorId == 0)
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                        }
                                        else
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                        }
                                    }
                                }
                                else
                                {
                                    body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                    ToEmailId = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        if (VisitorId == 0)
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                        }
                                        else
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                        }
                                    }
                                }
                            }
                        }

                    }
                    if (dtType.Rows[0]["MessageBox2"].ToString() == "True")
                    {
                        if (Host == true)
                        {
                            if (dtMessage != null && dtMessage.Rows.Count > 0)
                            {
                                if (dtMessage.Rows[0]["eM_HostEmailBody"].ToString() != string.Empty)
                                {
                                    body = dtMessage.Rows[0]["eM_HostEmailBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                    ToEmailId = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        if (VisitorId == 0)
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                        }
                                        else
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                        }
                                    }
                                }
                                else
                                {
                                    body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                    ToEmailId = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        if (VisitorId == 0)
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                        }
                                        else
                                        {
                                            if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                        }
                                    }
                                }
                            }
                        }

                    }
                    if (dtType.Rows[0]["MessageBox3"].ToString() == "True")
                    {
                        if (dtMessage != null && dtMessage.Rows.Count > 0)
                        {
                            ToEmailId = dtMessage.Rows[0]["eM_Reciepients"].ToString();
                            string[] array = ToEmailId.Split(',');
                            string CommonsCond = "where " + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + " in " + "(" + ToEmailId + ")";
                            DataTable dt = new DataTable();
                            dt = dbcon.executeSelectQuery("select * from Columns where EINS_RBMS_EMAIL.[dbo].ColumnName='" + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + "' and FormID='" + FormId + "' ");
                            if (dt != null && dt.Rows.Count > 0)
                            {

                                if (array.Length != 0)
                                {
                                    if (dtMessage != null && dtMessage.Rows.Count > 0)
                                    {
                                        if (dtMessage.Rows[0]["eM_CommonEmailBody"].ToString() != string.Empty)
                                        {
                                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), CommonsCond);
                                            ToEmailId = body.Split('`')[1].ToString();
                                            array = ToEmailId.Split(',');
                                            for (int i = 0; i < array.Length; i++)
                                            {
                                                DataTable dtGetEmpEmail = new DataTable();
                                                dtGetEmpEmail = dbcon.executeSelectQuery("select EmpEmail from [EINSVMS2013].dbo.EmployeeMaster where EmpID='" + array[i] + "'");
                                                if (dtGetEmpEmail != null && dtGetEmpEmail.Rows.Count > 0)
                                                {
                                                    array[i].Insert(i, dtGetEmpEmail.Rows[0]["EmpEmail"].ToString());
                                                }
                                            }
                                        }
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        for (int i = 0; i < array.Length; i++)
                                        {
                                            if (array[i] != "")
                                            {
                                                if (VisitorId == 0)
                                                {
                                                    if (body != string.Empty)
                                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                                }
                                                else
                                                {
                                                    if (body != string.Empty)
                                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                                //ToEmailId = body.Split('`')[1].ToString();
                                //array = ToEmailId.Split(',');
                                body = body.Split('`')[0].ToString();
                                if (body.Contains("'"))
                                {
                                    body = body.Replace("'", "''");
                                }

                                for (int i = 0; i < array.Length; i++)
                                {
                                    string email = "";
                                    DataTable dtGetEmpEmail = new DataTable();
                                    dtGetEmpEmail = dbcon.executeSelectQuery("select EmpEmail from [EINSVMS2013].dbo.EmployeeMaster where EmpID='" + array[i] + "'");
                                    if (dtGetEmpEmail != null && dtGetEmpEmail.Rows.Count > 0)
                                    {
                                        email = dtGetEmpEmail.Rows[0]["EmpEmail"].ToString();
                                        if (email != "")
                                        {
                                            if (VisitorId == 0)
                                            {
                                                if (body != string.Empty)
                                                    dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + email + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'')");
                                            }
                                            else
                                            {
                                                if (body != string.Empty)
                                                    dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + email + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0,'" + FormId + "^" + VisitorId + "')");
                                            }
                                        }
                                    }
                                }
                                //for (int i = 0; i < array.Length; i++)
                                //{
                                //    if (array[i] == "")
                                //    {
                                //        dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0)");
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
        }

        ///SendAppointNotification
        ///
        public void SendAppointmentNotification(long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname, bool Visitor, bool Host, DateTime FromTime, bool RepeatOnce, int RepeatOnceMinutes, bool RepeatTwice, int RepeatTwiceMinutes, bool RepeatThrice, int RepeatThriceMinutes, string Type,long AppointmentId)
        {
            string ToEmailId = string.Empty;
            string body = string.Empty;
            DataTable dtMessage = new DataTable();
            dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='" + LocationID + "'");
            if (dtMessage == null || dtMessage.Rows.Count == 0)
            {
                dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select eM_Subject,eM_CommonEmailBody,eM_HostEmailBody,eM_VisitorEmailBody,eM_ConfigId,eM_Subject,eM_Reciepients from [EINS_RBMS_EMAIL].[dbo].[EventManager] where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                }                
            }
          //  DeleteAppointmentNotification(AppointmentId);
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                if (Type == "Both")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EMAIL.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        ///////////////////////Host
                        if (dtMessage.Rows[0]["eM_HostEmailBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["eM_HostEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }

                        //////////////////////Visitor
                        if (dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                    }
                    //////////////////////
                }
                else if (Type == "Visitor")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_EMAIL.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        if (dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["eM_VisitorEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime= FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                    }
                }
                else if (Type == "Contact Person")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_EMAIL.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        ///////////////////////Host
                        if (dtMessage.Rows[0]["eM_HostEmailBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["eM_HostEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["eM_CommonEmailBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToEmailId = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToEmailId != "" && ToEmailId != "NA" && ToEmailId != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body.Contains("'"))
                                    {
                                        body = body.Replace("'", "''");
                                    }
                                    if (body != string.Empty)
                                        dbcon.executeScalarQuery("insert into EINS_RBMS_EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToEmailId + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ",'')");

                                }
                            }
                        }
                    }
                    //////////////////////
                }
            }

        }

        ///Delete Notification
        ///
        public void DeleteAppointmentNotification(long AppointmentId)
        {
            if (AppointmentId != 0)
            {
                dbcon.executeScalarQuery("delete from EINS_RBMS_EMAIL.[dbo].BroadcastEmail where AppointmentId=" + AppointmentId + " and bE_EmailStatus='False'");
            }
        }

        /// <summary>
        /// /// Replace Function ////
        /// </summary>
        /// <param name="body"></param>
        /// <param name="ModuleID"></param>
        /// <param name="FormId"></param>
        /// <param name="Event"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LocationID"></param>
        /// <param name="cond"></param>
        /// <param name="strEmailId"></param>
        /// <param name="CommonsCond"></param>
        /// <returns></returns>
        public string ReplaceTemplateDataFields(string body, long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname, string strEmailId, string CommonsCond)
        {
            string[] strArr = null;
            RichTextBox rtb = new RichTextBox();
            strArr = null;
            rtb.Text = body;
            int pos_open, pos_close;
            pos_close = 0;
            pos_open = 0;
            bool exit = true;
            do
            {
                if (rtb.Find("[", pos_open, RichTextBoxFinds.None) >= 0)
                {
                    pos_open = rtb.Find("[", pos_open, RichTextBoxFinds.None);
                    pos_close = rtb.Find("]", pos_open, RichTextBoxFinds.None);
                    if (pos_close < 0)
                    {
                        pos_close = pos_open + 1;
                    }
                    if (strArr == null)
                    {
                        Array.Resize(ref strArr, 1);
                        strArr[strArr.Length - 1] = rtb.Text.Substring(pos_open + 1, pos_close - pos_open - 1);
                    }
                    else
                    {
                        if (!strArr.Contains(rtb.Text.Substring(pos_open + 1, pos_close - pos_open - 1)))
                        {
                            Array.Resize(ref strArr, strArr.Length + 1);
                            strArr[strArr.Length - 1] = rtb.Text.Substring(pos_open + 1, pos_close - pos_open - 1);

                        }
                    }

                 //   strArr[strArr.Length - 1] = rtb.Text.Substring(pos_open + 1, pos_close - pos_open - 1);

                    pos_open = pos_close;
                }
                else
                {
                    exit = false;
                }

            } while (exit == true);

            if (strArr == null)
            {
                Array.Resize(ref strArr, 1);
                strArr[0] = "";
            }
            do
            {
                if (rtb.Find("[", pos_open, RichTextBoxFinds.None) >= 0)
                {
                    pos_open = rtb.Find("[", pos_open, RichTextBoxFinds.None);
                    pos_close = rtb.Find("]", pos_open, RichTextBoxFinds.None);
                    if (pos_close < 0)
                    {
                        pos_close = pos_open + 1;
                    }
                    if (strArr == null)
                    {
                        Array.Resize(ref strArr, 1);
                    }
                    else
                    {
                        Array.Resize(ref strArr, strArr.Length + 1);
                    }

                    strArr[strArr.Length - 1] = rtb.Text.Substring(pos_open + 1, pos_close - pos_open - 1);

                    pos_open = pos_close;
                }
                else
                {
                    exit = false;
                }

            } while (exit == true);

            if (strArr == null)
            {
                Array.Resize(ref strArr, 1);
                strArr[0] = "";
            }

            //return strArr;





            rtb.Text = body;
            pos_open = 0;
            pos_close = 0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dtContacts = new DataTable();
            string emailid = "";

            if (strArr.Length <= 0)
            {

            }
            else
            {
                for (int i = 0; i < strArr.Length; i++)
                {
                    string Id = strArr[i].ToString();
                    string newField = "";
                    dt = dbcon.executeSelectQuery("select * from [EINS_RBMS_EMAIL].[dbo].[Columns]  where ColumnName='" + Id + "' and FormID='" + FormId + "' ");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                        dt2 = dbcon.executeSelectQuery("Select " + dt.Rows[0][2] + " from " + dt.Rows[0][3] + " " + cond + "");
                        if (dt2 != null && dt2.Rows.Count > 0)
                        {

                            if (rtb.Text.Contains('['))
                            {
                                pos_open = rtb.Find("[", pos_open, RichTextBoxFinds.None);
                                pos_close = rtb.Find("]", pos_open, RichTextBoxFinds.None);
                                if (pos_close < 0)
                                {
                                    pos_close = pos_open + 1;
                                }
                                // dt3 = dbcon.executeSelectQuery("");
                                
                                if (dt2.Rows[0][0].ToString() == "")
                                {
                                    newField = "NA";
                                }
                                else
                                {
                                    if (Id == "Image")
                                    {
                                        VisitorId = Convert.ToInt64(dt2.Rows[0][0].ToString());
                                        newField = "<IMG>";
                                    }
                                    else if (Id.StartsWith("-"))
                                    {
                                        if (Convert.ToDateTime(dt2.Rows[0][0].ToString()) == Convert.ToDateTime("1900-01-01 00:00:00.000"))
                                        {
                                            newField = "NA";
                                        }
                                        else
                                        {
                                            newField = Convert.ToDateTime(dt2.Rows[0][0].ToString()).ToString("dd/MM/yyyy");
                                        }
                                    }
                                    else if (Id.StartsWith(":"))
                                    {
                                        if (Convert.ToDateTime(dt2.Rows[0][0].ToString()) == Convert.ToDateTime("1900-01-01 00:00:00.000"))
                                        {
                                            newField = "NA";
                                        }
                                        else
                                        {
                                            newField = Convert.ToDateTime(dt2.Rows[0][0].ToString()).ToShortTimeString();
                                        }
                                    }
                                    //else if (Id == "1900-01-01 00:00:00.000")
                                    //{
                                    //    newField = "NA";
                                    //}
                                    else
                                    {
                                        newField = dt2.Rows[0][0].ToString();
                                    }
                                }
                                //strArr[i] = strArr[i].Replace(strArr[i], newField);
                                if (strArr[i] != "")
                                    strArr[i] = strArr[i].Replace(strArr[i], newField);
                                else
                                    strArr[i] = newField;
                                rtb.Text = rtb.Text.Replace(rtb.Text.Substring(pos_open, pos_close - pos_open + 1), strArr[i]);
                            }
                            body = rtb.Text;


                        }
                        else
                        {
                            if (rtb.Text.Contains('['))
                            {
                                pos_open = rtb.Find("[", pos_open, RichTextBoxFinds.None);
                                pos_close = rtb.Find("]", pos_open, RichTextBoxFinds.None);
                                if (pos_close < 0)
                                {
                                    pos_close = pos_open + 1;
                                }
                                // dt3 = dbcon.executeSelectQuery("");
                                newField = "NA";
                              //  strArr[i] = strArr[i].Replace(strArr[i], newField);
                                if (strArr[i] != "")
                                    strArr[i] = strArr[i].Replace(strArr[i], newField);
                                else
                                    strArr[i] = newField;
                                rtb.Text = rtb.Text.Replace(rtb.Text.Substring(pos_open, pos_close - pos_open + 1), strArr[i]);
                            }
                            body = rtb.Text;
                        }
                    }
                    else
                    {
                        if (rtb.Text.Contains('['))
                        {
                            pos_open = rtb.Find("[", pos_open, RichTextBoxFinds.None);
                            pos_close = rtb.Find("]", pos_open, RichTextBoxFinds.None);
                            if (pos_close < 0)
                            {
                                pos_close = pos_open + 1;
                            }
                            // dt3 = dbcon.executeSelectQuery("");
                            newField = "NA";
                           // strArr[i] = strArr[i].Replace(strArr[i], newField);
                            if (strArr[i] != "")
                                strArr[i] = strArr[i].Replace(strArr[i], newField);
                            else
                                strArr[i] = newField;
                            rtb.Text = rtb.Text.Replace(rtb.Text.Substring(pos_open, pos_close - pos_open + 1), strArr[i]);
                        }
                        body = rtb.Text;
                    }
                }
            }
            string Commonemail = "";
            if (CommonsCond == "")
            {
                dtContacts = dbcon.executeSelectQuery("Select " + strEmailId + " from " + TableOrViewname + " " + cond + " ");
                if (dtContacts != null && dtContacts.Rows.Count > 0)
                {
                    emailid = dtContacts.Rows[0][0].ToString();
                }
            }
            else
            {


                DataTable dtGetContactQuery = new DataTable();
                dtGetContactQuery = dbcon.executeSelectQuery("Select tS_Query from EINS_RBMS_EMAIL.dbo.TableStructure where tS_FormName='Email Sending' and tS_ControlName='Email Sending Function' and DataBaseName='EINSVMS2013'");
                if (dtGetContactQuery != null && dtGetContactQuery.Rows.Count > 0)
                {
                    DataTable dtCommomContacts = new DataTable();
                    dtCommomContacts = dbcon.executeSelectQuery(dtGetContactQuery.Rows[0]["tS_Query"] + " " + CommonsCond + " ");
                    if (dtCommomContacts != null && dtCommomContacts.Rows.Count > 0)
                    {
                        List<string> emailarray = new List<string>();
                        if (CommonsCond != "")
                        {
                            for (int j = 0; j < dtCommomContacts.Rows.Count; j++)
                            {

                                if (Commonemail == "")
                                {
                                    Commonemail = (dtCommomContacts.Rows[j][0].ToString());
                                }
                                else
                                {
                                    Commonemail = Commonemail + "," + dtCommomContacts.Rows[j][0].ToString();
                                }
                                //emailarray[j].Insert(j, dtCommomContacts.Rows[j][0].ToString());
                            }
                        }
                    }
                }
            }

            if (Commonemail == "")
            {
                return body + "`" + emailid;
            }
            else
            {
                return body + "`" + Commonemail;
            }
        }



    }
}
