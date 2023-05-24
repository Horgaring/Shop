using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WEBAPP.Dbmodels;

[Route("api/[controller]/")]
[ApiController]
public class ProductController : Controller
{ 
    private dbcontextproduct db;
    
    public ProductController(dbcontextproduct db)=>this.db = db;
    
   
    [HttpGet("products/{id?}")]
    public JsonResult GetProd(int id) => Json(new {Prod = db.product.Include(p => p.account).ToList().First(p => p.Id == id),length = db.product.ToList().Count}); 

    [HttpGet("products/{id?}/{cat?}")]
    public JsonResult GetProd(int id, string cat,[FromQuery] string? sear)  {
        var prod = db.product.Include(p => p.categ).ToList().Where(p => (p.categ.Find(p => p.Name == cat) != null)).Take(20).Where(p => sear != null? p.Name.Contains(sear) : true );
        return cat == "All"? Json(new {Prod = db.product.Include(p => p.categ).ToList()
        .Skip(--id * 20)
        .Take(20).Where(p => sear != null? p.Name.Contains(sear) : true),length = db.product.ToList().Count})
        : Json(new {Prod =prod,length = prod.Count()}) ;
        
    }

    [HttpGet("image/{id?}")]
    public IActionResult Getimg(string id){
        return PhysicalFile(@"G:\app\WEBAPP\img\" + id,"image/webp");
    }
    [HttpGet("categs")]
    public IActionResult Getcateg() => Json(db.Categ.ToArray());

    [Authorize]
    [HttpPost("products")]
    [Consumes("multipart/form-data")]
    public IActionResult add([FromQuery] string[] categ, [FromForm] ProductRequest pr,[FromServices] IAccountService maneg)
    {
        Account User = maneg.GetUser();
        List<Categ> categ2 = new();
        for (int i = 0; i < categ.Length; i++){
            categ2.Add(db.Categ.FirstOrDefault(p => p.Name == categ[i].Trim(new char[]{'"','/','%','&'}))!);
        }
        Product pro = new Product(pr.Name){Description = pr.Description,Price = pr.Price,categ=categ2};
        pro.accountId = User.Id;
        pro.PathImage = Img.save_img(pr.File,User.Id,pr.Name);
        db.product.Add(pro);
        db.SaveChanges();  
        
        return Ok(categ2[0]);
    }
    [Authorize]
    [HttpDelete("products/{id?}")]
    [ProducesResponseType(typeof(string),400)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> remove(int id,[FromServices] IAccountService maneg)
    {
        var a = maneg.GetUser();
        Product? product = await db.product.Include(p => p.account).FirstOrDefaultAsync(p => p.Id == id);
        if(product == null || a == null)
            return BadRequest("пользователь или продукт не наден");
        if(a != product.account)
            return BadRequest("н");
        db.product.Remove(product);
        await db.SaveChangesAsync();
        return Ok();
    }
}