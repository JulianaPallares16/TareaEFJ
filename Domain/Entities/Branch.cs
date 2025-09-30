using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Branch
{
    public int Id { get; private set; }
    public int NumeroComercial { get; private set; }
    public string Address { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string ContactName { get; private set; } = null!;
    public int Phone { get; private set; }
    public int? CityId { get; set; }
    public virtual City? City { get; set; }
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public Branch() { }
    public Branch(int numeroComercial, string address, string email, string contactName, int phone) {NumeroComercial = numeroComercial; Address = address; Email = email; ContactName = contactName; Phone = phone; }
}