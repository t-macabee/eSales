﻿using System;
using System.Collections.Generic;

namespace eSales.Services.Database;

public partial class Skladistum
{
    public int SkladisteId { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Adresa { get; set; }

    public string? Opis { get; set; }

    public virtual ICollection<Izlazi> Izlazis { get; set; } = new List<Izlazi>();

    public virtual ICollection<Ulazi> Ulazis { get; set; } = new List<Ulazi>();
}
