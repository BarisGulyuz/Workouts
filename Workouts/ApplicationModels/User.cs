using DocumentFormat.OpenXml.Drawing.Charts;
using static Workouts.ListToHtmlTable.ListToHtmlExtension;

namespace Workouts.ApplicationModels
{
    public class User
    {
        [HtmlTableColumnInfo(Order = 1, ColumnName = "Id")]
        public int Id { get; set; } = new Random().Next(1, 100);
        [HtmlTableColumnInfo(Order = 1, ColumnName = "Adı")]
        public string Name { get; set; }
        [HtmlTableColumnInfo(Order = 1, ColumnName = "Mail Adresi")]
        public string Mail { get; set; }


        public User ShallowCopy()
        {
            return this.MemberwiseClone() as User;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(User))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            User user = (User)obj;

            if (user.Id != this.Id || user.Mail != this.Mail || user.Name != this.Name)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
