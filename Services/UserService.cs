using AutoMapper.QueryableExtensions;
using Dotnet_API.Dto;
using Dotnet_API.Exceptions;
using Dotnet_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API.Services;

public class UserService
{
    private readonly DatabaseContext _db;

    public UserService(DatabaseContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<User> RegisterUser(CreateUserDto userRegisterDto)
    {
        var foundUser = await GetByEmail(userRegisterDto.Email);
        if (foundUser != null) throw new ConflictException("User already exists");
        var newUser = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            Password = PasswordEncryption.HashPassword(userRegisterDto.Password),
            Phone = userRegisterDto.Phone,
            Address = userRegisterDto.Address,
            Role = userRegisterDto.Role
        };
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return newUser;
    }

    public async Task<Page<ViewUserDto>> GetUsers(PaginationParams paginationParams)
    {
        var query = _db.Users.Where(u => !u.IsDeleted).ProjectTo<ViewUserDto>(MappingUtil.Map<User, ViewUserDto>());
        var results = await PaginationUtil.Paginate(query, paginationParams.Page, paginationParams.Size);
        return results;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(user => !user.IsDeleted && user.Email.Equals(email));
    }
}