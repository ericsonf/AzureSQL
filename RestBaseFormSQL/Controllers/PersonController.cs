using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestBaseFormSQL.ApiModels;
using RestBaseFormSQL.Core.Entities;
using RestBaseFormSQL.Core.Interfaces;

namespace RestBaseFormSQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPerson _personRepository;

        public PersonController(IPerson personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(int id)
        {
            return await _personRepository.GetById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Get()
        {
            return await _personRepository.Get();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PersonDTO person)
        {
            if (person is null) { return NoContent(); }
            var byteArray = await _personRepository.GetByteArray(person.Picture);
            var personItem = new Person()
            {
                Name = person.Name,
                Email = person.Email,
                Picture = byteArray
            };
            await _personRepository.Add(personItem);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] PersonDTO person)
        {
            if (person is null || id == 0) { return NoContent(); }
            var personToUpdate = await _personRepository.GetById(id);
            if (personToUpdate is null) { return NotFound(); }
            else
            {
                var byteArray = await _personRepository.GetByteArray(person.Picture);
                personToUpdate.Name = person.Name;
                personToUpdate.Email = person.Email;
                personToUpdate.Picture = (byteArray == null) ? personToUpdate.Picture : byteArray;
                await _personRepository.Edit(personToUpdate);
            }
            return Ok(personToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NoContent(); }
            var personToDelete = await _personRepository.GetById(id);
            if (personToDelete is null) { return NotFound(); }
            else
            {
                await _personRepository.Delete(personToDelete);
            }
            return Ok();
        }
    }
}
