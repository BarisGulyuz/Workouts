using System.Reflection;

namespace Workouts.MiniMapper
{
    public static class MyMapper<T> where T : class
    {
        #region Map Methods
        public static T Map(object baseObject)
        {
            if (baseObject != null)
            {
                T t = (T)Activator.CreateInstance(typeof(T), new object[] { });
                PropertyInfo[] propertyInfos = t.GetType().GetProperties();


                foreach (PropertyInfo property in propertyInfos)
                {
                    SetPropValue(t, property.Name, GetPropValue(baseObject, property.Name));
                }


                return (T)Convert.ChangeType(t, typeof(T));
            }

            return null;
        }
        public static List<T> MapList(IEnumerable<object> baseObject)
        {
            if (baseObject != null)
            {
                T t = (T)Activator.CreateInstance(typeof(T), new object[] { });
                PropertyInfo[] propertyInfos = t.GetType().GetProperties();

                List<T> ts = (List<T>)Activator.CreateInstance(typeof(List<T>), new object[] { });

                foreach (var item in baseObject)
                {
                    t = (T)Activator.CreateInstance(typeof(T), new object[] { });

                    foreach (PropertyInfo property in propertyInfos)
                    {

                        SetPropValue(t, property.Name, GetPropValue(item, property.Name));
                    }

                    ts.Add(t);
                }

                return ts;
            }
            return null;
        }

        #endregion

        #region GetValue - SetValue
        private static object GetPropValue(object baseObject, string propName)
        {
            PropertyInfo propertyInfo = baseObject.GetType().GetProperty(propName);
            if (propertyInfo is null)
                return null;

            return propertyInfo.GetValue(baseObject, null);
        }
        private static void SetPropValue(object objectToMapped, string propName, object value)
        {
            objectToMapped.GetType().GetProperty(propName).SetValue(objectToMapped, value);
        }

        #endregion
    }
}
