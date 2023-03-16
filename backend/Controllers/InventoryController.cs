using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly AppDbContext appDbContext;

    public InventoryController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }


    // create
    [HttpPost]
    public async Task<ActionResult<List<Phone>>> AddPhone(Phone newPhone)
    {
        if (!ModelState.IsValid)

            return BadRequest(ModelState);


        appDbContext.Phones.Add(newPhone);
        await appDbContext.SaveChangesAsync();
        return Ok(await appDbContext.Phones.ToListAsync());

    }

    [HttpGet]
    [Route("items")]
    public ActionResult<PaginatedResult<Phone>> GetItems(int pageNumber = 1, int pageSize = 5)
    {
        var totalCount = appDbContext.Phones.Count();
        var startIndex = (pageNumber - 1) * pageSize;
        var items = appDbContext.Phones.Skip(startIndex).Take(pageSize).ToList();
        var pagedItems = new PaginatedResult<Phone>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
        return Ok(pagedItems);
    }

    // Read all phones
    [HttpGet]
    public async Task<ActionResult<List<Phone>>> GetAllPhones()
    {
        var phones = await appDbContext.Phones.ToListAsync();
        return Ok(phones);
    }

    // Read single phone
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Phone>> GetPhone(int id)
    {
        var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
        return (phone != null) ? Ok(phone) : NotFound("Phone is not available");
    }

    // update phone
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Phone>> UpdatePhone(int id, Phone updatePhone)
    {
        if (updatePhone != null)
        {
            var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
            phone!.Model = updatePhone.Model;
            phone.CompanyId = updatePhone.CompanyId;
            phone.Quantity = updatePhone.Quantity;
            // phone.Stock = updatePhone.Stock;
            phone.Specs = updatePhone.Specs;
            await appDbContext.SaveChangesAsync();
            return Ok(phone);
        }
        return BadRequest("Phone not found");
    }

    // delete phone
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<List<Phone>>> DeletePhone(int id)
    {
        var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
        if (phone != null)
        {
            appDbContext.Phones.Remove(phone);
            await appDbContext.SaveChangesAsync();
            return Ok(await appDbContext.Phones.ToListAsync());
        }

        return NotFound();
    }
}