using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class LoginBE
    {
        public class EmployeeType
        {
            public int TypeId { get; set; }
            public string EmployeeTypeName { get; set; }
            public string TypeDescription { get; set; }
            public bool TypeStatus { get; set; }
            public bool IsPeroidic { get; set; }
            public long AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
        public class LoginDetails
        {
            public long ID { get; set; }
            public string EmployeeManualID { get; set; }
            public string Name { get; set; }
            public long? Designation { get; set; }
            public string Email { get; set; }
            public string CountryCode { get; set; }
            public string Mobile { get; set; }
            public string ExtensionNo { get; set; }
            public string UserType { get; set; }
            public string ActivationStatus { get; set; }
            public string LoginName { get; set; }
            public string Password { get; set; }
            public long Company { get; set; }
            public long Location { get; set; }
            public string DepartmentName { get; set; }
            public long Department { get; set; }
            public bool Status { get; set; }
            public int WrongAttempts { get; set; }
            public bool Locked { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
            public byte[] Photo { get; set; }

            //newly added fields

            public DateTime DOB { get; set; }
            public string Gender { get; set; }
            public string EmpAddress { get; set; }
            public long? Category { get; set; }
            public long? SubCategory { get; set; }
            public long EmployeeTypeId { get; set; }
            public string ReportingTo { get; set; }
            public string WorkStatus { get; set; }
            public DateTime? EmpJoiningDate { get; set; }
            public int? ProbationPeroid { get; set; }
            public DateTime? ConfirmationDate { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
        }
        public class LoginDetailsView : LoginDetails
        {
            public string CompanyName { get; set; }
            public string LocationName { get; set; }
            public string DepartmentName{ get; set; }
            public string DesignationName { get; set; }
            public string Categories { get; set; }
            public string SubCategories { get; set; }
            public string EmployeeTypeName { get; set; }
            public DateTime joiningdate { get; set; }
        }
        public class Employee_WeekOffBE
        {
            public long WeekOffID { get; set; }
            public long EmployeeID { get; set; }
            public string KeyWord { get; set; }
            public string Day { get; set; }
            public string DayType { get; set; }
            public bool First { get; set; }
            public bool Second { get; set; }
            public bool Third { get; set; }
            public bool Fourth { get; set; }
            public bool Fifth { get; set; }
            public bool WeekOffStatus { get; set; }
            public long AddedBy { get; set; }
            public DateTime AddedOn { get; set; }
            public long? LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }
    }
}
