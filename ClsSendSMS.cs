using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace RBDL
{
    public class ClsSendSMS
    {
        CommonDL dbcon = new CommonDL();


        public string AdditionalContacts = "";
        public string GroupContacts = "";

        /// <summary>
        ///  Email Sending Funtion  /// 
        /// </summary>
        public void SendMessage(long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname)
        {
            string ToMobileNo = string.Empty;
            string body = string.Empty;
            DataTable dtMessage = new DataTable();
            dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "'  and eM_LocationId='" + LocationID + "'");
            if (dtMessage == null || dtMessage.Rows.Count == 0)
            {
                dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                }
            }
            RichTextBox rtb = new RichTextBox();
            DataTable dtType = new DataTable();
            dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_SMS.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
            if (dtType != null && dtType.Rows.Count > 0)
            {
                if (dtType.Rows[0]["MessageBox1"].ToString() == "True")
                {
                    if (dtMessage != null && dtMessage.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtMessage.Rows[0]["CheckForVisitor"]))
                        {
                            if (dtMessage.Rows[0]["VisitorSMSBody"].ToString() == string.Empty)
                            {
                                body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                ToMobileNo = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                {
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //   dbcon.executeScalarQuery("insert into EINS_RBMS_SMS.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                            else
                            {
                                body = dtMessage.Rows[0]["VisitorSMSBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                ToMobileNo = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                {
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //    dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "',1)");
                                    }
                                }
                            }
                        }
                    }
                }
                if (dtType.Rows[0]["MessageBox2"].ToString() == "True")
                {
                    if (dtMessage != null && dtMessage.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtMessage.Rows[0]["CheckForHost"]))
                        {
                            if (dtMessage.Rows[0]["HostSMSBody"].ToString() == string.Empty)
                            {
                                body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                ToMobileNo = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                {
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //     dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                            else
                            {
                                body = dtMessage.Rows[0]["HostSMSBody"].ToString();
                                body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                ToMobileNo = body.Split('`')[1].ToString();
                                body = body.Split('`')[0].ToString();
                                if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                {
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
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
                        AdditionalContacts = dtMessage.Rows[0]["AdditionalContact"].ToString();
                        GroupContacts = dtMessage.Rows[0]["GroupIds"].ToString();
                        ToMobileNo = dtMessage.Rows[0]["EmployeeContact"].ToString();
                        string[] array = ToMobileNo.Split(',');
                        string CommonsCond = "where " + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + " in " + "(" + ToMobileNo + ")";
                        DataTable dt = new DataTable();
                        dt = dbcon.executeSelectQuery("select * from Columns where ColumnName='" + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + "' and FormID='" + FormId + "' ");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (array.Length != 0)
                            {
                                if (dtMessage != null && dtMessage.Rows.Count > 0)
                                {
                                    if (dtMessage.Rows[0]["CommonSMSBody"].ToString() != string.Empty)
                                    {
                                        body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                        body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                                        ToMobileNo = body.Split('`')[1].ToString();
                                        array = ToMobileNo.Split(',');
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
                                            if (body != string.Empty)
                                            {
                                                
                                                // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                                if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + array[i] + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {

                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                            //ToMobileNo = body.Split('`')[1].ToString();
                            //array = ToMobileNo.Split(',');
                            body = body.Split('`')[0].ToString();
                            if (body.Contains("'"))
                            {
                                body = body.Replace("'", "''");
                            }
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (array[i] != "")
                                {
                                    if (body != string.Empty)
                                    {
                                       
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + array[i] + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "','1')");
                                    }
                                }
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
            string ToMobileNo = string.Empty;
            string body = string.Empty;
            DataTable dtMessage = new DataTable();
            dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "'  and eM_LocationId='" + LocationID + "'");
            if (dtMessage == null || dtMessage.Rows.Count == 0)
            {
                dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Events_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                }
            }
            RichTextBox rtb = new RichTextBox();
            DataTable dtType = new DataTable();
            dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_SMS.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
            if (dtType != null && dtType.Rows.Count > 0)
            {
                if (dtType.Rows[0]["MessageBox1"].ToString() == "True")
                {

                    if (Visitor == true)
                    {
                        if (dtMessage != null && dtMessage.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dtMessage.Rows[0]["CheckForVisitor"]))
                            {
                                if (dtMessage.Rows[0]["VisitorSMSBody"].ToString() != string.Empty)
                                {
                                    body = dtMessage.Rows[0]["VisitorSMSBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                    ToMobileNo = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                    {
                                        if (body != string.Empty)
                                        {
                                            if (body.Contains("'"))
                                            {
                                                body = body.Replace("'", "''");
                                            }
                                            // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                            dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                        }
                                    }
                                }
                                else
                                {
                                    body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                                    ToMobileNo = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                    {
                                        if (body != string.Empty)
                                        {
                                            if (body.Contains("'"))
                                            {
                                                body = body.Replace("'", "''");
                                            }
                                            // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                            dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                        }
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
                            if (Convert.ToBoolean(dtMessage.Rows[0]["CheckForHost"]))
                            {
                                if (dtMessage.Rows[0]["HostSMSBody"].ToString() != string.Empty)
                                {
                                    body = dtMessage.Rows[0]["HostSMSBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                    ToMobileNo = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                    {
                                        if (body != string.Empty)
                                        {
                                            if (body.Contains("'"))
                                            {
                                                body = body.Replace("'", "''");
                                            }
                                            // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                            dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                        }
                                    }
                                }
                                else
                                {
                                    body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                    body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                                    ToMobileNo = body.Split('`')[1].ToString();
                                    body = body.Split('`')[0].ToString();
                                    if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                                    {
                                        if (body != string.Empty)
                                        {
                                            if (body.Contains("'"))
                                            {
                                                body = body.Replace("'", "''");
                                            }
                                            //    dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                            dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                        }
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
                        AdditionalContacts = dtMessage.Rows[0]["AdditionalContact"].ToString();
                        GroupContacts = dtMessage.Rows[0]["GroupIds"].ToString();
                        ToMobileNo = dtMessage.Rows[0]["EmployeeContact"].ToString();
                        string[] array = ToMobileNo.Split(',');
                        string CommonsCond = "where " + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + " in " + "(" + ToMobileNo + ")";
                        DataTable dt = new DataTable();
                        dt = dbcon.executeSelectQuery("select * from Columns where ColumnName='" + dtType.Rows[0]["MessageBox3_Contacts"].ToString() + "' and FormID='" + FormId + "' ");
                        if (dt != null && dt.Rows.Count > 0)
                        {

                            if (array.Length != 0)
                            {
                                if (dtMessage != null && dtMessage.Rows.Count > 0)
                                {
                                    if (dtMessage.Rows[0]["CommonSMSBody"].ToString() != string.Empty)
                                    {
                                        body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                                        body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), CommonsCond);
                                        ToMobileNo = body.Split('`')[1].ToString();
                                        array = ToMobileNo.Split(',');
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
                                            if (body != string.Empty)
                                            {
                                                //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                                if (body != string.Empty)
                                                dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + array[i] + "'      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ,0     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][8].ToString(), CommonsCond);
                            ToMobileNo = body.Split('`')[1].ToString();
                            array = ToMobileNo.Split(',');
                            body = body.Split('`')[0].ToString();
                            if (body.Contains("'"))
                            {
                                body = body.Replace("'", "''");
                            }
                            for (int i = 0; i < array.Length; i++)
                            {

                                if (array[i] != "")
                                {
                                    if (body != string.Empty)
                                    {                                        
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + array[i] + "','False','0','','','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','',GETDATE(),'0',0,'',0)");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + array[i] + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + body + "','Pending','EventManager',0 ,0,0,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        ///SendAppointNotification
        ///
        public void SendAppointmentNotification(long ModuleID, long FormId, long Event, long CompanyID, long LocationID, string cond, string TableOrViewname, bool Visitor, bool Host, DateTime FromTime, bool RepeatOnce, int RepeatOnceMinutes, bool RepeatTwice, int RepeatTwiceMinutes, bool RepeatThrice, int RepeatThriceMinutes, string Type, long AppointmentId)
        {
            string ToMobileNo = string.Empty;
            string body = string.Empty;
            DataTable dtMessage = new DataTable();
            
            dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "'  and eM_LocationId='" + LocationID + "'");
            if (dtMessage == null || dtMessage.Rows.Count == 0)
            {
                dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='" + CompanyID + "' and eM_LocationId='0'");
                if (dtMessage == null || dtMessage.Rows.Count == 0)
                {
                    dtMessage = dbcon.executeSelectQuery("Select [CommonSMSBody],[HostSMSBody],[VisitorSMSBody],[EmployeeContact],[AdditionalContact],[GroupIds],[CheckForHost],[CheckForVisitor] from EINS_RBMS_SMS.dbo.EventManager where eM_ModuleId='" + ModuleID + "'  and eM_FormId='" + FormId + "' and eM_Event_Id='" + Event + "' and eM_CompanyId='0' and eM_LocationId='0'");
                }
            }
            //  DeleteAppointmentNotification(AppointmentId);
            if (dtMessage != null && dtMessage.Rows.Count > 0)
            {
                if (Type == "Both")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_SMS.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        ///////////////////////Host
                        if (dtMessage.Rows[0]["HostSMSBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["HostSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }

                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }

                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }

                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");

                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }

                        //////////////////////Visitor
                        if (dtMessage.Rows[0]["VisitorSMSBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["VisitorSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                    }
                    //////////////////////
                }
                else if (Type == "Visitor")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_SMS.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        if (dtMessage.Rows[0]["VisitorSMSBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["VisitorSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][6].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        // dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Type == "Contact Person")
                {
                    DataTable dtType = new DataTable();
                    dtType = dbcon.executeSelectQuery("Select * from EINS_RBMS_SMS.dbo.Forms where ModuleID=" + ModuleID + " and FormID=" + FormId + "");
                    if (dtType != null && dtType.Rows.Count > 0)
                    {
                        ///////////////////////Host
                        if (dtMessage.Rows[0]["HostSMSBody"].ToString() != string.Empty)
                        {
                            body = dtMessage.Rows[0]["HostSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                            }
                        }
                        else
                        {
                            body = dtMessage.Rows[0]["CommonSMSBody"].ToString();
                            body = ReplaceTemplateDataFields(body, ModuleID, FormId, Event, CompanyID, LocationID, cond, TableOrViewname, dtType.Rows[0][7].ToString(), "");
                            ToMobileNo = body.Split('`')[1].ToString();
                            body = body.Split('`')[0].ToString();
                            if (ToMobileNo != "" && ToMobileNo != "NA" && ToMobileNo != "Not Available")
                            {
                                if (RepeatOnce)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatOnceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatTwice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatTwiceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
                                }
                                if (RepeatThrice)
                                {
                                    DateTime BroadCastTime = FromTime.AddMinutes(-RepeatThriceMinutes);
                                    if (body != string.Empty)
                                    {
                                        if (body.Contains("'"))
                                        {
                                            body = body.Replace("'", "''");
                                        }
                                        //dbcon.executeScalarQuery("insert into EMAIL.[dbo].BroadcastEmail values('" + dtMessage.Rows[0]["eM_ConfigId"].ToString() + "','" + ToMobileNo + "','False','1','','" + DateTime.Today.Date + "','','','" + body + "','" + dtMessage.Rows[0]["eM_Subject"].ToString() + "','" + CompanyID + "','" + LocationID + "','','" + BroadCastTime + "','0',0,''," + AppointmentId + ")");
                                        dbcon.executeScalarQuery("insert into [EINS_RBMS_SMS].[dbo].[Sent] values      ('" + ToMobileNo + "'      ,'" + BroadCastTime + "'     ,'" + body + "'      ,'Pending'      ,'EventManager'      ,0      ," + AppointmentId + "     ,0      ,'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  + "')");
                                    }
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
                dbcon.executeScalarQuery("delete from [EINS_RBMS_SMS].[dbo].[Sent] where AppointmentId=" + AppointmentId+" and Message_Status='Pending'");
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
            string EmpMobile = "";

            if (strArr.Length <= 0)
            {

            }
            else
            {
                for (int i = 0; i < strArr.Length; i++)
                {
                    string Id = strArr[i].ToString();
                    string newField = "";
                    dt = dbcon.executeSelectQuery("select * from EINS_RBMS_SMS.dbo.Columns where ColumnName='" + Id + "' and FormID='" + FormId + "' ");
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
                                    if (Id.StartsWith("-"))
                                    {
                                        if (Convert.ToDateTime(dt2.Rows[0][0].ToString()) == Convert.ToDateTime("1900-01-01 00:00:00.000"))
                                        {
                                            newField = "NA";
                                        }
                                        else
                                        {
                                            newField = Convert.ToDateTime(dt2.Rows[0][0].ToString()).ToString("dd/MM/yyyy");
                                        }
                                       // newField = Convert.ToDateTime(dt2.Rows[0][0].ToString()).ToShortDateString();
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
                                        //newField = Convert.ToDateTime(dt2.Rows[0][0].ToString()).ToShortTimeString();
                                    }
                                    else
                                    {
                                        newField = dt2.Rows[0][0].ToString();
                                    }
                                }
                           //     strArr[i] = strArr[i].Replace(strArr[i], newField);
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
                             //   strArr[i] = strArr[i].Replace(strArr[i], newField);
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
            string MobileNumbers = "";
            if (CommonsCond == "")
            {
                dtContacts = dbcon.executeSelectQuery("Select " + strEmailId + " from " + TableOrViewname + " " + cond + " ");
                if (dtContacts != null && dtContacts.Rows.Count > 0)
                {
                    EmpMobile = dtContacts.Rows[0][0].ToString();
                }
            }
            else
            {


                DataTable dtGetContactQuery = new DataTable();
                dtGetContactQuery = dbcon.executeSelectQuery("Select tS_Query from EINS_RBMS_SMS.dbo.TableStructure where tS_FormName='SMS Sending' and tS_ControlName='SMS Sending Function' and DataBaseName='EINSVMS2013'");
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

                                if (MobileNumbers == "")
                                {
                                    MobileNumbers = (dtCommomContacts.Rows[j][0].ToString());
                                }
                                else
                                {
                                    if(!MobileNumbers.Contains(dtCommomContacts.Rows[j][0].ToString()))
                                        MobileNumbers = MobileNumbers + "," + dtCommomContacts.Rows[j][0].ToString(); 
                                }
                                //emailarray[j].Insert(j, dtCommomContacts.Rows[j][0].ToString());
                            }
                        }
                    }
                }
                if (AdditionalContacts != "")
                {
                    DataTable dtAdditional = new DataTable();
                    dtAdditional = dbcon.executeSelectQuery("select AdditionalContId,ContactNo from [EINS_RBMS_SMS].[dbo].[AdditionalContacts] where AdditionalContId in (" + AdditionalContacts + ")");
                    if (dtAdditional != null && dtAdditional.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtAdditional.Rows.Count; j++)
                        {

                            if (MobileNumbers == "")
                            {
                                MobileNumbers = (dtAdditional.Rows[j][1].ToString());
                            }
                            else
                            {
                                if (!MobileNumbers.Contains(dtAdditional.Rows[j][0].ToString()))
                                    MobileNumbers = MobileNumbers + "," + dtAdditional.Rows[j][1].ToString();
                            }
                            //emailarray[j].Insert(j, dtCommomContacts.Rows[j][0].ToString());
                        }
                    }
                }
                if (GroupContacts != "")
                {
                    string GroupContacts_Emp = "";
                    string GroupContacts_Add = "";
                    DataTable dtGroup = new DataTable();
                    dtGroup = dbcon.executeSelectQuery("select [GroupID],[Grp_Contacts_Emp],[Grp_Contacts_Add] from [EINS_RBMS_SMS].[dbo].[Group_Details] where GroupID in (" + GroupContacts + ")");
                    if (dtGroup != null && dtGroup.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtGroup.Rows.Count; i++)
                        {
                            if (GroupContacts_Emp == "")
                            {
                                GroupContacts_Emp = dtGroup.Rows[i]["Grp_Contacts_Emp"].ToString();
                            }
                            else
                            {
                                GroupContacts_Emp = GroupContacts_Emp+","+ dtGroup.Rows[i]["Grp_Contacts_Emp"].ToString();
                            }

                            if (GroupContacts_Add == "")
                            {
                                GroupContacts_Add = dtGroup.Rows[i]["Grp_Contacts_Add"].ToString();
                            }
                            else
                            {
                                GroupContacts_Add = GroupContacts_Add + "," + dtGroup.Rows[i]["Grp_Contacts_Add"].ToString();
                            }
                        }

                        if (GroupContacts_Emp != "")
                        {
                            DataTable dtGetQuery = new DataTable();
                            dtGetQuery = dbcon.executeSelectQuery("Select tS_Query,Select_Clause from EINS_RBMS_SMS.dbo.TableStructure where tS_FormName='SMS Sending' and tS_ControlName='SMS Sending Function' and DataBaseName='EINSVMS2013'");
                            if (dtGetQuery != null && dtGetQuery.Rows.Count > 0)
                            {
                                DataTable dtCommomContacts = new DataTable();
                                dtCommomContacts = dbcon.executeSelectQuery(dtGetQuery.Rows[0]["tS_Query"] + " Where " + dtGetQuery.Rows[0]["Select_Clause"] + " in (" + GroupContacts_Emp + ")");
                                if (dtCommomContacts != null && dtCommomContacts.Rows.Count > 0)
                                {
                                    for (int j = 0; j < dtCommomContacts.Rows.Count; j++)
                                    {

                                        if (MobileNumbers == "")
                                        {
                                            MobileNumbers = (dtCommomContacts.Rows[j][0].ToString());
                                        }
                                        else
                                        {
                                            if (!MobileNumbers.Contains(dtCommomContacts.Rows[j][0].ToString()))
                                            MobileNumbers = MobileNumbers + "," + dtCommomContacts.Rows[j][0].ToString();
                                        }
                                        //emailarray[j].Insert(j, dtCommomContacts.Rows[j][0].ToString());
                                    }
                                }
                            }
                        }
                        if (GroupContacts_Add != "")
                        {
                            DataTable dtAdditional = new DataTable();
                            dtAdditional = dbcon.executeSelectQuery("select AdditionalContId,ContactNo from [EINS_RBMS_SMS].[dbo].[AdditionalContacts] where AdditionalContId in (" + GroupContacts_Add + ")");
                            if (dtAdditional != null && dtAdditional.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtAdditional.Rows.Count; j++)
                                {

                                    if (MobileNumbers == "")
                                    {
                                        MobileNumbers = (dtAdditional.Rows[j][1].ToString());
                                    }
                                    else
                                    {
                                        if (!MobileNumbers.Contains(dtAdditional.Rows[j][1].ToString()))
                                        MobileNumbers = MobileNumbers + "," + dtAdditional.Rows[j][1].ToString();
                                    }
                                    //emailarray[j].Insert(j, dtCommomContacts.Rows[j][0].ToString());
                                }
                            }
                        }
                    }
                }
            }

            if (MobileNumbers == "")
            {
                return body + "`" + EmpMobile;
            }
            else
            {
                return body + "`" + MobileNumbers;
            }
        }


    }
}
