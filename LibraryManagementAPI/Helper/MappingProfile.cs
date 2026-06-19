using AutoMapper;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
namespace LibraryManagementAPI.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateBorrowingDto, BorrowRecord>()
                .ForMember(src => src.Member,opt => opt.Ignore())
                .ForMember(src => src.Book,opt => opt.Ignore());

            CreateMap<BorrowRecord, BorrowingDetailsDto>();

            CreateMap<UpdateBookDto, Book>()
                .ForMember(src => src.CoverImage, opt => opt.Ignore());

            CreateMap<CreateBookDto, Book>()
                .ForMember(src => src.CoverImage, opt => opt.Ignore());

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName));
        }
    }
}
