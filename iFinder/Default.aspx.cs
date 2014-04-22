using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
{

    private String connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database2.mdf;Integrated Security=True";
    private bool sql_defined = false;
    private bool rewrite_table = true;
    private String tableName;
    private String data_xml_path = "data.xml";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!sql_defined || rewrite_table)
        {
            try
            {
                DataSet ds = new DataSet();

                ds.ReadXml(Server.MapPath(data_xml_path));
                String type = "", name = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    type = row["ProductType"].ToString();
                    name = row["ProductName"].ToString();
                }

                StringBuilder table_name = new StringBuilder();
                table_name.Append(type);
                table_name.Append("_");
                table_name.Append(name);
                table_name.Append("_table");

                tableName = table_name.ToString();

                deleteTable(tableName);
                List<String> cols = createTable(tableName, ds);
                insertDataIntoTable(tableName, ds, cols);
                sql_defined = true;
            }
            catch (Exception ex)
            {
                debug.Text = "Error in loading database";
            }
        }   
    }

    private void deleteTable(String tableName)
    {
        SqlConnection sqlConn = new SqlConnection(connectionString);
        StringBuilder delete_table_query = new StringBuilder();
        sqlConn.Open();
        delete_table_query.Append("IF OBJECT_ID ( '");
        delete_table_query.Append(tableName.ToString());
        delete_table_query.Append("', 'U') IS NOT NULL DROP TABLE ");
        delete_table_query.Append(tableName.ToString());
        SqlCommand sqlQuery = new SqlCommand(delete_table_query.ToString(), sqlConn);
        sqlQuery.ExecuteNonQuery();
        sqlConn.Close();
    }

    private List<String> createTable(String tableName, DataSet data)
    {
        SqlConnection sqlConn = new SqlConnection(connectionString);
        StringBuilder query = new StringBuilder();
        query.Append("CREATE TABLE ");
        query.Append(tableName);
        query.Append(" ( ");

        List<String> cols = new List<String>();

        foreach (DataColumn col in data.Tables[2].Columns)
        {
            query.Append(col.ColumnName);
            cols.Add(col.ColumnName);
            query.Append(" nvarchar(max), ");
        }

        query.Length -= 2;
        query.Append(")");

        sqlConn.Open();
        SqlCommand sqlQuery = new SqlCommand(query.ToString(), sqlConn);
        sqlQuery.ExecuteNonQuery();
        sqlConn.Close();
        return cols;
    }

    private void insertDataIntoTable(String tableName, DataSet data, List<String> cols)
    {
        foreach (DataRow row in data.Tables[2].Rows)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString);
            StringBuilder insert_query = new StringBuilder();
            insert_query.Append("INSERT INTO ");
            insert_query.Append(tableName);
            insert_query.Append(" VALUES (");
            foreach (String str in cols)
            {
                insert_query.Append("@");
                insert_query.Append(str);
                insert_query.Append(", ");
            }
            insert_query.Length -= 2;
            insert_query.Append(");");
            sqlConn.Open();
            SqlCommand sqlQuery = new SqlCommand(insert_query.ToString(), sqlConn);
            foreach (String str in cols)
            {
                StringBuilder parameter_name = new StringBuilder();
                parameter_name.Append("@");
                parameter_name.Append(str);
                sqlQuery.Parameters.AddWithValue(parameter_name.ToString(), row[str].ToString());
            }
            sqlQuery.ExecuteNonQuery();
            sqlConn.Close();
        }
    }

    private void searchQuery()
    {

    }
}