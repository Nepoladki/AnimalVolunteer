using AnimalVolunteer.Accounts.Application.Interfaces;
using AnimalVolunteer.Accounts.Contracts.Responses;
using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.Framework.Authorization;
using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace AnimalVolunteer.Accounts.Application.Commands.RefreshUser;

public class RefreshUserHandler : ICommandHandler<LoginResponse, RefreshUserCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshUserHandler(
        IRefreshSessionManager refreshSessionManager,
        IJwtTokenProvider jwtTokenProvider,
        UserManager<User> userManager,
        [FromKeyedServices(Modules.Accounts)]IUnitOfWork unitOfWork)
    {
        _refreshSessionManager = refreshSessionManager;
        _jwtTokenProvider = jwtTokenProvider;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshUserCommand command, CancellationToken cancellationToken = default)
    {
        var oldRefreshSessionResult = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);
        if (oldRefreshSessionResult.IsFailure)
            return oldRefreshSessionResult.Error.ToErrorList();

        var oldRefreshSession = oldRefreshSessionResult.Value;
        if (oldRefreshSession.ExpiresAt > DateTime.UtcNow)
            return Errors.Authentication.RefreshExpired().ToErrorList();

        var userClaims = await _jwtTokenProvider.GetClaimsFromToken(command.AccessToken);
        if (userClaims.IsFailure)
            return userClaims.Error.ToErrorList();

        var userIdClaim = userClaims.Value.FirstOrDefault(c => c.Type == JwtClaimTypes.ID);
        if (Guid.TryParse(userIdClaim?.ToString(), out var userId) == false)
            return Errors.Authentication.InvalidToken().ToErrorList();

        var jtiClaim = userClaims.Value.FirstOrDefault(c => c.Type == JwtClaimTypes.JTI);
        if (Guid.TryParse(jtiClaim?.ToString(), out var jti) == false)
            return Errors.Authentication.InvalidToken().ToErrorList();

        if (oldRefreshSession.UserId != userId || oldRefreshSession.Jti != jti)
            return Errors.Authentication.IdMismatch().ToErrorList();

        _refreshSessionManager.DeleteSession(oldRefreshSession);
        await _unitOfWork.SaveChanges(cancellationToken);

        var accessTokenData = _jwtTokenProvider.GenerateAccessToken(oldRefreshSession.User);

        var refreshToken = await _jwtTokenProvider.GenerateRefreshToken(
            oldRefreshSession.User,
            accessTokenData.Jti,
            cancellationToken);

        return new LoginResponse(accessTokenData.AccessToken, refreshToken);
    }
}

