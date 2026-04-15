namespace Project.DAL
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public ICartRepository CartRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        Task SaveAsync();
    }
}
