using Doctor.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Doctor.DAL
{
    public class RecordDAL
    {
        public static long Insert(RecordModel record)
        {
            try
            {
                //这里的result居然是decimal类型
                decimal result = (decimal)SqlHelper.ExecuteScalar(@"insert into Record(user_id, description, answers, time, citycode, score)
				values(@user_id, @description, @answers, @time, @citycode, @score) select @@identity",
                    new SqlParameter("@user_id", record.User_id),
                    new SqlParameter("@description", SqlHelper.ToDBValue(record.Description)),
                    new SqlParameter("@answers", SqlHelper.ToDBValue(record.Answers)),
                    new SqlParameter("@time", record.Time),
                    new SqlParameter("@citycode", SqlHelper.ToDBValue(record.Citycode)),
                    new SqlParameter("@score", SqlHelper.ToDBValue(record.Score))
                );
                
                return decimal.ToInt64(result);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static bool DeleteById(System.Int64 id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"delete from Record where record_id = @id",
                    new SqlParameter("@id", id));
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool Update(RecordModel record)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"update Record set
				user_id = @user_id,
				description = @description,
                answers = @answers
				time = @time,
                citycode = @citycode,
                score = @score
				where record_id = @record_id",
                    new SqlParameter("@user_id", record.User_id),
                    new SqlParameter("@description", SqlHelper.ToDBValue(record.Description)),
                    new SqlParameter("@answers", SqlHelper.ToDBValue(record.Answers)),
                    new SqlParameter("@time", record.Time),
                    new SqlParameter("@citycode", SqlHelper.ToDBValue(record.Citycode)),
                    new SqlParameter("@score", SqlHelper.ToDBValue(record.Score)),
                    new SqlParameter("@record_id", record.Record_id)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static RecordModel GetById(System.Int64 id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from Record where record_id = @id",
                new SqlParameter("@id", id));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table Record");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static RecordModel[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from Record");
            RecordModel[] record = new RecordModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                record[i] = ToModel(table.Rows[i]);
            }
            return record;
        }

        private static RecordModel ToModel(DataRow row)
        {
            RecordModel record = new RecordModel();
            record.Record_id = (System.Int64)row["record_id"];
            record.User_id = (System.Int64)row["user_id"];
            record.Description = (System.String)SqlHelper.FromDBValue(row["description"]);
            record.Answers = (System.String)SqlHelper.FromDBValue(row["answers"]);
            record.Time = (System.DateTime)row["time"];
            record.Citycode = (System.String)SqlHelper.FromDBValue(row["citycode"]);
            record.Score = (System.Double?)SqlHelper.FromDBValue(row["score"]);
            return record;
        }

//        public static RecordModel[] GetAllByProvinceName(string province)
//        {
//            try
//            {
//                //获取省的编号
//                DataTable table = SqlHelper.ExecuteDataTable(@"select * from hat_province where province like '@province%'",
//                    new SqlParameter("@province", province));
//                DataRow provinceRow = table.Rows[0];

//                SqlHelper.ExecuteDataTable(@"select * from Record where hat_area_")
//            }
//            catch (SqlException)
//            {
//                return null;
//            }
//        }

//        public static RecordModel[] GetAllCityFirst(string city)
//        {
//            try
//            {
//                DataTable cityTable = SqlHelper.ExecuteDataTable(@"select * from hat_city where city like '@city%'",
//                    new SqlParameter("@city", city));
//                DataRow cityRow = cityTable.Rows[0];

//                //通过city找所有底下的区域
//                Hat_areaModel[] areas = Hat_areaDAL.GetAllByCityId((int)cityRow["id"]);

//                //通过区域找相关联的自检
//                List<RecordModel> recordList = new List<RecordModel>();
//                foreach (var area in areas)
//                {
//                    DataTable table = SqlHelper.ExecuteDataTable(@"select * from Record where hat_area_id = @id 
//                        union select * from Record where hat_area_id != @id",
//                        new SqlParameter("@id", area.Id));
//                    foreach (DataRow row in table.Rows)
//                    {
//                        recordList.Add(ToModel(row));
//                    }
//                }
//                return recordList.ToArray();
//            }
//            catch (SqlException)
//            {
//                return null;
//            }
//        }

//        public static RecordModel[] GetAllAreaFirst(string area)
//        {
//            try
//            {
//                //获取地区编号
//                int area_id = (int)SqlHelper.ExecuteScalar(@"select id from hat_area where area like '@area'",
//                    new SqlParameter("@area", area));

//                //根据地区编号获取所有记录
//                DataTable table = SqlHelper.ExecuteDataTable(@"select * from Record where hat_area_id = @id 
//                    union select * from Record where hat_area_id != @id",
//                    new SqlParameter("@id", area_id));

//                int nbRows = table.Rows.Count;
//                RecordModel[] models = new RecordModel[nbRows];
//                for (int i = 0; i < nbRows; i++)
//                {
//                    models[i] = ToModel(table.Rows[i]);
//                }
//                return models;
//            }
//            catch (SqlException)
//            {
//                return null;
//            }
//        }

        public static RecordModel[] GetByUsername(string username)
        {
            UserModel user = UserDAL.GetByUsername(username);
            long user_id = user.User_id;

            DataTable table = SqlHelper.ExecuteDataTable(@"select * from Record where user_id = @user_id",
                new SqlParameter("@user_id", user_id));
            RecordModel[] records = new RecordModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                records[i] = ToModel(table.Rows[i]);
            }
            return records;
        }

        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="score"></param>
        public static bool UpdateScore(float score, long record_id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"update Record set
                    score = @score
				    where record_id = @record_id",
                    new SqlParameter("@score", SqlHelper.ToDBValue(score)),
                    new SqlParameter("@record_id", record_id)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }

}
