using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Student_CRUD_Web_App.Models;
using System.Text;

namespace Student_CRUD_Web_App.Controllers
{
    public class StudentModelsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public StudentModelsController(
            IHttpClientFactory factory,
            IConfiguration configuration)
        {
            _httpClient = factory.CreateClient();
            _apiUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (!response.IsSuccessStatusCode)
                return View(new List<StudentModel>());

            var json = await response.Content.ReadAsStringAsync();
            var students = JsonConvert.DeserializeObject<List<StudentModel>>(json);

            return View(students);
        }


        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var student = JsonConvert.DeserializeObject<StudentModel>(
                await response.Content.ReadAsStringAsync());

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var content = new StringContent(
                JsonConvert.SerializeObject(student),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create student.");
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var student = JsonConvert.DeserializeObject<StudentModel>(
                await response.Content.ReadAsStringAsync());

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentModel student)
        {
            if (id != student.ID)
                return BadRequest();

            var content = new StringContent(
                JsonConvert.SerializeObject(student),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to update student.");
            return View(student);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var student = JsonConvert.DeserializeObject<StudentModel>(
                await response.Content.ReadAsStringAsync());

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest();
        }
    }
}
