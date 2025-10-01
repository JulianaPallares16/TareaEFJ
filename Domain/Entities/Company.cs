using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Company
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Nit { get; private set; }
    public string Address { get; private set; }
    public int? CityId { get; set; }
    public virtual City? City { get; set; }
    public string Email { get; private set; }
    public virtual ICollection<Branch> Branches { get; set; } = new HashSet<Branch>();
    public Company() { }
    public Company(string name, string nit, string address, string email ) { Name = name; Nit = nit; Address = address; Email = email; }
}
