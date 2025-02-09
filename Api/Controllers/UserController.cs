using Domain.Interfaces;
using Domain.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;

    [HttpGet("users/{userId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var result = await _userRepository.GetBySpecificationAsync(new UserByIdSpecification(userId));
        return result.Match<IActionResult>(Ok, NotFound);
    }
}
