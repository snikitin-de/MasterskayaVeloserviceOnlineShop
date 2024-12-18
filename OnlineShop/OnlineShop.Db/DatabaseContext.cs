using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : IdentityDbContext<User, Role, string>
    {
        // Доступ к таблицам
        public DbSet<Service> Services { get; set; }
        public DbSet<BikePart> BikeParts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<BikePart>("BikePart")
                .HasValue<Service>("Service");

            // Создаём последовательность, если её ещё нет
            modelBuilder.HasSequence<int>("OrderNumberSequence")
                .StartsAt(1)
                .IncrementsBy(1);

            // Настраиваем использование последовательности для поля Number
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Привязка последовательности к полю Number
                entity.Property(e => e.Number)
                    .HasDefaultValueSql("nextval('\"OrderNumberSequence\"')");
            });

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Avatar)
                .WithOne()
                .HasForeignKey<Image>(img => img.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Инициализация данных для BikePart
            modelBuilder.Entity<BikePart>().HasData(
                new BikePart
                {
                    Id = "1a83dfdd-6260-4b96-809a-a4d48b220981",
                    Name = "Shimano Ultegra R8000 11 Speed Rear Cassette",
                    Price = 7990.0m,
                    Description = "Легкая 11-скоростная кассета HG-EV с жестким держателем звездочек из сплава для снижения веса и улучшения характеристик переключения, даже при самых сложных переключениях. Звездочки Hyperglide были разработаны для снижения веса и улучшения формы зубьев, что обеспечивает точность переключения и снижение износа. Более широкий диапазон передач для удовлетворения потребностей всех типов велосипедистов.",
                    Category = Category.BikeParts,
                    IsComparable = true,
                    Manufacturer = "Shimano",
                    Model = "Ultegra R8000",
                    Material = "Сталь",
                    Color = "Серебристый",
                    TypeAndPurpose = "Для дорожного велосипеда",
                    PartCategory = BikePartsCategories.Cassettes
                },
                new BikePart
                {
                    Id = "78227e3f-f067-4174-8f7b-167550aa6732",
                    Name = "Shimano CS-M5100 Deore 11-Speed Cassette, 11-51T",
                    Price = 9990.0m,
                    Description = "11-скоростная кассета Deore M5100 с легкой минимальной конструкцией держателя звездочек, предназначенная для горных велосипедов. Звездочки Hyperglide имеют разработанную компьютером конфигурацию зубьев с контурными зацеплениями, что обеспечивает четкое плавное переключение даже под нагрузкой.",
                    Category = Category.BikeParts,
                    IsComparable = true,
                    Manufacturer = "Shimano",
                    Model = "CS-M5100 Deore",
                    Material = "Сталь",
                    Color = "Серебристый",
                    TypeAndPurpose = "Для горного велосипеда",
                    PartCategory = BikePartsCategories.Cassettes
                },
                new BikePart
                {
                    Id = "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c",
                    Name = "Nukeproof Horizon V2 Front Wheel 29in",
                    Price = 12990.0m,
                    Description = "Это переднее колесо для MTB имеет обновленную конструкцию, которая обеспечивает быстрый и плавный ход, когда вы рассекаете по тропам. В конструкции обода используется сплав Nukeproof, прошедший всестороннюю проверку и испытания, и он специально создан для удовлетворения требований гонщиков эндуро и DH. Обладая твердостью, как у сплавов 7-й серии, но пластичностью и текучестью, как у сплавов 6-й серии, это переднее колесо дает вам лучшее из двух миров в плане прочности, жесткости и веса.",
                    Category = Category.BikeParts,
                    IsComparable = true,
                    Manufacturer = "Nukeproof",
                    Model = "Horizon V2",
                    Material = "Сплав",
                    Color = "Чёрный",
                    Diameter = 29.5f,
                    TypeAndPurpose = "Для горного велосипеда",
                    PartCategory = BikePartsCategories.Wheels
                }
            );

            // Инициализация данных для Service
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = "40fbb918-a67a-4650-9716-59609e74638b",
                    Name = "Сборка велосипеда из коробки",
                    Price = 1500.0m,
                    Description = "Купили велосипед в коробке и не знаете, с чего начать? Наша команда профессионалов поможет вам! Мы предлагаем услугу по качественной сборке велосипеда из коробки. Независимо от модели и сложности конструкции, наши опытные механики быстро и надежно подготовят ваш велосипед к поездке.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "2a7bb4d5-8e14-4f59-87c2-1a7df507b312",
                    Name = "Перевод трансмиссии на парафиновую смазку",
                    Price = 2500.0m,
                    Description = "Хотите продлить срок службы трансмиссии вашего велосипеда и повысить её эффективность? Мы предлагаем профессиональную услугу по переводу трансмиссии на парафиновую смазку. Этот метод смазки снижает износ компонентов, улучшает работу цепи и обеспечивает более плавное переключение передач.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1",
                    Name = "Настройка заднего переключателя",
                    Price = 400.0m,
                    Description = "Ищете идеальное переключение передач без задержек и шумов? Мы предлагаем услугу профессиональной настройки заднего переключателя, чтобы ваш велосипед работал плавно и точно.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "b943c472-2e8a-4bb5-8f1d-226fd287afc2",
                    Name = "Настройка переднего переключателя",
                    Price = 400.0m,
                    Description = "Нуждаетесь в плавной работе переднего переключателя? Мы предлагаем профессиональную настройку переднего переключателя, чтобы обеспечить точное и быстрое переключение передач.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1",
                    Name = "Замена заднего переключателя и настройка",
                    Price = 800.0m,
                    Description = "Нужна замена заднего переключателя? Мы предлагаем комплексную услугу, включающую замену старого переключателя, установку нового, а также настройку его работы.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "3e769c34-4f43-4e92-905e-b9f6258476df",
                    Name = "Замена переднего переключателя",
                    Price = 800.0m,
                    Description = "Необходима замена переднего переключателя? Мы предлагаем полное обслуживание, включая замену старого переключателя, установку нового, а также снятие и установку цепи для оптимальной работы трансмиссии.",
                    Category = Category.Services,
                    IsComparable = false
                },
                new Service
                {
                    Id = "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e",
                    Name = "Замена кассеты/трещотки",
                    Price = 300.0m,
                    Description = "Требуется замена кассеты или трещотки на вашем велосипеде? Мы предоставляем услугу по замене изношенных кассет и трещоток для восстановления эффективного переключения передач и улучшения работы трансмиссии.",
                    Category = Category.Services,
                    IsComparable = false
                }
            );

            // Добавление изображения товаров
            modelBuilder.Entity<Image>().HasData(
                new Image
                {
                    Id = "2e49aefe-214e-408c-8f29-cba1a23220e3",
                    Path = "/images/products/bikeparts/6b5785a5-f3dc-4ebc-a807-0676002e860d.png",
                    ProductId = "1a83dfdd-6260-4b96-809a-a4d48b220981"
                },
                new Image
                {
                    Id = "87b267cb-18c4-4b91-a858-731156dfb235",
                    Path = "/images/products/bikeparts/c76e9f7f-25a9-43cc-a5ee-fd8a2563aa40.png",
                    ProductId = "78227e3f-f067-4174-8f7b-167550aa6732"
                },
                new Image
                {
                    Id = "19761726-bd85-42e2-a4a3-8da87f5ad71c",
                    Path = "/images/products/bikeparts/54076d35-d6af-494c-9c5b-1852f1ebad35.png",
                    ProductId = "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c"
                },
                new Image
                {
                    Id = "b4a82ab4-faec-4b80-9d98-413c32471478",
                    Path = "/images/products/services/858fa2fc-bbd0-45e7-a7a4-e52f27f04c3a.jpg",
                    ProductId = "40fbb918-a67a-4650-9716-59609e74638b"
                },
                new Image
                {
                    Id = "d781af42-a288-48b9-a6d3-44dc59ec6258",
                    Path = "/images/products/services/b52bdcf4-643d-436b-a90c-6ca8c7c1ed9b.jpg",
                    ProductId = "2a7bb4d5-8e14-4f59-87c2-1a7df507b312"
                },
                new Image
                {
                    Id = "52eb9beb-2fdf-4d1b-867f-d37dad3438b0",
                    Path = "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg",
                    ProductId = "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1"
                },
                new Image
                {
                    Id = "e329c54d-fa0f-48a6-907e-043d8dfb6611",
                    Path = "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg",
                    ProductId = "b943c472-2e8a-4bb5-8f1d-226fd287afc2"
                },
                new Image
                {
                    Id = "984e4ed6-1ef2-4a90-8b14-f9ded3904c3f",
                    Path = "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg",
                    ProductId = "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1"
                },
                new Image
                {
                    Id = "e11c656d-b637-4974-b54c-21f92a6cc4f8",
                    Path = "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg",
                    ProductId = "3e769c34-4f43-4e92-905e-b9f6258476df"
                },
                new Image
                {
                    Id = "c6b259a1-1835-43c6-91b7-7799e2e633bd",
                    Path = "/images/products/services/3b7fc121-e244-47c4-b6a2-dac06fc3f9d2.jpg",
                    ProductId = "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e"
                }
            );
        }
    }
}
