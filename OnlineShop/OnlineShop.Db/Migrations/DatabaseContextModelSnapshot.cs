﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence<int>("OrderNumberSequence");

            modelBuilder.Entity("ComparisonProduct", b =>
                {
                    b.Property<string>("ComparisonId")
                        .HasColumnType("text");

                    b.Property<string>("ProductsId")
                        .HasColumnType("text");

                    b.HasKey("ComparisonId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ComparisonProduct");
                });

            modelBuilder.Entity("FavoritesProduct", b =>
                {
                    b.Property<string>("FavoritesId")
                        .HasColumnType("text");

                    b.Property<string>("ProductsId")
                        .HasColumnType("text");

                    b.HasKey("FavoritesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("FavoritesProduct");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CartId")
                        .HasColumnType("text");

                    b.Property<string>("ProductId")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Favorites", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("ProductId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = "2e49aefe-214e-408c-8f29-cba1a23220e3",
                            Path = "/images/products/bikeparts/6b5785a5-f3dc-4ebc-a807-0676002e860d.png",
                            ProductId = "1a83dfdd-6260-4b96-809a-a4d48b220981"
                        },
                        new
                        {
                            Id = "87b267cb-18c4-4b91-a858-731156dfb235",
                            Path = "/images/products/bikeparts/c76e9f7f-25a9-43cc-a5ee-fd8a2563aa40.png",
                            ProductId = "78227e3f-f067-4174-8f7b-167550aa6732"
                        },
                        new
                        {
                            Id = "19761726-bd85-42e2-a4a3-8da87f5ad71c",
                            Path = "/images/products/bikeparts/54076d35-d6af-494c-9c5b-1852f1ebad35.png",
                            ProductId = "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c"
                        },
                        new
                        {
                            Id = "b4a82ab4-faec-4b80-9d98-413c32471478",
                            Path = "/images/products/services/858fa2fc-bbd0-45e7-a7a4-e52f27f04c3a.jpg",
                            ProductId = "40fbb918-a67a-4650-9716-59609e74638b"
                        },
                        new
                        {
                            Id = "d781af42-a288-48b9-a6d3-44dc59ec6258",
                            Path = "/images/products/services/b52bdcf4-643d-436b-a90c-6ca8c7c1ed9b.jpg",
                            ProductId = "2a7bb4d5-8e14-4f59-87c2-1a7df507b312"
                        },
                        new
                        {
                            Id = "52eb9beb-2fdf-4d1b-867f-d37dad3438b0",
                            Path = "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg",
                            ProductId = "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1"
                        },
                        new
                        {
                            Id = "e329c54d-fa0f-48a6-907e-043d8dfb6611",
                            Path = "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg",
                            ProductId = "b943c472-2e8a-4bb5-8f1d-226fd287afc2"
                        },
                        new
                        {
                            Id = "984e4ed6-1ef2-4a90-8b14-f9ded3904c3f",
                            Path = "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg",
                            ProductId = "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1"
                        },
                        new
                        {
                            Id = "e11c656d-b637-4974-b54c-21f92a6cc4f8",
                            Path = "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg",
                            ProductId = "3e769c34-4f43-4e92-905e-b9f6258476df"
                        },
                        new
                        {
                            Id = "c6b259a1-1835-43c6-91b7-7799e2e633bd",
                            Path = "/images/products/services/3b7fc121-e244-47c4-b6a2-dac06fc3f9d2.jpg",
                            ProductId = "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Middlename")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"OrderNumberSequence\"')");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductId")
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrdersItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsComparable")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasDiscriminator<string>("ProductType").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("CanBeDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("CanBeEdited")
                        .HasColumnType("boolean");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Middlename")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Db.Models.BikePart", b =>
                {
                    b.HasBaseType("OnlineShop.Db.Models.Product");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<float?>("Depth")
                        .HasColumnType("real");

                    b.Property<float?>("Diameter")
                        .HasColumnType("real");

                    b.Property<float?>("InstallationDiameter")
                        .HasColumnType("real");

                    b.Property<float?>("InstallationWidth")
                        .HasColumnType("real");

                    b.Property<float?>("Length")
                        .HasColumnType("real");

                    b.Property<int?>("LinksNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("text");

                    b.Property<string>("Material")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<int>("PartCategory")
                        .HasColumnType("integer");

                    b.Property<string>("TypeAndPurpose")
                        .HasColumnType("text");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.Property<float?>("Width")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("BikePart");

                    b.HasData(
                        new
                        {
                            Id = "1a83dfdd-6260-4b96-809a-a4d48b220981",
                            Category = 1,
                            Description = "Легкая 11-скоростная кассета HG-EV с жестким держателем звездочек из сплава для снижения веса и улучшения характеристик переключения, даже при самых сложных переключениях. Звездочки Hyperglide были разработаны для снижения веса и улучшения формы зубьев, что обеспечивает точность переключения и снижение износа. Более широкий диапазон передач для удовлетворения потребностей всех типов велосипедистов.",
                            IsComparable = true,
                            Name = "Shimano Ultegra R8000 11 Speed Rear Cassette",
                            Price = 7990.0m,
                            Color = "Серебристый",
                            Manufacturer = "Shimano",
                            Material = "Сталь",
                            Model = "Ultegra R8000",
                            PartCategory = 1,
                            TypeAndPurpose = "Для дорожного велосипеда"
                        },
                        new
                        {
                            Id = "78227e3f-f067-4174-8f7b-167550aa6732",
                            Category = 1,
                            Description = "11-скоростная кассета Deore M5100 с легкой минимальной конструкцией держателя звездочек, предназначенная для горных велосипедов. Звездочки Hyperglide имеют разработанную компьютером конфигурацию зубьев с контурными зацеплениями, что обеспечивает четкое плавное переключение даже под нагрузкой.",
                            IsComparable = true,
                            Name = "Shimano CS-M5100 Deore 11-Speed Cassette, 11-51T",
                            Price = 9990.0m,
                            Color = "Серебристый",
                            Manufacturer = "Shimano",
                            Material = "Сталь",
                            Model = "CS-M5100 Deore",
                            PartCategory = 1,
                            TypeAndPurpose = "Для горного велосипеда"
                        },
                        new
                        {
                            Id = "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c",
                            Category = 1,
                            Description = "Это переднее колесо для MTB имеет обновленную конструкцию, которая обеспечивает быстрый и плавный ход, когда вы рассекаете по тропам. В конструкции обода используется сплав Nukeproof, прошедший всестороннюю проверку и испытания, и он специально создан для удовлетворения требований гонщиков эндуро и DH. Обладая твердостью, как у сплавов 7-й серии, но пластичностью и текучестью, как у сплавов 6-й серии, это переднее колесо дает вам лучшее из двух миров в плане прочности, жесткости и веса.",
                            IsComparable = true,
                            Name = "Nukeproof Horizon V2 Front Wheel 29in",
                            Price = 12990.0m,
                            Color = "Чёрный",
                            Diameter = 29.5f,
                            Manufacturer = "Nukeproof",
                            Material = "Сплав",
                            Model = "Horizon V2",
                            PartCategory = 0,
                            TypeAndPurpose = "Для горного велосипеда"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Service", b =>
                {
                    b.HasBaseType("OnlineShop.Db.Models.Product");

                    b.HasDiscriminator().HasValue("Service");

                    b.HasData(
                        new
                        {
                            Id = "40fbb918-a67a-4650-9716-59609e74638b",
                            Category = 0,
                            Description = "Купили велосипед в коробке и не знаете, с чего начать? Наша команда профессионалов поможет вам! Мы предлагаем услугу по качественной сборке велосипеда из коробки. Независимо от модели и сложности конструкции, наши опытные механики быстро и надежно подготовят ваш велосипед к поездке.",
                            IsComparable = false,
                            Name = "Сборка велосипеда из коробки",
                            Price = 1500.0m
                        },
                        new
                        {
                            Id = "2a7bb4d5-8e14-4f59-87c2-1a7df507b312",
                            Category = 0,
                            Description = "Хотите продлить срок службы трансмиссии вашего велосипеда и повысить её эффективность? Мы предлагаем профессиональную услугу по переводу трансмиссии на парафиновую смазку. Этот метод смазки снижает износ компонентов, улучшает работу цепи и обеспечивает более плавное переключение передач.",
                            IsComparable = false,
                            Name = "Перевод трансмиссии на парафиновую смазку",
                            Price = 2500.0m
                        },
                        new
                        {
                            Id = "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1",
                            Category = 0,
                            Description = "Ищете идеальное переключение передач без задержек и шумов? Мы предлагаем услугу профессиональной настройки заднего переключателя, чтобы ваш велосипед работал плавно и точно.",
                            IsComparable = false,
                            Name = "Настройка заднего переключателя",
                            Price = 400.0m
                        },
                        new
                        {
                            Id = "b943c472-2e8a-4bb5-8f1d-226fd287afc2",
                            Category = 0,
                            Description = "Нуждаетесь в плавной работе переднего переключателя? Мы предлагаем профессиональную настройку переднего переключателя, чтобы обеспечить точное и быстрое переключение передач.",
                            IsComparable = false,
                            Name = "Настройка переднего переключателя",
                            Price = 400.0m
                        },
                        new
                        {
                            Id = "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1",
                            Category = 0,
                            Description = "Нужна замена заднего переключателя? Мы предлагаем комплексную услугу, включающую замену старого переключателя, установку нового, а также настройку его работы.",
                            IsComparable = false,
                            Name = "Замена заднего переключателя и настройка",
                            Price = 800.0m
                        },
                        new
                        {
                            Id = "3e769c34-4f43-4e92-905e-b9f6258476df",
                            Category = 0,
                            Description = "Необходима замена переднего переключателя? Мы предлагаем полное обслуживание, включая замену старого переключателя, установку нового, а также снятие и установку цепи для оптимальной работы трансмиссии.",
                            IsComparable = false,
                            Name = "Замена переднего переключателя",
                            Price = 800.0m
                        },
                        new
                        {
                            Id = "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e",
                            Category = 0,
                            Description = "Требуется замена кассеты или трещотки на вашем велосипеде? Мы предоставляем услугу по замене изношенных кассет и трещоток для восстановления эффективного переключения передач и улучшения работы трансмиссии.",
                            IsComparable = false,
                            Name = "Замена кассеты/трещотки",
                            Price = 300.0m
                        });
                });

            modelBuilder.Entity("ComparisonProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Comparison", null)
                        .WithMany()
                        .HasForeignKey("ComparisonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FavoritesProduct", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Favorites", null)
                        .WithMany()
                        .HasForeignKey("FavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", "Cart")
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId");

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Image", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Db.Models.User", null)
                        .WithOne("Avatar")
                        .HasForeignKey("OnlineShop.Db.Models.Image", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.OrderItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Navigation("Avatar");
                });
#pragma warning restore 612, 618
        }
    }
}
