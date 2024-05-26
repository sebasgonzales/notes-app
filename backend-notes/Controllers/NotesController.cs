using Core.Dtos.request;
using Core.Dtos.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_notes.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;
        public NotesController(INoteService note)
        {
            _service = note;
        }

        [HttpGet]
        public async Task<IEnumerable<NoteResponse>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteResponse>> GetById(int id)
        {
            var note = await _service.GetDtoById(id);

            if (note is null)
                return NoteNotFound(id);

            return note;
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteRequest note)
        {
            try
            {
                var newNote = await _service.Create(note);

                return CreatedAtAction(nameof(GetById), new { id = newNote.Id }, newNote);
            }catch (Exception ex)
            {
                
                return BadRequest((new { message = $"Hubo error agregando la nota" }));
            }

            
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, NoteRequest note)
        {

            if (id != note.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({note.Id}) del cuerpo de la solicitud.  " });

            var noteToUpdate = await _service.GetById(id);

            if (noteToUpdate is not null)
            {
                await _service.Update(id, note);
                return NoContent();
            }
            else
            {
                return NoteNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var noteToDelete = await _service.GetById(id);

            if (noteToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return NoteNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult NoteNotFound(int id)
        {
            return NotFound(new { message = $"la nota con ID = {id} no existe. " });
        }

    }
