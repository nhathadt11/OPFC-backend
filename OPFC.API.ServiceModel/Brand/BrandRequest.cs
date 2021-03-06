﻿using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Brand
{
    [EnableCors("*", "*")]
    [Route("/Brand/CreateBrand/", "POST")]
    public class CreateBrandRequest : IReturn<CreateBrandResponse>
    {
        public BrandDTO Brand { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Brand/GetBrandById/{id}", "GET")]
    public class GetBrandByIdRequest : IReturn<GetBrandByIdReponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "OPTIONS,POST")]
    [Route("/Brand/CreateCaterer/", "POST")]
    public class CreateCatererRequest : IReturn<CreateCatererResponse>
    {
        public UserDTO User { get; set; }

        public BrandDTO Brand { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Brand/UpdateBrand/", "POST")]
    public class UpdateBrandRequest : IReturn<UpdateBrandResponse>
    {
       public BrandDTO Brand { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Brand/ChangeBrandStatus/", "POST")]
    public class ChangeBrandStatusRequest : IReturn<ChangeBrandStatusResponse>
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Photo/SavePhoto/", "POST")]
    public class SavePhotoRequest : IReturn<SavePhotoResponse>
    {
        public PhotoDTO Photo { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Brand/DeleteBrand/", "POST")]
    public class DeleteBrandRequest : IReturn<DeleteBrandResponse>
    {
        public BrandDTO Brand { get; set; }
    }
}
