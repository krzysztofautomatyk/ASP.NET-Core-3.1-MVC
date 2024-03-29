﻿using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    public interface IKategoriaRepository : IRepository<Kategoria>
    {
        void Update(Kategoria kategoria);
    }
}
