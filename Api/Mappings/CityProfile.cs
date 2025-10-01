using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.City;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings;
public class CityProfile:Profile
{
    public CityProfile()
    {
        CreateMap<City, CityDto>();

        CreateMap<CreateCityDto, City>()
            .ConstructUsing(src => new City(src.Name)
            {
                RegionId = src.RegionId
            });


        CreateMap<UpdateCityDto, City>()
            .ConstructUsing(src => new City(src.Name)
            {
                RegionId = src.RegionId
            });
    }
}
