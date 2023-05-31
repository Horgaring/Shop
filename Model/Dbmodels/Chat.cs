namespace WEBAPP.Dbmodels;
public class ChatModel
{
    public int Id { get; set; }
    
    public  int productId { get; set; }
    public  Product Product { get; set; } = null!;
    public required Guid GroupName { get; set; }
    public int Online { get; set; }
    public  required List<Account>  Users { get; set; }
    public  required List<Message>  Messages { get; set; } = default;
}