
public class Product{
    
    public int Id { get; set; }
    public string? PathImage { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public int Price {get;set;} 
    public List<Categ> categ { get; set; } = null!;
    
    public Account account { get; set; } = null!;
    public Product(string name) => Name = name;

}
public class Categ{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product>? products { get; set; }
}
public class Productmodel{
    
    
    public IFormFile File { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public int Price {get;set;} 
    

}