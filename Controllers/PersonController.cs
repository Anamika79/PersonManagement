using Microsoft.AspNetCore.Mvc;
using PersonManagement.DAL;
using PersonManagement.Models;

namespace PersonManagement.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonDAL dal;
        public PersonController(PersonDAL dataAccess)
        {
            dal = dataAccess;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Person> persons = new List<Person>();
            try
            {
                persons = dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(persons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Model data is not valid.";
                return View();
            }
            bool result = dal.Insert(model);

            if (!result)
            {
                TempData["errorMessage"] = "Unable to save the data";
                return View();
            }
            TempData["successMessage"] = "Employee details saved";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Person person = dal.GetPersonById(id);
            if (person == null)
            {
                TempData["errorMessage"] = "Person not found.";
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person model)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Model data is not valid.";
                return View();
            }
            model.DOB = model.DOB.Date;
            bool result = dal.Update(model);

            if (!result)
            {
                TempData["errorMessage"] = "Unable to update the person details";
                return View();
            }
            TempData["successMessage"] = "Person details updated";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Person person = dal.GetPersonById(id);
            if (person == null)
            {
                TempData["errorMessage"] = "Person not found.";
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Deleted(int id)
        {
            bool result = dal.Delete(id);

            if (!result)
            {
                TempData["errorMessage"] = "Unable to delete the person";
            }
            else
            {
                TempData["successMessage"] = "Person deleted successfully";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Found(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                TempData["errorMessage"] = "Please provide an email address.";
                return RedirectToAction("Search");
            }

            Person person = dal.GetPersonByEmail(Email);
            if (person == null)
            {
                TempData["errorMessage"] = "Person not found.";
                return RedirectToAction("Search");
            }

            return View(person);
        }

    }

}