using Microsoft.AspNetCore.Mvc;
using MongoDbGunduz.Dtos.CategoryDtos;
using MongoDbGunduz.Dtos.ProductDtos;
using MongoDbGunduz.Services.ProductServices;
using MongoDbGunduz.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MongoDbGunduz.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.GetProductWithCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            List<SelectListItem> values = (from x in await _categoryService.GetAllCategoryAsync()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.CategoryList = values;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {

            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> RemoveProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await _productService.GetByIdProductAsync(id);

            // Kategorileri al
            var categories = await _categoryService.GetAllCategoryAsync(); // Kategori listesini al

            // Kategorileri SelectListItem'a dönüştür
            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            ViewBag.CategoryList = categorySelectList; // Dropdown için veriyi sağlar

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(updateProductDto);
                return RedirectToAction("ProductList");
            }

            // Kategorileri tekrar al ve viewbag'e ekle
            var categories = await _categoryService.GetAllCategoryAsync(); // Kategori listesini al
            var categorySelectList = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            ViewBag.CategoryList = categorySelectList;

            return View(updateProductDto); // Hatalı modelle formu tekrar göster
        }





    }
}
