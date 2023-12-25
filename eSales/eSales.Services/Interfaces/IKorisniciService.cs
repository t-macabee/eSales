﻿using eSales.Model.Requests.Korisnici;

namespace eSales.Services.Interfaces
{
    public interface IKorisniciService
    {
        List<Model.Korisnici> Get();
        Model.Korisnici Insert(KorisniciInsertRequest request);
        Model.Korisnici Update(int id, KorisniciUpdateRequest request);
    }
}