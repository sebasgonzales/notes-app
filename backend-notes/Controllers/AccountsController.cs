using Core.Dtos.request;
using Core.Dtos.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_accounts.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;
    public AccountsController(IAccountService account)
    {
        _service = account;
    }

    // GET Accounts/
    [HttpGet]
    public async Task<IEnumerable<AccountResponse>> Get()
    {
        return await _service.GetAll();
    }

    // GET Accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountResponse>> GetById(int id)
    {
        var account = await _service.GetDtoById(id);

        if (account is null)
            return AccountNotFound(id);

        return account;
    }

    // POST 
    [HttpPost]
    public async Task<IActionResult> Create(AccountRequest account)
    {
        try
        {
            var newAccount = await _service.Create(account);

            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);

        }
        catch (Exception ex) { }
        return BadRequest((new { message = $"Hubo error agregando la cuenta" }));

    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AccountRequest account)
    {

        if (id != account.Id)
            return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({account.Id}) del cuerpo de la solicitud.  " });

        var accountToUpdate = await _service.GetById(id);

        if (accountToUpdate is not null)
        {
            await _service.Update(id, account);
            return NoContent();
        }
        else
        {
            return AccountNotFound(id);
        }

    }
    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var accountToDelete = await _service.GetById(id);

        if (accountToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return AccountNotFound(id);
        }
    }

    // LOGIN
    [HttpGet("{user}/{password}")]
    public async Task<ActionResult<AccountResponse>> GetLogin(string user, string password)
    {
        var login = await _service.GetLogin(user, password);

        if (login is null)
            return NotFound();

        return login;
    }


    [NonAction]
    public NotFoundObjectResult AccountNotFound(int id)
    {
        return NotFound(new { message = $"la account con ID = {id} no existe. " });
    }

}
