using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Country;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings;

public class CountryProfile:Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDto>();

        CreateMap<CreateCountryDto, Country>()
            .ConstructUsing(src => new Country(
                src.Name
            ));


        CreateMap<UpdateCountryDto, Country>()
            .ConstructUsing(src => new Country(
                src.Name
            ));
    }
}
