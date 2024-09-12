using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openCV0820
{
    internal class DBConnect
    {
        string Connecting = "server=172.31.6.28,1433;" +
            $"database=ProductList;" +
                $"uid=sa;" +
                $"pwd=12345;";

        SqlConnection conn;
        SqlDataAdapter dataAdapter;
        DataSet dataSet;
        public DataTable dt;
        public string msg = "";//메세지
        public string dbresult = "";//디비 결과

        public void connect()
        {
            try
            {
                conn = new SqlConnection(Connecting);
                conn.Open();
                if (conn.State == ConnectionState.Open) msg = "연결";
                else msg = "실패";
            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }

        }
        public void insert(string company, string product, string place, DateTime yearMonth)
        {
            try
            {
                dataAdapter = new SqlDataAdapter();
                dt = new DataTable();
                dataSet = new DataSet();

                if (conn.State != ConnectionState.Open) conn.Open();

                string queryString = "INSERT INTO Table_2 (company, product, place, yearMonth)" +
                    "VALUES (@company, @product, @place, @yearMonth)";

                dataAdapter.InsertCommand = new SqlCommand(queryString, conn);
                dataAdapter.InsertCommand.Parameters.AddWithValue("@company", company);
                dataAdapter.InsertCommand.Parameters.AddWithValue("@product", product);
                dataAdapter.InsertCommand.Parameters.AddWithValue("@place", place);
                dataAdapter.InsertCommand.Parameters.AddWithValue("@yearMonth", yearMonth);

                dataAdapter.InsertCommand.ExecuteNonQuery();

                string selectQuery = "SELECT * FROM Table_2";
                dataAdapter.SelectCommand = new SqlCommand(selectQuery, conn);

                dataSet.Clear();
                dataAdapter.Fill(dataSet, "Table_2");
                dt = dataSet.Tables["Table_2"];

            }
            catch (Exception ex)
            {
                msg = $"{ex.Message} : 입력 부분";
            }
            finally
            {
                conn.Close();
                dataAdapter.InsertCommand.Dispose();
            }

        }

        public void select(string company)
        {
            try
            {
                dataAdapter = new SqlDataAdapter();
                dt = new DataTable();
                dataSet = new DataSet();

                conn.Open();

                string queryString = "select * from Table_2 where company = @company";

                dataAdapter.SelectCommand = new SqlCommand(queryString, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@company", company);

                dataSet.Clear();
                dataAdapter.Fill(dataSet, "Table_2");
                dt = dataSet.Tables["Table_2"];

            }
            catch (Exception ex)
            {
                msg = $"{ex.Message} : 조회 부분";
            }
            finally
            {
                conn.Close();
                dataAdapter.SelectCommand.Dispose();
            }

        }

    }
}
