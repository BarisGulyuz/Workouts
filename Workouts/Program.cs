using Workouts.MiniMapper;
using Workouts.ApplicationModels;
using Workouts;
using Workouts.ToPaginate;
using Workouts.Mail;
using Workouts.Mail.DummyTemplate;
using Workouts.Expressions;
using System.Linq;

Data data = new Data();

#region Mapper


Currency currency = new Currency() { Name = "Deneme", BanknoteSelling = "100" };
CurrencyDto currencyDto = MyMapper<CurrencyDto>.Map(currency);

List<Currency> currencies = data.GetCurrenyData();
List<CurrencyDto> currencyDtos = MyMapper<CurrencyDto>.MapList(currencies);


#endregion

#region Pagination

IQueryable<Currency> currencies1 = currencies.AsQueryable();

var paginatedData = currencies1.Paginate(5, 3);

#endregion

#region MailKit

User user = data.GetDefaultUser();
int verificationCode = new Random().Next(100000, 999999);

MailDto mailReq = new MailDto
{
    To = user.Mail,
    Subject = "Kayıt Aktivasyonu",
    Body = new Templates().GetVerificationTemplate(user.Name, verificationCode)
};

IMailSender mailSender = new MailSender();
mailSender.SendMail(mailReq);

#endregion

#region Create Expression

List<Currency> currencies2 = data.GetCurrenyData();

int count = currencies.Count(x => x.Name == "a" && x.BanknoteSelling == "20" && x.BanknoteSelling.Contains("1"));

List<ExpressionModel> expressionModels = new List<ExpressionModel>
{
    new ExpressionModel() {ColumnName = "Name", OperatorEnum= OperatorEnum.Equal, Value = "a" },
    new ExpressionModel() {ColumnName = "BanknoteSelling",OperatorEnum= OperatorEnum.Equal, Value="20"},
    new ExpressionModel() {ColumnName = "BanknoteSelling",OperatorEnum= OperatorEnum.Contains, Value="1"}
};

List<ExpressionModel> expressionModels1 = new List<ExpressionModel>
{
    new ExpressionModel() {ColumnName = "BanknoteSelling",OperatorEnum= OperatorEnum.Contains, Value="1"}
};

var expression = new MyExpression<Currency>(isAndConjunction: true).GetExpression(expressionModels);
List<Currency> newCurrencies = currencies2.AsQueryable().Where(expression).ToList();


var expression2 = new MyExpression<Currency>().GetExpression(expressionModels1);
List<Currency> newCurrencies2 = currencies1.AsQueryable().Where(expression2).ToList();

//Exception
//var expression3 = new MyExpression<Currency>().GetExpression(new List<ExpressionModel>());


#endregion

#region Equality
//User u1 = new User
//{
//   Name =   "Baris",
//   Mail = "bar.77@windowslive.com"
//};

//User u2 = new User
//{
//    Id = u1.Id,
//    Name = u1.Name,
//    Mail = u1.Mail
//};

//User u3 = u1;

//User u4 = u1.ShallowCopy();

//u1.Name = "baris";

//bool isUser1And2Equal = u1.Equals(u2);
//bool isUser1And3Equal = u1.Equals(u3);
//bool isUSer1And4Equal = u1.Equals(u4);

//Console.WriteLine(isUser1And2Equal);
//Console.WriteLine(isUser1And3Equal);
//Console.WriteLine(isUSer1And4Equal);
#endregion



Console.ReadKey();