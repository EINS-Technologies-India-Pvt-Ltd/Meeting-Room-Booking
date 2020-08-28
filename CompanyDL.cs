using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;

namespace RBDL
{
    public class CompanyDL
    {
        EINS_RBMSEntities _ObjEINS_RBMSEntities = new EINS_RBMSEntities();

        public List<CompanyBE.CompanyMasterBE> GetCompanyDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.CompanyMasters
                      where _Obj.Status == true
                      select new CompanyBE.CompanyMasterBE
                      {
                          CompanyID = _Obj.CompanyID,
                          CompanyName = _Obj.CompanyName,
                          CompanyBuilding = _Obj.CompanyBuilding,
                          CompanyHead = _Obj.CompanyHead,
                          CompanyAlias = _Obj.CompanyAlias,
                          Street = _Obj.Street,
                          City = _Obj.City,
                          Pincode = _Obj.Pincode,
                          CountryId = _Obj.CountryId.Value,
                          StateId = _Obj.StateId.Value,
                          CompanyTelephone = _Obj.CompanyTelephone,
                          CompanyEmailid = _Obj.CompanyEmailid,
                          CompanyFax = _Obj.CompanyFax,
                          CompanyWebsite = _Obj.CompanyWebsite,
                          CompanyLogo = _Obj.CompanyLogo
                      };

            return lst.ToList();
        }
        public long GetCompanyCount()
        {
            var count = _ObjEINS_RBMSEntities.CompanyMasters.Where(x => x.Status == true).Count();
            return count;
        }
        public List<CompanyBE.CompanyMasterBE> GetCompanyDetails(long id)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.CompanyMasters
                      where _Obj.Status == true && _Obj.CompanyID == id
                      select new CompanyBE.CompanyMasterBE
                      {
                          CompanyID = _Obj.CompanyID,
                          CompanyName = _Obj.CompanyName,
                          CompanyBuilding = _Obj.CompanyBuilding,
                          CompanyHead = _Obj.CompanyHead,
                          CompanyAlias = _Obj.CompanyAlias,
                          Street = _Obj.Street,
                          City = _Obj.City,
                          Pincode = _Obj.Pincode,
                          CountryId = _Obj.CountryId.Value,
                          StateId = _Obj.StateId.Value,
                          CompanyTelephone = _Obj.CompanyTelephone,
                          CompanyEmailid = _Obj.CompanyEmailid,
                          CompanyFax = _Obj.CompanyFax,
                          CompanyWebsite = _Obj.CompanyWebsite,
                          CompanyLogo = _Obj.CompanyLogo
                      };

