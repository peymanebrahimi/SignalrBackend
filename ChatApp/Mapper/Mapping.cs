using AutoMapper;
using ChatApp.Data;
using ChatApp.Models.Expense;

namespace ChatApp.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Client, MiniClient>().ReverseMap();
            CreateMap<Parvandeh, MiniParvandeh>().ReverseMap();
            CreateMap<Received, ReceivedListVm>().ReverseMap();
            CreateMap<ApiResult<Received>, ApiResult<ReceivedListVm>>().ReverseMap();
        }
    }
}
