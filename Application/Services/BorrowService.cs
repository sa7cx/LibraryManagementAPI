using Application.Common;
using Application.DTOs.Borrow;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;
using System.Data.Common;

namespace LibraryManagementAPI.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBorrowRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public BorrowService(IBorrowRepository borrowRepository, IBookRepository bookRepository, IMemberRepository memberRepository,IUnitOfWork unitOfWork)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<ApiResponse> Borrow(CreateBorrowingDto borrowDto, string userId)
        {
            
            var book = await _bookRepository.GetById(borrowDto.BookID );
            if (book == null)
                throw new NotFoundException("Book not found");

            if (book.Quantity <= 0)
                throw new BadRequestException("This book is not available for borrowing");
            
            var member = await _memberRepository.GetByUserID(userId);
            if (member == null)
                throw new NotFoundException("Member not found");

            var borrowRecord = new BorrowRecord
            {
                BookID = borrowDto.BookID,
                MemberID = member.MemberID,
                BorrowDate = borrowDto.BorrowDate,
                ReturnDate = borrowDto.ReturnDate,
                status = BorrowStatus.Borrowed
            };
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                book.Quantity--;
                await _borrowRepository.Add(borrowRecord);
                await _bookRepository.Update(book);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch(Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception();
            }

            return ApiResponse.Success("Book borrowed successfully");
        }

        public async Task<ApiResponse> Return(int id)
        {
            var borrowRecord = await _borrowRepository.GetById(id);
            if (borrowRecord == null)
                throw new NotFoundException("Borrow record not found");

            if (borrowRecord.status == BorrowStatus.Returned)
                throw new InvalidOperationException("This book has already been returned");

            var book = await _bookRepository.GetById(borrowRecord.BookID);
            if (book == null)
                throw new NotFoundException("Book not found");
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                borrowRecord.status = BorrowStatus.Returned;
                book.Quantity++;

                await _bookRepository.Update(book);
                await _borrowRepository.Update(borrowRecord);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("An error occurred while returning the book");
            }
            return ApiResponse.Success("Book returned successfully");
        }
    }
}

