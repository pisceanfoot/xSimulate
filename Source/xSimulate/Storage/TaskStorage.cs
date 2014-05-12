using System;
using System.Collections.Generic;

namespace xSimulate.Storage
{
    public class TaskStorage
    {
        private string DefaultKey = Guid.NewGuid().ToString("N");

        private Dictionary<string, object> storageDictory = new Dictionary<string, object>();

        public object Storage
        {
            get
            {
                return GetKey(DefaultKey);
            }
            set
            {
                SetKey(DefaultKey, value);
            }
        }

        public void SetKey(string key, object value)
        {
            storageDictory[key] = value;
        }

        public object GetKey(string key)
        {
            object obj;
            storageDictory.TryGetValue(DefaultKey, out obj);

            return obj;
        }

        public void Clear()
        {
            storageDictory.Clear();
        }
    }
}