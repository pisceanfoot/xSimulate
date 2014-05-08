using System;
namespace xSimulate.Storage
{
    public static class TaskStorage
    {
        [ThreadStatic]
        private static object storage;
        
        public static object Storage
        {
            get { return storage; }
            set { storage = value; }
        }
    }
}