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
    private String feature_type_dataset_xml_path = "feature_type.xml";
    private List<List<String>> filters;     //filters
    private List<String> usable_filters;    //filter categories
    private List<String> type_filters;      //filter types - Number or Text

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
                DataSet feature_type_dataset = new DataSet();
                feature_type_dataset.ReadXml(Server.MapPath(feature_type_dataset_xml_path));
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
                usable_filters = new List<String>();
                type_filters = new List<String>();
                filters = new List<List<String>>();

                foreach (DataRow row in ds.Tables[2].Rows)
                {
                    usable_filters.Add(row[feature_name].ToString().Trim());
                    filters.Add(new List<String>());
                    DataRow rr = feature_type_dataset.Tables[1].Rows[0];
                    String str = row[feature_name].ToString();
                    str = str.Trim();
                    type_filters.Add(rr[str].ToString().Trim());
                }

                /*** filters ***/
                ds = new DataSet();
                ds.ReadXml(Server.MapPath(filter_xml_path));
                List<String> tables = new List<String>();

                for (int ii = 0; ii < ds.Tables.Count; ii++)
                {
                    list_debug.Items.Add(ii.ToString());
                    list_debug.Items.Add(ds.Tables[ii].TableName);
                    foreach (DataColumn cc in ds.Tables[ii].Columns)
                    {
                        list_debug.Items.Add(cc.ColumnName);
                    }
                    list_debug.Items.Add("------------------");

                    tables.Add(ds.Tables[ii].TableName);
                }

                for (int ii = 0; ii < usable_filters.Count; ii++)
                {
                    if (type_filters[ii].Equals("Number"))
                    {
                        int index = -1;
                        for (int jj = 0; jj < tables.Count; jj++)
                        {
                            if (tables[jj].Equals(usable_filters[ii]))
                            {
                                index = jj;
                                break;
                            }
                        }
                        if(index != -1){
                            StringBuilder qq = new StringBuilder();
                            String high, low;
                            qq.Append("high_");
                            qq.Append(usable_filters[ii]);
                            DataRow row = ds.Tables[index].Rows[0];
                            high = row[qq.ToString()].ToString().Trim();
                            qq = new StringBuilder();
                            qq.Append("low_");
                            qq.Append(usable_filters[ii]);
                            low = row[qq.ToString()].ToString().Trim();
                            double low_i, high_i, range, next;
                            low_i = Convert.ToDouble(low);
                            next = low_i;
                            high_i = Convert.ToDouble(high);
                            range = (high_i - low_i) / 5;
                            next += range;
                            if (low_i == high_i)
                            {
                                filters[ii].Add(low);
                            }
                            else
                            {
                                while (next < high_i)
                                {
                                    StringBuilder range_str = new StringBuilder();
                                    range_str.Append(low_i.ToString());
                                    range_str.Append(" - ");
                                    range_str.Append(next.ToString());
                                    filters[ii].Add(range_str.ToString());
                                    low_i += range;
                                    next += range;
                                }
                                StringBuilder final_range_str = new StringBuilder();
                                final_range_str.Append(low_i.ToString());
                                final_range_str.Append(" - ");
                                final_range_str.Append(high_i.ToString());
                                filters[ii].Add(final_range_str.ToString());
                            }
                        }
                    }
                    else
                    {
                        int index = -1;
                        StringBuilder search_str = new StringBuilder();
                        search_str.Append("item_");
                        search_str.Append(usable_filters[ii]);
                        for (int jj = 0; jj < tables.Count; jj++)
                        {
                            if (tables[jj].Equals(search_str.ToString()))
                            {
                                index = jj;
                                search_str.Append("_Text");
                                break;
                            }
                        }

                        if (index == -1)
                        {
                            for (int jj = 0; jj < tables.Count; jj++)
                            {
                                if (tables[jj].Equals(usable_filters[ii]))
                                {
                                    index = jj;
                                    break;
                                }
                            }
                        }

                        if (index != -1)
                        {
                            foreach (DataRow row in ds.Tables[index].Rows)
                            {
                                filters[ii].Add(row[search_str.ToString()].ToString().Trim());
                            }
                        }


                    }
                }
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