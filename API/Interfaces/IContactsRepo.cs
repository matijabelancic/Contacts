using API.Dtos;
using API.Entities;
using API.Utilities;

namespace API.Interfaces;

public interface IContactsRepo
{
    public Contact? GetById (Guid id);
    public PaginationResponse<Contact> GetAll(RequestParameters requestParameters);
    public void Create(Contact contact);
    public void Update(Contact contact);
    public void Delete(Guid id);
}