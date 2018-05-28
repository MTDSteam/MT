using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MTDS.Model;
using MTDS.Common;
namespace MTDS.Dal
{
   public class AssetPropertyDal
    {
        /// <summary>
        ///获取所有属性值
        /// </summary>
        /// <returns></returns>
        public List<AssetProperty> GetList()
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Select * from AssetProperty";
                return conn.Query<AssetProperty>(sql).ToList();
            }
        }
       /// <summary>
       /// 根据设备编号获取所有属性值
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public List<AssetProperty> GetByAssetType(Guid id)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Select * from AssetProperty where AssetTypeId=@AssetTypeId";
                return conn.Query<AssetProperty>(sql, new {AssetTypeId = id}).ToList();
            }
        }
        /// <summary>
        /// 根据大分类和设备编号获取数据
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="assetId"></param>
        /// <returns></returns>
        public List<AssetProperty> GetByDicAndAsset(Guid dictionary, Guid assetId)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql =
                    "Select * from AssetProperty where DictionaryId=@DictionaryId and AssetTypeId=@AssetTypeId";
                return conn.Query<AssetProperty>(sql, new {DictionaryId = dictionary, AssetTypeId = assetId}).ToList();
            }
        }
        /// <summary>
        /// 根据assetType获取内容
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="assetId"></param>
        /// <returns></returns>
        public DataTable GetAssetbyType(Guid assetTypeId)
        {
            var conn = new SqlConnection(Config.PlatformConnectionString);
            
                conn.Open();
                var sqlcommand = new SqlCommand("exec sp_getAssetTablebyAssetType @assetTypeId", conn);
                //sqlcommand.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parms = { new SqlParameter("@assetTypeId", assetTypeId) };
                sqlcommand.Parameters.AddRange(parms);
                SqlDataAdapter sqa = new SqlDataAdapter(sqlcommand);

                DataSet ds = new DataSet();

                sqa.Fill(ds);

                return ds.Tables[0];
            
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(AssetProperty model)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql =
                    "Insert into AssetProperty(Id, Name, DictionaryId, AssetTypeId, CreateTime, CreateBy, ModifyTime, ModifyBy) values(@Id, @Name, @DictionaryId, @AssetTypeId, @CreateTime, @CreateBy, @ModifyTime, @ModifyBy)";
                return conn.Execute(sql, model);
            }
        }
       /// <summary>
       /// 更新数据
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        public int Update(AssetProperty model)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql =
                    "Update AssetProperty set Name=@Name,ModifyTime=@ModifyTime,ModifyBy=@ModifyBy where AssetTypeId=@AssetTypeId";
                return conn.Execute(sql, model);
            }
        }
       /// <summary>
       /// 根据设备编号删除属性数据
       /// </summary>
       /// <param name="assetTypeId"></param>
       /// <returns></returns>
        public int Delete(Guid assetTypeId)
        {
            using (var conn=DapperHelper.CreateConnection())
            {
                string sql = "Delete from AssetProperty where AssetTypeId=@AssetTypeId";
                return conn.Execute(sql, new {AssetTypeId = assetTypeId});
            }
        }

    }
}
