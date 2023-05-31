using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using WEBAPP.Dbmodels;

public static class Img{
    public static string save_img(IFormFile File,int Id_Prod,string prodname){
        using (Stream fs = new FileStream($"./img/{Id_Prod}_{prodname}.webp",FileMode.Create)){
            File.CopyTo(fs);
        }
        return $"{Id_Prod}_{prodname}.webp";
    }
    public static string save_img_acc(IFormFile File,string Email){
        using (Stream fs = new FileStream($"./img/Account/{Email}.webp",FileMode.Create)){
            File.CopyTo(fs);
        }
        return $"{Email}.webp";
    }
}
public  class castombinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        throw new NotImplementedException();
    }
}
[Authorize]
public class Habchat : Hub{
    dbcontextproduct db;
    IMemoryCache cache;
    Serilog.ILogger log = Log.Logger;
    
     IAccountService maneg;
    public Habchat(dbcontextproduct db ,IAccountService maneg,IMemoryCache cache) => 
        (this.db,this.maneg,this.cache) = (db,maneg,cache);
    public override Task OnConnectedAsync(){
       
        return base.OnConnectedAsync();
    }
    public  override Task OnDisconnectedAsync(Exception? exception){
        int id;
        cache.TryGetValue(Context.ConnectionId,out id);
        db.Group.Find(id)!.Online--;
        db.SaveChanges();
        return base.OnDisconnectedAsync(exception);
    }
    [Authorize]
    public async Task Enter(int Id_Product, string? GroupName = null){
        
            ChatModel?  group;
            Account? a = maneg.GetUser();
            db.Attach<Account>(a);
            if (GroupName is not null){
                group = await db.Group
                .Include(p => p.Product)
                .Include(p => p.Users)
                .Include(p => p.Messages)
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.GroupName.ToString() == GroupName);
            }
            else{
                group = await db.Group
                .Include(p => p.Users)
                .Include(p => p.Product)
                .Include(p => p.Messages)
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.productId == Id_Product);
            }
            if ( group == null && a != null) {
                var guid = Guid.NewGuid();
                await Groups.AddToGroupAsync(Context.ConnectionId, guid.ToString());
                var prod = db.product.Include(p => p.account).First(p => p.Id == Id_Product);
                db.Group.Add(new ChatModel{GroupName = guid,Product = prod,Users = new List<Account>(){prod.account,a},Online = 1,Messages = new()});
                await Clients.Group(guid.ToString()).SendAsync("Receive", $"{Context.ConnectionId} вошел в чат");
            }
            else if(group != null  ){
                group.Online++;
                await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName.ToString());
                var mass = group.Messages;
                if (mass is not null){
                foreach (var item in mass){
                    if(item.accountId != a.Id) item.IsRead = true;
                    await this.Clients.Caller.SendAsync("Receive", item.message,item.Account.Name);
                }  
                }
            }
        db.SaveChanges();
        cache.Set(Context.ConnectionId,group.Id);

    }
    [Authorize]
    public async Task Send(string message,int Id_Product ,string username,[FromServices] IAccountService maneg)
    {
        
        Account? a = maneg.GetUser();
        
        var group = await db.Group
                .Include(p => p.Users)
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.productId == Id_Product);
       
        db.Message.Add(new Message{Group = group,Account = a,message = message,IsRead = group.Online == 2});
        db.SaveChanges();
        
        await this.Clients.Group(group.GroupName.ToString()).SendAsync("Receive", message,username);
        
    }
}
