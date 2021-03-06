﻿using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Repositories.Interfaces
{ 
    public interface IServiceLocationRepository : IRepository<ServiceLocation>
    {
        ServiceLocation CreateServiceLocation(ServiceLocation serviceLocation);

        ServiceLocation UpdateServiceLocation(ServiceLocation serviceLocation);

        ServiceLocation GetServiceLocationById(long serviceLocationId);

        List<ServiceLocation> GetAllServiceLocation();

        List<ServiceLocation> GetServiceLocationByDistrictId(long districtId);

        List<ServiceLocation> GetServiceLocationsByBrandId(long brandId);
        
        void RemoveRange(List<ServiceLocation> serviceLocations);

        void AddRange(List<ServiceLocation> serviceLocations);
    }
}
