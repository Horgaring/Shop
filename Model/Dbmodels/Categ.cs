namespace WEBAPP.Dbmodels;
public class Categ{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product>? products { get; set; }
}