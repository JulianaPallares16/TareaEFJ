using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Cities;
public sealed record CreateCity(string Name) : IRequest<Guid>;
  