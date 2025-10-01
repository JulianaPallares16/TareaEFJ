using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.Company;

public record UpdateCompanyDto(Guid Id, string Name, string Nit, string Address, string Email, Guid CityId);