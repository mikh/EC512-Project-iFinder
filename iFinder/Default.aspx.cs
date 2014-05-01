using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{

    String connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database2.mdf;Integrated Security=True";

    string[] data_xml_paths = { "electronics_resistor.xml" };
    string[] filter_xml_paths = { "electronics_resistor_filters.xml" };

    List<List<List<List<String>>>> filters;     //filters
    List<String> categories;    //name of all categories available
    List<List<String>> types;       //product types in those categories
    List<List<List<String>>> filter_names;  //usable features from notation
    List<List<List<String>>> filter_types;  //types of usable features
    List<List<List<String>>> notation;      //names of all fields of data
    List<List<CheckBox>> list_of_boxes;
    int active_cat, active_prod;
    List<List<bool>> active_filters;



    List<String> search_results_notation;
    List<List<String>> search_results;
    Table tbl_filters = new Table();

    protected void Page_PreInit(Object sender, EventArgs e)
    {

        this.EnsureChildControls();

        session_Init();
        table_Init();
        filter_Init();
        notation_Init();
        bool force_postback;
        try
        {
            force_postback = (bool)Session["force_postback"];
            if (force_postback)
            {
                //dynamicFilters_Init();
                //search_results = (List<List<String>>)Session["search_results"];
                //search_results_notation = (List<String>)Session["search_results_notation"];
                //search_bar.Text = (string)Session["search_str"];
                //results_label.Text = (string)Session["results_label"];
                ////Session["force_postback"] = false;
            }
        }
        catch (Exception ex)
        {
            Session["force_postback"] = false;
        }
        if (IsPostBack)
        {
            dynamicFilters_Init();
            //search_results = (List<List<String>>)Session["search_results"];
            //search_results_notation = (List<String>)Session["search_results_notation"];
        }
    }

    private void session_Init()
    {
        Session.Timeout = 525600;       //five hundred twenty five thousand six hundred minutes 
        Object obj;

        obj = Session["active_cat"];
        if (obj == null) Session["active_cat"] = -1;
        obj = Session["active_prod"]; if (obj == null) Session["active_prod"] = -1;
        obj = Session["force_postback"]; if (obj == null) Session["force_postback"] = false;
        obj = Session["search_str"]; if (obj == null) Session["search_str"] = "";
        obj = Session["results_label"]; if (obj == null) Session["results_label"] = "";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CartLink.Text = "My Cart [" + cartCount + "]"; 
        ball_image.ImageUrl = "bouncy-ball.png";
        ball_image.Visible = false;
        bool force_postback = (bool)Session["force_postback"];

        if (!IsPostBack && !force_postback)
        {
            Session.Timeout = 525600;       //five hundred twenty five thousand six hundred minutes 
            active_cat = -1;
            active_prod = -1;
            Session["active_cat"] = active_cat;
            Session["active_prod"] = active_prod;

            active_filters = null;
            Session["active_filters"] = active_filters;
            Session["search_results"] = search_results;
            Session["search_results_notation"] = search_results_notation;
            Session["force_postback"] = false;
            Session["search_str"] = "";
            Session["results_label"] = "";
            dynamicFilters_Init();
            ball_image.Visible = true;

        }

       // results_repeater.DataSource = search_results;
       // results_repeater.DataBind();

        search_results = (List<List<String>>)Session["search_results"];
        if (search_results == null || search_results.Count == 0)
            ball_image.Visible = true;

        if (force_postback)
            Session["force_postback"] = false;



        //**********************USER AUTHENTICATION CODE**********************//
        if (User.Identity.IsAuthenticated)
        {
            logged_in.Text = User.Identity.Name;
            user_label.Visible = false;
            userName.Visible = false;
            passWord.Visible = false;
            password_label.Visible = false;
            Login.Visible = false;
            Register.Visible = false;
            message_label.Text = "Welcome";
            logged_in.Visible = true;
            bLogout.Visible = true;
            iForgotPass.Visible = false;
        }
        else
        {
            user_label.Visible = true;
            userName.Visible = true;
            passWord.Visible = true;
            password_label.Visible = true;
            Login.Visible = true;
            Register.Visible = true;
            message_label.Text = "";
            logged_in.Visible = false;
            bLogout.Visible = false;
            iForgotPass.Visible = true;
        }
        try
        {
            //Check for Cookies
            HttpCookie userInfoCookies = Request.Cookies["UserName"];
            string userNameCookie;
            if (userInfoCookies != null)
            {
                userNameCookie = userInfoCookies["UserName"];
                //check if userName is currently logged.

            }
            else
            {
                //Setting values inside it
                userInfoCookies = new HttpCookie("UserName");
                userInfoCookies["UserName"] = "Guest";
                userInfoCookies["Expire"] = "5 Days";

                //Adding Expire Time of cookies
                userInfoCookies.Expires = DateTime.Now.AddDays(5);

                //Adding cookies to current web response
                Response.Cookies.Add(userInfoCookies);
            }
        }
        catch (Exception ex)
        {
            message_label.Text = "Error with cookies";
        }
        //****************************************************END USER AUTHENTICATION CODE*************************************//
    }

    private void dynamicFilters_Init()
    {
        int cat_index = (int)Session["active_cat"];
        int prod_index = (int)Session["active_prod"];

        if (cat_index != -1 && prod_index != -1)
        {
            list_of_boxes = new List<List<CheckBox>>();
            ContentPlaceHolder _cpl1 = Master.FindControl("items") as ContentPlaceHolder;
            PlaceHolder _pl1 = _cpl1.FindControl("PlaceHolder1") as PlaceHolder;
            PlaceHolder controlsParent = new PlaceHolder();
            _pl1.Controls.Add(controlsParent);
            active_filters = (List<List<bool>>)Session["active_filters"];

            for (int ii = 0; ii < filters[cat_index][prod_index].Count; ii++)
            {
                Label filterHeader = new Label();
                filterHeader.Attributes.CssStyle.Add("font-size", "100%");
                filterHeader.Attributes.CssStyle.Add("font-weight", "bold");
                filterHeader.Attributes.CssStyle.Add("color", "#266A2E");
                filterHeader.Text = UppercaseFirst(CorrectString(filter_names[cat_index][prod_index][ii]));
                filterHeader.ID = filter_names[cat_index][prod_index][ii];
                controlsParent.Controls.Add(filterHeader);
                controlsParent.Controls.Add(new LiteralControl("<br />"));
                list_of_boxes.Add(new List<CheckBox>());
                for (int jj = 0; jj < filters[cat_index][prod_index][ii].Count; jj++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Attributes.CssStyle.Add("margin", "0px 0px 6px 0px");
                    cb.Attributes.CssStyle.Add("padding", "0px 0px 6px 0px");
                    cb.Attributes.CssStyle.Add("list-style-type", "none");
                    cb.Attributes.CssStyle.Add("list-style-position", "outside");
                    cb.Attributes.CssStyle.Add("font-size", "100%");
                    cb.Attributes.CssStyle.Add("text-align", "left");
                    cb.ID = "CheckBox_" + ii.ToString() + "_" + jj.ToString() + "_";
                    cb.Text = filters[cat_index][prod_index][ii][jj];
                    if (active_filters[ii][jj] == true)
                        cb.Checked = true;
                    else
                        cb.Checked = false;
                    cb.CheckedChanged += new EventHandler(this.checkChanged);
                    controlsParent.Controls.Add(cb);
                    controlsParent.Controls.Add(new LiteralControl("<br />"));
                    list_of_boxes[ii].Add(cb);
                }
                controlsParent.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    private void checkChanged(object sender, EventArgs e)
    {
        CheckBox c_box = (CheckBox)sender;
        string name = c_box.ID;
        StringBuilder word = new StringBuilder();
        int index = 0;
        while (name[index] != '_')
            index++;
        index++;
        while (name[index] != '_')
        {
            word.Append(name[index++]);
        }

        int index_ii = Convert.ToInt32(word.ToString());
        word = new StringBuilder();
        index++;
        while (name[index] != '_')
        {
            word.Append(name[index++]);
        }
        int index_jj = Convert.ToInt32(word.ToString());
        active_filters = (List<List<bool>>)Session["active_filters"];
        active_filters[index_ii][index_jj] = c_box.Checked;
        Session["active_filters"] = active_filters;
        // results_label.Text = "BOXED CHECKED_" + index_ii.ToString() + "_" + index_jj.ToString();
        // results_label.Text = "BOXED CHECKED";
    }

    private void updateActiveFilters()
    {
        active_filters = (List<List<bool>>)Session["active_filters"];
        if (list_of_boxes != null && active_filters != null)
        {
            for (int ii = 0; ii < list_of_boxes.Count; ii++)
            {
                for (int jj = 0; jj < list_of_boxes[ii].Count; jj++)
                {
                    CheckBox c_box = list_of_boxes[ii][jj];
                    string name = c_box.ID;
                    StringBuilder word = new StringBuilder();
                    int index = 0;
                    while (name[index] != '_')
                        index++;
                    index++;
                    while (name[index] != '_')
                    {
                        word.Append(name[index++]);
                    }

                    int index_ii = Convert.ToInt32(word.ToString());
                    word = new StringBuilder();
                    index++;
                    while (name[index] != '_')
                    {
                        word.Append(name[index++]);
                    }
                    int index_jj = Convert.ToInt32(word.ToString());
                    active_filters = (List<List<bool>>)Session["active_filters"];
                    active_filters[index_ii][index_jj] = c_box.Checked;
                    Session["active_filters"] = active_filters;
                }
            }
        }
    }

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    static string CorrectString(string s)
    {
        string newStr;
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] delimiterChars = { '_' };
        string[] words = s.Split(delimiterChars);
        if (words.Length > 1)
        {
            newStr = words[0] + ' ' + '(' + words[1] + ')';
            return newStr;
        }
        else
            return s;
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

    private bool searchQuery(String search, String conn_string)
    {
        search_results = new List<List<String>>();
        Session["search_results"] = search_results;
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
        for (int jj = 0; jj < search_words.Count; jj++)
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
        {
            Session["active_cat"] = -1;
            Session["active_prod"] = -1;
            Session["active_filters"] = null;
            Session["search_results"] = search_results;
            Session["search_results_notation"] = search_results_notation;
            Session["search_str"] = search_bar.Text;
            Session["force_postback"] = true;
            Session["results_label"] = results_label.Text;
            updateActiveFilters();
            Response.Redirect("Default.aspx");
            return false;
        }

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
                int cat_index = findCategory(getTableCategory(tables_to_search[ii]));
                int prod_index = findProduct(getTableProduct(tables_to_search[ii]), cat_index);
                if (cat_index == -1 || prod_index == -1)
                    throw new Exception();
                Session["active_cat"] = cat_index;
                Session["active_prod"] = prod_index;

                active_filters = (List<List<bool>>)Session["active_filters"];
                if (active_filters == null)
                {
                    active_filters = new List<List<bool>>();
                    for (int tt = 0; tt < filters[cat_index][prod_index].Count; tt++)
                    {
                        active_filters.Add(new List<bool>());
                        for (int yy = 0; yy < filters[cat_index][prod_index][tt].Count; yy++)
                            active_filters[tt].Add(false);
                    }
                    Session["active_filters"] = active_filters;

                    dynamicFilters_Init();
                }



                search_results_notation = notation[cat_index][prod_index];
                filterSearchResults(active_filters, cat_index, prod_index);
                Session["search_results"] = search_results;
                Session["search_results_notation"] = search_results_notation;
                StringBuilder res = new StringBuilder();
                res.Append(search_results.Count.ToString());
                res.Append(" results found.");
                results_label.Text = res.ToString();          //************************************************RESULTS LABEL**************************//
                Session["search_str"] = search_bar.Text;
                Session["force_postback"] = true;
                Session["results_label"] = results_label.Text;
                updateActiveFilters();
                results_repeater.DataSource = search_results;
                results_repeater.DataBind();
                dynamicFilters_Init();
                search_results = (List<List<String>>)Session["search_results"];
                search_results_notation = (List<String>)Session["search_results_notation"];
                search_bar.Text = (string)Session["search_str"];
                results_label.Text = (string)Session["results_label"];
                //Session["force_postback"] = false;
                //Response.Redirect("Default.aspx");
            }
            sqlConn.Close();
        }

        return true;
    }

    private void filterSearchResults(List<List<bool>> filter_data, int cat_index, int prod_index)
    {
        //do it 1 filter at a time
        for (int ii = 0; ii < filter_data.Count; ii++)
        {
            bool need_filter = false;
            for (int jj = 0; jj < filter_data[ii].Count; jj++)
            {
                if (filter_data[ii][jj] == true)
                {
                    need_filter = true;
                    break;
                }
            }


            if (need_filter)
            {
                List<bool> to_filter = filter_data[ii];
                List<List<String>> new_results = new List<List<string>>();
                List<String> filter = filters[cat_index][prod_index][ii];// filters[ii];                  
                String type = filter_types[cat_index][prod_index][ii];
                String filter_category = filter_names[cat_index][prod_index][ii];
                int index = -1;
                for (int kk = 0; kk < search_results_notation.Count; kk++)
                {
                    if (search_results_notation[kk].Equals(filter_category))
                    {
                        index = kk;
                        break;
                    }
                }

                if (index != -1)
                {
                    for (int kk = 0; kk < search_results.Count; kk++)
                    {
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
            String word = list[list.Count - 1];
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
        try  //catches blank User name
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //status
            }
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord.Text, "SHA1");
            DataRow row = dv.Table.Rows[0];
            string temppass = (string)row["Password"];
            if (temppass == hashpass)
            {
                //authenticated
                FormsAuthentication.RedirectFromLoginPage(userName.Text, false);
                message_label.Text = "Login OK.";
                Session["force_postback"] = true;
                return;
            }

        }
        catch
        {
            //Not authenticated
            message_label.Text = "Error with authentication.";
        }
        message_label.Text = "Login failed.";
    }
    protected void Register_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session["search_results"] = search_results;
        Session["search_results_notation"] = search_results_notation;
        Session["search_str"] = search_bar.Text;
        Session["force_postback"] = true;
        Session["results_label"] = results_label.Text;
        updateActiveFilters();
        Response.Redirect("Default.aspx");
    }
    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        if (search_bar.Text == "")
        {
            results_label.Text = "No results found.";
        }
        else
        {

            searchQuery(search_bar.Text, connectionString);
        }
    }

    int cartCount = 0;
    protected void results_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int idcounter = 0;
        if (e.CommandName == "add2cart")
        {
            try
            {
                cartCount++;
                if (User.Identity.IsAuthenticated)
                {
                    SqlDS_Cart.InsertParameters["username"].DefaultValue = User.Identity.Name;
                }
                search_results = new List<List<String>>();
                search_results = (List<List<String>>)Session["search_results"];

                int count;
                if (User.Identity.Name.Equals(""))
                   count = item_exists(SqlDS_Cart.ConnectionString, "MyCart", "quantity", "productname", search_results[e.Item.ItemIndex][0], "username", "default");
                else
                    count = item_exists(SqlDS_Cart.ConnectionString, "MyCart", "Quantity", "ProductName", search_results[e.Item.ItemIndex][0], "UserName", User.Identity.Name);

                if (count == -1)
                {
                    SqlDS_Cart.InsertParameters["price"].DefaultValue = search_results[e.Item.ItemIndex][4];
                    SqlDS_Cart.InsertParameters["productname"].DefaultValue = search_results[e.Item.ItemIndex][0];
                    SqlDS_Cart.InsertParameters["quantity"].DefaultValue = "1";
                    //SqlDS_Cart.InsertParameters["quantity"].DefaultValue = search_results[e.Item.ItemIndex][2];

                    SqlDS_Cart.InsertParameters["id"].DefaultValue = cartCount.ToString();
                    //SqlDS_Cart.InsertParameters["price"].DefaultValue = cartPrice.Value;
                    //SqlDS_Cart.InsertParameters["quantity"].DefaultValue = cartQuantity.Value;
                    //SqlDS_Cart.InsertParameters["productname"].DefaultValue = cartPName.Value;
                    //AddItemToCart(e);

                    SqlDS_Cart.Insert();
                }
                else
                {
                    update_item(SqlDS_Cart.ConnectionString, "MyCart", "quantity", (count + 1).ToString() , "productname", search_results[e.Item.ItemIndex][0], "username", User.Identity.Name);
                }
            }
            catch (SqlException)
            {
                cartCount++;
            }
            catch (Exception)
            {
                cartCount++;  
            }
           // CartLink.Text = "My Cart [" + cartCount + "]" ; 
            //Response.Redirect("MyCart.aspx");

        }
    }


    protected void AddItemToCart(RepeaterCommandEventArgs e)
    {
        //HiddenField cartPrice = (HiddenField)e.Item.FindControl("cartPrice");
        //HiddenField cartQuantity = (HiddenField)e.Item.FindControl("cartQuantity");
        //HiddenField cartPName = (HiddenField)e.Item.FindControl("cartPName");
        //HiddenField cartUser = (HiddenField)e.Item.FindControl("cartUser");
        //Label Label3 = (Label)e.Item.FindControl("Label3");
        //Label Label2 = (Label)e.Item.FindControl("Label2");
        //Label Label1 = (Label)e.Item.FindControl("Label1");    


        //SqlDS_Cart.InsertParameters["id"].DefaultValue = cartCount.ToString();
        ////SqlDS_Cart.InsertParameters["price"].DefaultValue = cartPrice.Value;
        ////SqlDS_Cart.InsertParameters["quantity"].DefaultValue = cartQuantity.Value;
        ////SqlDS_Cart.InsertParameters["productname"].DefaultValue = cartPName.Value;
        ////SqlDS_Cart.InsertParameters["price"].DefaultValue = Label3.Text;
        ////SqlDS_Cart.InsertParameters["quantity"].DefaultValue = Label2.Text;
        ////SqlDS_Cart.InsertParameters["productname"].DefaultValue = Label1.Text;
    }


    private List<String> getTableNames(String conn_string)
    //SQL Query to get all current tables
    {
        String query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ";
        SqlConnection sqlConn = new SqlConnection(conn_string);
        sqlConn.Open();
        SqlCommand sqlQuery = new SqlCommand(query, sqlConn);
        SqlDataReader reader = sqlQuery.ExecuteReader();
        List<String> table_names = new List<String>();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Object[] values = new Object[reader.FieldCount];
                int fieldCount = reader.GetValues(values);
                if (fieldCount == 1)
                {
                    table_names.Add(values[0].ToString());
                }
            }
        }
        sqlConn.Close();
        return table_names;
    }

    private void update_item(String conn_string, String table_name, String up_parameter, String up_value, String find_parameter, String find_value, String find_parameter2, String find_value2)
    {
        StringBuilder query = new StringBuilder();
        SqlConnection sqlConn = new SqlConnection(conn_string);
        sqlConn.Open();
        query.Append("UPDATE ");
        query.Append(table_name);
        query.Append(" SET ");
        query.Append(up_parameter);
        query.Append("=");
        query.Append(up_value);
        query.Append(" WHERE ");
        query.Append(find_parameter);
        query.Append("='");
        query.Append(find_value);
        query.Append("' AND ");
        query.Append(find_parameter2);
        query.Append("='");
        query.Append(find_value2);
        query.Append("'");
        SqlCommand sqlQuery = new SqlCommand(query.ToString(), sqlConn);
        sqlQuery.ExecuteNonQuery();
        sqlConn.Close();
    }

    private int item_exists(String conn_string, String table_name, String q_parameter, String parameter, String value, String parameter2, String value2)
    {
        StringBuilder query = new StringBuilder();
        SqlConnection sqlConn = new SqlConnection(conn_string);
        sqlConn.Open();
        query.Append("SELECT ");
        query.Append(q_parameter);
        query.Append(" FROM ");
        query.Append(table_name);
        query.Append(" WHERE ");
        query.Append(parameter);
        query.Append("='");
        query.Append(value);
        query.Append("'");
        query.Append(" AND ");
        query.Append(parameter);
        query.Append("='");
        query.Append(value2);
        query.Append("'");
        SqlCommand sqlQuery = new SqlCommand(query.ToString(), sqlConn);
        SqlDataReader reader = sqlQuery.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            Object[] obj = new Object[reader.FieldCount];
            reader.GetValues(obj);
            sqlConn.Close();
            return Convert.ToInt32(obj.ToString());
        }
        sqlConn.Close();
        return -1;
    }



    private List<String> getColumnNames(String conn_string, string table_name)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SELECT COLUMN_NAME,* FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '");
        query.Append(table_name);
        query.Append("' ORDER BY ORDINAL_POSITION ");
        SqlConnection sqlConn = new SqlConnection(conn_string);
        sqlConn.Open();
        SqlCommand sqlQuery = new SqlCommand(query.ToString(), sqlConn);
        SqlDataReader reader = sqlQuery.ExecuteReader();
        List<String> column_names = new List<String>();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Object[] values = new Object[reader.FieldCount];
                int fieldCount = reader.GetValues(values);
                for (int ii = 0; ii < fieldCount; ii++)
                {
                    column_names.Add(values[ii].ToString());
                }
            }
        }
        sqlConn.Close();
        return column_names;
    }

    private List<String> getNeededTables(List<String> tableNames, string[] file_list)
    //Make a list of all the tables you don't currently have
    {
        //first get all the tables obtainined - all the tables have names of department_component_table, we just need to delete the '_table'
        List<String> made_tables = new List<String>();
        for (int ii = 0; ii < tableNames.Count; ii++)
        {
            StringBuilder t_name = new StringBuilder();
            t_name.Append(tableNames[ii]);
            t_name.Length -= ("_table".Length);
            made_tables.Add(t_name.ToString());
        }

        //now get all the needed tables
        List<String> need_tables = new List<String>();
        for (int ii = 0; ii < file_list.Length; ii++)
        {
            StringBuilder t_name = new StringBuilder();
            t_name.Append(file_list[ii]);
            t_name.Length -= (".xml".Length);
            need_tables.Add(t_name.ToString());
        }

        //now compare
        List<String> necess_tables = new List<String>();
        for (int ii = 0; ii < need_tables.Count; ii++)
        {
            if (!made_tables.Contains(need_tables[ii]))
            {
                StringBuilder t_name = new StringBuilder();
                t_name.Append(need_tables[ii]);
                t_name.Append(".xml");
                necess_tables.Add(t_name.ToString());
            }
        }

        return necess_tables;
    }

    private String getTableCategory(String str)
    //Gets the category of a table
    {
        StringBuilder word = new StringBuilder();
        for (int ii = 0; ii < str.Length; ii++)
        {
            if (str[ii] == '_')
                break;
            else
            {
                word.Append(str[ii]);
            }
        }

        return word.ToString();
    }

    private String getTableProduct(String str)
    //Get the prodcut of a table
    {
        StringBuilder word = new StringBuilder();
        int index = 0;
        while (str[index] != '_')
            index++;
        index++;
        for (int ii = index; ii < str.Length; ii++)
        {
            if (str[ii] == '.' || str[ii] == '_')
                break;
            else
            {
                word.Append(str[ii]);
            }
        }

        return word.ToString();
    }




    private void table_Init()
    {
        //get all table names
        categories = new List<String>();
        types = new List<List<String>>();
        filter_names = new List<List<List<String>>>();
        filter_types = new List<List<List<String>>>();
        List<String> tableNames = getTableNames(connectionString);
        for (int ii = 0; ii < tableNames.Count; ii++)
        {
            int index = -1;
            string cat = getTableCategory(tableNames[ii]);
            for (int jj = 0; jj < categories.Count; jj++)
            {
                if (categories[jj].Equals(cat))
                {
                    index = jj;
                    break;
                }
            }
            if (index == -1)
            {
                categories.Add(cat);
                types.Add(new List<String>());
                index = categories.Count - 1;
            }

            types[index].Add(getTableProduct(tableNames[ii]));
        }
        tableNames = getNeededTables(tableNames, data_xml_paths);

        //load in unused tables
        if (tableNames.Count != 0)
        {
            try
            {
                for (int ii = 0; ii < tableNames.Count; ii++)
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(Server.MapPath(tableNames[ii]));
                    String category = "", product = "";
                    category = getTableCategory(tableNames[ii]);
                    product = getTableProduct(tableNames[ii]);
                    int index = -1;
                    string cat = getTableCategory(tableNames[ii]);
                    for (int jj = 0; jj < categories.Count; jj++)
                    {
                        if (categories[jj].Equals(cat))
                        {
                            index = jj;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        categories.Add(cat);
                        types.Add(new List<String>());
                        notation.Add(new List<List<String>>());
                        index = categories.Count - 1;
                    }

                    types[index].Add(getTableProduct(tableNames[ii]));
                    StringBuilder table_name = new StringBuilder();
                    table_name.Append(cat);
                    table_name.Append("_");
                    table_name.Append(getTableProduct(tableNames[ii]));
                    table_name.Append("_table");

                    List<String> cols = createTable(table_name.ToString(), ds, connectionString);
                    insertDataIntoTable(table_name.ToString(), ds, cols, connectionString);
                }
            }
            catch (Exception ex)
            {

            }
        }

        for (int ii = 0; ii < categories.Count; ii++)
        {
            filter_names.Add(new List<List<String>>());
            filter_types.Add(new List<List<String>>());
            for (int jj = 0; jj < types[ii].Count; jj++)
            {
                filter_names[ii].Add(new List<String>());
                filter_types[ii].Add(new List<String>());
            }
        }
    }

    private int findCategory(string cat)
    {
        for (int ii = 0; ii < categories.Count; ii++)
        {
            if (categories[ii].Equals(cat))
                return ii;
        }
        return -1;
    }

    private int findProduct(string prod, int cat)
    {
        for (int ii = 0; ii < types[cat].Count; ii++)
        {
            if (types[cat][ii].Equals(prod))
                return ii;
        }
        return -1;
    }

    private void filter_Init()
    {
        //initialize empty set of filters
        filters = new List<List<List<List<String>>>>();
        for (int ii = 0; ii < categories.Count; ii++)
        {
            filters.Add(new List<List<List<String>>>());
            for (int jj = 0; jj < types[ii].Count; jj++)
            {
                filters[ii].Add(new List<List<String>>());
            }
        }


        //load all the filters
        for (int ii = 0; ii < filter_xml_paths.Length; ii++)
        {
            //get the filter table name
            string cat = getTableCategory(filter_xml_paths[ii]);
            string prod = getTableProduct(filter_xml_paths[ii]);
            int cat_index = findCategory(cat);
            if (cat_index == -1)
            {
                throw new Exception();
            }
            int prod_index = findProduct(prod, cat_index);
            if (prod_index == -1)
            {
                throw new Exception();
            }

            //get the usable features
            DataSet ds = new DataSet();
            StringBuilder temp_xml_name = new StringBuilder();
            temp_xml_name.Append(cat);
            temp_xml_name.Append("_");
            temp_xml_name.Append(prod);
            temp_xml_name.Append("_usable_features.xml");
            ds.ReadXml(Server.MapPath(temp_xml_name.ToString()));
            List<String> u_feat = new List<String>();
            string xml_tag_name = ds.Tables[0].Columns[0].ColumnName;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                u_feat.Add(row[xml_tag_name].ToString().Trim());
            }
            filter_names[cat_index][prod_index] = u_feat;

            //get the feature types
            ds = new DataSet();
            temp_xml_name.Length -= ("_usable_features.xml".Length);
            temp_xml_name.Append("_feature_type.xml");
            ds.ReadXml(Server.MapPath(temp_xml_name.ToString()));
            List<String> t_feat = new List<String>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                for (int jj = 0; jj < u_feat.Count; jj++)
                    t_feat.Add(row[u_feat[jj]].ToString().Trim());
            }
            filter_types[cat_index][prod_index] = t_feat;

            //now get the filters themselves
            List<List<String>> filt_temp = new List<List<String>>();
            ds = new DataSet();
            temp_xml_name.Length -= ("_feature_type.xml".Length);
            temp_xml_name.Append("_filters.xml");
            ds.ReadXml(Server.MapPath(temp_xml_name.ToString()));

            //get list of tables in dataset
            List<String> tab_list = new List<String>();
            foreach (DataTable tab in ds.Tables)
            {
                tab_list.Add(tab.TableName);
            }

            for (int jj = 0; jj < u_feat.Count; jj++)
            {
                filt_temp.Add(new List<String>());

                if (t_feat[jj].Equals("Text"))
                {
                    int index = -1;
                    StringBuilder search_str = new StringBuilder();
                    search_str.Append("item_");
                    search_str.Append(u_feat[jj]);
                    for (int kk = 0; kk < tab_list.Count; kk++)
                    {
                        if (tab_list[kk].Equals(search_str.ToString()))
                        {
                            index = kk;
                            search_str.Append("_Text");
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        for (int kk = 0; kk < tab_list.Count; kk++)
                        {
                            if (tab_list[kk].Equals(u_feat[jj]))
                            {
                                index = kk;
                                break;
                            }
                        }
                    }

                    if (index != -1)
                    {
                        foreach (DataRow row in ds.Tables[index].Rows)
                        {
                            filt_temp[jj].Add(row[search_str.ToString()].ToString().Trim());
                        }
                    }
                }
                else            //number
                {
                    int index = -1;
                    for (int kk = 0; kk < tab_list.Count; kk++)
                    {
                        if (tab_list[kk].Equals(u_feat[jj]))
                        {
                            index = kk;
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        throw new Exception();
                    }
                    StringBuilder qq = new StringBuilder();
                    String high, low;
                    qq.Append("high_");
                    qq.Append(u_feat[jj]);
                    DataRow row = ds.Tables[index].Rows[0];
                    high = row[qq.ToString()].ToString().Trim();
                    qq = new StringBuilder();
                    qq.Append("low_");
                    qq.Append(u_feat[jj]);
                    low = row[qq.ToString()].ToString().Trim();
                    double low_i, high_i, range, next;
                    low_i = Convert.ToDouble(low);
                    next = low_i;
                    high_i = Convert.ToDouble(high);
                    range = (high_i - low_i) / 5;
                    next += range;
                    if (low_i == high_i)
                    {
                        filt_temp[jj].Add(low);
                    }
                    else
                    {
                        while (next < high_i)
                        {
                            StringBuilder range_str = new StringBuilder();
                            range_str.Append(low_i.ToString());
                            range_str.Append(" - ");
                            range_str.Append(next.ToString());
                            filt_temp[jj].Add(range_str.ToString());
                            low_i += range;
                            next += range;
                        }
                        StringBuilder final_range_str = new StringBuilder();
                        final_range_str.Append(low_i.ToString());
                        final_range_str.Append(" - ");
                        final_range_str.Append(high_i.ToString());
                        filt_temp[jj].Add(final_range_str.ToString());
                    }
                }
            }

            filters[cat_index][prod_index] = filt_temp;
        }
    }

    private void notation_Init()
    {
        notation = new List<List<List<String>>>();
        for (int ii = 0; ii < categories.Count; ii++)
        {
            notation.Add(new List<List<String>>());
            for (int jj = 0; jj < types[ii].Count; jj++)
            {
                notation[ii].Add(new List<String>());
                StringBuilder table_name = new StringBuilder();
                table_name.Append(categories[ii]);
                table_name.Append("_");
                table_name.Append(types[ii][jj]);
                table_name.Append("_notation.xml");
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath(table_name.ToString()));
                List<String> f_name = new List<String>();
                string xml_tag_name = ds.Tables[0].Columns[0].ColumnName;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    f_name.Add(row[xml_tag_name].ToString().Trim());
                }
                notation[ii][jj] = f_name;
            }
        }
    }

}