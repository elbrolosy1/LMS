using BLL__Buisness_Logic_Layer_.Dtos.AuthorDto;
using BLL__Buisness_Logic_Layer_.Dtos.CategoryDto;
using DAL__Data_Access_Layer_.Models;
using DAL__Data_Access_Layer_.Repo.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IGenericRepo<Author> _repo;

        public AuthorService(IGenericRepo<Author> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IEnumerable<ReadAuthorDto>> GetAllAsync()
        {
            var authors = await _repo.GetAllAsync();
            return await authors
                .Select(c => new ReadAuthorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    BornYear = c.BornYear,
                    DeathYear = c.DeathYear,
                    Gender = c.Gender
                })
                .ToListAsync();
        }

        public async Task<ReadAuthorDto?> GetByIdAsync(int id)
        {
            var author = await _repo.GetByIdAsync(id);
            if (author == null) return null;

            return new ReadAuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                BornYear = author.BornYear,
                DeathYear = author.DeathYear,
                Gender = author.Gender

            };
        }

        public async Task CreateAsync(CreateAuthorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var author = new Author
            {
                Name = dto.Name,
                Description = dto.Description,
                BornYear = dto.BornYear,
                DeathYear = dto.DeathYear,
                Gender = dto.Gender
            };

            await _repo.AddAsync(author);
            await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateAuthorDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var author = await _repo.GetByIdAsync(dto.Id);
            if (author == null) return false;

            author.Name = dto.Name;
            author.Description = dto.Description;
            author.BornYear = dto.BornYear;
            author.DeathYear = dto.DeathYear;
            author.Gender = dto.Gender;

            await _repo.UpdateAsync(author);
            await _repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _repo.GetByIdAsync(id);
            if (author == null) return false;

            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();

            return true;
        }

    }
}
