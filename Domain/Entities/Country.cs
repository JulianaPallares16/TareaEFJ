using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Country
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
    public Country() { }
    public Country(string name) {Name = name; }
}
