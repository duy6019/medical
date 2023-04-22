using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Bravure.Entities;
using Bravure.Models.User;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bravure.Services;

public class UserAppService : IUserAppService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly BravureDbContext _dbContext;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IMapper _mapper;

    public UserAppService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        BravureDbContext dbContext,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto input)
    {
        var user = _mapper.Map<ApplicationUser>(input);
        await _userManager.CreateAsync(user, input.Password);
        await _userManager.AddToRoleAsync(user, "staff");
        return MapToEntityDto(user);
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto input)
    {
        var user = await _userManager.FindByIdAsync(input.Id.ToString());
        MapToEntity(input, user);
        user.PasswordHash = _passwordHasher.HashPassword(user, input.Password);
        await _userManager.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        await _userManager.DeleteAsync(user);
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return MapToEntityDto(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    protected ApplicationUser MapToEntity(CreateUserDto createInput)
    {
        var user = _mapper.Map<ApplicationUser>(createInput);
        return user;
    }

    protected void MapToEntity(UserDto input, ApplicationUser user)
    {
        _mapper.Map(input, user);
    }

    protected void MapToEntity(UpdateUserDto input, ApplicationUser user)
    {
        _mapper.Map(input, user);
    }

    protected UserDto MapToEntityDto(ApplicationUser user)
    {
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}

