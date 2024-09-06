using EF.RepositoryPattern.NET.Entities;
using EF.RepositoryPattern.NET.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EF.RepositoryPattern.NET.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomersController(ICustomersRepository<CustomersEntity> customersRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCustomersAsync()
    {
        return Ok(await customersRepository.GetAllAsync());
    }
    [HttpPost]
    public async Task<IActionResult> CreateCustomersAsync(string firstName, string lastName, string email)
    {
        var newCustomer = new CustomersEntity
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
        await customersRepository.AddAsync(newCustomer);

        return Ok(newCustomer);
    }
}