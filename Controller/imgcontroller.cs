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
}
