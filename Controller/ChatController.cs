using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPP.Dbmodels;

//[Route("api")]
[ApiController]
public class ChatController : Controller{
    public dbcontextproduct db;
    
    public ChatController(dbcontextproduct db) => this.db = db;

    [HttpGet("api/chat")]
    [Authorize]
    public IActionResult GetChats( [FromServices] IAccountService maneg){
        Account User = maneg.GetUser();

        return Json(new{Groups = User!.Groups!
            .Select(p => new{GroupName = p.GroupName.ToString(),productId = p.productId,Notification = p.Messages.Where(p => p.Account == User && p.IsRead == false).Count() ,Users = p.Users.Where(p => p.Id != User.Id).Select(p => new{PathImage = p.PathImage,Name = p.Name})})});
    }

    
}