using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabitMQ.Common
{
    public static class ObjectSerializeHelper
    {
        public static byte[] Serialize(object obj)
        {
            string stringData = JsonSerializer.Serialize(obj);
            byte[] byteArrData = Encoding.UTF8.GetBytes(stringData);
            return byteArrData;
        }
        public static T Deserialize<T>(string strinData)
        {
            return JsonSerializer.Deserialize<T>(strinData);
        }
        public static T Deserialize<T>(byte[] objByteArr)
        {
            string stringData = Encoding.UTF8.GetString(objByteArr);
            return Deserialize<T>(stringData);
        }
        public static object Deserialize(string strinData, Type objectType)
        {
            return JsonSerializer.Deserialize(strinData,objectType);
        }
    }
}
