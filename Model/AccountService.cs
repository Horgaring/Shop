using Microsoft.EntityFrameworkCore;
using WEBAPP.Dbmodels;

public class AccountService : IAccountService{
    private  IHttpContextAccessor Accessor { get; set; }
    private  dbcontextproduct db;
    public  AccountService(IHttpContextAccessor accessor,dbcontextproduct db) => 
        (this.db,this.Accessor) = (db,accessor);

    public Account GetUser()
    {
        var email = Accessor.HttpContext!.User!.Claims.First(p => p.Type == "Email").Value;
       return db.Accounts
                .AsNoTracking()
                .Include(p => p.Products)
                .First(p => p.Email == email);
    }

    
}
