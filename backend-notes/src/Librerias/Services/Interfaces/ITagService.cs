using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public interface ITagService
    {
        Task<Tag> Create(TagRequest newTagDTO);
        Task Delete(int id);
        Task<IEnumerable<TagResponse>> GetAll();
        Task<Tag?> GetById(int id);
        Task<TagResponse?> GetDtoById(int id);
        Task Update(int id, TagRequest tag);
    }
}