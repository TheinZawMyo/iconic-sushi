using backend.Data;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PhoneService : IPhoneService
{

    private readonly AppDbContext appDbContext;

    public PhoneService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<ActionResult<List<Phone>>> AddPhone(Phone newPhone)
    {

        appDbContext.Phones.Add(newPhone);
        await appDbContext.SaveChangesAsync();
        return await appDbContext.Phones.ToListAsync();

    }


    public ActionResult<PaginatedResult<Phone>> GetItems(int pageNumber, int pageSize)
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
        return pagedItems;
    }


    public async Task<ActionResult<Phone>?> GetPhone(int id)
    {
        var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
        if(phone == null)
            return null;
        return phone;
    }

    public async Task<ActionResult<Phone>?> UpdatePhone(int id, Phone updatePhone)
    {
        if (updatePhone == null)
        {
            return null;
        }
        var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
        phone!.Model = updatePhone.Model;
        phone.CompanyId = updatePhone.CompanyId;
        phone.Quantity = updatePhone.Quantity;
        // phone.Stock = updatePhone.Stock;
        phone.Specs = updatePhone.Specs;
        await appDbContext.SaveChangesAsync();
        return phone;
        
    }


    public async Task<bool> DeletePhone(int id)
    {
        var phone = await appDbContext.Phones.FirstOrDefaultAsync(e => e.Id == id);
        if (phone != null)
        {
            appDbContext.Phones.Remove(phone);
            await appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}