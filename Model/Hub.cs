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
    Serilog.ILogger log = Log.Logger;
    IMemoryCache cache;
     IAccountService maneg;
    public Habchat(dbcontextproduct db ,IMemoryCache cache,[FromServices] IAccountService maneg) => 
        (this.db,this.cache,this.maneg) = (db,cache,maneg);
    public override Task OnConnectedAsync(){
        cache.Set(Context.ConnectionId,maneg.GetUser());
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception){
        cache.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
    [Authorize]
    public async Task Enter(int Id_Product, string? GroupName = null){
        
            ChatModel?  group;
            Account? a;
            if(!cache.TryGetValue(Context.ConnectionId,out a)){
                throw new NullReferenceException("Account not found");
            }
            if (GroupName != null){
                group = await db.Group
                .Include(p => new {p.Users,p.Product})
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.GroupName.ToString() == GroupName);
            }
            else{
                group = await db.Group
                .Include(p => p.Users)
                .Include(p => p.Product)
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.productId == Id_Product);
            }
            if ( group == null && a != null) {
                var guid = Guid.NewGuid();
                await Groups.AddToGroupAsync(Context.ConnectionId, guid.ToString());
                var prod = db.product.Include(p => p.account).AsNoTracking().First(p => p.Id == Id_Product);
                db.Group.Add(new ChatModel{GroupName = guid,Product = prod,Users = new List<Account>(){a,prod.account}});
                db.SaveChanges();
                await Clients.Group(guid.ToString()).SendAsync("Receive", $"{Context.ConnectionId} вошел в чат");
            }
            else if(group != null  ){
                await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName.ToString());
                var mass = group.UserMessage.Split(".");
                if (group.UnreadMessages != string.Empty){
                    mass = mass.Concat(group.UnreadMessages!.Split(".")).ToArray();
                    group.UnreadMessages = string.Empty;
                    group.UserMessage = string.Join('.',mass);
                    db.SaveChanges();
                }
                if (mass.Length == 2){
                   await this.Clients.Caller.SendAsync("Receive", mass[1],mass[0]);
                }
                else{
                    for (int i = 0; i < mass.Length/2; i += 2){
                        await this.Clients.Caller.SendAsync("Receive", mass[i + 1],mass[i]);
                    }
                }
                
                
            }
        
        
        
    }
    [Authorize]
    public async Task Send(string message,int Id_Product ,string username)
    {
        Account? a;
        if(!cache.TryGetValue(Context.ConnectionAborted,out a)){
            throw new NullReferenceException("Account not found");
        }
        var group = await db.Group
                .Include(p => new {p.Users,p.Product})
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Users.Contains(a) && p.productId == Id_Product);
       
            group.UserMessage += a.Name+"."+message+".";
            db.SaveChanges();
        
        await this.Clients.Group(group.GroupName.ToString()).SendAsync("Receive", message,username);
        
    }
}
