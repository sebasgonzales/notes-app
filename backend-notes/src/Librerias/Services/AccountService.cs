using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Services;

public class AccountService : IAccountService
{
    private readonly notesContext _context;
    public AccountService(notesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountResponse>> GetAll()
    {
        return await _context.Account.Select(a => new AccountResponse
        {
            User = a.User,
            Password = a.Password
        }).ToListAsync();

    }

    public async Task<AccountResponse?> GetDtoById(int id)
    {
        return await _context.Account
            .Where(a => a.Id == id)
            .Select(a => new AccountResponse
            {
                User = a.User,
                Password = a.Password
            }).SingleOrDefaultAsync();

    }

    public async Task<Account?> GetById(int id)
    {
        return await _context.Account.FindAsync(id);
    }

    public async Task<Account> Create(AccountRequest newAccountDTO)
    {
        var newAccount = new Account();

        newAccount.User = newAccountDTO.User;
        newAccount.Password = newAccountDTO.Password;

        _context.Account.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;

    }

    public async Task Update(int id, AccountRequest account)
    {
        var existingAccount = await GetById(id);

        if (existingAccount is not null)
        {

            existingAccount.User = account.User;
            existingAccount.Password = account.Password;
            await _context.SaveChangesAsync();
        }

    }

    public async Task Delete(int id)
    {
        var accountToDelete = await GetById(id);

        if (accountToDelete is not null)
        {

            _context.Account.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }

    }

    public async Task <ActionResult<AccountResponse>> GetLogin (string user, string password)
    {
        return await _context.Account
            .Where(a => a.User == user && a.Password==password)
            .Select(a => new AccountResponse
            {
                User = a.User,
                Password = a.Password
            }).SingleOrDefaultAsync();
    }
}


