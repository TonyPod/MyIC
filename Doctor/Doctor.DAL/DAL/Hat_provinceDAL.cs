using Doctor.Model;
using System;
using System.Data;
using System.Data.SqlClient;
namespace Doctor.DAL
{

    public class Hat_provinceDAL
    {
        public static Hat_provinceModel GetById(int id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_province where id = @id",
                new SqlParameter("@id", id));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table hat_province");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static Hat_provinceModel[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from hat_province");
            Hat_provinceModel[] hat_province = new Hat_provinceModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hat_province[i] = ToModel(table.Rows[i]);
            }
            return hat_province;
        }

        private static Hat_provinceModel ToModel(DataRow row)
        {
            Hat_provinceModel hat_province = new Hat_provinceModel();
            hat_province.Id = (System.Int32)row["id"];
            hat_province.ProvinceID = (System.String)row["provinceID"];
            hat_province.Province = (System.String)row["province"];
            return hat_province;
        }

        public static Hat_provinceModel GetByName(string name)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_province where province = @name",
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
    }
}
