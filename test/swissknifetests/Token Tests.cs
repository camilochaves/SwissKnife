using System.Security.Claims;
using System.Security.Principal;
using SwissKnife;
namespace swissknifetests;
public partial class TestSwissKnife
{
    [TestMethod]
    public void CreateAccessTokenTest()
    {
        var token = TokenTools.CreateAccessToken("camilo", "secretKey", "salt", "127.0.0.1", "console", 1);
        var result1 = TokenTools.IsValidAccessToken(token, "127.0.0.1", "console", "secretKey", "salt");
        var result2 = TokenTools.IsValidAccessToken(token, "127.0.0.2", "console", "secretKey", "salt");
        var result3 = TokenTools.IsValidAccessToken(token, "127.0.0.1", "console1", "secretKey", "salt");
        var result4 = TokenTools.IsValidAccessToken(token, "127.0.0.1", "console", "secretKey2", "salt");
        var result5 = TokenTools.IsValidAccessToken(token, "127.0.0.1", "console", "secretKey", "salt2");
        var tokenParts = TokenTools.DecodeAccessTokenAndSeparateParts(token);
        var hash = tokenParts[0];
        var userName = tokenParts[1];
        var time = tokenParts[2];
        Assert.IsTrue(userName == "camilo");
        Assert.IsTrue(result1);
        Assert.IsFalse(result2);
        Assert.IsFalse(result3);
        Assert.IsFalse(result4);
        Assert.IsFalse(result5);
    }

    [TestMethod]
    public void IDTokenTests()
    {
        var identityOwner = "Anakin Skywalker";
        var ownerEmail = "lordvader@deathstar.com";
        var darkLordRole = "Dark Lord of The Sith";
        var emailClaim = new Claim("Email", ownerEmail);
        var roleClaim = new Claim("Role", darkLordRole);
        var anakinIdentity = new GenericIdentity(identityOwner);
        anakinIdentity.AddClaim(emailClaim);
        anakinIdentity.AddClaim(roleClaim);
        var id = new ClaimsIdentity(anakinIdentity);
        //Create ID Token
        var secretLongPassword = "SithRulez_PasswordMustBeLong";
        var issuer = "The Empire";
        var audience = "Sith apprentices";
        var token = TokenTools.CreateIdToken(id, secretLongPassword, issuer, audience);
        var tokenIdIsValid = TokenTools.ValidateIdToken(token, secretLongPassword, issuer, audience);
        var whois = TokenTools.ExtractPrincipalFromIdToken(token, secretLongPassword, issuer, audience);
        var user = whois.Identity.Name;
        Assert.IsTrue(user == identityOwner);
        Assert.IsTrue(whois.HasClaim("Email", ownerEmail));
        Assert.IsTrue(whois.HasClaim("Role", darkLordRole));
        Assert.IsTrue(tokenIdIsValid);
    }
}