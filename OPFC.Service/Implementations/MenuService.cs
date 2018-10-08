﻿using System;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;

namespace OPFC.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IOpfcUow _opfcUow;

        public MenuService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Menu CreateMenu(Menu menu)
        {
            try
            {
                var result = _opfcUow.MenuRepository.CreateMenu(menu);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Menu> GetAllMenu()
        {
            try
            {
                var result = _opfcUow.MenuRepository.GetAllMenu();

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Menu GetMenuById(long id)
        {
            try
            {
                return _opfcUow.MenuRepository.GetMenuById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Menu UpdateMenu(Menu menu)
        {
            try
            {
                var result = _opfcUow.MenuRepository.UpdateMenu(menu);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
