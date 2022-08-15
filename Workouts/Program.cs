using Workouts.MiniMapper;
using Workouts.ApplicationModels;
using Workouts;
using Workouts.ToPaginate;

Data data = new Data();

#region Mapper


Currency currency = new Currency() { Name = "Deneme", BanknoteSelling = "100" };
CurrencyDto currencyDto = MyMapper<CurrencyDto>.Map(currency);

List<Currency> currencies = data.GetCurrenyData();
List<CurrencyDto> currencyDtos = MyMapper<CurrencyDto>.MapList(currencies);


#endregion


#region Pagination

IQueryable<Currency> currencies1 = currencies.AsQueryable();

var paginatedData = currencies1.Paginate(5, 1);




#endregion






Console.ReadKey();