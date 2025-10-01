using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;

namespace Application.Countries;
public sealed record CreateCountry(string Name) : IRequest<Guid>;
  