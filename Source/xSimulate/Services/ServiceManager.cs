using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate.Services
{
    public class ServiceManager
    {
        public static TaskService CreateTaskService()
        {
            TaskService taskService = new TaskService();
            taskService.Url = "http://localhost:9990/Service/TaskService.asmx";

            return taskService;
        }
    }
}
