using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.City;
public record UpdateCityDto(string Name, Guid RegionId);

