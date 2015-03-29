using Doctor.Model;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Doctor.DAL
{
    public class Hat_areaDAL
    {
        public static Hat_areaModel GetById(int id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_area where id = @id",
                new SqlParameter("@id", id));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table hat_area");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static Hat_areaModel[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_area");
            Hat_areaModel[] hat_area = new Hat_areaModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hat_area[i] = ToModel(table.Rows[i]);
            }
            return hat_area;
        }

        public static Hat_areaModel[] GetAllByCityId(int id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_area where hat_area.father = (select cityID from hat_city where hat_city.id = @id)",
                new SqlParameter("@id", id));
            Hat_areaModel[] hat_area = new Hat_areaModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hat_area[i] = ToModel(table.Rows[i]);
            }
            return hat_area;
        }

        private static Hat_areaModel ToModel(DataRow row)
        {
            Hat_areaModel hat_area = new Hat_areaModel();
            hat_area.Id = (System.Int32)row["id"];
            hat_area.AreaID = (System.String)row["areaID"];
            hat_area.Area = (System.String)row["area"];
            hat_area.Father = (System.String)row["father"];
            return hat_area;
        }

        /// <summary>
        /// 通过地区取得所在城市
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static Hat_cityModel GetFatherCity(Hat_areaModel area)
        {
            int id = (int)SqlHelper.ExecuteScalar("select id from hat_city where cityID = @cityID",
                new SqlParameter("@cityID", area.Father));

            return Hat_cityDAL.GetById(id);
        }

        /// <summary>
        /// 通过地区Area获取包括上级市，省的字符串
        /// e.g: 双流县的Hat_areaModel -> "四川省_成都市_双流县"
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static string GetFullLocationString(Hat_areaModel area)
        {
            Hat_cityModel fatherCity = Hat_areaDAL.GetFatherCity(area);
            Hat_provinceModel fatherProvince = Hat_cityDAL.GetFatherProvince(fatherCity);

            string result = fatherProvince.Province + "_" + fatherCity.City + "_" + area.Area;
            return result;
        }

        /// <summary>
        /// 通过名字来找到主键(e.g: 青羊区->2262)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Hat_areaModel GetByName(string name)
        {
            //注意保存在数据库的是“郫 县”而不是“郫县”
            if (name.Length == 2)
            {
                name = name.Insert(1, " ");
            }
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_area where area = @name",
                new SqlParameter("@name", name));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static Hat_areaModel GetByAreaId(string areaId)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_area where areaID = @areaId",
                new SqlParameter("@areaId", areaId));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }
    }

}
