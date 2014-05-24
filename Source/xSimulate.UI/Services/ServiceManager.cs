using System;
using System.Collections.Generic;
using System.Text;

namespace xSimulate.UI.Services
{
    public class ServiceManager
    {
        public static CustomerService CreateCustomerService()
        {
            CustomerService customerService = new CustomerService();
            customerService.Url = "http://localhost:54706/Service/CustomerService.asmx";

            return customerService;
        }

        public static TaskService CreateTaskService()
        {
            TaskService taskService = new TaskService();
            taskService.Url = "http://localhost:54706/Service/TaskService.asmx";

            return taskService;
        }
    }
}
