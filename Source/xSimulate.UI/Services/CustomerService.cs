using xSimulate.Web.Model;

namespace xSimulate.UI.Services
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CustomerServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class CustomerService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        /// <remarks/>
        public CustomerService()
        {
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                base.Url = value;
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Register", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResponseInfo<Customer> Register(Customer customer)
        {
            object[] results = this.Invoke("Register", new object[] {
                        customer});
            return ((ResponseInfo<Customer>)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResponseInfo<Customer> Login(Customer customer)
        {
            object[] results = this.Invoke("Login", new object[] {
                        customer});
            return ((ResponseInfo<Customer>)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SaveCustomerSetting", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SaveCustomerSetting(CustomerSetting customerSetting)
        {
            object[] results = this.Invoke("SaveCustomerSetting", new object[] {
                        customerSetting});
            return (string)(results[0]);
        }
        
    }
}