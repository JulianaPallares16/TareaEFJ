using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Domain.Entities;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? RegionId { get; set; }
    public virtual Region? Region { get; set; }
    public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    public City() { }
    public City(string name) { Name = name; }
}