using xSimulate.Web.Model;

namespace xSimulate.UI.Services
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "TaskServiceServiceSoap", Namespace = "http://tempuri.org/")]
    public partial class TaskService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        /// <remarks/>
        public TaskService()
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CreateTask", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateTask(Task task)
        {
            object[] results = this.Invoke("CreateTask", new object[] {
                        task});
            return ((string)(results[0]));
        }
    }
}