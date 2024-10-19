using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Create;

namespace AnimalVolunteer.Volunteers.Web.Requests.Volunteer
{
    public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    IEnumerable<SocialNetworkDto> SocialNetworkList,
    IEnumerable<PaymentDetailsDto> PaymentDetailsList)
    {
        public CreateVolunteerCommand ToCommand()
        {
            return new(
                FullName,
                Email,
                Description,
                SocialNetworkList,
                PaymentDetailsList);
        }
    }
}
