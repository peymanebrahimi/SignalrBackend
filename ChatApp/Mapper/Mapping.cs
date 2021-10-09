using AutoMapper;
using ChatApp.Models.Expense.Receive;

namespace ChatApp.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            
            CreateMap<CreateReceivedCommand, Received>().ReverseMap();
            CreateMap<UpdateReceivedCommand, Received>().ReverseMap();
            //CreateMap<CreateReceivedCommand, Received>().ReverseMap();
            //CreateMap<CreateReceivedCommand, Received>().ReverseMap();
        }
    }
}
