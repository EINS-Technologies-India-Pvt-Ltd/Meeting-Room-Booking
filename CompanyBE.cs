using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBBE
{
    public class CompanyBE
    {
        public class CompanyMasterBE
        {
            public long CompanyID { get; set; }
            public string CompanyName { get; set; }
            public string CompanyHead { get; set; }
            public string CompanyAlias { get; set; }
            public string CompanyBuilding { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string Pincode { get; set; }
            public long CountryId { get; set; }
            public long StateId { get; set; }
            public string CompanyTelephone { get; set; }
            public string CompanyEmailid { get; set; }
            public string CompanyFax { get; set; }
            public string CompanyWebsite { get; set; }
            public byte[] CompanyLogo { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }

        public class LocationMasterBE
        {
            public long LocationID { get; set; }
            public string LocationName { get; set; }
            public string Description { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }

        public class DepartmentBE
        {
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string Description { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }

        public class DesignationBE
        {
            public long DesignationID { get; set; }
            public string DesignationName { get; set; }
            public string Description { get; set; }
            public long AddedBy { get; set; }
            public DateTime? AddedOn { get; set; }
            public long LastModifiedBy { get; set; }
            public DateTime? LastModifiedOn { get; set; }
        }

        public class CategoryBE : CommonBE
        {
            public long CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string CategoryDescription { get; set; }
            public bool CategoryStatus { get; set; }
        }
        public class SubCategoryBE : CommonBE
        {
            public long SubCategoryId { get; set; }
            public long CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string SubCategoryName { get; set; }
            public string SubCategoryDescription { get; set; }
            public bool SubCategoryStatus { get; set; }
        }
    }
}
