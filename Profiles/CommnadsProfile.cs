using AutoMapper;
using Commander.API.Dtos;
using Commander.API.Models;

namespace Commander.API.Profiles
{
    public class CommnadsProfile : Profile
    {
        public CommnadsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandCreateDto, Command>();
            // Patch
            CreateMap<Command, CommandCreateDto>();
        }
    }
}
