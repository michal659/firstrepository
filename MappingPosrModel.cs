using AutoMapper;
using smr.Entitis;
using smr.Models;

namespace smr
{
    public class MappingPosrModel :Profile
    {
        public MappingPosrModel()
        {
            CreateMap<renterPostModel,Renter>().ReverseMap();
            CreateMap<toirstPostModel, Renter>().ReverseMap();
            CreateMap<TurnPostModel, Renter>().ReverseMap();

        }
    }
}
