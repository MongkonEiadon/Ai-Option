using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;

namespace ai.option.web.AutoMapper {
    public class IqOptionAccountDtoMappedProfile : Profile {
        public IqOptionAccountDtoMappedProfile() {
            CreateMap<IqOptionRequestViewModel, IqOptionAccountDto>()
                .ForMember(m => m.IqOptionUserName, c => c.MapFrom(s => s.EmailAddress))
                .ForMember(m => m.Password, c => c.MapFrom(s => s.Password))
                .ForMember(m => m.User, s => s.Ignore())
                .ForMember(m => m.Id, s => s.Ignore())
                .ForMember(m => m.BalanceId, c => c.MapFrom(s => s.ProfileResponseViewModel.BalanceId))
                .ForMember(m => m.Balance, c => c.MapFrom(s => s.ProfileResponseViewModel.Balance))
                .ForMember(m => m.IqOptionUserId, c => c.MapFrom(s => s.ProfileResponseViewModel.UserId))
                .ForMember(m => m.Currency, c => c.MapFrom(s => s.ProfileResponseViewModel.Currency))
                .ForMember(m => m.CurrencyChar, c => c.MapFrom(s => s.ProfileResponseViewModel.CurrencyChar))
                .ForMember(m => m.Address, c => c.MapFrom(s => s.ProfileResponseViewModel.Address))
                .ForMember(m => m.City, c => c.MapFrom(s => s.ProfileResponseViewModel.City))
                .ForMember(m => m.BirthDate, c => c.MapFrom(s => s.ProfileResponseViewModel.Birthdate))
                .ForMember(m => m.Avartar, c => c.MapFrom(s => s.ProfileResponseViewModel.Avartar))
                .ForMember(m => m.CreatedOn, s => s.Ignore())
                .ForMember(m => m.UpdatedOn, s => s.Ignore())
                .ForMember(m => m.LastSyned, s => s.Ignore())
                .ForMember(m => m.IsActive, s => s.Ignore());
        }
    }
}