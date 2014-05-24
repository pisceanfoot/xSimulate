using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetFramework.DataAccess;

namespace xSimulate.Web.DAL
{
    public class TaskDAL
    {
        public static int CreateTask(Model.Task task)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Task_CreateTask");
            dataCommand.SetParameter("@CustomerSysNo", task.CustomerSysNo);
            dataCommand.SetParameter("@Cost", task.Costs);

            dataCommand.ExecuteNonQuery();
            int result = Convert.ToInt32(dataCommand.Parameters["@Result"].Value);
            return result;
        }

        public static Model.Task RetrieveTask(int customerSysNo)
        {
            IDataCommand dataCommand = DataCommandManager.GetCommand("Task_RetrieveTask");
            dataCommand.SetParameter("@CustomerID", customerSysNo);

            return dataCommand.ExecuteEntity<Model.Task>();
        }
    }
}