using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPP.Dbmodels;

[Route("api/[controller]")]
[ApiController]
class ChatController : Controller{
    public dbcontextproduct db;
    
    public ChatController(dbcontextproduct db) => this.db = db;

    [HttpGet()]
    [Authorize]
    public IActionResult GetAllChat( [FromServices] IAccountService maneg){
        Account User = maneg.GetUser();
        return Json(User!.Groups!.Select(p => p.GroupName.ToString()));
    }

    
}