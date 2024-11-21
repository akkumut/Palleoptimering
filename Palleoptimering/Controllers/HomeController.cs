﻿using Microsoft.AspNetCore.Mvc;
using Palleoptimering.Models;
using Palleoptimering.Models.ViewModels;

namespace Palleoptimering.Controllers
{
    public class HomeController : Controller
    {
        private IPalletRepository repository;

        public HomeController(IPalletRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult ListPallets()
        {
            return View(new PalletListViewModel { Pallets = repository.Pallets});
        }
    }
}
