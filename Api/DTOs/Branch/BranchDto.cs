using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.Branch;    
public record BranchDto(Guid Id, int NumeroComercial, string Address, string Email, string ContactName, int Phone, Guid CityId, Guid CompanyId );
