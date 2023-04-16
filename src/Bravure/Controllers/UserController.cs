using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bravure.Models.User;
using Bravure.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public UserController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto input)
    {
        try
        {
            var createdUser = await _userAppService.CreateAsync(input);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            // Handle any exceptions and return an appropriate error response
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("id")]
    public async Task<ActionResult<UserDto>> GetUserById([FromQuery]Guid id)
    {
        try
        {
            var user = await _userAppService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userAppService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUserAsync([FromRoute]Guid id, [FromBody]UserDto input)
    {
        try
        {
            input.Id = id;
            var updatedUser = await _userAppService.UpdateAsync(input);
            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userAppService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
