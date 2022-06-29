using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1) => View(new ProductListViewModel
        {
            Products = repository.Products.Where(p => category == null||p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                repository.Products.Count():
                repository.Products.Where(e => e.Category == category).Count()
            },
            CurrentCategory = category
        });

        //Needs a method here to display manual product searching
        public ViewResult Search(string name, int page = 1) => View(new ProductListViewModel
        {
            //A view is created that displayes all products that match the name
            //The total products selected is determined by if a given product matches the query name, these are then ordered by their ID
            //Then they are placed onto the page for the user to view
            Products = repository.Products.Where(p => name == null || p.Name == name).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = name == null ?
                repository.Products.Count() :
                repository.Products.Where(e => e.Name == name).Count()
            },
            //Hujack the Current Category component of the ProductListViewModel for use in searching
            CurrentCategory = name
        });
    }
}
