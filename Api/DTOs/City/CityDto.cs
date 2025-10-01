using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.City;
public record CityDto(Guid Id,string Name, Guid RegionId);
