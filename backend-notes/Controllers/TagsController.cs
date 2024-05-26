using Core.Dtos.request;
using Core.Dtos.response;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace backend_notes.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;
        public TagsController(ITagService tag)
        {
            _service = tag;
        }

        [HttpGet]
        public async Task<IEnumerable<TagResponse>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagResponse>> GetById(int id)
        {
            var tag = await _service.GetDtoById(id);

            if (tag is null)
                return TagNotFound(id);

            return tag;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagRequest tag)
        {
            try
            {
                var newTag = await _service.Create(tag);

                return CreatedAtAction(nameof(GetById), new { id = newTag.Id }, newTag);

            }
             catch (Exception ex)
            {
            return BadRequest((new { message = $"Hubo error agregando el tag" }));
        }
    }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, TagRequest tag)
        {

            if (id != tag.Id)
                return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({tag.Id}) del cuerpo de la solicitud.  " });

            var tagToUpdate = await _service.GetById(id);

            if (tagToUpdate is not null)
            {
                await _service.Update(id, tag);
                return NoContent();
            }
            else
            {
                return TagNotFound(id);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tagToDelete = await _service.GetById(id);

            if (tagToDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return TagNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult TagNotFound(int id)
        {
            return NotFound(new { message = $"el tag con ID = {id} no existe. " });
        }

    }

