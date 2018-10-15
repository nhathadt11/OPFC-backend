﻿using System;
using System.Collections.Generic;

namespace OPFC.API.DTO
{
    public class MenuDTO
    {
        public long Id { get; set; }

        public string MenuName { get; set; }

        public string Description { get; set; }

        public int ServingNumber { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public long BrandId { get; set; }
        
        public List<long> MealIds { get; set; }

        public List<string> MealNames { get; set; }

        public List<long> EventTypeIds { get; set; }

        public List<string> EventTypeNames { get; set; }

        public List<RatingDTO> RatingList { get; set; }
    }
}
