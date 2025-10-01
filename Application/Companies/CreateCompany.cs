using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Companies;
public sealed record CreateCompany(string Name, string Nit, string Address, string Address, string Email) : IRequest<Guid>;
