using Application;
using Application.DTOs.Borrow;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public BorrowService(IBorrowRepository borrowRepository, IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<ApiResponse<IEnumerable<BorrowingDetailsDto>>> GetAll()
        {
            var borrowRecords = await _borrowRepository.GetAll();
            var result = borrowRecords.Select(borrow => new BorrowingDetailsDto
            {
                Id = borrow.Id,
                BookID = borrow.BookID,
                MemberID = borrow.MemberID,
                BorrowDate = borrow.BorrowDate,
                ReturnDate = borrow.ReturnDate,
                status = borrow.status
            });
            return ApiResponse<IEnumerable<BorrowingDetailsDto>>.Success(result);
        }

        public async Task<ApiResponse<BorrowingDetailsDto>> GetById(int id)
        {
            var borrowRecord = await _borrowRepository.GetById(id);
            if (borrowRecord == null)
                throw new NotFoundException("Borrow record not found");

            var result = new BorrowingDetailsDto
            {
                Id = borrowRecord.Id,
                BookID = borrowRecord.BookID,
                MemberID = borrowRecord.MemberID,
                BorrowDate = borrowRecord.BorrowDate,
                ReturnDate = borrowRecord.ReturnDate,
                status = borrowRecord.status
            };
            return ApiResponse<BorrowingDetailsDto>.Success(result);
        }

        public async Task<ApiResponse> Borrow(CreateBorrowingDto borrowDto)
        {
            var book = await _bookRepository.GetById(borrowDto.BookID);
            if (book == null)
                throw new NotFoundException("Book not found");

            if (book.Quantity <= 0)
                throw new InvalidOperationException("This book is not available for borrowing");

            var member = await _memberRepository.GetById(borrowDto.MemberID);
            if (member == null)
                throw new NotFoundException("Member not found");

            var borrowRecord = new BorrowRecord
            {
                BookID = borrowDto.BookID,
                MemberID = borrowDto.MemberID,
                BorrowDate = borrowDto.BorrowDate,
                ReturnDate = borrowDto.ReturnDate,
                status = BorrowStatus.Borrowed
            };

            book.Quantity--;
            await _bookRepository.Update(book);
            await _borrowRepository.Add(borrowRecord);

            return ApiResponse.Success("Book borrowed successfully");
        }

        public async Task<ApiResponse> Return(int id)
        {
            var borrowRecord = await _borrowRepository.GetById(id);
            if (borrowRecord == null)
                throw new NotFoundException("Borrow record not found");

            if (borrowRecord.status == BorrowStatus.Avilable)
                throw new InvalidOperationException("This book has already been returned");

            var book = await _bookRepository.GetById(borrowRecord.BookID);
            if (book == null)
                throw new NotFoundException("Book not found");

            borrowRecord.status = BorrowStatus.Avilable;
            book.Quantity++;

            await _bookRepository.Update(book);
            await _borrowRepository.Update(borrowRecord);

            return ApiResponse.Success("Book returned successfully");
        }
    }
}

