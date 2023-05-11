public class ChatModel
{
    public int Id { get; set; }
    public required string GroupName { get; set; }
    public string UserMessage { get; set; } = "";
    public string User2Message { get; set; } = "";
    public  required List<Account>  Users { get; set; }
}