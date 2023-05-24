namespace WEBAPP.Dbmodels;
public class Product{
    
    public int Id { get; set; }
    public string? PathImage { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public int Price {get;set;} 
    public List<Categ> categ { get; set; } = null!;
    public int accountId {get;set;} 
    public Account account { get; set; } = null!;
    public Product(string name) => Name = name;

}
