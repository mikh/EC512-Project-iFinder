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
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        
        ds.ReadXml(Server.MapPath("data.xml"));
        String type = "", name = "";
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            type = row["ProductType"].ToString();
            name = row["ProductName"].ToString();
        }
        StringBuilder query = new StringBuilder(), table_name = new StringBuilder(), delete_table_query = new StringBuilder();
        String connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database2.mdf;Integrated Security=True";
        SqlConnection sqlConn = new SqlConnection(connectionString);
        SqlCommand sqlQuery;
        SqlDataReader reader;
        

        table_name.Append(type);
        table_name.Append("_");
        table_name.Append(name);
        table_name.Append("_table");

        /*** Need to remove the table if it already exists ***/
        sqlConn.Open();
        delete_table_query.Append("IF OBJECT_ID ( '");
        delete_table_query.Append(table_name.ToString());
        delete_table_query.Append("', 'U') IS NOT NULL DROP TABLE ");
        delete_table_query.Append(table_name.ToString());
        sqlQuery = new SqlCommand(delete_table_query.ToString(), sqlConn);
        reader = sqlQuery.ExecuteReader();
        sqlConn.Close();
        /*** Table removed ***/
        

        /*** Create the table ***/
        query.Append("CREATE TABLE ");
        query.Append(type);
        query.Append("_");
        query.Append(name);
        query.Append("_table ( ");

        List<String> cols = new List<String>();
        
        foreach (DataColumn col in ds.Tables[2].Columns)
        {
            query.Append(col.ColumnName);
            cols.Add(col.ColumnName);
            query.Append(" nvarchar(max), ");
        }

        query.Length -= 2;
        query.Append(")");

        sqlConn.Open();
        sqlQuery = new SqlCommand(query.ToString(), sqlConn);
        reader = sqlQuery.ExecuteReader();
        sqlConn.Close();
        /*** Table Created ***/


        /*** Insert Data Values ***/
        
        foreach (DataRow row in ds.Tables[2].Rows)
        {
            StringBuilder insert_query = new StringBuilder();
          //  insert_query.Append("INSERT INTO ");
          //  insert_query.Append(table_name);
          //  insert_query.Append(" (ID) VALUES (HEllo);");
          //  insert_query.Append("'(");
          //  insert_query.Append("ID) VALUES (HEllo);");
         /*   foreach (String str in cols)
            {
                insert_query.Append(str);
                insert_query.Append(", ");
            }
            insert_query.Length -= 2;
            insert_query.Append(") VALUES (");
            foreach (String str in cols)
            {
              //  insert_query.Append("@");
              //  insert_query.Append(str);
               // insert_query.Append(", ");
                insert_query.Append(row[str].ToString());
                insert_query.Append(", ");
            } 

            insert_query.Length -= 2; 
            insert_query.Append(")"); */
          //  sqlConn.Open();
          //  sqlQuery = new SqlCommand(insert_query.ToString(), sqlConn); 
          /*  foreach (String str in cols)
            {
                StringBuilder parameter_name = new StringBuilder();
                parameter_name.Append("@");
                parameter_name.Append(str);
                
            }*/
         //   sqlQuery.ExecuteNonQuery();
           // reader = sqlQuery.ExecuteReader();
        //    sqlConn.Close();
            insert_query.Append("INSERT INTO ");
            insert_query.Append(table_name);
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
            sqlQuery = new SqlCommand(insert_query.ToString(), sqlConn);
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
        /*** Done Inserting ***/

        debug.Text = "database loaded";
    }
}