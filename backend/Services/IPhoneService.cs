namespace backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPhoneService
{
    Task<ActionResult<List<Phone>>> AddPhone(Phone newPhone);
    ActionResult<PaginatedResult<Phone>> GetItems(int pageNumber, int pageSize);
    Task<ActionResult<Phone>?> GetPhone(int id);
    Task<ActionResult<Phone>?> UpdatePhone(int id, Phone updatePhone);
    Task<bool> DeletePhone(int id);
}