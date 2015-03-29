using Doctor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Doctor.DAL
{
    public class UserDAL
    {
        public static bool Insert(UserModel user)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"insert into [User](password, date_of_birth, male, name)
				values(@password, @date_of_birth, @male, @name)",
                    new SqlParameter("@password", user.Password),
                    new SqlParameter("@date_of_birth", SqlHelper.ToDBValue(user.Date_of_birth)),
                    new SqlParameter("@male", SqlHelper.ToDBValue(user.Male)),
                    new SqlParameter("@name", user.Name)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool DeleteById(System.Int64 id)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"delete from [User] where user_id = @id",
                    new SqlParameter("@id", id));
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool Update(UserModel user)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(@"update [User] set
				password = @password,
				date_of_birth = @date_of_birth,
                male = @male,
				name = @name
				where user_id = @user_id",
                    new SqlParameter("@password", user.Password),
                    new SqlParameter("@date_of_birth", SqlHelper.ToDBValue(user.Date_of_birth)),
                    new SqlParameter("@male", SqlHelper.FromDBValue(user.Male)),
                    new SqlParameter("@name", user.Name),
                    new SqlParameter("@user_id", user.User_id)
                );
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static UserModel GetByUsername(System.String username)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from [User] where [name] = @username",
                new SqlParameter("@username", username));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table User");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static UserModel GetById(System.Int64 id)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select * from [User] where user_id = @id",
                new SqlParameter("@id", id));
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("Fatal Error: Duplicated Id in Table User");
            }
            else
            {
                DataRow row = table.Rows[0];
                return ToModel(row);
            }
        }

        public static UserModel[] GetAll()
        {
            DataTable table = SqlHelper.ExecuteDataTable("select * from [User]");
            UserModel[] user = new UserModel[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                user[i] = ToModel(table.Rows[i]);
            }
            return user;
        }

        private static UserModel ToModel(DataRow row)
        {
            UserModel user = new UserModel();
            user.User_id = (System.Int64)row["user_id"];
            user.Password = (System.String)row["password"];
            user.Date_of_birth = (System.DateTime?)SqlHelper.FromDBValue(row["date_of_birth"]);
            user.Male = (System.Boolean?)SqlHelper.FromDBValue(row["male"]);
            user.Name = (System.String)row["name"];
            return user;
        }

        /// <summary>
        /// 检查用户名和密码是否正确
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="state">返回的状态:username not exist;password error;success</param>
        /// <returns>正确返回用户所有信息</returns>
        public static UserModel CheckPassword(string name, string password, ref string state)
        {
            if (!CheckUserExist(name))
            {
                state = "username not exist";
                return null;
            }
            else
            {

                DataTable table = SqlHelper.ExecuteDataTable("select * from [User] where name = @name and password = @password",
                    new SqlParameter("@name", name),
                    new SqlParameter("@password", password));

                if (table.Rows.Count == 1)
                {
                    state = "success";
                    UserModel user = UserDAL.ToModel(table.Rows[0]);
                    return user;
                }
                else
                {
                    state = "password error";
                    return null;
                }
            }
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckUserExist(string name)
        {
            DataTable table = SqlHelper.ExecuteDataTable(@"select name from [User] where name = @name",
                new SqlParameter("@name", name));
            if (table.Rows.Count <= 0)
            {
                return false;
            }
            else if (table.Rows.Count > 1)
            {
                throw new Exception("数据库异常：存在相同用户名的用户");
            }
            else
            {
                return true;
            }
        }
    }
}
