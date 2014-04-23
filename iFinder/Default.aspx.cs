﻿using System;
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
    private List<String> categories = new List<String>();
    private List<List<String>> types = new List<List<String>>();
    private List<String> notation = new List<String>();

    private List<String> search_results_notation = new List<String>();
    private List<List<String>> search_results = new List<List<String>>();

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
                categories.Add(type);
                types.Add(new List<String>());
                types[types.Count - 1].Add(name);

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
                List<List<bool>> filter_test = new List<List<bool>>();
                for (int ii = 0; ii < filters.Count; ii++)
                {
                    filter_test.Add(new List<bool>());
                    for (int jj = 0; jj < filters[ii].Count; jj++)
                    {
                        filter_test[ii].Add(false);
                    }
                }
                filter_test[0][0] = true;
                filter_test[0][1] = true;
                searchQuery("resistor", filter_test, connectionString);
               // debug.Text = search_results.Count.ToString();
            }
            catch (Exception ex)
            {
                //debug.Text = "Error in loading database";
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
            notation.Add(col.ColumnName);
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

    private bool searchQuery(String search, List<List<bool>> filter_data, String conn_string){
        //first see if you can classify the search
        //so break the search up
        List<String> search_words = new List<String>();
        StringBuilder word = new StringBuilder();
        for (int ii = 0; ii < search.Length; ii++)
        {
            if (search[ii] == ' ' || search[ii] == '\t')
            {
                if (word.Length != 0)
                {
                    search_words.Add(word.ToString());
                    word = new StringBuilder();
                }
            }
            else
            {
                word.Append(search[ii]);
            }
        }
        if (word.Length != 0)
            search_words.Add(word.ToString());

        //try to match words to categories
        List<bool> categories_matched = new List<bool>();
        for(int jj = 0; jj < search_words.Count; jj++)
        {
            for (int ii = 0; ii < categories.Count; ii++)
            {
                if (categories[ii].Equals(search_words[jj]))
                    categories_matched.Add(true);
                else
                    categories_matched.Add(false);
            }
        }

        //try to match words to type
        List<List<bool>> types_matched = new List<List<bool>>();
        for (int ii = 0; ii < search_words.Count; ii++)
        {
            for (int jj = 0; jj < types.Count; jj++)
            {
                types_matched.Add(new List<bool>());
                for (int kk = 0; kk < types[jj].Count; kk++)
                {
                    if (types[jj][kk].Equals(search_words[ii]))
                        types_matched[jj].Add(true);
                    else
                        types_matched[jj].Add(false);
                }
            }
        }

        //count up tables to search
        List<String> tables_to_search = new List<String>();
        for (int ii = 0; ii < categories_matched.Count; ii++)
        {
            if (categories_matched[ii] == true)
            {
                StringBuilder cat = new StringBuilder();
                cat.Append(categories[ii]);
                cat.Append("_");
                for (int jj = 0; jj < types[ii].Count; jj++)
                {
                    StringBuilder typ = new StringBuilder();
                    typ.Append(cat.ToString());
                    typ.Append(types[ii][jj]);
                    typ.Append("_table");
                    tables_to_search.Add(typ.ToString());
                }
            }
        }

        for (int ii = 0; ii < types_matched.Count; ii++)
        {
            for (int jj = 0; jj < types_matched[ii].Count; jj++)
            {
                if (types_matched[ii][jj] == true)
                {
                    StringBuilder tab = new StringBuilder();
                    tab.Append(categories[ii]);
                    tab.Append("_");
                    tab.Append(types[ii][jj]);
                    tab.Append("_table");
                    tables_to_search.Add(tab.ToString());
                }
            }
        }

        //remove duplicates
        tables_to_search = removeDuplicates(tables_to_search);

        //check if any tables to search
        if (tables_to_search.Count == 0)
            return false;

        //construct search query
        for (int ii = 0; ii < tables_to_search.Count; ii++)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT * FROM ");
            query.Append(tables_to_search[ii]);
            SqlConnection sqlConn = new SqlConnection(conn_string);
            sqlConn.Open();
            SqlCommand sqlQuery = new SqlCommand(query.ToString(), sqlConn);
            SqlDataReader reader = sqlQuery.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Object[] values = new Object[reader.FieldCount];
                    int fieldCount = reader.GetValues(values);
                    List<String> results = new List<String>();
                    for (int jj = 0; jj < reader.FieldCount; jj++)
                    {
                        results.Add(values[jj].ToString());
                    }
                    search_results.Add(results);
                }
                
                search_results_notation = notation;
                filterSearchResults(filter_data);
            }
            sqlConn.Close();
        }

        return true;
    }

    private void filterSearchResults(List<List<bool>> filter_data)
    {
        //do it 1 filter at a time
        for (int ii = 0; ii < filter_data.Count; ii++)
        {
            bool need_filter = false;
            for(int jj = 0; jj < filter_data[ii].Count; jj++){
                if(filter_data[ii][jj] == true){
                    need_filter = true;
                    break;
                }
            }


            if(need_filter){
                List<bool> to_filter = filter_data[ii];
                List<List<String>> new_results = new List<List<string>>();
                List<String> filter = filters[ii];
                String type = type_filters[ii];
                String filter_category = usable_filters[ii];
                int index = -1;
                for (int kk = 0; kk < search_results_notation.Count; kk++)
                {
                    if(search_results_notation[kk].Equals(filter_category)){
                        index = kk;
                        break;
                    }
                }

                if(index != -1){
                    for(int kk = 0; kk < search_results.Count; kk++){
                        bool pass = false;
                        for (int jj = 0; jj < to_filter.Count; jj++)
                        {
                            if (to_filter[jj] == true && type.Equals("Text"))
                            {
                                String sear_resul_str = search_results[kk][index].ToString().Trim();
                                String filter_str = filter[jj].ToString().Trim();
                                if (sear_resul_str.Equals(filter_str))
                                {
                                    pass = true;
                                    break;
                                }
                            }
                            else if (to_filter[jj] == true)
                            {
                                List<String> ranges = new List<String>();
                                StringBuilder word = new StringBuilder();
                                for (int qq = 0; qq < filter[jj].Length; qq++)
                                {
                                    if (filter[jj][qq] == ' ' || filter[jj][qq] == '\t')
                                    {
                                        if (word.Length != 0)
                                        {
                                            ranges.Add(word.ToString());
                                            word = new StringBuilder();
                                        }
                                    }
                                    else
                                    {
                                        word.Append(filter[jj][qq]);
                                    }
                                }
                                if (word.Length != 0)
                                    ranges.Add(word.ToString());
                                double low = Convert.ToDouble(ranges[0]);
                                double high = Convert.ToDouble(ranges[2]);
                                double ss = Convert.ToDouble(search_results[kk][index]);
                                if (ss >= low && ss <= high)
                                {
                                    pass = true;
                                    break;
                                }
                            }
                        }
                        if (pass)
                        {
                            new_results.Add(search_results[kk]);
                        }
                    }
                }
                search_results = new_results;
            }
        }
    }

    private List<String> removeDuplicates(List<String> list)
    {
        List<String> noDupes = new List<String>();
        while (list.Count > 0)
        {
            String word = list[list.Count-1];
            list.RemoveAt(list.Count - 1);
            noDupes.Add(word);
            for (int ii = list.Count - 1; ii >= 0; ii--)
            {
                if (list[ii].Equals(word))
                    list.RemoveAt(ii);
            }
        }
        return noDupes;
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (userName.Text == "" || passWord.Text == "")
        {
            message_label.Text = "Please Enter a Username and a Password";
        }
        else
        {

        }
    }
}