using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderNumberSequence");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CanBeDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CanBeEdited = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Middlename = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comparisons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparisons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OrderNumberSequence\"')"),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Middlename = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    IsComparable = table.Column<bool>(type: "boolean", nullable: false),
                    ProductType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Material = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Diameter = table.Column<float>(type: "real", nullable: true),
                    Width = table.Column<float>(type: "real", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: true),
                    Depth = table.Column<float>(type: "real", nullable: true),
                    LinksNumber = table.Column<int>(type: "integer", nullable: true),
                    InstallationWidth = table.Column<float>(type: "real", nullable: true),
                    InstallationDiameter = table.Column<float>(type: "real", nullable: true),
                    TypeAndPurpose = table.Column<string>(type: "text", nullable: true),
                    PartCategory = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: true),
                    CartId = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComparisonProduct",
                columns: table => new
                {
                    ComparisonId = table.Column<string>(type: "text", nullable: false),
                    ProductsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonProduct", x => new { x.ComparisonId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ComparisonProduct_Comparisons_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "Comparisons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritesProduct",
                columns: table => new
                {
                    FavoritesId = table.Column<string>(type: "text", nullable: false),
                    ProductsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritesProduct", x => new { x.FavoritesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_FavoritesProduct_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritesProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: true),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Color", "Depth", "Description", "Diameter", "InstallationDiameter", "InstallationWidth", "IsComparable", "Length", "LinksNumber", "Manufacturer", "Material", "Model", "Name", "PartCategory", "Price", "ProductType", "TypeAndPurpose", "Weight", "Width" },
                values: new object[] { "1a83dfdd-6260-4b96-809a-a4d48b220981", 1, "Серебристый", null, "Легкая 11-скоростная кассета HG-EV с жестким держателем звездочек из сплава для снижения веса и улучшения характеристик переключения, даже при самых сложных переключениях. Звездочки Hyperglide были разработаны для снижения веса и улучшения формы зубьев, что обеспечивает точность переключения и снижение износа. Более широкий диапазон передач для удовлетворения потребностей всех типов велосипедистов.", null, null, null, true, null, null, "Shimano", "Сталь", "Ultegra R8000", "Shimano Ultegra R8000 11 Speed Rear Cassette", 1, 7990.0m, "BikePart", "Для дорожного велосипеда", null, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsComparable", "Name", "Price", "ProductType" },
                values: new object[,]
                {
                    { "2a7bb4d5-8e14-4f59-87c2-1a7df507b312", 0, "Хотите продлить срок службы трансмиссии вашего велосипеда и повысить её эффективность? Мы предлагаем профессиональную услугу по переводу трансмиссии на парафиновую смазку. Этот метод смазки снижает износ компонентов, улучшает работу цепи и обеспечивает более плавное переключение передач.", false, "Перевод трансмиссии на парафиновую смазку", 2500.0m, "Service" },
                    { "3e769c34-4f43-4e92-905e-b9f6258476df", 0, "Необходима замена переднего переключателя? Мы предлагаем полное обслуживание, включая замену старого переключателя, установку нового, а также снятие и установку цепи для оптимальной работы трансмиссии.", false, "Замена переднего переключателя", 800.0m, "Service" },
                    { "40fbb918-a67a-4650-9716-59609e74638b", 0, "Купили велосипед в коробке и не знаете, с чего начать? Наша команда профессионалов поможет вам! Мы предлагаем услугу по качественной сборке велосипеда из коробки. Независимо от модели и сложности конструкции, наши опытные механики быстро и надежно подготовят ваш велосипед к поездке.", false, "Сборка велосипеда из коробки", 1500.0m, "Service" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Color", "Depth", "Description", "Diameter", "InstallationDiameter", "InstallationWidth", "IsComparable", "Length", "LinksNumber", "Manufacturer", "Material", "Model", "Name", "PartCategory", "Price", "ProductType", "TypeAndPurpose", "Weight", "Width" },
                values: new object[] { "78227e3f-f067-4174-8f7b-167550aa6732", 1, "Серебристый", null, "11-скоростная кассета Deore M5100 с легкой минимальной конструкцией держателя звездочек, предназначенная для горных велосипедов. Звездочки Hyperglide имеют разработанную компьютером конфигурацию зубьев с контурными зацеплениями, что обеспечивает четкое плавное переключение даже под нагрузкой.", null, null, null, true, null, null, "Shimano", "Сталь", "CS-M5100 Deore", "Shimano CS-M5100 Deore 11-Speed Cassette, 11-51T", 1, 9990.0m, "BikePart", "Для горного велосипеда", null, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsComparable", "Name", "Price", "ProductType" },
                values: new object[] { "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1", 0, "Ищете идеальное переключение передач без задержек и шумов? Мы предлагаем услугу профессиональной настройки заднего переключателя, чтобы ваш велосипед работал плавно и точно.", false, "Настройка заднего переключателя", 400.0m, "Service" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Color", "Depth", "Description", "Diameter", "InstallationDiameter", "InstallationWidth", "IsComparable", "Length", "LinksNumber", "Manufacturer", "Material", "Model", "Name", "PartCategory", "Price", "ProductType", "TypeAndPurpose", "Weight", "Width" },
                values: new object[] { "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c", 1, "Чёрный", null, "Это переднее колесо для MTB имеет обновленную конструкцию, которая обеспечивает быстрый и плавный ход, когда вы рассекаете по тропам. В конструкции обода используется сплав Nukeproof, прошедший всестороннюю проверку и испытания, и он специально создан для удовлетворения требований гонщиков эндуро и DH. Обладая твердостью, как у сплавов 7-й серии, но пластичностью и текучестью, как у сплавов 6-й серии, это переднее колесо дает вам лучшее из двух миров в плане прочности, жесткости и веса.", 29.5f, null, null, true, null, null, "Nukeproof", "Сплав", "Horizon V2", "Nukeproof Horizon V2 Front Wheel 29in", 0, 12990.0m, "BikePart", "Для горного велосипеда", null, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsComparable", "Name", "Price", "ProductType" },
                values: new object[,]
                {
                    { "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e", 0, "Требуется замена кассеты или трещотки на вашем велосипеде? Мы предоставляем услугу по замене изношенных кассет и трещоток для восстановления эффективного переключения передач и улучшения работы трансмиссии.", false, "Замена кассеты/трещотки", 300.0m, "Service" },
                    { "b943c472-2e8a-4bb5-8f1d-226fd287afc2", 0, "Нуждаетесь в плавной работе переднего переключателя? Мы предлагаем профессиональную настройку переднего переключателя, чтобы обеспечить точное и быстрое переключение передач.", false, "Настройка переднего переключателя", 400.0m, "Service" },
                    { "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1", 0, "Нужна замена заднего переключателя? Мы предлагаем комплексную услугу, включающую замену старого переключателя, установку нового, а также настройку его работы.", false, "Замена заднего переключателя и настройка", 800.0m, "Service" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Path", "ProductId", "UserId" },
                values: new object[,]
                {
                    { "19761726-bd85-42e2-a4a3-8da87f5ad71c", "/images/products/bikeparts/54076d35-d6af-494c-9c5b-1852f1ebad35.png", "9fa9f0e8-f5a8-4c94-8445-7af80d446d2c", null },
                    { "2e49aefe-214e-408c-8f29-cba1a23220e3", "/images/products/bikeparts/6b5785a5-f3dc-4ebc-a807-0676002e860d.png", "1a83dfdd-6260-4b96-809a-a4d48b220981", null },
                    { "52eb9beb-2fdf-4d1b-867f-d37dad3438b0", "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg", "83a76d89-bb74-4e0d-a1da-bbf3e326d7d1", null },
                    { "87b267cb-18c4-4b91-a858-731156dfb235", "/images/products/bikeparts/c76e9f7f-25a9-43cc-a5ee-fd8a2563aa40.png", "78227e3f-f067-4174-8f7b-167550aa6732", null },
                    { "984e4ed6-1ef2-4a90-8b14-f9ded3904c3f", "/images/products/services/6dc1873c-b77b-45c9-bd19-6aaeee31d504.jpeg", "ff30bb0f-eeb6-4530-8c0d-4ac841204dc1", null },
                    { "b4a82ab4-faec-4b80-9d98-413c32471478", "/images/products/services/858fa2fc-bbd0-45e7-a7a4-e52f27f04c3a.jpg", "40fbb918-a67a-4650-9716-59609e74638b", null },
                    { "c6b259a1-1835-43c6-91b7-7799e2e633bd", "/images/products/services/3b7fc121-e244-47c4-b6a2-dac06fc3f9d2.jpg", "adf5c5b4-7cf5-4205-98c6-dcb6e6b59a2e", null },
                    { "d781af42-a288-48b9-a6d3-44dc59ec6258", "/images/products/services/b52bdcf4-643d-436b-a90c-6ca8c7c1ed9b.jpg", "2a7bb4d5-8e14-4f59-87c2-1a7df507b312", null },
                    { "e11c656d-b637-4974-b54c-21f92a6cc4f8", "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg", "3e769c34-4f43-4e92-905e-b9f6258476df", null },
                    { "e329c54d-fa0f-48a6-907e-043d8dfb6611", "/images/products/services/b179e954-7f79-4305-b33a-a8f2319971ce.jpeg", "b943c472-2e8a-4bb5-8f1d-226fd287afc2", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonProduct_ProductsId",
                table: "ComparisonProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesProduct_ProductsId",
                table: "FavoritesProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_OrderId",
                table: "OrdersItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_ProductId",
                table: "OrdersItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ComparisonProduct");

            migrationBuilder.DropTable(
                name: "FavoritesProduct");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OrdersItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Comparisons");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropSequence(
                name: "OrderNumberSequence");
        }
    }
}
