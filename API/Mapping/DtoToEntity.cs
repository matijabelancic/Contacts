using API.Dtos;
using API.Entities;

namespace API.Mapping;

public static class DtoToEntity
{
    public static Contact ToContactEntity(this ContactRequestDto contactRequestDto, Guid? contactId = null)
    {
        return new Contact
        {
            Id = contactId ?? Guid.NewGuid(),
            Name = contactRequestDto.Name,
            Surname = contactRequestDto.Surname,
            Email = contactRequestDto.Email,
        };
    }
}
