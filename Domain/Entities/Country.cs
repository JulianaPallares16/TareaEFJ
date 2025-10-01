using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Country
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public virtual ICollection<Region> Regions { get; set; } = new HashSet<Region>();
    public Country() { }
    public Country(string name) {Name = name; }
}
