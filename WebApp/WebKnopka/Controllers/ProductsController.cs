using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebKnopka.Data;
using WebKnopka.Data.Entities;
using WebKnopka.Models;
using WebKnopka.Services;

namespace WebKnopka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppEFContext _context;
        public ProductsController(AppEFContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm]UploadProductImageViewModel model)
        {
            string fileName = string.Empty;
            if(model.Image!=null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName).Trim();
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
                fileName = Path.GetRandomFileName() + fileExp;
                using(var stream = System.IO.File.Create(Path.Combine(dir, fileName)))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }
            return Ok();
        }
        public class SearchProduct
        {
            public int[] filterValueSearch { get; set; } 
        }

        [HttpGet]
        [Route("FilterSearch")]
        public IActionResult GetFilterProducts([FromQuery] int[] filterValueSearch)
        {
            var filters = GetFilterSelect();

            //var filterValueSearch = search.filterValueSearch;//усі товари, у яких процесор i5
            var query = _context.Products.AsQueryable();
            foreach (var fName in filters)
            {
                int countFilter = 0; //Кількість співпадінь у даній групі
                var predicate = PredicateBuilder.False<ProductEntity>();
                //іду по дочірніх елементах групи, тобто по значеннях фільтра
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearch.Length; i++)
                    {
                        var idValue = fValue.Id;
                        if (filterValueSearch[i] == idValue)
                        {
                            predicate = predicate
                                .Or(p => p.Filters.Any(f => f.FilterValueId == idValue));
                            countFilter++;
                        }
                    }
                }
                if (countFilter != 0)
                {
                    query = query.Where(predicate);
                }
            }

            int count = query.Count();

            var products = query.Select(
                p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Filters = p.Filters.Select(f => new { Value = f.FilterValue.Name })
                }
                ).ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("FilterGroup")]
        public IActionResult GetFilterGroup() 
        {
            var filterGroup = GetFilterSelect();
            return Ok(filterGroup);
        }


        private List<FilterNameModel> GetFilterSelect()
        {
            var queryName = from f in _context.FilterNames.AsQueryable()
                            select f;
            var queryGroup = from g in _context.FilterNameGroups.AsQueryable()
                             select g;
            //Загальна множина значень
            var query = from u in queryName
                        join g in queryGroup on u.Id equals g.FilterNameId into ua
                        from empty in ua.DefaultIfEmpty()
                        select new
                        {
                            FNameId = u.Id,
                            FName = u.Name,
                            FValueId = empty != null ? empty.FilterValueId : 0,
                            FValue = empty != null ? empty.FilterValue.Name : null,
                        };
            //var info = query.Where(x=>x.FValueId!=0).ToList();

            var groupData = query
                .Where(x => x.FValueId != 0)
                .AsEnumerable()
                .GroupBy(f => new { Id = f.FNameId, Name = f.FName })
                .Select(g => g)
                .OrderBy(x => x.Key.Name);

            var result = groupData.Select(fName => new FilterNameModel
            {
                Id = fName.Key.Id,
                Name = fName.Key.Name,
                Children = fName
                    .GroupBy(v => new FilterItem { Id = v.FValueId, Name = v.FValue })
                    .Select(g => g.Key)
                    .OrderBy(x => x.Name)
                    .ToList()
            }).ToList();

            return result;
        }

    }
}
