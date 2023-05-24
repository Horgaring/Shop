public class ProductRequest{
    
    
    public IFormFile File { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public int Price {get;set;} 
    

}