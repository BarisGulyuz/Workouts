using Workouts.MiniMapper;
using Workouts.ApplicationModels;
using Workouts;
using Workouts.ToPaginate;
using Workouts.Mail;
using Workouts.Mail.DummyTemplate;
using Workouts.Expressions;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Workouts.XML;
using Workouts.DesignPatterns;
using User = Workouts.ApplicationModels.User;
using System.Text;
using Workouts.HttpClientX;
using Workouts.RandomQuestions;
using ObserverPatternLikeMediatR.Concrete;
using ObserverPatternLikeMediatR.Interfaces;
using System.Reflection;
using static Workouts.ApplicationModels.ObserverPatternModel.ObserverPatternModel;
using Workouts.ListToHtmlTable;

Data data = new Data();
bool httpReqEnable = false;

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
//mailSender.SendMail(mailReq);


#endregion

#region Create Expression

List<Currency> currencies2 = data.GetCurrenyData();

int count = currencies.Count(x => x.Name == "a" && x.BanknoteSelling == "20" && x.BanknoteSelling.Contains("1"));

List<ExpressionModel> expressionModels = new List<ExpressionModel>
{
    new ExpressionModel() {ColumnName = "Name", OperatorEnum= OperatorEnum.Equal, Value = "a" },
    new ExpressionModel() {ColumnName = "BanknoteSelling",OperatorEnum= OperatorEnum.Equal, Value="20"}
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

Workouts.ExcelReport.Reporter.ExportToExcel(data.GetCurrenyData());

#region HttpReq With Token
if (httpReqEnable)
{
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "sadasdasdasdasdldsadsa");
    HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

    if (response.IsSuccessStatusCode)
    {
        List<Post> json = await response.Content.ReadFromJsonAsync<List<Post>>();
    }
    else
    {
        Console.WriteLine(response.StatusCode);
    }


    using (HttpClientService httpClient = new HttpClientService("https://jsonplaceholder.typicode.com/posts"))
    {
        List<Post> json = httpClient.SendRequest<List<Post>>(HttpMethod.Get);
    }
}


#endregion

#region Delegate - Action

void TryCondition(Action action, bool condition)
{
    if (condition)
    {
        action();
    }
}
TryCondition(WriteLine, false);

void WriteLine()
{
    Console.WriteLine("çalıştı");
}


void Plus(int a, int b, Action<int> action = null)
{
    int result = a + b;
    if (action != null)
        action(result);
}


void SendResult(int item)
{
    Console.WriteLine("Result is {0}", item.ToString());
}

Plus(1, 2, SendResult);
Plus(2, 3, (result) =>
{
    result = result + 1;
});

#endregion

#region XML Search
XMLWorkout xMLWorkout = new XMLWorkout();

bool barisResult = xMLWorkout.IsNameExist("Baris");
bool zeynepResult = xMLWorkout.IsNameExist("Zeynep");
#endregion

#region Signleton Test

Console.WriteLine(SingletonClass.Instance.Method1());
Console.WriteLine(SingletonClass.Instance.Method1());

#endregion


//PasswordBuilder passwordBuilder = new PasswordBuilder();



//Console.WriteLine(passwordBuilder.AddMultipleCharacterSet(requiredSmallCharacterQ: 1,
//                                                          requiredBigCharacterQ: 1,
//                                                          requiredNumberQ: 1,
//                                                          requiredSpecialCharacterQ: 1,
//                                                          passwordLength: 10).Build());
//Console.WriteLine(passwordBuilder.AddMultipleCharacterSet(1, 1, 1, 1, 10).Build());
//Console.WriteLine(passwordBuilder.AddMultipleCharacterSet(1, 1, 1, 1, 10).Build());
//Console.WriteLine(passwordBuilder.AddMultipleCharacterSet(1, 1, 1, 1, 10).Build());


//PasswordBuilder passwordBuilder1 = new PasswordBuilder();

//Console.WriteLine(passwordBuilder.AddCharacterSet("ABC", 10).Build());


#region Dependency Compiler 

Compiler compiler = new();
compiler.Compile();

#endregion


//MyEventHandler eventHandler = new MyEventHandler(Assembly.GetExecutingAssembly());

//eventHandler.Notify(new UserCreated { Id = 1 });
//eventHandler.Notify(new UserCreated { Id = 2 });

string htmlCurrencyTable = data.GetCurrenyData().ToHtmlTable();
string htmlUserTable = data.GetUserData().ToHtmlTable();


Console.ReadKey();








