using AutoMapper;
using SimpleService.API.Features.Customer.Models;

namespace SimpleService.API.Features.Customer.Mappers
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile() {
            CreateMap<CreateCustomerRequest, Core.Customer.Models.CustomerModel>()
                .ForMember(c => c.LastName, opt => opt.MapFrom(m => m.Surname));
            CreateMap<Core.Customer.Models.CustomerModel, CustomerModelApi>();
            CreateMap<CustomerModelApi, Core.Customer.Models.CustomerModel>();
        }
    }
}
