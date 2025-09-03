using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpsPolicy;
using MyWebApi.Interfaces;
using MyWebApi.Services;
using System.Security.Claims;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IIceCreamService IceCreamService;
    private IUserService IuserService;
    public UserController(IUserService IuserService, IIceCreamService IceCreamService)
    {
        this.IuserService = IuserService;
        this.IceCreamService = IceCreamService;
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<List<User>> GetAll() => IuserService.GetAll();

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<User> Get(int id)
    {
        var user = IuserService.GetById(id);
        if (user == null)
            return NotFound();
            
        return user;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Create(User newItem)
    {
        var newId = IuserService.Add(newItem);
        return CreatedAtAction(nameof(Create), new { id = newId }, IuserService.GetById(newId));
    }

    [HttpPost]
    [Route("/login")]
    public ActionResult<objectToReturn> Login([FromBody] User User)
    {
        int UserExistID = IuserService.ExistUser(User.userName, User.password);
        if (UserExistID == -1)
            return Unauthorized();

        var claims = new List<Claim> { };

        if (User.password == "1234")
            claims.Add(new Claim("type", "Admin"));

        claims.Add(new Claim("type", "User"));

        claims.Add(new Claim("id", UserExistID.ToString()));

        var token = IceTokenService.GetToken(claims);
        return new OkObjectResult(new { Id=UserExistID, token=IceTokenService.WriteToken(token) });
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public IActionResult Update(int userId, User user)
    {
        var result = IuserService.Update(userId, user);
        if (!result)
            return BadRequest();
       
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public IActionResult Delete(int id)
    {
        var user = IuserService.GetById(id);
        if (user is null)
            return  NotFound();

        IceCreamService.DeleteIceCreamsOfUser(id);
        IuserService.Delete(id);
        
        return Content(IuserService.Count.ToString());
    }
}

public class objectToReturn
{
    public int Id { get; set; }
    public string token { get; set; }
}
