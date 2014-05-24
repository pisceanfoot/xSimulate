using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace xSimulate.Web.Service
{
    /// <summary>
    /// TasService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class TaskService : System.Web.Services.WebService
    {

        [WebMethod]
        public string CreateTask(Model.Task task)
        {
            int result =  BLL.TaskBLL.CreateTask(task);
            string content = string.Empty;

            return content;
        }

        public Model.Task RetrieveTask(int customerSysNo)
        {
            return BLL.TaskBLL.RetrieveTask(customerSysNo);
        }
    }
}
