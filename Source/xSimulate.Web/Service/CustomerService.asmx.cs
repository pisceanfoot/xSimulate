using System.Web.Services;

using xSimulate.Web.BLL;
using xSimulate.Web.Model;

namespace xSimulate.Web.Service
{
    /// <summary>
    /// Customer 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CustomerService : System.Web.Services.WebService
    {
        [WebMethod]
        public ResponseInfo<Model.Customer> Register(Customer customer)
        {
            ResponseInfo<Model.Customer> response = new ResponseInfo<Customer>();

            if (customer == null)
            {
                return response;
            }

            bool result = CustomerBLL.Register(customer);

            if (result)
            {
                Model.Customer customerDetail = CustomerBLL.GetCustomerDetail(customer);

                response.Code = 1;
                response.Value = customerDetail;
            }
            else
            {
                response.Code = -1;
                response.Message = "注册用户已存在";
            }

            return response;
        }

        [WebMethod]
        public ResponseInfo<Model.Customer> Login(Customer customer)
        {
            ResponseInfo<Model.Customer> response = new ResponseInfo<Customer>();

            if (customer == null)
            {
                return response;
            }

            bool result = CustomerBLL.Login(customer);

            if (result)
            {
                Model.Customer customerDetail = CustomerBLL.GetCustomerDetail(customer);

                response.Code = 1;
                response.Value = customerDetail;
            }
            else
            {
                response.Code = -1;
                response.Message = "用户名或密码错误";
            }

            return response;
        }
    }
}