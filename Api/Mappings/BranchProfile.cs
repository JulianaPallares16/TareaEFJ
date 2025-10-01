using Api.DTOs.Branch;
using AutoMapper;
using Domain.Entities;

namespace Api.Mappings;
public class BranchProfile : Profile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>();

        CreateMap<CreateBranchDto, Branch>()
            .ConstructUsing(src => new Branch(
                src.NumeroComercial,
                src.Address,
                src.Email,
                src.ContactName,
                src.Phone
            )
            {
                CityId = src.CityId,
                CompanyId = src.CompanyId
            });

        CreateMap<UpdateBranchDto, Branch>()
            .ConstructUsing(src => new Branch(
                src.NumeroComercial,
                src.Address,
                src.Email,
                src.ContactName,
                src.Phone
            )
            {
                CityId = src.CityId,
                CompanyId = src.CompanyId
            });
    }
}
