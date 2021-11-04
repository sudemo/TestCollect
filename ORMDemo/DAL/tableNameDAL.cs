using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMDemo.DAL
{

    class tableNameDAL
    {
        MySqlHelper mySqlHelper;
        public tableNameDAL()
        {
            mySqlHelper = new MySqlHelper();
        }
        public bool AddUser(string name,string passwd)
        {
            try
            {
            string sql = "insert into t_user(Name,password,create_time) VALUES(@name,@password,@time)";
            MySqlParameter[] cmdParam = new MySqlParameter[]{
                //new SQLiteParameter("@p1",1),
                //new SQLiteParameter("@p1",11),
                new MySqlParameter("@name",name),
                new MySqlParameter("@password",passwd),
                new MySqlParameter("@time",DateTime.Now)
            };
            return mySqlHelper.ExecuteNonQuery(sql,cmdParam)>0;
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show("error:" + ex.Message);
  
                return false;
            }
        }
        public void DeleteUser(string name)
        {
            string sqlSelect = " DELETE FROM t_user WHERE name=@name";
            MySqlParameter parms = new MySqlParameter("@name", name);
            mySqlHelper.ExecuteNonQuery(sqlSelect,parms);
        }
        public void DeleteAllUser()
        {
            string sqlSelect = " DELETE  from t_user ";
            //MySqlParameter parms = new MySqlParameter("@name", name);
            mySqlHelper.ExecuteNonQuery(sqlSelect);
        }
        public List<object> QueryUser(string name)
        {
            try
            {
            string sql = "SELECT * FROM t_user where name=@name";
            MySqlParameter parms = new MySqlParameter("@name", name);
            var reader = mySqlHelper.ExecuteReader(sql,parms);
            
            List<object> res = new List<object>();
            while (reader.Read())
            {
                res.Add(reader[1]);
            }
            reader.Close();
            return res;
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("error:"+ex.Message);
                return null;
            }
        }
           
}
            
}



