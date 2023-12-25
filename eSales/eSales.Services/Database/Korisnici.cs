﻿using System;
using System.Collections.Generic;

namespace eSales.Services.Database;

public partial class Korisnici
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefon { get; set; }

    public string KorisnickoIme { get; set; } = null!;

    public string LozinkaHash { get; set; } = null!;

    public string LozinkaSalt { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<Izlazi> Izlazis { get; set; } = new List<Izlazi>();

    public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; } = new List<KorisniciUloge>();

    public virtual ICollection<Ulazi> Ulazis { get; set; } = new List<Ulazi>();
}
