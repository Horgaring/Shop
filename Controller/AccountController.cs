using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Serilog;

[Route("api/[controller]/")]
[ApiController]
public class AccountController : Controller{
    public dbcontextproduct db;
    public Serilog.ILogger log = Log.Logger;
    public AccountController(dbcontextproduct db) => this.db = db;

    
    [HttpPost("Sigin")]
    public JsonResult sigin([FromForm] AccountLogin pr){
        //log.LogInformation(HttpContext.User.Identity.IsAuthenticated.ToString());
        log.Information(pr.Email);
       var acc = db.Accounts.ToArray().FirstOrDefault(e => e.Email == pr.Email);
        var Refreshtoken = JWTModel.rnd();
        db.Refresh.Add(new JWTdb() {Refreshtoken = Refreshtoken,Time = DateTime.UtcNow});
        db.SaveChanges();
        return Json(new JWTModel() {Accesstoken = JWTModel.jwtcreator(acc.Name,pr.Email), Refreshtoken = Refreshtoken});
    }
    [HttpGet("isauth")]
    [Authorize()]
    public IActionResult Founduser([FromServices] IRepository memoryCache){
        Account? User = null;
        foreach (var item in HttpContext.User.Claims)
            if( item.Type == "Email") 
               User = db.Accounts.AsNoTracking().Include(p => p.Products).FirstOrDefault(p => p.Email == item.Value);
        
        return   Json(new {Wallet = User!.Wallet, Ref = db.Group.AsNoTracking().FirstOrDefault(p => p.Users.Contains(User) == true),Name = User.Name });
        
    }

    [HttpGet("GetAcc/{id?}")]
    [Authorize]
    public IActionResult getacc(int id){
        string a = "";string b = "";
        if (id == 0){
            foreach (var item in HttpContext.User.Claims){
                if( item.Type == "Name") 
                    a = item.Value;
                if( item.Type == "Password") 
                    b = item.Value;
            }
            return Json(db.Accounts.Include(p => p.Products).FirstOrDefault(p => p.Name == a && p.Password == b));
        }
        return Json(db.Accounts.Include(p => p.Products).FirstOrDefault(p => p.Id == id));
    }
    
    [HttpPost("reg")]
    public IActionResult register([FromForm] AccountDTO pr){ 
        log.Debug("\t"+pr.Name + "\n"+ "\t"+pr.Email + "\n"+"\t"+pr.Password + "\n");
        log.Debug( ("Valid "+ModelState.IsValid).ToString());
        if(!ModelState.IsValid){return BadRequest();}     
        db.Accounts.Add(Account.Create(pr));
        var Refreshtoken = JWTModel.rnd();
        db.Refresh.Add(new JWTdb() {Refreshtoken = Refreshtoken,Time = DateTime.UtcNow});
        db.SaveChanges();
        return Json(new JWTModel() {Accesstoken = JWTModel.jwtcreator(pr.Name,pr.Email), Refreshtoken = Refreshtoken});
    }
    
    
}


