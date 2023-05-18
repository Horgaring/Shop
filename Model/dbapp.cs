using Microsoft.EntityFrameworkCore;


public class dbcontextproduct : DbContext{
       public DbSet<Product> product {get; set;} = null!;
       public DbSet<Account> Accounts {get; set;} = null!;
       public DbSet<Categ> Categ {get; set;} = null!;
       public DbSet<JWTdb> Refresh {get; set;} = null!;
       public DbSet<AccountChatModel> AccountChatModel {get; set;}
       public DbSet<ChatModel> Group {get; set;} = null!;
        public  dbcontextproduct(DbContextOptions<dbcontextproduct> options) : base(options)
        {
        
            
          

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=app;Password=root");
            
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<ChatModel>().Property(p=>p.UserMessage).HasDefaultValue(string.Empty);
          modelBuilder.Entity<ChatModel>().Property(p=>p.UnreadMessages).HasDefaultValue(string.Empty);
          modelBuilder.Entity<Account>().Property(p => p.Wallet).HasDefaultValue<int>(0);
          modelBuilder.Entity<Account>().HasIndex(p => p.Name).IsUnique();
          modelBuilder.Entity<AccountChatModel>().HasKey(u => new { u.GroupsId, u.UsersId});
          modelBuilder.Entity<Account>()
            .HasMany(p => p.Groups)
            .WithMany(p => p.Users);
          modelBuilder.Entity<Product>()
          .HasOne(p => p.account)
          .WithMany(p => p.Products)
          .OnDelete(DeleteBehavior.Cascade);
          
          
        }
        
    
}