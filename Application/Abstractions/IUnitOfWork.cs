using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IUnitOfWork
{
    ICountryRepository Countries { get; }
    IRegionRepository Regions { get; }
    ICityRepository Cities { get; }
    ICompanyRepository Companies { get; }
    IBranchRepository Branches { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default);
}