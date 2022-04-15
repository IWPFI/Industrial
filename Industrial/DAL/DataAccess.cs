using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Industrial.DAL
{
    public class DataAccess
    {
        private string strDBConfig = ConfigurationManager.ConnectionStrings["db_config"].ToString();
        SqlConnection conn = null;
        SqlCommand comm = null;
        SqlDataAdapter adapter = null;
        SqlTransaction trans = null;

        private static DataAccess instance = null;
        private DataAccess() { }
        public static DataAccess GetInstance()
        {
            lock ("da")
            {
                if (instance == null)
                    instance = new DataAccess();
                return instance;
            }
        }
        
        /// <summary>
        /// 资源回收
        /// </summary>
        /// <remarks>销毁作用</remarks>
        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (trans != null)
            {
                trans.Dispose();
                trans = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private bool DBConnection()
        {
            if (conn == null)
                conn = new SqlConnection(strDBConfig);
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable GetDatas(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (DBConnection())
                {
                    adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }

            return dt;
        }

        /// <summary>
        /// Gets the storage area.
        /// </summary>
        public DataTable GetStorageArea()
        {
            string strSql = "select * from storage_area";
            return GetDatas(strSql);
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        public DataTable GetDevices()
        {
            string strSql = "select * from devices";
            return GetDatas(strSql);
        }

        /// <summary>
        /// Gets the monitor values.
        /// </summary>
        public DataTable GetMonitorValues()
        {
            string strSql = "select * from monitor_values order by d_id,address";
            return GetDatas(strSql);
        }
    }
}
