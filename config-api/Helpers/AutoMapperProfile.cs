namespace config_api.Helpers;

using AutoMapper;
using config_api.Entities;
using config_api.Models.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> Data
        CreateMap<CreateRequest, Data>();

        // UpdateRequest -> Data
        CreateMap<UpdateRequest, Data>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
            ));
    }
}