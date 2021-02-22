using Microsoft.EntityFrameworkCore;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class DB_PAMYSContext : DbContext
    {

        public DB_PAMYSContext()
        {
        }

        public DB_PAMYSContext(DbContextOptions<DB_PAMYSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3307;database=DB_PAMYS;user=root;password=root;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.13-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);
                entity.ToTable("categorys");

                entity.Property(e => e.IdCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnType("bit(1)")
                    .HasColumnName("active");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("description")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.HasIndex(e => e.idRol, "FKfo9aws26amofdlkjpw2hit7ne");

                entity.HasKey(e => e.idClient);

                entity.Property(e => e.idClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.active)
                    .HasColumnType("bit(1)")
                    .HasColumnName("active");

                entity.Property(e => e.address)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("address")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.email)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.firstName)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("first_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.idRol)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_rol");

                entity.Property(e => e.lastName)
                    .HasColumnType("varchar(45)")
                    .HasColumnName("last_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.password)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("_password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.phone)
                    .HasColumnType("varchar(9)")
                    .HasColumnName("phone")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.profilePictureUrl)
                    .HasColumnType("varchar(200)")
                    .HasColumnName("profile_picture_url")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.username)
                    .HasColumnType("varchar(30)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.zip_code)
                    .HasColumnType("int(11)")
                    .HasColumnName("zip_code");

                entity.HasOne(d => d.role)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.idRol)
                    .HasConstraintName("FKfo9aws26amofdlkjpw2hit7ne");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("document_types");

                entity.HasKey(e => e.idDocumentType);

                entity.Property(e => e.idDocumentType)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.doctype)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("doctype")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.idPaymentStatus, "FK4xx58ik5qol72gccuhnpbq29m");

                entity.HasIndex(e => e.idClient, "FK7qtjfyw39780y67hhk1cq58wl");

                entity.HasIndex(e => e.idOrderStatus, "FKbp6k71rsko970ooke8me82k9");

                entity.HasIndex(e => e.idDocumentType, "FKhtvn3gkhqh1uc70qp2fnhd6qd");

                entity.HasIndex(e => e.idVoucher, "FKk2a9incs212uvouc7oh3g4782");

                entity.HasKey(e => e.idOrder);

                entity.Property(e => e.idOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.comment)
                    .HasColumnType("varchar(150)")
                    .HasColumnName("comment")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.dateCreated)
                    .HasMaxLength(6)
                    .HasColumnName("date_created");

                entity.Property(e => e.idClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_client");

                entity.Property(e => e.idDocumentType)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_document_type");

                entity.Property(e => e.idOrderStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order_status");

                entity.Property(e => e.idPaymentStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_payment_status");

                entity.Property(e => e.idVoucher)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_voucher");

                entity.Property(e => e.igv).HasColumnName("igv");

                entity.Property(e => e.shippingAddress)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("shipping_address")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.subtotal).HasColumnName("subtotal");

                entity.Property(e => e.total).HasColumnName("total");

                entity.Property(e => e.zipCode)
                    .HasColumnType("int(11)")
                    .HasColumnName("zip_code");

                entity.HasOne(d => d.client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.idClient)
                    .HasConstraintName("FK7qtjfyw39780y67hhk1cq58wl");

                entity.HasOne(d => d.documentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.idDocumentType)
                    .HasConstraintName("FKhtvn3gkhqh1uc70qp2fnhd6qd");

                entity.HasOne(d => d.orderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.idOrderStatus)
                    .HasConstraintName("FKbp6k71rsko970ooke8me82k9");

                entity.HasOne(d => d.paymentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.idPaymentStatus)
                    .HasConstraintName("FK4xx58ik5qol72gccuhnpbq29m");

                entity.HasOne(d => d.voucher)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.idVoucher)
                    .HasConstraintName("FKk2a9incs212uvouc7oh3g4782");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.idOrder, e.idProduct })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("order_details");

                entity.HasIndex(e => e.idProduct, "FK41ypdnsfa4cd6poqkbthg94nc");

                entity.Property(e => e.idOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.idProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.Property(e => e.price)
                    .HasColumnType("decimal()")
                    .HasColumnName("price");

                entity.Property(e => e.quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.HasOne(d => d.order)
                    .WithMany(p => p.products)
                    .HasForeignKey(d => d.idOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK6h10g6el6eyicu33ddse0gm3v");

                entity.HasOne(d => d.product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.idProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK41ypdnsfa4cd6poqkbthg94nc");

            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.HasKey(e => e.idOrderStatus);

                entity.Property(e => e.idOrderStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.status)
                    .HasColumnType("varchar(20)")
                    .HasColumnName("status")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("payment_types");

                entity.HasKey(e => e.idPaymentType);

                entity.Property(e => e.idPaymentType)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.type)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("type")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct);
                entity.ToTable("products");

                entity.HasIndex(e => e.IdVendor, "FK6723w37bbu82613owfe9o17ln");

                entity.HasIndex(e => e.IdCategory, "FKi08vuuyvs43cumvl0eqm21nfg");

                entity.HasIndex(e => e.Sku, "UK_fhmd06dsmj6k0n90swsh8ie9g")
                    .IsUnique();

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateCreated)
                    .HasMaxLength(6)
                    .HasColumnName("date_created");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("description")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdCategory)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_category");

                entity.Property(e => e.IdVendor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_vendor");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SalePrice).HasColumnName("sale_price");

                entity.Property(e => e.Sku)
                    .HasColumnType("int(11)")
                    .HasColumnName("sku");

                entity.Property(e => e.Slug)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("slug")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Stock)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock");

                entity.Property(e => e.ThumbnailUrl)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("thumbnail_url")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FKi08vuuyvs43cumvl0eqm21nfg");

                entity.HasOne(d => d.vendor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdVendor)
                    .HasConstraintName("FK6723w37bbu82613owfe9o17ln");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.IdProductImages);
                entity.ToTable("product_images");

                entity.HasIndex(e => e.IdProduct, "FKpbcjehp1361qtyyilvx87d26d");

                entity.Property(e => e.IdProductImages)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IdProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_product");

                entity.Property(e => e.Url)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("url")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.productsImages)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FKpbcjehp1361qtyyilvx87d26d");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasKey(e => e.idRole);

                entity.Property(e => e.idRole)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.name)
                    .HasColumnType("varchar(15)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasKey(e => e.IdVendor);
                entity.ToTable("vendors");

                entity.Property(e => e.IdVendor)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Company)
                    .HasColumnType("varchar(40)")
                    .HasColumnName("company")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("description")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("vouchers");

                entity.HasKey(e => e.idVoucher);

                entity.Property(e => e.idVoucher)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.amount).HasColumnName("amount");

                entity.Property(e => e.paymentDate)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.idClient)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_client");

                entity.Property(e => e.idClientAccount)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_client_account");

                entity.Property(e => e.idOperation)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_operation");

                entity.Property(e => e.idStoreAccount)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_store_account");

                entity.Property(e => e.imageUrl)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("image_url")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

