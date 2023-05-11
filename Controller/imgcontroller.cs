using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/")]
[ApiController]
public class ImgController : Controller{
    public dbcontextproduct db;
    public ImgController(dbcontextproduct db) => this.db = db;

    [Route("GetImg/{id?}")]
    [HttpGet]
    public VirtualFileResult getimg(string id) =>  File($"./img/product_{id}.png","image/png");
    
    [Route("reg")]
    [HttpPost]
    public SignInResult register([FromBody] Account pr){ 
        
        db.Accounts.Add(pr);
        return cookielog(pr.Name,pr.Password);
    }
    private SignInResult cookielog(string Name, string Password){
        var Claims = new List<Claim> {new Claim("Name",Name),new Claim("Password",Password)};
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(Claims, "Cookies");
        return SignIn(new ClaimsPrincipal(claimsIdentity));
    }
    
}
