﻿using System;
using System.Collections.Generic;
using OPFC.Models;

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

        public string BrandName { get; set; }

        public string BrandPhone { get; set; }

        public int BrandParticipantNumber { get; set; }

        public string BrandEmail { get; set; }
        
        public List<long> MealIds { get; set; }

        public List<string> MealNames { get; set; }

        public List<long> EventTypeIds { get; set; }

        public List<string> EventTypeNames { get; set; }
        
        public List<long> CategoryIds { get; set; }
        
        public List<string> CategoryNames { get; set; }

        public List<Meal> MealList { get; set; }
        
        public List<EventType> EventTypeList { get; set; }

        public List<RatingDTO> RatingList { get; set; }

        public List<Category> CategoryList { get; set; }

        public int TotalBookmark { get; set; }

        public int TotalRating { get; set; }

        public decimal AverageRatingPoint { get; set; }

        public decimal TotalRatingPoint { get; set; }
        
        public BrandSummary BrandSummary { get; set; }

        public string[] Photo { get; set; }
    }
}
