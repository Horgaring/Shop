using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
class ChatController : Controller{
    public dbcontextproduct db;
    
    public ChatController(dbcontextproduct db) => this.db = db;

    [HttpGet()]
    [Authorize]
    public IActionResult GetAllChat([FromServices] IRepository memoryCache){
        
        Account? User = null;
        foreach (var item in HttpContext.User.Claims)
            if( item.Type == "Email") 
               User = db.Accounts.AsNoTracking().Include(p => p.Groups).FirstOrDefault(p => p.Email == item.Value);
        return Json(User!.Groups!.Select(p => p.GroupName.ToString()));
        
        
    }

    
}