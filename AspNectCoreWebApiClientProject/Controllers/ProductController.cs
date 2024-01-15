
using AspNectCoreWebApiClientProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AspNectCoreWebApiClientProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {

            List<Product> products = new List<Product>();
            using(var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:5013/api/Product"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(products);
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {

            var product = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:5013/api/Product/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:5013/api/Product", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorData = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(errorResponse);

                        if (errorData != null)
                        {
                            foreach (var key in errorData.Keys)
                            {
                                foreach (var errorMessage in errorData[key])
                                {
                                    // Assuming the key is "" for model level errors
                                    // Adjust if your API returns a different key for model level errors
                                    ModelState.AddModelError(key, errorMessage);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Log the exception (optional)
            }
            return View(product);
        }


        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)        {

            var product = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:5013/api/Product/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(product);

       
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"http://localhost:5013/api/Product/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        var errorData = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(errorResponse);

                        if (errorData != null)
                        {
                            foreach (var key in errorData.Keys)
                            {
                                foreach (var errorMessage in errorData[key])
                                {
                                    // Assuming the key is "" for model level errors
                                    // Adjust if your API returns a different key for model level errors
                                    ModelState.AddModelError(key, errorMessage);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message); // Log the exception (optional)
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = new Product();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("http://localhost:5013/api/Product/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

            return View(product);

        }

    
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"http://localhost:5013/api/Product/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                // Log the exception (optional)
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
