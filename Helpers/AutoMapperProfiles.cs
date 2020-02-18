using AutoMapper;
using fix_it_tracker_back_end.Dtos;
using fix_it_tracker_back_end.Model;

namespace fix_it_tracker_back_end.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerGetDto>();
            CreateMap<Fault, FaultGetDto>();
            CreateMap<Item, ItemGetDto>();
            CreateMap<ItemType, ItemTypeGetDto>();
            CreateMap<Repair, RepairGetDto>();
            CreateMap<Resolution, ResolutionGetDto>();
        }
    }
}
