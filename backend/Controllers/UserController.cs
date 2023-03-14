using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public UserController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    // create
    [HttpPost]
    public async Task<ActionResult<List<User>>> AddUser(User newUser)
    {
        if (!ModelState.IsValid)

            return BadRequest(ModelState);

        // if (newUser != null)
        // {
        appDbContext.Users.Add(newUser);
        await appDbContext.SaveChangesAsync();
        return Ok(await appDbContext.Users.ToListAsync());
        // }

        // return BadRequest("Object instance not set");
    }

    // Read all users
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await appDbContext.Users.ToListAsync();
        return Ok(users);
    }

    // Read single user
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound("User is not available");
    }

    // update user
    [HttpPut("{id:int}")]
    public async Task<ActionResult<User>> UpdateUser(int id, User updateUser)
    {
        if (updateUser != null)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
            user!.Name = updateUser.Name;
            user.Address = updateUser.Address;
            await appDbContext.SaveChangesAsync();
            return Ok(user);
        }
        return BadRequest("User not found");
    }

    // delete user
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<List<User>>> DeleteUser(int id)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
        if (user != null)
        {
            appDbContext.Users.Remove(user);
            await appDbContext.SaveChangesAsync();
            return Ok(await appDbContext.Users.ToListAsync());
        }

        return NotFound();
    }

}