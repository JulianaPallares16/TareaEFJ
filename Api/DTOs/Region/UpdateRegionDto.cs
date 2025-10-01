using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs.Region;

public record UpdateRegionDto(string Name, Guid CountryId);
