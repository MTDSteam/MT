using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDS.Common;
using MTDS.Dal;
using MTDS.Model;

namespace MTDS.BLL
{
  public  class AreaBll
    {
        private readonly AreaDal dal = new AreaDal();

        /// <summary>
        /// 缓存地区列表
        /// </summary>
        private static List<Area> allAreas;
        public List<Area> AllAreas
        {
            get
            {
                return allAreas ?? (allAreas = dal.GetAll());
            }
        }

       

        /// <summary>
        /// 根据地区编号筛选地区列表
        /// </summary>
        /// <param name="areaNoList"></param>
        /// <returns></returns>
        public List<Area> GetFilterdAreas(int[] areaNoList)
        {
            List<int> selectedAreas = new List<int>();

            // 获取地区的所有编号
            foreach (var no in areaNoList.Select(GetAreaFullNo)
                .SelectMany(thisAreaNo => thisAreaNo.Where(no => !selectedAreas.Contains(no))))
            {
                selectedAreas.Add(no);
            }

            return AllAreas.Where(area => selectedAreas.Contains(area.No)).ToList();
        }

        /// <summary>
        /// 根据编号获得地区
        /// </summary>
        /// <param name="areaNo"></param>
        /// <returns></returns>
        public Area GetByNo(int areaNo)
        {
            return AllAreas.SingleOrDefault(a => a.No == areaNo);
        }

        /// <summary>
        /// 根据编号获得子地区列表
        /// </summary>
        /// <param name="parentNo"></param>
        /// <returns></returns>
        public List<Area> GetByParentNo(int parentNo)
        {
            return AllAreas.Where(a => a.ParentNo == parentNo).ToList();
        }

        /// <summary>
        /// 获得父级地区编号
        /// </summary>
        /// <param name="areaNo"></param>
        /// <returns></returns>
        public int GetParentNo(int areaNo)
        {
            if (areaNo > 0)
                return GetByNo(areaNo).ParentNo;
            return -1;
        }

        /// <summary>
        /// 获得父级地区
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Area GetParent(Area area)
        {
            int parentNo = area.ParentNo;
            return parentNo > 0 ? GetByNo(parentNo) : null;
        }

        /// <summary>
        /// 生成地区Json字符串
        /// </summary>
        /// <param name="lstAll">所有列表</param>
        /// <param name="parentNo">父级地区</param>
        /// <param name="depth">默认展开深度</param>
        /// <returns></returns>
        public string GetAreaJsonString(List<Area> lstAll, int parentNo, int depth)
        {
            var selected = lstAll.Where(l => l.ParentNo == parentNo).ToList();
            if (selected.Count == 0 || depth == 0) return "[]";
            depth = depth - 1;

            StringBuilder json = new StringBuilder();
            json.Append("[");

            foreach (var area in selected)
            {
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", area.No);
                json.AppendFormat("\"title\":\"{0}\",", area.Name);

                var children = lstAll.Where(l => l.ParentNo == area.No).ToList();
                json.AppendFormat("\"hasChilds\":\"{0}\",", children.Count > 0 ? "1" : "0");
                json.AppendFormat("\"childs\":{0}", children.Count > 0 ? GetAreaJsonString(lstAll, area.No, depth) : "[]");
                json.Append("},");
            }
            if (json.Length > 1) json.Remove(json.Length - 1, 1); // 删除末尾的逗号

            json.Append("]");

            return json.ToString();
        }

     

        /// <summary>
        /// 根据地区编号获得下拉列表框
        /// </summary>
        /// <param name="parentNo">父级地区</param>
        /// <param name="value">选中的项value</param>
        /// <returns></returns>
        public string GetOptionsByParentNo(int parentNo, int value = 0)
        {
            var options = new StringBuilder();

            List<Area> areas = GetByParentNo(parentNo);

            foreach (var area in areas)
            {
                options.AppendFormat("<option value=\"{0}\"{1}>{2}</option>", area.No, area.No == value ? " selected=\"selected\"" : "", area.Name);
            }

            if (options.Length > 0) options.Insert(0, DefaultOption);

            return options.ToString();
        }

        /// <summary>
        /// 地区默认空值选择项
        /// </summary>
        public string DefaultOption
        {
            get
            {
                return "<option value=''>请选择..</option>";
            }
        }

        /// <summary>
        /// 根据地区编号字符串获得地区完整名称
        /// </summary>
        /// <param name="areaNo">地区编号</param>
        /// <returns></returns>
        public string GetAreaFullName(int areaNo)
        {
            StringBuilder sbAreaString = new StringBuilder();

            if (areaNo > 0)
                sbAreaString.Append(GetByNo(areaNo).Name.Trim());
            else
                return "全部";

            while ((areaNo = GetParentNo(areaNo)) > 0)
            {
                sbAreaString.Insert(0, GetByNo(areaNo).Name.Trim() + " ");
            }
            return sbAreaString.ToString();
        }

        /// <summary>
        /// 根据地区编号字符串获得地区完整编号
        /// </summary>
        /// <param name="areaNo">地区编号</param>
        /// <returns></returns>
        public int[] GetAreaFullNo(int areaNo)
        {
            List<int> areaList = new List<int>();

            if (areaNo > 0) areaList.Add(GetByNo(areaNo).No);

            while ((areaNo = GetParentNo(areaNo)) > 0)
            {
                areaList.Insert(0, GetByNo(areaNo).No);
            }
            return areaList.ToArray();
        }
    }
}
