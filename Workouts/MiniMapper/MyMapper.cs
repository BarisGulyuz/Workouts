using System.Reflection;

namespace Workouts.MiniMapper
{
    public static class MyMapper<TSource, TDest> where TSource : class 
                                                 where TDest : class
    {
        #region Map Methods
        public static TDest Map(TSource sourceObject)
        {
            TDest returnValue = default(TDest);

            if (sourceObject != null)
            {
                returnValue = (TDest)Activator.CreateInstance(typeof(TDest), new object[] { });
                PropertyInfo[] propertyInfos = returnValue.GetType().GetProperties();

                foreach (PropertyInfo property in propertyInfos)
                {
                    SetPropValue(returnValue, property.Name, GetPropValue(sourceObject, property.Name));
                }
            }

            return returnValue;
        }
        public static List<TDest> Map(IEnumerable<TSource> sourceObjects)
                            => sourceObjects.Select(o => Map(o)).ToList();

        #endregion

        #region GetValue - SetValue
        private static object GetPropValue(TSource sourceObject, string propName)
        {
            PropertyInfo propertyInfo = sourceObject.GetType().GetProperty(propName);
            if (propertyInfo is null)
                return null;

            return propertyInfo.GetValue(sourceObject, null);
        }
        private static void SetPropValue(TDest destObject, string propName, object value)
        {
            destObject.GetType().GetProperty(propName)
                                .SetValue(destObject, value);
        }

        #endregion
    }

}
