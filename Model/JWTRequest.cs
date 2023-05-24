using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

public class JWTRequest{
    public string? Accesstoken { get; set; }
    public string? Refreshtoken { get; set; }
    public static string jwtcreate(string Name, string Email){
        
        var Claims = new List<Claim> {new Claim("Name",Name),new Claim("Email",Email)};
        var jwt = new JwtSecurityToken(
            issuer: "webapp",
            audience: "reactapp",
            claims: Claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(50)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEYANYgfjfuthflmqnvbxw")), SecurityAlgorithms.HmacSha256));
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    public static string rnd() =>  Guid.NewGuid().ToString();

}


