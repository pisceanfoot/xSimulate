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
            dataCommand.SetParameter("@CustomerSettingSysNo", task.Setting.SysNo);
            dataCommand.SetParameter("@RunTimes", task.RunTimes);
            dataCommand.SetParameter("@BeginDate", task.BeginDate);
            if (task.EndDate == DateTime.MinValue)
            {
                dataCommand.SetParameter("@EndDate", null);
            }
            else
            {
                dataCommand.SetParameter("@EndDate", task.EndDate);
            }
            dataCommand.SetParameter("@Costs", task.Costs);

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