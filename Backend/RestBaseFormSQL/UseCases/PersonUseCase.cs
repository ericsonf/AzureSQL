
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestBaseFormSQL.Core.Entities;
using RestBaseFormSQL.Core.Interfaces;
using RestBaseFormSQL.Infra.Data;

namespace RestBaseFormSQL.UseCases
{
    public class PersonUseCase : IPerson
    {
        private readonly Context _context;

        public PersonUseCase(Context context)
        {
            _context = context;
        }

        public async Task<Person> GetById(int id)
        {
            return await _context.Person.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> Get()
        {
            return await _context.Person.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task Add(Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Person person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Person person)
        {
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> GetByteArray(IFormFile picture)
        {
            byte[] fileBytes = null;
            if (picture != null && picture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await picture.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
            }
            return fileBytes;
        }
    }
}