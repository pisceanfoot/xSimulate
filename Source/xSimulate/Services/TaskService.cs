using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate.Services
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RetrieveTask", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public xSimulate.Web.Model.Task RetrieveTask(int customerSysNo)
        {
            object[] results = this.Invoke("RetrieveTask", new object[] {
                        customerSysNo});
            return ((xSimulate.Web.Model.Task)(results[0]));
        }
    }

}
