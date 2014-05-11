using System;
using System.Collections.Generic;

namespace xSimulate.Storage
{
    public static class TaskStorage
    {
        private static string DefaultKey = Guid.NewGuid().ToString("N");

        [ThreadStatic]
        private static Dictionary<string, object> storageDictory = new Dictionary<string, object>();

        public static object Storage
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

        public static void SetKey(string key, object value)
        {
            storageDictory[key] = value;
        }

        public static object GetKey(string key)
        {
            object obj;
            storageDictory.TryGetValue(DefaultKey, out obj);

            return obj;
        }

        public static void Clear()
        {
            storageDictory.Clear();
        }
    }
}