using System.Reflection;
using System.Text;


namespace Workouts.ListToHtmlTable
{
    /// <summary>
    /// Listeyi html tabloya çevirir
    /// </summary>
    public static class ListToHtmlExtension
    {
        #region Html Table Tags
        private static string TableStartTag = "<table style='border-collapse: collapse; background-color: #fff;width: 100%; margin-top: 10px;'>";
        private static string TableEndTag = "</table>";
        private static string TrStartTag = "<tr style='border: 1px solid #bbb;padding: 10px 20px;text-align: center;'>";
        private static string TrEndTag = "</tr>";
        private static string ThStartTag = "<th style='border: 1px solid #bbb;padding: 10px 20px;text-align: center; background-color: #A084DC; color: #fff; font-weight: 600;'>";
        private static string ThEndTag = "</th>";
        private static string TdStartTag = "<td style='border: 1px solid #bbb;padding: 10px 20px;text-align: center;'>";
        private static string TdEndTag = "</td>";
        #endregion

        /// <summary>
        /// Verilen listeyi [HtmlTableColumnInfo] adlı attribute'u kullanarak html table oluşturur ve string string olarak döndürür.
        /// T nesnesinin html tablaya yansıması istenilen propertylerinde [HtmlTableColumnInfo] attribute'u kullanılmalıdır.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHtmlTable<T>(this List<T> sourceData)
        {
            IEnumerable<PropertyInfo> targetProps = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(HtmlTableColumnInfo)));

            if (!targetProps.Any())
                throw new Exception("HTML Tablo oluşturmak için hedef class'ın propertyleri üzerinde HtmlTableColumnInfo attribute'ü kullanılamlıdır");

            List<TableInfoModel> tableInfoData = new List<TableInfoModel>();
            HtmlTableColumnInfo htmlTableInfoAttribute;

            foreach (PropertyInfo prop in targetProps)
            {
                htmlTableInfoAttribute = prop.GetCustomAttribute(typeof(HtmlTableColumnInfo)) as HtmlTableColumnInfo;
                tableInfoData.Add(new TableInfoModel(htmlTableInfoAttribute.Order, prop.Name, 
                                                     htmlTableInfoAttribute.ColumnName ?? prop.Name,
                                                     htmlTableInfoAttribute.DatePattern, htmlTableInfoAttribute.MoneyPattern));
            }
            tableInfoData = tableInfoData.OrderBy(d => d.Order).ToList();

            StringBuilder htmlTableBuilder = new StringBuilder();
            htmlTableBuilder.Append(TableStartTag);

            CreateTableHead(htmlTableBuilder, tableInfoData);
            CreateTableBody(htmlTableBuilder, tableInfoData, sourceData);

            htmlTableBuilder.Append(TableEndTag);

            return htmlTableBuilder.ToString();
        }

        /// <summary>
        /// HTML Table için başlık(th) kısımlarını oluştur.
        /// </summary>
        /// <param name="htmlTableBuilder"></param>
        /// <param name="tableInfoData"></param>
        private static void CreateTableHead(StringBuilder htmlTableBuilder, List<TableInfoModel> tableInfoData)
        {
            htmlTableBuilder.Append(TrStartTag);
            foreach (TableInfoModel tableInfo in tableInfoData)
            {
                htmlTableBuilder.Append(ThStartTag);
                htmlTableBuilder.Append(tableInfo.ColumnName);
                htmlTableBuilder.Append(ThEndTag);
            }
            htmlTableBuilder.Append(TrEndTag);
        }

        /// <summary>
        /// HTML Table için data(td) kısımlarını oluştur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlTableBuilder"></param>
        /// <param name="tableInfoData"></param>
        /// <param name="sourceData"></param>
        private static void CreateTableBody<T>(StringBuilder htmlTableBuilder, List<TableInfoModel> tableInfoData, List<T> sourceData)
        {
            foreach (T data in sourceData)
            {
                htmlTableBuilder.Append(TrStartTag);
                foreach (TableInfoModel tableInfo in tableInfoData)
                {
                    htmlTableBuilder.Append(TdStartTag);
                    PropertyInfo prop = data.GetType().GetProperty(tableInfo.PropertyName);
                    object value = prop.GetValue(data);

                    if (prop.PropertyType == typeof(DateTime) && !string.IsNullOrEmpty(tableInfo.DatePattern))
                    {
                        value = ((DateTime)value).ToString(tableInfo.DatePattern);
                    }
                    else if ((prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(float))
                        && !string.IsNullOrEmpty(tableInfo.DatePattern))
                    {
                        value = Convert.ToDecimal(value).ToString(tableInfo.MoneyPattern);
                    }

                    htmlTableBuilder.Append(value);
                    htmlTableBuilder.Append(TdEndTag);
                }
                htmlTableBuilder.Append(TrEndTag);
            }
        }

        /// <summary>
        /// Katılım alınamaz.
        /// Yalnızca propertyler üzerinde kullanılabilir.
        /// Html tablo oluşturmak için listesi oluşacak class'ın propertylerinde kullanılır.
        /// Money formatlanmak isteniyorsa MoneyPattern doldurulmalıdır.
        /// Date formatlanmak isteniyorsa DatePattern doldurulmalıdır.
        /// Doğru bir sonuç almak için Order prop kesinlikle doğru bir şekilde set edilmelidir.
        /// ColumnName verilmez ise property name columnName olarak kabul edilmektedir.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public sealed class HtmlTableColumnInfo : Attribute
        {
            public int Order { get; set; }
            public string ColumnName { get; set; }
            public string DatePattern { get; set; }
            public string MoneyPattern { get; set; }
        }

        /// <summary>
        /// Table oluştururken kullanılacak model class'ı
        /// </summary>
        public class TableInfoModel
        {
            public TableInfoModel()
            {

            }
            public TableInfoModel(int order, string propertyName, string columnName, string datePattern, string moneyPattern)
            {
                Order = order;
                PropertyName = propertyName;
                ColumnName = columnName;
                DatePattern = datePattern;
                MoneyPattern = moneyPattern;
            }
            public int Order { get; set; }
            public string PropertyName { get; set; }
            public string ColumnName { get; set; } = null;
            public string DatePattern { get; set; }
            public string MoneyPattern { get; set; }
        }

    }
}
