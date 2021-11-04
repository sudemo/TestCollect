
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace ORMDemo
{
    public class MySqlHelper
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        static MySqlConnection conn; //一次创建
        static MySqlCommand command;

        public MySqlHelper()
        {
            GetConnection();
        }
        /// <summary> 
        /// 获取一个有效的数据库连接对象 
        /// </summary> 
        /// <returns></returns> 
        public MySqlConnection GetConnection()
        {
            conn = new MySqlConnection(connectionString);
            return conn;
        }

        /// <summary> 
        /// 给定连接的数据库用假设参数执行一个sql命令（不返回数据集） 
        /// </summary> 

        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public int ExecuteNonQuery(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            //using (MySqlConnection conn = new MySqlConnection(connectionString))
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                {
                    PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    return val;
                }
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }

        /// <summary> 
        ///使用现有的SQL事务执行一个sql命令（不返回数据集） 
        /// </summary> 
        /// <remarks> 
        ///举例: 
        /// int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="trans">一个现有的事务</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>执行命令所影响的行数</returns> 
        public int ExecuteNonQuery(MySqlTransaction trans, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            return val;
        }

        /// <summary> 
        /// 用执行的数据库连接执行一个返回数据集的sql命令 
        /// </summary> 
        /// <remarks> 
        /// 举例: 
        /// MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 

        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>包含结果的读取器</returns> 
        public MySqlDataReader ExecuteReader(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return reader;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
            finally
            {
                //conn.Close();前面设置了执行完后 就close
            }


        }

        /// <summary> 
        /// 返回DataSet 
        /// </summary> 
        /// <param name="connectionString">一个有效的连接字符串</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns></returns> 
        public DataSet GetDataSet(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据表 
        /// </summary>
        ///<param name="connectionString">一个有效的连接字符串</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        public DataTable GetDataTable(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdText, commandParameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable ds = new DataTable();

                adapter.Fill(ds);
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary> 
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列 
        /// </summary> 
        /// <remarks> 
        ///例如: 
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        ///<param name="connectionString">一个有效的连接字符串</param> 
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param> 
        /// <param name="cmdText">存储过程名称或者sql命令语句</param> 
        /// <param name="commandParameters">执行命令所用参数的集合</param> 
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns> 
        public object ExecuteScalar(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 返回插入值ID
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public object ExecuteNonExist(string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdText, commandParameters);
                object val = cmd.ExecuteNonQuery();

                return cmd.LastInsertedId;
            }
        }


        /// <summary> 
        /// 准备执行一个命令 
        /// </summary> 
        /// <param name="cmd">sql命令</param> 
        /// <param name="conn">OleDb连接</param> 
        /// <param name="trans">OleDb事务</param> 
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param> 
        /// <param name="cmdText">命令文本,例如:Select * from Products</param> 
        /// <param name="cmdParms">执行命令的参数</param> 
        private void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            //using (conn = new MySqlConnection(connectionString))
            //{
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = CommandType.Text;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
            //}
        }

        public void CreateTable()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string sql =
                "  CREATE TABLE `t_user2` (" +
                "id INT(10) NOT NULL AUTO_INCREMENT," +
                "`Name` VARCHAR(45) NOT NULL COLLATE 'utf8mb4_unicode_ci'," +
                "`password` VARCHAR(45) NOT NULL COLLATE 'utf8mb4_unicode_ci'," +
                "`create_time` DATETIME(3) NULL DEFAULT CURRENT_TIMESTAMP(3)," +
                "PRIMARY KEY(`id`) USING BTREE," +
                "UNIQUE INDEX `Name_UNIQUE` (`Name`) USING BTREE," +
                "UNIQUE INDEX `id_UNIQUE` (`id`) USING BTREE" +
                ")" +
                "COLLATE = 'utf8mb4_unicode_ci'" +

                "AUTO_INCREMENT = 0;";
            GetConnection();
            ExecuteNonQuery(sql);
        }
    }
}