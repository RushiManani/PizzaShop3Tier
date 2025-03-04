using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PizzaShop.DAL.Models;

namespace PizzaShop.DAL.Data;

public partial class PizzaShopDbContext : DbContext
{
    public PizzaShopDbContext()
    {
    }

    public PizzaShopDbContext(DbContextOptions<PizzaShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Itemtype> Itemtypes { get; set; }

    public virtual DbSet<Kot> Kots { get; set; }

    public virtual DbSet<Kotitem> Kotitems { get; set; }

    public virtual DbSet<Mappingmodifiergroupwithitem> Mappingmodifiergroupwithitems { get; set; }

    public virtual DbSet<Mappingorderitemwithmodifier> Mappingorderitemwithmodifiers { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Modifiergroup> Modifiergroups { get; set; }

    public virtual DbSet<Modifieritem> Modifieritems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Permissiontype> Permissiontypes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Taxandfee> Taxandfees { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Waitinglist> Waitinglists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=pizzashop;Username=postgres;Password=Tatva@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .HasColumnName("city_name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_country_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_state_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.Iswaiting)
                .HasDefaultValueSql("(0)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("iswaiting");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(10)
                .HasColumnName("mobile_no");
            entity.Property(e => e.Noofperson).HasColumnName("noofperson");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(15)
                .HasColumnName("invoice_no");
            entity.Property(e => e.OrderitemsId).HasColumnName("orderitems_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Orderitems).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderitemsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_orderitems_id_fkey");
        });

        modelBuilder.Entity<Itemtype>(entity =>
        {
            entity.HasKey(e => e.ItemtypeId).HasName("itemtype_pkey");

            entity.ToTable("itemtype");

            entity.Property(e => e.ItemtypeId).HasColumnName("itemtype_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.ItemtypeName)
                .HasMaxLength(100)
                .HasColumnName("itemtype_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Kot>(entity =>
        {
            entity.HasKey(e => e.KotId).HasName("kot_pkey");

            entity.ToTable("kot");

            entity.Property(e => e.KotId).HasColumnName("kot_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Order).WithMany(p => p.Kots)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kot_order_id_fkey");
        });

        modelBuilder.Entity<Kotitem>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("kotitem_pkey");

            entity.ToTable("kotitem");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.KotId).HasColumnName("kot_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Item).WithMany(p => p.Kotitems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kotitem_item_id_fkey");

            entity.HasOne(d => d.Kot).WithMany(p => p.Kotitems)
                .HasForeignKey(d => d.KotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kotitem_kot_id_fkey");
        });

        modelBuilder.Entity<Mappingmodifiergroupwithitem>(entity =>
        {
            entity.HasKey(e => e.MappingmodifiergroupwithitemId).HasName("mappingmodifiergroupwithitem_pkey");

            entity.ToTable("mappingmodifiergroupwithitem");

            entity.Property(e => e.MappingmodifiergroupwithitemId)
                .HasDefaultValueSql("nextval('mappingmodifiergroupwithitem_mappingmodifiergroupwithitem_i_seq'::regclass)")
                .HasColumnName("mappingmodifiergroupwithitem_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");
            entity.Property(e => e.ModifieritemId).HasColumnName("modifieritem_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Mappingmodifiergroupwithitems)
                .HasForeignKey(d => d.ModifiergroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mappingmodifiergroupwithitem_modifiergroup_id_fkey");

            entity.HasOne(d => d.Modifieritem).WithMany(p => p.Mappingmodifiergroupwithitems)
                .HasForeignKey(d => d.ModifieritemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mappingmodifiergroupwithitem_modifieritem_id_fkey");
        });

        modelBuilder.Entity<Mappingorderitemwithmodifier>(entity =>
        {
            entity.HasKey(e => e.Mappingorderitemwithmodifier1).HasName("mappingorderitemwithmodifier_pkey");

            entity.ToTable("mappingorderitemwithmodifier");

            entity.Property(e => e.Mappingorderitemwithmodifier1).HasColumnName("mappingorderitemwithmodifier");
            entity.Property(e => e.ModifieritemId).HasColumnName("modifieritem_id");
            entity.Property(e => e.OrderitemsId).HasColumnName("orderitems_id");

            entity.HasOne(d => d.Modifieritem).WithMany(p => p.Mappingorderitemwithmodifiers)
                .HasForeignKey(d => d.ModifieritemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mappingorderitemwithmodifier_modifieritem_id_fkey");

            entity.HasOne(d => d.Orderitems).WithMany(p => p.Mappingorderitemwithmodifiers)
                .HasForeignKey(d => d.OrderitemsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mappingorderitemwithmodifier_orderitems_id_fkey");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("menuitem_pkey");

            entity.ToTable("menuitem");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("true")
                .HasColumnName("isAvailable");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isDeleted");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPhoto)
                .HasMaxLength(300)
                .HasColumnName("item_photo");
            entity.Property(e => e.ItemtypeId).HasColumnName("itemtype_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("1")
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasColumnType("money")
                .HasColumnName("rate");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menuitem_category_id_fkey");

            entity.HasOne(d => d.Itemtype).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.ItemtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menuitem_itemtype_id_fkey");

            entity.HasOne(d => d.Tax).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.TaxId)
                .HasConstraintName("menuitem_tax_id_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menuitem_unit_id_fkey");
        });

        modelBuilder.Entity<Modifiergroup>(entity =>
        {
            entity.HasKey(e => e.ModifiergroupId).HasName("modifiergroup_pkey");

            entity.ToTable("modifiergroup");

            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("(0)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.ModifiergroupName)
                .HasMaxLength(100)
                .HasColumnName("modifiergroup_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Modifieritem>(entity =>
        {
            entity.HasKey(e => e.ModifieritemId).HasName("modifieritem_pkey");

            entity.ToTable("modifieritem");

            entity.Property(e => e.ModifieritemId).HasColumnName("modifieritem_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("(0)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.ModifiergroupId).HasColumnName("modifiergroup_id");
            entity.Property(e => e.ModifieritemName)
                .HasMaxLength(100)
                .HasColumnName("modifieritem_name");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("1")
                .HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasColumnType("money")
                .HasColumnName("rate");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Modifiergroup).WithMany(p => p.Modifieritems)
                .HasForeignKey(d => d.ModifiergroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifieritem_modifiergroup_id_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Modifieritems)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifieritem_unit_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_customer_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_table_id_fkey");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.OrderitemsId).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.Property(e => e.OrderitemsId).HasColumnName("orderitems_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Itemwisecomments)
                .HasMaxLength(200)
                .HasColumnName("itemwisecomments");
            entity.Property(e => e.ModifieritemId).HasColumnName("modifieritem_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Orderwisecomments)
                .HasMaxLength(200)
                .HasColumnName("orderwisecomments");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Item).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_item_id_fkey");

            entity.HasOne(d => d.Modifieritem).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.ModifieritemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_modifieritem_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_order_id_fkey");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_payment_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .HasColumnName("payment_mode");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_order_id_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permission_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Permissiontype>(entity =>
        {
            entity.HasKey(e => e.PermissiontypeId).HasName("permissiontype_pkey");

            entity.ToTable("permissiontype");

            entity.Property(e => e.PermissiontypeId).HasColumnName("permissiontype_id");
            entity.Property(e => e.CanAddEdit)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("can_add_edit");
            entity.Property(e => e.CanDelete)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("can_delete");
            entity.Property(e => e.CanView)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("can_view");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Permission).WithMany(p => p.Permissiontypes)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permissiontype_permisssion_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissiontypes)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("permissiontype_role_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("review_pkey");

            entity.ToTable("review");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.AmbienceReview).HasColumnName("ambience_review");
            entity.Property(e => e.Comment)
                .HasMaxLength(300)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.FoodReview).HasColumnName("food_review");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ServiceReview).HasColumnName("service_review");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_order_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("section_pkey");

            entity.ToTable("section");

            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("(0)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("isdeleted");
            entity.Property(e => e.SectionName)
                .HasMaxLength(100)
                .HasColumnName("section_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("state_pkey");

            entity.ToTable("state");

            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("state_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("state_country_id_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.StatusName)
                .HasMaxLength(100)
                .HasColumnName("status_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("tables_pkey");

            entity.ToTable("tables");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .HasColumnName("table_name");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tables_section_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Tables)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tables_status_id_fkey");
        });

        modelBuilder.Entity<Taxandfee>(entity =>
        {
            entity.HasKey(e => e.TaxId).HasName("taxandfees_pkey");

            entity.ToTable("taxandfees");

            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.DefaultTax)
                .HasDefaultValueSql("(1)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("default_tax");
            entity.Property(e => e.Isenabled)
                .HasDefaultValueSql("(0)::bit(1)")
                .HasColumnType("bit(1)")
                .HasColumnName("isenabled");
            entity.Property(e => e.TaxAmount)
                .HasColumnType("money")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TaxName)
                .HasMaxLength(100)
                .HasColumnName("tax_name");
            entity.Property(e => e.TaxType)
                .HasMaxLength(100)
                .HasColumnName("tax_type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("unit_pkey");

            entity.ToTable("unit");

            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.UnitName)
                .HasMaxLength(100)
                .HasColumnName("unit_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsLoginFirstTime).HasColumnName("isLoginFirstTime");
            entity.Property(e => e.Isactive)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("isactive");
            entity.Property(e => e.Isadmin).HasColumnName("isadmin");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(10)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(500)
                .HasColumnName("profile_picture");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(10)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_city_id_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_country_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_state_id_fkey");
        });

        modelBuilder.Entity<Waitinglist>(entity =>
        {
            entity.HasKey(e => e.WaitinglistId).HasName("waitinglist_pkey");

            entity.ToTable("waitinglist");

            entity.Property(e => e.WaitinglistId).HasColumnName("waitinglist_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Customer).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitinglist_customer_id_fkey");

            entity.HasOne(d => d.Table).WithMany(p => p.Waitinglists)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("waitinglist_table_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
