﻿using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IMenuRepository :IRepository<Menu>
    {
        Menu CreateMenu(Menu menu);

        Menu UpdateMenu(Menu menu);

        Menu GetMenuById(long MenuId);

        List<Menu> GetAllMenu();

        List<Menu> GetAllByBrandId(long id);

        List<Menu> GetAllByBrandIds(List<long> brandIds);

        List<Menu> GetAllMenuWithCollaborative();

        List<Menu> GetAllMenuByIdsWithCollaborative(List<long> menuIds);
    }
}
