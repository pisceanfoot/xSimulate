using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xSimulate.Web.DAL;
using xSimulate.Web.Genration;

namespace xSimulate.Web.BLL
{
    public class TaskBLL
    {
        public static int CreateTask(Model.Task task)
        {
            return TaskDAL.CreateTask(task);
        }

        public static Model.Task RetrieveTask(int customerSysNo)
        {
            Model.Task task = TaskDAL.RetrieveTask(customerSysNo);
            if (task == null || task.Setting == null || string.IsNullOrEmpty(task.Setting.Setting))
            {
                return null;
            }

            try
            {
                string content = ConfigHelper.Create(task.Setting.Setting);
                if (string.IsNullOrEmpty(content))
                {
                    // feedback error
                    return null;
                }

                task.Setting.Setting = content;
            }
            catch 
            {
                // feedback error
                return null;
            }
            

            return task;
        }
    }
}