using System.ComponentModel.DataAnnotations.Schema;
namespace WEBAPP.Dbmodels;

public class Message{
    
    public int Id { get; set; }
    public  int groupId { get; set; }
    public  ChatModel Group { get; set; } = null!;
    public  int accountId { get; set; }
    public  Account Account { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTimeOffset Time { get; set; }
    [Column("Message")]
    public string message { get; set; } = null!;
    
    
}