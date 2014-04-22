using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label filterHeader = new Label();

        filterHeader.Attributes.CssStyle.Add("font-size","100%");
        filterHeader.Attributes.CssStyle.Add("font-weight", "bold");
        filterHeader.Attributes.CssStyle.Add("color", "#FF9900");
        filterHeader.Text = "Label" + "1";
        filterHeader.ID = "Label" + "1";
        PlaceHolder1.Controls.Add(filterHeader);
        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));

        CheckBoxList checkList = new CheckBoxList();

        checkList.Attributes.CssStyle.Add("margin", "0px 0px 6px 0px");
        checkList.Attributes.CssStyle.Add("padding", "0px 0px 6px 0px");
        checkList.Attributes.CssStyle.Add("list-style-type", "none");
        checkList.Attributes.CssStyle.Add("list-style-position", "outside");
        checkList.Attributes.CssStyle.Add("font-size", "100%");
        checkList.ID = "Checklist" + "1";
        checkList.Items.Add("Hello");
        PlaceHolder1.Controls.Add(checkList);
        PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
    }
}
