using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.Mapping;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController(IContactsRepo repository) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contact))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var contact = repository.GetById(id);
        if (contact == null)
            return NotFound();
        return Ok(contact);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<Contact>))]
    public IActionResult GetAll([FromQuery] RequestParameters requestParameters)
    {
        var contacts = repository.GetAll(requestParameters);
        return Ok(contacts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Contact))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] ContactRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var entity = request.ToContactEntity();

        repository.Create(entity);
        
        return Created($"/contacts/{entity.Id}", entity);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, [FromBody] ContactRequestDto contact)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingContact = repository.GetById(id);
        if (existingContact == null)
            return NotFound();
        
        repository.Update(contact.ToContactEntity(existingContact.Id));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var existingContact = repository.GetById(id);
        if (existingContact == null)
            return NotFound();

        repository.Delete(id);
        return NoContent();
    }
}