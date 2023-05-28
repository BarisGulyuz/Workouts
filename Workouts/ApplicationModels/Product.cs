namespace Workouts.ApplicationModels
{
    public class Product
    {
        public int Stock { get; set; }

        public Task AddStock(int stock)
        {
            Stock += stock;
            return Task.CompletedTask;
        }
     
        public Task RemoveStock(int stock)
        {
            Stock -= stock;
            return Task.CompletedTask;
        }
    }
}
