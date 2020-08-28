using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBBE;
using RBDL;
using System.Data;

namespace RBDL
{
    public class ResourceDL
    {
        EINS_RBMSEntities _ObjEINS_RBMSEntities = new EINS_RBMSEntities();
        CommonDL objCom = new CommonDL();

        public List<ResourceBE.Resource> GetResourceDetails()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.ResourceMasters
                      where _Obj.Status == true
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          AssetCode = _Obj.AssetCode,
                          ResourceName = _Obj.ResourceName,
                          Brand = _Obj.Brand,
                          ModelNo = _Obj.ModelNo,
                          SerialNo = _Obj.SerialNo,
                          Quantity = _Obj.Quantity,
                          Description = _Obj.ResourceDescription,
                          Transferrable = _Obj.Transferrable,
                          RequireQuantity = 0,
                          Status = false
                      };

            return lst.ToList();
        }

        public List<ResourceBE.Resource> GetResourceDetailsWithAvailableQuantity()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.vw_RoomResourceData
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          ResourceName = _Obj.ResourceName,
                          Quantity = _Obj.Quantity,
                          AvailableQuantity = _Obj.AvailableQuantity,
                          UsedQuantity = _Obj.UsedQuantity,
                          RequireQuantity = 0,
                          Status = false
                      };

            return lst.ToList();
        }

        public List<ResourceBE.Resource> GetResourceDetailsByResourceId(Int64 ResourceId)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.ResourceMasters
                      where _Obj.Status == true && _Obj.ResourceID == ResourceId
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          AssetCode = _Obj.AssetCode,
                          ResourceName = _Obj.ResourceName,
                          Brand = _Obj.Brand,
                          ModelNo = _Obj.ModelNo,
                          SerialNo = _Obj.SerialNo,
                          Quantity = _Obj.Quantity,
                          Description = _Obj.ResourceDescription,
                          Transferrable = _Obj.Transferrable,
                          ResourceBoughtFrom = _Obj.ResourceBoughtFrom,
                          ResourceBoughtOn = _Obj.ResourceBoughtOn.Value,
                          ResourceImage = _Obj.ResourceImage,
                          SpecificationImage = _Obj.SpecificationImage,
                          RequireQuantity = 0,
                          Status = false
                      };

            return lst.ToList();
        }

        public List<ResourceBE.Resource> GetResourceDetailsByRoomID(long RoomId)
        {
            var lst = from _ObjMap in _ObjEINS_RBMSEntities.Room_Resources_Map
                      join _objResourse in _ObjEINS_RBMSEntities.ResourceMasters
                      on _ObjMap.ResourcesId equals _objResourse.ResourceID
                      where _ObjMap.RoomId == RoomId && _objResourse.Status == true && _ObjMap.Status==true
                      select new ResourceBE.Resource
                      {
                          ResourceID = _objResourse.ResourceID,
                          Quantity = _objResourse.Quantity,
                          ResourceName = _objResourse.ResourceName,
                          RequireQuantity = _ObjMap.Quantity,
                          Status = _ObjMap.Status
                      };

            return lst.ToList();
        }
        public List<ResourceBE.Resource> GetResourceDetailsByRoomIDFromView(long RoomId)
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.vw_RoomResourceData.Where(x => x.RoomID == RoomId)
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          ResourceName = _Obj.ResourceName,
                          Quantity = _Obj.Quantity,
                          AvailableQuantity = _Obj.AvailableQuantity,
                          UsedQuantity = _Obj.UsedQuantity,
                          RequireQuantity = _Obj.UsedQuantity,
                          Status = false
                      };

            return lst.ToList();
        }
        public List<ResourceBE.Resource> GetResourceDetailsByRoomIDFromEdit(long RoomId)
        {
            var lst = (from _Obj in _ObjEINS_RBMSEntities.vw_RoomResourceData
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          ResourceName = _Obj.ResourceName,
                          Quantity = _Obj.Quantity,
                          AvailableQuantity = _Obj.AvailableQuantity,
                          UsedQuantity = _Obj.UsedQuantity,
                          RequireQuantity = _Obj.UsedQuantity,
                          Status = false
                      }).ToList();

            List<ResourceBE.Resource> lstRes = new List<ResourceBE.Resource>();
            lstRes = (from _Obj in _ObjEINS_RBMSEntities.Room_Resources_Map.Where(x => x.RoomId == RoomId)
                     select new ResourceBE.Resource
                     {
                         UsedQuantity = _Obj.Quantity,
                         Status=_Obj.Status
                     }).ToList();

            if (lst != null && lst.Count() > 0 && lstRes != null && lstRes.Count() > 0 && lstRes.Count()==lst.Count())
            {
                for (int i = 0; i < lst.Count(); i++) {
                   lst[i].UsedQuantity= lstRes[i].UsedQuantity;
                   lst[i].RequireQuantity= lstRes[i].UsedQuantity;
                   lst[i].Status = lstRes[i].Status;
                }
            }
            return lst;
        }
        public long InsertUpdateResourceDetails(ResourceBE.Resource lst)
        {
            if (lst.ResourceID != 0)
            {
                var lstResource = _ObjEINS_RBMSEntities.ResourceMasters.FirstOrDefault(x => x.ResourceID == lst.ResourceID);

                lstResource.AssetCode = lst.AssetCode;
                lstResource.ResourceName = lst.ResourceName;
                lstResource.Brand = lst.Brand;
                lstResource.ModelNo = lst.ModelNo;
                lstResource.SerialNo = lst.SerialNo;
                lstResource.Quantity = lst.Quantity;
                lstResource.ResourceDescription = lst.Description;
                lstResource.Transferrable = lst.Transferrable;
                lstResource.ResourceBoughtFrom = lst.ResourceBoughtFrom;
                lstResource.ResourceBoughtOn = lst.ResourceBoughtOn;
                lstResource.ResourceImage = lst.ResourceImage;
                lstResource.SpecificationImage = lst.SpecificationImage;
                lstResource.Status = true;
                lstResource.LastModifiedBy = lst.LastModifiedBy;
                lstResource.LastModifiedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.SaveChanges();
                return lstResource.ResourceID;
            }
            else
            {
                ResourceMaster _Obj = new ResourceMaster();

                _Obj.AssetCode = lst.AssetCode;
                _Obj.ResourceName = lst.ResourceName;
                _Obj.Brand = lst.Brand;
                _Obj.ModelNo = lst.ModelNo;
                _Obj.SerialNo = lst.SerialNo;
                _Obj.Quantity = lst.Quantity;
                _Obj.ResourceDescription = lst.Description;
                _Obj.Transferrable = lst.Transferrable;
                _Obj.ResourceBoughtFrom = lst.ResourceBoughtFrom;
                _Obj.ResourceBoughtOn = lst.ResourceBoughtOn;
                _Obj.ResourceImage = lst.ResourceImage;
                _Obj.SpecificationImage = lst.SpecificationImage;
                _Obj.Status = true;
                _Obj.AddedBy = lst.AddedBy;
                _Obj.AddedOn = DateTime.Now;

                _ObjEINS_RBMSEntities.AddToResourceMasters(_Obj);
                _ObjEINS_RBMSEntities.SaveChanges();
                return _Obj.ResourceID;
            }
        }

        public bool InsertIntoRoomResMappingWhenResourceAdded(long ResourceId)
        {
            try
            {
                Room_Resources_Map objRRMap = new Room_Resources_Map();
                foreach (var obj in _ObjEINS_RBMSEntities.RoomMasters.Where(x => x.Status == true))
                {
                     objRRMap = new Room_Resources_Map();
                    objRRMap.RoomId = obj.RoomId;
                    objRRMap.ResourcesId = ResourceId;
                    objRRMap.Quantity = 0;
                    objRRMap.Status = false;
                    _ObjEINS_RBMSEntities.AddToRoom_Resources_Map(objRRMap);
                }
                _ObjEINS_RBMSEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public long DeleteResourceDetails(long ResourceID)
        {
            var lst = _ObjEINS_RBMSEntities.ResourceMasters.FirstOrDefault(x => x.ResourceID == ResourceID);
            lst.Status = false;
            _ObjEINS_RBMSEntities.SaveChanges();
            return lst.ResourceID;
        }
        public bool IsResourceAlreadyExist(string strName,string strBrand)
        {
            var count = _ObjEINS_RBMSEntities.ResourceMasters.Where(x => x.Status == true && x.ResourceName == strName && x.Brand == strBrand).Count();
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool getResource_ExistsInRoom(int ResourceId)
        {
            var IsResExists = _ObjEINS_RBMSEntities.Room_Resources_Map.Where(x => x.ResourcesId == ResourceId && x.Status == true).Any();
            return IsResExists;
        }
        public long GetAssetMaxID()
        {
            if (_ObjEINS_RBMSEntities.ResourceMasters.Any(x => x.ResourceID != null))
            {
                return _ObjEINS_RBMSEntities.ResourceMasters.OrderByDescending(x => x.ResourceID).FirstOrDefault().ResourceID;
            }
            else
            {
                return 0;
            }
        }

        public List<ResourceBE.Resource> GetResourceName()
        {
            var lst = from _Obj in _ObjEINS_RBMSEntities.ResourceMasters
                      where _Obj.Status == true
                      select new ResourceBE.Resource
                      {
                          ResourceID = _Obj.ResourceID,
                          ResourceName = _Obj.ResourceName,
                      };

            return lst.ToList();
        }
    }
}
