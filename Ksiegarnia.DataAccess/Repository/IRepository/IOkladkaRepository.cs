﻿using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksiegarnia.DataAccess.Repository.IRepository
{
    public interface IOkladkaRepository : IRepository<Okladka>
    {
        void Update(Okladka okladka);
    }
}
