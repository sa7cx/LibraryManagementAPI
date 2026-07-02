using Application.DTOs.Author;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;


namespace LibraryManagementAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDetailsDto>> GetAll()
        {
            var authors = await _authorRepository.GetAll();
            var result = authors.Select(author => new AuthorDetailsDto
            {
                AuthorId = author.AuthorId,
                FullName = author.FullName,
                Country = author.Country,
            });
            return result;
        }

        public async Task<AuthorDetailsDto> GetById(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
                 throw new NotFoundException("Author not found");

            var result = new AuthorDetailsDto
            {
                AuthorId = author.AuthorId,
                Country = author.Country,
                FullName = author.FullName,
            };
            return result;
        }

        public async Task Add(CreateAuthorDto authorDto)
        {
            var author = new Author
            {
                FullName = authorDto.FullName,
                Country = authorDto.Country,
            };
            await _authorRepository.Add(author);
        }

        public async Task Update(int id, CreateAuthorDto authorDto)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
                throw new NotFoundException("Author not found");
            author.FullName = authorDto.FullName;
            author.Country = authorDto.Country;
            await _authorRepository.Update(author);
        }

        public async Task Delete(int id)
        {
            var author = await _authorRepository.GetById(id);
            if (author == null)
                throw new NotFoundException("Author not found");
            await _authorRepository.Delete(author);
        }

    }
}
