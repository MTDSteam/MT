using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDS.Dal;
using MTDS.Model;
using System.Data;
namespace MTDS.BLL
{
   public class AssetPropertyBll
    {
        private readonly  AssetPropertyDal _dal=new AssetPropertyDal();

        /// <summary>
        ///获取所有属性值
        /// </summary>
        /// <returns></returns>
        public List<AssetProperty> GetList()
        {
            return _dal.GetList();
        }

        /// <summary>
        /// 根据设备编号获取所有属性值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AssetProperty> GetByAssetType(Guid id)
        {
            return _dal.GetByAssetType(id);
        }
        public DataTable GetAssetbyType(Guid assetTypeID)
        {
            return _dal.GetAssetbyType(assetTypeID);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(AssetProperty model)
        {
            return _dal.Insert(model);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(AssetProperty model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 根据设备编号删除属性数据
        /// </summary>
        /// <param name="assetTypeId"></param>
        /// <returns></returns>
        public int Delete(Guid assetTypeId)
        {
            return _dal.Delete(assetTypeId);
        }
        /// <summary>
        /// 根据大分类和设备编号获取数据
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="assetId"></param>
        /// <returns></returns>
        public List<AssetProperty> GetByDicAndAsset(Guid dictionary, Guid assetId)
        {
            return _dal.GetByDicAndAsset(dictionary, assetId);
        }
    }
}
