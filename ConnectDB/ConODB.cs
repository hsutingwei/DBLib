using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConnectDB
{
    /// <summary>
    /// SQL Server操作
    /// </summary>
    public class ConODB
    {
        /// <summary>
        /// 設定DB連結字串
        /// </summary>
        public string conDbStr = @"";
        private OracleConnection con = new OracleConnection();
        private OracleCommand cmd = new OracleCommand();
        /// <summary>
        /// 建立(初始化)與DB的連結物件
        /// </summary>
        /// <param name="sqlStr">SQL字串</param>
        public void InitConnAndSetSQL(string sqlStr)
        {
            try
            {
                con = new OracleConnection(conDbStr);
                cmd = new OracleCommand(sqlStr, con);
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
                CloseConn();
                throw e;
            }
        }
        /// <summary>
        /// 設定/重設SQL的參數
        /// </summary>
        /// <param name="ParameterArray"></param>
        public void SetOrReSetParameter(OracleParameter[] ParameterArray)
        {
            try
            {
                cmd.BindByName = true;
                cmd.Parameters.Clear();
                if (ParameterArray.Count() > 0)
                    cmd.Parameters.AddRange(ParameterArray);
            }
            catch (Exception e)
            {
                CloseConn();
                throw e;
            }
        }
        /// <summary>
        /// 執行SQL語句並取得回傳值(List string)
        /// </summary>
        /// <returns></returns>
        public List<string> DoSqlAndGetResultToList()
        {
            try
            {
                List<string> bList = new List<string>();

                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StringBuilder tmpStr = new StringBuilder();
                    for (int j = 0; ; j++)
                    {
                        try
                        {
                            tmpStr.Append(dt.Rows[i][j].ToString().Replace(",", "，") + ",");
                        }
                        catch
                        {
                            break;
                        }
                    }
                    string reStr = tmpStr.ToString();
                    int len = reStr.Length;
                    if (len > 0)
                        reStr = reStr.Substring(0, len - 1);
                    bList.Add(reStr);
                }

                return bList;
            }
            catch (Exception e)
            {
                CloseConn();
                throw e;
            }
        }
        /// <summary>
        /// 執行SQL語句並取得回傳值(DataTable)
        /// </summary>
        /// <returns></returns>
        public DataTable DoSqlAndGetResultToDataTable()
        {
            try
            {
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = cmd;

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception e)
            {
                CloseConn();
                throw e;
            }
        }
        /// <summary>
        /// 關閉釋放DB的連結物件
        /// </summary>
        public void CloseConn()
        {
            cmd.Dispose();
            con.Close();
            con.Dispose();

            cmd = null;
            con = null;
        }
    }
}
