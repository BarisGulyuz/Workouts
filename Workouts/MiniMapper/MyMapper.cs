﻿using System.Reflection;

namespace Workouts.MiniMapper
{
    public static class MyMapper<T> where T : class
    {
        #region Map Methods
        public static T Map(object sourceObject)
        {
            if (sourceObject != null)
            {
                T t = (T)Activator.CreateInstance(typeof(T), new object[] { });
                PropertyInfo[] propertyInfos = t.GetType().GetProperties();

                foreach (PropertyInfo property in propertyInfos)
                {
                    SetPropValue(t, property.Name, GetPropValue(sourceObject, property.Name));
                }

                return (T)Convert.ChangeType(t, typeof(T));
            }
            return null;
        }
        public static List<T> MapList(IEnumerable<object> sourceObject)
        {
            List<T> objectList = null;
            if (sourceObject != null)
            {
                T obj = default(T);
                PropertyInfo[] propertyInfos = obj.GetType().GetProperties();

                objectList = (List<T>)Activator.CreateInstance(typeof(List<T>), new object[] { });

                foreach (var item in sourceObject)
                {
                    obj = (T)Activator.CreateInstance(typeof(T), new object[] { });

                    foreach (PropertyInfo property in propertyInfos)
                    {
                        SetPropValue(obj, property.Name, GetPropValue(item, property.Name));
                    }

                    objectList.Add(obj);
                }
            }
            return objectList;
        }

        #endregion

        #region GetValue - SetValue
        private static object GetPropValue(object sourceObject, string propName)
        {
            PropertyInfo propertyInfo = sourceObject.GetType().GetProperty(propName);
            if (propertyInfo is null)
                return null;

            return propertyInfo.GetValue(sourceObject, null);
        }
        private static void SetPropValue(object objectToMapped, string propName, object value)
        {
            objectToMapped.GetType().GetProperty(propName).SetValue(objectToMapped, value);
        }

        #endregion
    }
}
