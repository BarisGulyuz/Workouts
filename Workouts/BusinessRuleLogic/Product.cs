using static Workouts.BusinessRuleLogic.BusinessRuleObjects;

namespace Workouts.BusinessRuleLogic.Product
{
    public sealed class Product
    {
        public Product()
        {

        }
        public Product(int id, string name, int categoryId, int stock)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Stock = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
    public class ProductBusinessRule
    {
        static List<Product> Products = new List<Product>
            {
                new Product(1,"Elma", 1, 10),
                new Product(2,"Armut", 1, 20),
            };

        const int MIN_STOCK = 5;
        const int MAX_STOCK = 1000;
        const int MAX_PRODCUCT_COUNT_FOREACH_CATEGORY = 5;

        private Product _product;

        public ProductBusinessRule(Product product)
        {
            _product = product;
        }

        public BusinessRuleResponse CheckProductCategoryCount()
        {
            BusinessRuleResponse businessRuleResponse = new BusinessRuleResponse();
            if (Products.Where(p => p.CategoryId == _product.CategoryId).Count() > MAX_PRODCUCT_COUNT_FOREACH_CATEGORY)
            {
                string message = $"Bu kategoride hali hazırda {MAX_PRODCUCT_COUNT_FOREACH_CATEGORY} adet ürün var. Daha fazla eklenemez";
                businessRuleResponse.AddError(message);
            }

            return businessRuleResponse;
        }
        public BusinessRuleResponse CheckProductStock()
        {
            BusinessRuleResponse businessRuleResponse = new BusinessRuleResponse();
            if (_product.Stock < MIN_STOCK)
            {
                string message = "Ürün Stoğu 5'in altında olmamaz";
                businessRuleResponse.AddError(message);
            }
            else if (_product.Stock > MAX_STOCK)
            {
                string message = "Ürün Stoğu 100'ün üstünde olmamaz";
                businessRuleResponse.AddError(message);
            }

            return businessRuleResponse;
        }
        public BusinessRuleResponse CheckProductName()
        {
            BusinessRuleResponse businessRuleResponse = new BusinessRuleResponse();
            if (Products.Any(p => p.Name == _product.Name))
            {
                string message = "Bu isimde ürün mevcut";
                businessRuleResponse.AddError(message);
            }

            return businessRuleResponse;
        }
    }
    public class ProductService
    {
        public void Add(Product product)
        {
            ProductBusinessRule productBusinessRule = new ProductBusinessRule(product);

            var response = BusinessRuleManager.Control(stopOnFirstError: false,
                                                             productBusinessRule.CheckProductName,
                                                             productBusinessRule.CheckProductStock,
                                                             productBusinessRule.CheckProductCategoryCount);

            if (response.IsValid == false)
            {
                throw new Exception(response.ToMessage());
            }

            Console.WriteLine("Ürün Kaydedildi");
        }

        public void Update(Product product)
        {
            ProductBusinessRule productBusinessRule = new ProductBusinessRule(product);
            var response = BusinessRuleManager.Control(stopOnFirstError: false,
                                                             productBusinessRule.CheckProductName,
                                                             productBusinessRule.CheckProductStock,
                                                             productBusinessRule.CheckProductCategoryCount);

            if (response.IsValid == false)
            {
                throw new Exception(response.ToMessage());
            }

            Console.WriteLine("Ürün Update Edildi");
        }

        public void AddStock(int productId, int stock)
        {
            Product product = Products.First(p => p.Id == productId);

            if (product is null)
            {
                throw new Exception("Böyle bir ürün bulunamadı");
            }

            product.Stock = product.Stock + stock;

            ProductBusinessRule productBusinessRule = new ProductBusinessRule(product);
            var response = BusinessRuleManager.Control(stopOnFirstError: true,
                                                       productBusinessRule.CheckProductStock);

            if (response.IsValid == false)
            {
                throw new Exception(response.ToMessage());
            }

            Console.WriteLine("Ürün Stoğu Eklendi");
        }
    }

}
