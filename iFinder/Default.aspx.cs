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
    private String connectionString_filters = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security=True";
    private bool sql_defined = false;
    private bool rewrite_table = true;
    private String tableName;
    private String data_xml_path = "data.xml";
    private String usable_filter_xml_path = "usable_features.xml";
    private String filter_xml_path = "filters.xml";

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

                deleteTable(tableName, connectionString);
                List<String> cols = createTable(tableName, ds, connectionString);
                insertDataIntoTable(tableName, ds, cols, connectionString);


                /*** usable filters ***/
                ds = new DataSet();
                ds.ReadXml(Server.MapPath(usable_filter_xml_path));
                table_name = new StringBuilder();
  
                DataColumn col = ds.Tables[0].Columns[0];
                table_name.Append(col.ColumnName);
                table_name.Length -= 2;
                col = ds.Tables[1].Columns[0];
                table_name.Append(col.ColumnName);
                table_name.Length -= 2;
                table_name.Append("_filter_table");

                col = ds.Tables[2].Columns[0];
                String feature_name = col.ColumnName;
                List<String> usable_filters = new List<String>();

                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    usable_filters.Add(row[feature_name].ToString());
                }

                /*** filters ***/
                ds = new DataSet();
                ds.ReadXml(Server.MapPath(filter_xml_path));
                foreach (DataColumn ss in ds.Tables[4].Columns)
                    list_debug.Items.Add(ss.ColumnName);

                sql_defined = true;
            }
            catch (Exception ex)
            {
                debug.Text = "Error in loading database";
            }
        }   
    }

    private void deleteTable(String tableName, String conn_string)
    {
        SqlConnection sqlConn = new SqlConnection(conn_string);
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

    private List<String> createTable(String tableName, DataSet data, String conn_string)
    {
        SqlConnection sqlConn = new SqlConnection(conn_string);
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

    private void insertDataIntoTable(String tableName, DataSet data, List<String> cols, String conn_string)
    {
        foreach (DataRow row in data.Tables[2].Rows)
        {
            SqlConnection sqlConn = new SqlConnection(conn_string);
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