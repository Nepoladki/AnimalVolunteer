using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Create;

namespace AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer
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
