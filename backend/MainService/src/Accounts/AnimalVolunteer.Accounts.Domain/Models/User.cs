using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace AnimalVolunteer.Accounts.Domain.Models;

public sealed class User : IdentityUser<Guid>
{
    // Ef Core Ctor
    private User() { }

    private List<Role> _roles = [];

    private List<SocialNetwork> _socialNetworks = [];

    public FullName FullName { get; set; } = default!;

    public string? Photo { get; set; } = null;

    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    public IReadOnlyList<Role> Roles => _roles;

    public AdminAccount? AdminAccount { get; private set; }

    public VolunteerAccount? VolunteerAccount { get; private set; }

    public static Result<User, Error> CreateAdmin(
        string userName,
        string email,
        FullName fullName,
        Role role)
    {
        if (role.Name != AdminAccount.ADMIN_ACCOUNT_NAME)
            return Errors.Authentication.WrongRole();

        return new User
        {
            FullName = fullName,
            _socialNetworks = [],
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }

    public static Result<User, Error> CreateParticipant(
        FullName fullName, string userName, string email, Role role)
    {
        if (role.Name != ParticipantAccount.PARTICIPANT_ACCOUNT_NAME)
            return Errors.Authentication.WrongRole();

        return new User
        {
            FullName = fullName,
            _socialNetworks = [],
            UserName = userName,
            Email = email,
            _roles = [role]
        };
    }
    public VolunteerAccount CreateParticipantAccount() => new VolunteerAccount(this);

    public AdminAccount CreateAdminAccount() => new AdminAccount(this);

    public UnitResult<ErrorList> UpdateProfile(string userName, FullName fullName, IEnumerable<SocialNetwork> socials)
    {
        var socialsList = socials.ToList();

        if (socialsList.Count > 5)
        {
            return Errors.General.InvalidValue("Слишком много социальных сетей").ToErrorList();
        }

        UserName = userName;
        FullName = fullName;
        _socialNetworks = socialsList;

        return UnitResult.Success<ErrorList>();
    }

    public void UpdateEmail(string email)
    {
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
    }

    public void UpdatePhoneNumber(string? phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public UnitResult<Error> UpdateSocialNetworks(List<SocialNetwork> newSocialNetworks)
    {

        if (newSocialNetworks.Count > 5)
        {
            return Errors.General.InvalidValue("Слишком много социальных сетей");
        }

        _socialNetworks = newSocialNetworks;

        return UnitResult.Success<Error>();
    }
}