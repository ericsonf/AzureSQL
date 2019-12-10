using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestBaseFormSQL.Core.Entities;

namespace RestBaseFormSQL.Core.Interfaces
{
    public interface IPerson
    {
        Task<Person> GetById(int id);  
        Task<IEnumerable<Person>> Get();  
        Task Add(Person person);
        Task Edit(Person person);
        Task Delete(Person person);
        Task<byte[]> GetByteArray(IFormFile picture);
    }
}