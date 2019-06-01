using GoToWorkFactoryModel;
using System.Data.Entity;

namespace GoToWorkFactoryImplementDataBase
{
    public class FactoryDbContext : DbContext
    {
        public FactoryDbContext() : base("FactoryDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
    }
}