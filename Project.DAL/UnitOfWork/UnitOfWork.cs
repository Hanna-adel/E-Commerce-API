namespace Project.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepository {  get;}

        public IOrderRepository OrderRepository {  get;}

        public ICartRepository CartRepository {  get;}

        public ICategoryRepository CategoryRepository {  get;}

        public UnitOfWork(AppDbContext context, IProductRepository productRepository, IOrderRepository orderRepository, ICartRepository cartRepository, ICategoryRepository categoryRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            CartRepository = cartRepository;
            CategoryRepository = categoryRepository;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
