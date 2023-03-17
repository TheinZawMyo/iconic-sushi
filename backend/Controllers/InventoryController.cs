using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public InventoryController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }


    // create
    [HttpPost]
    public async Task<ActionResult<List<Phone>>> AddPhone(Phone newPhone)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var phones = await _phoneService.AddPhone(newPhone);
        return Ok(phones);

    }

    [HttpGet]
    public ActionResult<PaginatedResult<Phone>> GetItems(int pageNumber = 1, int pageSize = 5)
    {
        var items = _phoneService.GetItems(pageNumber, pageSize);
        if(items == null)
            return NotFound();
        return Ok(items.Value);
    }

    // Read all phones
    // [HttpGet]
    // public async Task<ActionResult<List<Phone>>> GetAllPhones()
    // {
    //     var phones = await appDbContext.Phones.ToListAsync();
    //     return Ok(phones);
    // }

    // Read single phone
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Phone>?> GetPhone(int id)
    {
        var phone = await _phoneService.GetPhone(id);
        return (phone != null) ? Ok(phone) : NotFound("Phone is not available");
    }

    // update phone
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Phone>?> UpdatePhone(int id, Phone updatePhone)
    {
        if (updatePhone != null)
        {
            var phone = await _phoneService.UpdatePhone(id, updatePhone);
            return Ok(phone);
        }
        return BadRequest("Phone not found");
    }

    // delete phone
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePhone(int id)
    {
        var deleted = await _phoneService.DeletePhone(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}