using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs.Company;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>();

        CreateMap<CreateCompanyDto, Company>()
            .ConstructUsing(src => new Company(
                src.Name,
                src.Nit,
                src.Address,
                src.Email
            )
            {
                CityId = src.CityId
            });

        CreateMap<UpdateCompanyDto, Company>()
            .ConstructUsing(src => new Company(
                src.Name,
                src.Nit,
                src.Address,
                src.Email
            )
            {
                CityId = src.CityId,
            });
    }
}
