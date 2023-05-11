using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;

public static class Img{
    public static string save_img(IFormFile File,int Id_Prod,string prodname){
        using (Stream fs = new FileStream($"./img/{Id_Prod}_{prodname}.webp",FileMode.Create)){
            File.CopyTo(fs);
        }
        return $"{Id_Prod}_{prodname}.webp";
    }
    public static string save_img_acc(IFormFile File,int Id_acc,string prodname){
        using (Stream fs = new FileStream($"./img/Account/{Id_acc}.webp",FileMode.Create)){
            File.CopyTo(fs);
        }
        return $"{Id_acc}.webp";
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
    public Habchat(dbcontextproduct db ){
        this.db = db; 
    }

    [Authorize]
    public async Task Enter(string groupname, int Id_Product){
        try{
            var email = Context.User!.Claims.First(p => p.Type == "Email").Value;
            var a = db.Accounts.AsNoTracking().AsQueryable().First(p => p.Email == email);
            var group = await db.Group.AsNoTracking().AsQueryable().FirstOrDefaultAsync(e => e.GroupName == groupname);
            if ( group == null && a != null) {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                var user2 = db.product.Include(p => p.account).AsNoTracking().AsQueryable().First(p => p.Id == Id_Product).account;
                db.Group.Add(new ChatModel{GroupName = groupname,Users = new List<Account>(){a,}});
                db.SaveChanges();
                await Clients.Group(groupname).SendAsync("Receive", $"{Context.ConnectionId} вошел в чат");
            }
            else if(group != null && group.Users.Count < 2 ){
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                group.Users.Add(a!);
            }
        }
        catch (Exception e){
          log.Error($"Chat error {e.Message}");
        }
        
    }
    [Authorize]
    public async Task Send(string message,string groupname,string username)
    {
        var email = Context.User!.Claims.First(p => p.Type == "Email").Value;
        var a = db.Accounts.AsNoTracking().AsQueryable().First(p => p.Email == email);
        var group = await db.Group.Include(p => p.Users).AsNoTracking().AsQueryable().FirstOrDefaultAsync(e => e.GroupName == groupname);
        if(a.Id == group.Users[0].Id){
            group.UserMessage += "."+message+".";
        }else{
            group.User2Message += "."+message+".";
        }
        await this.Clients.Group(groupname).SendAsync("Receive", message,username);
        
    }
}
