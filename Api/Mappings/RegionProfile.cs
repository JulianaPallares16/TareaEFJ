using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Region;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings;
public class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<Region, RegionDto>();

        CreateMap<CreateRegionDto, Region>()
            .ConstructUsing(src => new Region(src.Name)
            {
                CountryId = src.CountryId
            });


        CreateMap<UpdateRegionDto, Region>()
            .ConstructUsing(src => new Region(src.Name)
            {
                CountryId = src.CountryId
            });
    }
}