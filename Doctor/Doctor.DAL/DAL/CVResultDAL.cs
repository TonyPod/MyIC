using Doctor.Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Doctor.DAL.DAL
{
    public class CVResultDAL
    {
        public static bool Insert(CVResultModel cVResult)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"insert into CVResult(path, teeth_num, teeth_illnesses)
				values(@teeth_num, @teeth_illnesses)",
                    new SqlParameter("@path", cVResult.Path),
                    new SqlParameter("@teeth_num", cVResult.Teeth_num),
                    new SqlParameter("@teeth_illnesses", cVResult.Teeth_illnesses)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool DeleteById(System.String id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"delete from CVResult where path = @id",
                    new SqlParameter("@id", id));
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool Update(CVResultModel cVResult)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"update CVResult set
				teeth_num = @teeth_num,
				teeth_illnesses = @teeth_illnesses
				where path = @path",
                    new SqlParameter("@teeth_num", cVResult.Teeth_num),
                    new SqlParameter("@teeth_illnesses", cVResult.Teeth_illnesses),
                    new SqlParameter("@path", cVResult.Path)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static CVResultModel GetById(System.String id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from CVResult where path = @id",
                new SqlParameter("@id", id));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table CVResult");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static CVResultModel[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from CVResult");
            CVResultModel[] cVResult = new CVResultModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cVResult[i] = ToModel(table.Rows[i]);
            }
            return cVResult;
        }

        private static CVResultModel ToModel(DataRow row)
        {
            CVResultModel cVResult = new CVResultModel();
            cVResult.Path = (System.String)row["path"];
            cVResult.Teeth_num = (System.Int32)row["teeth_num"];
            cVResult.Teeth_illnesses = (System.String)row["teeth_illnesses"];
            return cVResult;
        }
    }
}
