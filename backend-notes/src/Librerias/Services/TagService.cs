using backend_notes.Models;
using Core.Dtos.request;
using Core.Dtos.response;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly notesContext _context;
        public TagService(notesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagResponse>> GetAll()
        {
            return await _context.Tag.Select(t => new TagResponse
            {
                Name = t.Name,
            }).ToListAsync();

        }

        public async Task<TagResponse?> GetDtoById(int id)
        {
            return await _context.Tag
                .Where(t => t.Id == id)
                .Select(t => new TagResponse
                {
                    Name = t.Name,
                }).SingleOrDefaultAsync();

        }

        public async Task<Tag?> GetById(int id)
        {
            return await _context.Tag.FindAsync(id);
        }

        public async Task<Tag> Create(TagRequest newTagDTO)
        {
            var newTag = new Tag();

            newTag.Name = newTagDTO.Name;

            _context.Tag.Add(newTag);
            await _context.SaveChangesAsync();

            return newTag;

        }

        public async Task Update(int id, TagRequest tag)
        {
            var existingTag = await GetById(id);

            if (existingTag is not null)
            {

                existingTag.Name = tag.Name;
                await _context.SaveChangesAsync();
            }

        }

        public async Task Delete(int id)
        {
            var tagToDelete = await GetById(id);

            if (tagToDelete is not null)
            {

                _context.Tag.Remove(tagToDelete);
                await _context.SaveChangesAsync();
            }

        }
    }
}
