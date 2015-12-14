using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormDemo
{
    public partial class _Default : Page
    {
        class UserInfo
        {
            public string Account { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public override string ToString()
            {
                return string.Format("{0}  {1}  {2}", Account, Password, Email);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Account.Valid().Required().Length(5, 10).Apply();
            this.Password.Valid().Required().Length(6, 12).Apply();
            this.Email.Valid().Required().Email().Apply();

            // 表单验证成功
            if (this.IsPostBack && this.Form.IsValid())
            {
                var model = new UserInfo();
                this.Form.TryUpdateModel(model);

                var modelString = model.ToString();
            }
        }
    }
}