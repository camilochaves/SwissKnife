using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwissKnife;

namespace apiExample.Controllers;
public class UserRegisterData
{
    public string Login { get; set; }
    public string Password { get; set; }
}

[ApiController]
[Route("[controller]")]
public class AccessControl : ControllerBase
{
    public AccessControl() { }

    [HttpPost]
    [Route("[action]")]
    public IActionResult LoginAndReturnToken(
       [FromBody] UserRegisterData body,
       [FromServices] SecretsHandlerService secrets)
    {
        if (body.Password != "123") return Unauthorized(new { msg = "Access Denied!" });
        //Generate JWT Token without Claims
        var secretKey = secrets.GetFromConfig("JWT:secretKey");
        var issuer = secrets.GetFromConfig("JWT:issuer");
        var audience = secrets.GetFromConfig("JWT:audience");
        var genericIdentity = new GenericIdentity(body.Login);
        var userIdentity = new ClaimsIdentity(genericIdentity);
        var token = TokenTools.CreateIdToken(userIdentity, secretKey, issuer, audience);
        return Ok(new { userIdentity, token });
    }

    //Calls default authorization with is JWTBearer
    [Authorize]
    [HttpGet]
    [Route("[action]")]
    public IActionResult IsValidToken()
    {
        return Ok(new { msg = "If you reached here then token is valid!" });
    }

    [Authorize]
    [HttpGet]
    [Route("[action]")]
    public IActionResult WhoAmIFromToken()
    {
        return Ok(new { user = HttpContext?.User?.Identity?.Name });
    }

}
