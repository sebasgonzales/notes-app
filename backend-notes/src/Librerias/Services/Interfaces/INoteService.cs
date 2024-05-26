using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;

namespace Services
{
    public interface INoteService
    {
        Task<Note> Create(NoteRequest newNoteDTO);
        Task Delete(int id);
        Task<IEnumerable<NoteResponse>> GetAll();
        Task<Note?> GetById(int id);
        Task<NoteResponse?> GetDtoById(int id);
        Task Update(int id, NoteRequest note);
    }
}