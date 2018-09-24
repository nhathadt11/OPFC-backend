﻿using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class BrandRepository: EFRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext dbContext) : base(dbContext) { }
    }
}
