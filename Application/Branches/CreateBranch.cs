using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Branches;
public sealed record CreateBranch(int NumeroComercial, string Address, string Email, string ContactName, int Phone) : IRequest<Guid>;

