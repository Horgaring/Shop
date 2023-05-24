using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Serilog;
using WEBAPP.Dbmodels;

[Route("api/[controller]/")]
[ApiController]
public class AccountController : Controller{
    public dbcontextproduct db;
    public Serilog.ILogger log = Log.Logger;
    public AccountController(dbcontextproduct db) => this.db = db;

    
    [HttpPost("Sigin")]
    public JsonResult sigin([FromForm] AccountLogin pr){
        var acc = db.Accounts.ToArray().FirstOrDefault(e => e.Email == pr.Email);
        string Refreshtoken = JWTRequest.rnd();
        db.Refresh.Add(new JWTModel() {Refreshtoken = Refreshtoken,Time = DateTime.UtcNow});
        db.SaveChanges();
        var resp =new JWTRequest{Refreshtoken = Refreshtoken,Accesstoken = JWTRequest.jwtcreate(acc.Name,acc.Email)};
        return Json(resp);
    }
    [HttpGet("isauth")]
    [Authorize()]
    public IActionResult Founduser([FromServices] IAccountService maneg){
        Account User = maneg.GetUser();
        return   Json(new {Wallet = User!.Wallet, Ref = db.Group.AsNoTracking().FirstOrDefault(p => p.Users.Contains(User) == true),Name = User.Name });
    }

    [HttpGet("{id?}")]
    [Authorize]
    public IActionResult getacc(int id,[FromServices] IAccountService maneg){
        if (id == 0){
            Account a = maneg.GetUser();
            return Json(new {Name = a.Name,PathImage = a.PathImage,Products = a.Products});
        }
        return Json(db.Accounts.Include(p => p.Products).FirstOrDefault(p => p.Id == id));
    }
    [Consumes("multipart/form-data")]
    [HttpPost("reg")]
    public IActionResult register([FromForm] AccountRequest pr){ 
        log.Debug("\t"+pr.Name + "\n"+ "\t"+pr.Email + "\n"+"\t"+pr.Password + "\n");
        log.Debug( ("Valid "+ModelState.IsValid).ToString());
        if(!ModelState.IsValid){return BadRequest();}     
        db.Accounts.Add(Account.Create(pr));
        var Refreshtoken = JWTRequest.rnd();
        db.Refresh.Add(new JWTModel() {Refreshtoken = Refreshtoken,Time = DateTime.UtcNow});
        db.SaveChanges();
        return Json(new JWTRequest() {Accesstoken = JWTRequest.jwtcreate(pr.Name,pr.Email), Refreshtoken = Refreshtoken});
    }
    
    
}