            return lst.ToList();
        }
        public bool IsCompanyAlreadyExist(string strCompanyName)
        {
            var count = _ObjEINS_RBMSEntities.CompanyMasters.Where(x => x.Status == true && x.CompanyName == strCompanyName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsCompanyAlreadyExistUpdate(string strCompanyName,long Id)
        {
            var count = _ObjEINS_RBMSEntities.CompanyMasters.Where(x => x.Status == true && x.CompanyID==Id && x.CompanyName == strCompanyName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<CompanyBE.LocationMasterBE> GetLocationDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.LocationMasters
                      where _Obj.LocationStatus == true
                      select new CompanyBE.LocationMasterBE
                      {
                          LocationID = _Obj.LocationId,
                          LocationName = _Obj.LocationName,
                          Description = _Obj.Description
                      };

            return lst.ToList();
        }
        public long GetLocationCount()
        {
            var count = _ObjEINS_RBMSEntities.LocationMasters.Where(x => x.LocationStatus == true).Count();
            return count;
        }
        public List<CompanyBE.LocationMasterBE> GetLocationDetails(long id)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.LocationMasters
                      where _Obj.LocationStatus == true && _Obj.LocationId == id
                      select new CompanyBE.LocationMasterBE
                      {
                          LocationID = _Obj.LocationId,
                          LocationName = _Obj.LocationName,
                          Description = _Obj.Description
                      };

            return lst.ToList();
        }
        public bool IsLocationAlreadyExist(string strName)
        {
            var count = _ObjEINS_RBMSEntities.LocationMasters.Where(x => x.LocationStatus == true && x.LocationName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsLocationAlreadyExistUpdate(string strName,long Id)
        {
            var count = _ObjEINS_RBMSEntities.LocationMasters.Where(x => x.LocationStatus == true && x.LocationId == Id && x.LocationName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<CompanyBE.DepartmentBE> GetDepartmentDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.DepartmentMasters
                      where _Obj.Status == true
                      select new CompanyBE.DepartmentBE
                      {
                          DepartmentID = _Obj.DepartmentID,
                          DepartmentName = _Obj.DepartmentName,
                          Description = _Obj.Description
                      };

            return lst.ToList();
        }
        public List<CompanyBE.DepartmentBE> GetDepartmentDetails(long id)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.DepartmentMasters
                      where _Obj.Status == true && _Obj.DepartmentID == id
                      select new CompanyBE.DepartmentBE
                      {
                          DepartmentID = _Obj.DepartmentID,
                          DepartmentName = _Obj.DepartmentName,
                          Description = _Obj.Description
                      };

            return lst.ToList();
        }
        public bool IsDepartmentAlreadyExist(string strName)
        {
            var count = _ObjEINS_RBMSEntities.DepartmentMasters.Where(x => x.Status == true && x.DepartmentName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsDepartmentAlreadyExistUpdate(string strName,long Id)
        {
            var count = _ObjEINS_RBMSEntities.DepartmentMasters.Where(x => x.Status == true && x.DepartmentID==Id && x.DepartmentName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<CompanyBE.DesignationBE> GetDesignationDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.DesignationMasters
                      where _Obj.Status == true
                      select new CompanyBE.DesignationBE
                      {
                          DesignationID = _Obj.DesignationID,
                          DesignationName = _Obj.DesignationName,
                          Description = _Obj.Description
                      };

            return lst.ToList();
        }
        public List<CompanyBE.DesignationBE> GetDesignationDetails(long id)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.DesignationMasters
                      where _Obj.Status == true && _Obj.DesignationID == id
                      select new CompanyBE.DesignationBE
                      {
                          DesignationID = _Obj.DesignationID,
                          DesignationName = _Obj.DesignationName,
                          Description = _Obj.Description
                      };
            return lst.ToList();
        }
        public bool IsDesignationAlreadyExist(string strName)
        {
            var count = _ObjEINS_RBMSEntities.DesignationMasters.Where(x => x.Status == true && x.DesignationName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsDesignationAlreadyExistUpdate(string strName,long Id)
        {
            var count = _ObjEINS_RBMSEntities.DesignationMasters.Where(x => x.Status == true && x.DesignationID==Id && x.DesignationName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<CompanyBE.CategoryBE> GetCategoryDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.CategoryMasters
                      where _Obj.CategoryStatus == true
                      select new CompanyBE.CategoryBE
                      {
                          CategoryId = _Obj.CategoryId,
                          CategoryName = _Obj.CategoryName,
                          CategoryDescription = _Obj.CategoryDescription == "" ? "NA" : _Obj.CategoryDescription,
                          CategoryStatus = _Obj.CategoryStatus.Value
                      };

            return lst.ToList();
        }
        public List<CompanyBE.CategoryBE> GetCategoryDetails(long id)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.CategoryMasters
                      where _Obj.CategoryStatus == true && _Obj.CategoryId == id
                      select new CompanyBE.CategoryBE
                      {
                          CategoryId = _Obj.CategoryId,
                          CategoryName = _Obj.CategoryName,
                          CategoryDescription = _Obj.CategoryDescription == "" ? "NA" : _Obj.CategoryDescription,
                          CategoryStatus = _Obj.CategoryStatus.Value
                      };

            return lst.ToList();
        }
        public bool IsCategoryAlreadyExist(string strName)
        {
            var count = _ObjEINS_RBMSEntities.CategoryMasters.Where(x => x.CategoryStatus == true && x.CategoryName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsCategoryAlreadyExistUpdate(string strName,long Id)
        {
            var count = _ObjEINS_RBMSEntities.CategoryMasters.Where(x => x.CategoryStatus == true && x.CategoryId==Id && x.CategoryName == strName).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<CompanyBE.SubCategoryBE> GetSubCategoryDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.SubCategoryMasters
                      join objC in _ObjEINS_RBMSEntities.CategoryMasters
                      on _Obj.CategoryId equals objC.CategoryId
                      where _Obj.SubCategoryStatus == true && objC.CategoryStatus == true
                      select new CompanyBE.SubCategoryBE
                      {
                          SubCategoryId = _Obj.SubCategoryId,
                          CategoryId = _Obj.CategoryId.Value,
                          CategoryName = objC.CategoryName,
                          SubCategoryName = _Obj.SubCategoryName,
                          SubCategoryDescription = _Obj.SubCategoryDescription == "" ? "NA" : _Obj.SubCategoryDescription,
                          SubCategoryStatus = _Obj.SubCategoryStatus.Value
                      };

            return lst.ToList();
        }
        public List<CompanyBE.SubCategoryBE> GetSubCategoryDetails(long CategoryId)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.SubCategoryMasters
                      join objC in _ObjEINS_RBMSEntities.CategoryMasters
                      on _Obj.CategoryId equals objC.CategoryId
                      where _Obj.CategoryId == CategoryId
                      select new CompanyBE.SubCategoryBE
                      {
                          SubCategoryId = _Obj.SubCategoryId,
                        CategoryId=(long)_Obj.CategoryId,
                          CategoryName = objC.CategoryName,
                          SubCategoryName = _Obj.SubCategoryName,
                          SubCategoryDescription = _Obj.SubCategoryDescription == "" ? "NA" : _Obj.SubCategoryDescription,
                          SubCategoryStatus = _Obj.SubCategoryStatus.Value
                      };

            return lst.ToList();
        }
        public List<CompanyBE.SubCategoryBE> GetSubCategoryDetailsBySubCategoryId(long SubCategoryId)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.SubCategoryMasters
                      join objC in _ObjEINS_RBMSEntities.CategoryMasters
                      on _Obj.CategoryId equals objC.CategoryId
                      where _Obj.SubCategoryStatus == true && objC.CategoryStatus == true && _Obj.SubCategoryId == SubCategoryId
                      select new CompanyBE.SubCategoryBE
                      {
                          SubCategoryId = _Obj.SubCategoryId,
                          CategoryId = _Obj.CategoryId.Value,
                          CategoryName = objC.CategoryName,
                          SubCategoryName = _Obj.SubCategoryName,
                          SubCategoryDescription = _Obj.SubCategoryDescription == "" ? "NA" : _Obj.SubCategoryDescription,
                          SubCategoryStatus = _Obj.SubCategoryStatus.Value
                      };

            return lst.ToList();
        }
        public bool IsSubCategoryAlreadyExist(string strName,long CategoryId)
        {
            var count = _ObjEINS_RBMSEntities.SubCategoryMasters.Where(x => x.SubCategoryStatus == true && x.SubCategoryName == strName && x.CategoryId == CategoryId).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsSubCategoryAlreadyExistUpdate(string strName, long CategoryId,long SubCategoryId)
        {
            var count = _ObjEINS_RBMSEntities.SubCategoryMasters.Where(x => x.SubCategoryStatus == true && x.SubCategoryId == SubCategoryId && x.SubCategoryName == strName && x.CategoryId == CategoryId).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }

        public long GetDepartmentCount()
        {
            var count = _ObjEINS_RBMSEntities.DepartmentMasters.Where(x => x.Status == true).Count();
            return count;
        }
        public long GetDesignationCount()
        {
            var count = _ObjEINS_RBMSEntities.DesignationMasters.Where(x => x.Status == true).Count();
            return count;
        }
        public long GetCategoryCount()
        {
            var count = _ObjEINS_RBMSEntities.CategoryMasters.Where(x => x.CategoryStatus == true).Count();
            return count;
        }
        public long GetSubCategoryCount()
        {
            var count = _ObjEINS_RBMSEntities.SubCategoryMasters.Where(x => x.SubCategoryStatus == true).Count();
            return count;
        }
        public long InsertUpdateCompanyDetails(CompanyBE.CompanyMasterBE lst)
        {
            if (lst.CompanyID != 0)
            {
                var lstCompany = _ObjEINS_RBMSEntities.CompanyMasters.FirstOrDefault(x => x.CompanyID == lst.CompanyID);
                lstCompany.CompanyName = lst.CompanyName;
                lstCompany.CompanyBuilding = lst.CompanyBuilding;
                lstCompany.CompanyHead = lst.CompanyHead;
                lstCompany.CompanyAlias = lst.CompanyAlias;
                lstCompany.Street = lst.Street;
                lstCompany.City = lst.City;
                lstCompany.Pincode = lst.Pincode;
                lstCompany.CountryId = lst.CountryId;
                lstCompany.StateId = lst.StateId;
                lstCompany.CompanyEmailid = lst.CompanyEmailid;
                lstCompany.CompanyTelephone = lst.CompanyTelephone;
                lstCompany.CompanyFax = lst.CompanyFax;
                lstCompany.CompanyWebsite = lst.CompanyWebsite;
                lstCompany.CompanyLogo = lst.CompanyLogo;
                lstCompany.Status = true;
                lstCompany.LastModifiedBy = lst.LastModifiedBy;
                lstCompany.LastModifiedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.SaveChanges();
                return lst.CompanyID;
            }
            else
            {
                CompanyMaster _ObjCompanyMaster = new CompanyMaster();

                _ObjCompanyMaster.CompanyName = lst.CompanyName;
                _ObjCompanyMaster.CompanyBuilding = lst.CompanyBuilding;
                _ObjCompanyMaster.CompanyHead = lst.CompanyHead;
                _ObjCompanyMaster.CompanyAlias = lst.CompanyAlias;
                _ObjCompanyMaster.Street = lst.Street;
                _ObjCompanyMaster.City = lst.City;
                _ObjCompanyMaster.Pincode = lst.Pincode;
                _ObjCompanyMaster.CountryId = lst.CountryId;
                _ObjCompanyMaster.StateId = lst.StateId;
                _ObjCompanyMaster.CompanyEmailid = lst.CompanyEmailid;
                _ObjCompanyMaster.CompanyTelephone = lst.CompanyTelephone;
                _ObjCompanyMaster.CompanyFax = lst.CompanyFax;
                _ObjCompanyMaster.CompanyWebsite = lst.CompanyWebsite;
                _ObjCompanyMaster.CompanyLogo = lst.CompanyLogo;
                _ObjCompanyMaster.Status = true;
                _ObjCompanyMaster.AddedBy = lst.AddedBy;
                _ObjCompanyMaster.AddedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.AddToCompanyMasters(_ObjCompanyMaster);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _ObjCompanyMaster.CompanyID;
            }
        }
        public long InsertUpdateLocationDetails(CompanyBE.LocationMasterBE lst)
        {
            if (lst.LocationID != 0)
            {
                var lstLocation = _ObjEINS_RBMSEntities.LocationMasters.FirstOrDefault(x => x.LocationId == lst.LocationID);

                lstLocation.LocationName = lst.LocationName;
                lstLocation.Description = lst.Description;
                lstLocation.LocationStatus = true;
                lstLocation.LastModifiedBy = lst.LastModifiedBy;
                lstLocation.LastModifiedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.SaveChanges();
                return lstLocation.LocationId;
            }
            else
            {
                LocationMaster _ObjLocation = new LocationMaster();

                _ObjLocation.LocationName = lst.LocationName;
                _ObjLocation.Description = lst.Description;
                _ObjLocation.LocationStatus = true;
                _ObjLocation.AddedBy = lst.AddedBy;
                _ObjLocation.AddedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.AddToLocationMasters(_ObjLocation);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _ObjLocation.LocationId;
            }
        }
        public long InsertUpdateDeaprtmentDetails(CompanyBE.DepartmentBE lst)
        {
            if (lst.DepartmentID != 0)
            {
                var lstDepartment = _ObjEINS_RBMSEntities.DepartmentMasters.FirstOrDefault(x => x.DepartmentID == lst.DepartmentID);
                lstDepartment.DepartmentName = lst.DepartmentName;
                lstDepartment.Description = lst.Description;
                lstDepartment.Status = true;
                lstDepartment.LastModifiedBy = lst.LastModifiedBy;
                lstDepartment.LastModifiedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.SaveChanges();
                return lstDepartment.DepartmentID;
            }
            else
            {
                DepartmentMaster _Obj = new DepartmentMaster();

                _Obj.DepartmentName = lst.DepartmentName;
                _Obj.Description = lst.Description;
                _Obj.Status = true;
                _Obj.AddedBy = lst.AddedBy;
                _Obj.AddedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.AddToDepartmentMasters(_Obj);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _Obj.DepartmentID;
            }
        }
        public long InsertUpdateDesignationDetails(CompanyBE.DesignationBE lst)
        {
            if (lst.DesignationID != 0)
            {
                var lstDepartment = _ObjEINS_RBMSEntities.DesignationMasters.FirstOrDefault(x => x.DesignationID == lst.DesignationID);
                lstDepartment.DesignationName = lst.DesignationName;
                lstDepartment.Description = lst.Description;
                lstDepartment.Status = true;
                lstDepartment.LastModifiedBy = lst.LastModifiedBy;
                lstDepartment.LastModifiedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.SaveChanges();
                return lstDepartment.DesignationID;
            }
            else
            {
                DesignationMaster _Obj = new DesignationMaster();
                _Obj.DesignationName = lst.DesignationName;
                _Obj.Description = lst.Description;
                _Obj.Status = true;
                _Obj.AddedBy = lst.AddedBy;
                _Obj.AddedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.AddToDesignationMasters(_Obj);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _Obj.DesignationID;
            }
        }
        public long InsertUpdateCategoryDetails(CompanyBE.CategoryBE lst)
        {
            if (lst.CategoryId != 0)
            {
                var objCategory = _ObjEINS_RBMSEntities.CategoryMasters.FirstOrDefault(x => x.CategoryId == lst.CategoryId);
                objCategory.CategoryName = lst.CategoryName;
                objCategory.CategoryDescription = lst.CategoryDescription;
                objCategory.CategoryStatus = true;
                objCategory.ModifiedBy = lst.LastModifiedBy;
                objCategory.ModifiedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.SaveChanges();
                return objCategory.CategoryId;
            }
            else
            {
                CategoryMaster _Obj = new CategoryMaster();
                _Obj.CategoryName = lst.CategoryName;
                _Obj.CategoryDescription = lst.CategoryDescription;
                _Obj.CategoryStatus = true;
                _Obj.AddedBy = lst.AddedBy;
                _Obj.AddedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.AddToCategoryMasters(_Obj);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _Obj.CategoryId;
            }
        }
        public long InsertUpdateSubCategoryDetails(CompanyBE.SubCategoryBE lst)
        {
            if (lst.SubCategoryId != 0)
            {
                var objSubCategory = _ObjEINS_RBMSEntities.SubCategoryMasters.FirstOrDefault(x => x.SubCategoryId == lst.SubCategoryId);
                objSubCategory.CategoryId = lst.CategoryId;
                objSubCategory.SubCategoryName = lst.SubCategoryName;
                objSubCategory.SubCategoryDescription = lst.SubCategoryDescription;
                objSubCategory.SubCategoryStatus = true;
                objSubCategory.ModifiedBy = lst.LastModifiedBy;
                objSubCategory.ModifiedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.SaveChanges();
                return objSubCategory.SubCategoryId;
            }
            else
            {
                SubCategoryMaster _Obj = new SubCategoryMaster();
                _Obj.CategoryId = lst.CategoryId;
                _Obj.SubCategoryName = lst.SubCategoryName;
                _Obj.SubCategoryDescription = lst.SubCategoryDescription;
                _Obj.SubCategoryStatus = true;
                _Obj.AddedBy = lst.AddedBy;
                _Obj.AddedOn = DateTime.Now;
                _ObjEINS_RBMSEntities.AddToSubCategoryMasters(_Obj);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _Obj.SubCategoryId;
            }
        }

        public long DeleteCompanyDetails(long CompanyID)
        {
            var lstCompany = _ObjEINS_RBMSEntities.CompanyMasters.FirstOrDefault(x => x.CompanyID == CompanyID);
            lstCompany.Status = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lstCompany.CompanyID;
        }
        public long DeleteLocationDetails(long LocationID)
        {
            var lstLocation = _ObjEINS_RBMSEntities.LocationMasters.FirstOrDefault(x => x.LocationId == LocationID);
            lstLocation.LocationStatus = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lstLocation.LocationId;
        }
        public long DeleteDepartmentDetails(long DepartmentID)
        {
            var lst = _ObjEINS_RBMSEntities.DepartmentMasters.FirstOrDefault(x => x.DepartmentID == DepartmentID);
            lst.Status = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.DepartmentID;
        }
        public long DeleteDesignationDetails(long DesignationID)
        {
            var lst = _ObjEINS_RBMSEntities.DesignationMasters.FirstOrDefault(x => x.DesignationID == DesignationID);
            lst.Status = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.DesignationID;
        }
        public long DeleteCategoryDetails(long CategoryID)
        {
            var lst = _ObjEINS_RBMSEntities.CategoryMasters.FirstOrDefault(x => x.CategoryId == CategoryID);
            lst.CategoryStatus = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.CategoryId;
        }
        public long DeleteSubCategoryDetails(long SubCategoryID)
        {
            var lst = _ObjEINS_RBMSEntities.SubCategoryMasters.FirstOrDefault(x => x.SubCategoryId == SubCategoryID);
            lst.SubCategoryStatus = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.SubCategoryId;
        }


        public bool getCompanyId_Exits(int CompanyID)
        {
            var companyID = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.Company == CompanyID).Any();
            return companyID;
        }
        public bool getLocation_Exits(int LocationId)
        {
            var LocID = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.Location == LocationId).Any();
            return LocID;
        }
        public bool getDept_Exits(int DepartmentId)
        {
            var Dep_Id = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.Department == DepartmentId).Any();
            return Dep_Id;
        }
        public bool getDesig_Exits(int DesignationId)
        {
            var Des_Id = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.Designation == DesignationId).Any();
            return Des_Id;
        }
        public bool getCategory_Exits(int CategoryId)
        {
            var Dep_Id = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.Category == CategoryId).Any();
            return Dep_Id;
        }
        public bool getSubCategory_Exits(int SubCategoryId)
        {
            var Des_Id = _ObjEINS_RBMSEntities.UserMasters.Where(x => x.SubCategory == SubCategoryId).Any();
            return Des_Id;
        }
    }
}
