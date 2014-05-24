using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xSimulate.Web.DAL;

namespace xSimulate.Web.BLL
{
    public class TaskBLL
    {
        public static int CreateTask(Model.Task task)
        {
            // TODO: 计算花费

            return TaskDAL.CreateTask(task);
        }

        public static Model.Task RetrieveTask(int customerSysNo)
        {
            return TaskDAL.RetrieveTask(customerSysNo);
        }
    }
}