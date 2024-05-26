using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public interface IAccountService
    {
        Task<Account> Create(AccountRequest newAccountDTO);
        Task Delete(int id);
        Task<IEnumerable<AccountResponse>> GetAll();
        Task<Account?> GetById(int id);
        Task<AccountResponse?> GetDtoById(int id);
        Task Update(int id, AccountRequest account);
        Task<ActionResult<AccountResponse>> GetLogin(string user, string password);
    }
}