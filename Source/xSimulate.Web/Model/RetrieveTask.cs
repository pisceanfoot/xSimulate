using System;
using NetFramework.DataAccess.EntityBuilder;

namespace xSimulate.Web.Model
{
    [Serializable]
    public class RetrieveTask
    {
        [DataMapping]
        public int SysNo { get; set; }

        [DataMapping]
        public int CustomerSysNo { get; set; }

        [DataMapping]
        public int RetrieveCustomerSysNo { get; set; }

        [DataMapping]
        public int RunTaskSysNo { get; set; }

        [DataMapping]
        public string Status { get; set; }

        [DataMapping]
        public int Description { get; set; }

        [DataMapping]
        public DateTime InDate { get; set; }

        [DataMapping]
        public int InUser { get; set; }

        [DataMapping]
        public DateTime EditDate { get; set; }

        [DataMapping]
        public DateTime EditUser { get; set; }

        [ReferencedEntity(Prefix="Task_")]
        public Task Task { get; set; }
    }
}