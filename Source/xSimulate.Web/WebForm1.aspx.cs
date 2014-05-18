using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using xSimulate.Web.DAL;

namespace xSimulate.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool result = CustomerDAL.Register(new Model.Customer() { CustomerID = "ss", Name = "11", Password = "dd" });
            if (result)
            {
                Response.Write("s");
            }
        }
    }
}