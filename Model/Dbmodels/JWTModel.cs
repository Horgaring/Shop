namespace WEBAPP.Dbmodels;
public class JWTModel{
    [System.ComponentModel.DataAnnotations.Key]
    public string? Refreshtoken { get; set; }
    public DateTime Time { get; set; }
}