using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class NoteService : INoteService
        {
            private readonly notesContext _context;
            public NoteService(notesContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<NoteResponse>> GetAll()
            {
            return await _context.Note.Select(n => new NoteResponse
                {
                Id = n.Id,
                Title = n.Title,
                    Description = n.Description,
                    Account = n.Account.User,
                    IsArchive = n.IsArchive,
                    Tag = n.Tag.Name
                }).ToListAsync();

            }

            public async Task<NoteResponse?> GetDtoById(int id)
            {
                return await _context.Note
                    .Where(n => n.Id == id)
                    .Select(n => new NoteResponse
                    {   
                        Id= n.Id,
                        Title = n.Title,
                        Description = n.Description,
                        Account = n.Account.User,
                        IsArchive = n.IsArchive,
                        Tag = n.Tag.Name

                    }).SingleOrDefaultAsync();

            }

            public async Task<Note?> GetById(int id)
            {
                return await _context.Note.FindAsync(id);
            }

            public async Task<Note> Create(NoteRequest newNoteDTO)
            {
                var newNote = new Note();

                newNote.Title = newNoteDTO.Title;
                newNote.Description = newNoteDTO.Description;
                newNote.IsArchive = newNoteDTO.IsArchive;
                newNote.IdAccount = newNoteDTO.IdAccount;
                newNote.IdTag = newNoteDTO.IdTag;

                _context.Note.Add(newNote);
                await _context.SaveChangesAsync();

                return newNote;

            }

            public async Task Update(int id, NoteRequest note)
            {
                var existingNote = await GetById(id);

                if (existingNote is not null)
                {

                    existingNote.Title = note.Title;
                    existingNote.Description = note.Description;
                    existingNote.IsArchive = note.IsArchive;
                    existingNote.IdAccount = note.IdAccount;
                    existingNote.IdTag= note.IdTag;
                    await _context.SaveChangesAsync();
                }

            }

            public async Task Delete(int id)
            {
                var noteToDelete = await GetById(id);

                if (noteToDelete is not null)
                {

                    _context.Note.Remove(noteToDelete);
                    await _context.SaveChangesAsync();
                }

            }
        }
    
}

