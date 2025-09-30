using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? CountryId { get; set; }
    public virtual Country? Country { get; set; }
    public virtual ICollection<City> Cities { get; set; } = new HashSet<City>();
    public Region() { }
    public Region(string name) { Name = name; }
}
