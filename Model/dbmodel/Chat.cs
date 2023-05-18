public class ChatModel
{
    public int Id { get; set; }
    
    public  int productId { get; set; }
    public  Product Product { get; set; } = null!;
    public required Guid GroupName { get; set; }
    public string UserMessage { get; set; } = "";
    public string? UnreadMessages { get; set; }
    
   
    public  required List<Account>  Users { get; set; }
}