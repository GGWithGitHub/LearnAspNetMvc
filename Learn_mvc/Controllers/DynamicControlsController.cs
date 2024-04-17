using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Controllers
{
    public class DynamicControlsController : Controller
    {
        List<Countries> countries;
        List<Hobbies> hobbies;
        List<Fruits> fruits;
        Comman_class comman_Class = new Comman_class();
        BigViewModel bigViewModel = new BigViewModel();

        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["action_name"] != null && TempData["action_name"].ToString() == "Get_selected_value_for_controls")
            {
                Load_countries();
                Load_hobbies();
                Load_fruits();

                var country = bigViewModel.CountryViewModels.FirstOrDefault(x => x.Id == 2);
                Countries countries = new Countries() { Id = country.Id, Name = country.Name };
                bigViewModel.CountryViewModel = countries;

                var hobby = bigViewModel.HobbyViewModels.FirstOrDefault(x => x.Id == 2);
                Hobbies hobbies = new Hobbies() { Id = hobby.Id, Name = hobby.Name };
                bigViewModel.HobbyViewModel = hobbies;

                //bigViewModel.FruitViewModels.Where(x => x.Id == 1 || x.Id == 2 || x.Id == 4).Select(x=>x.Checked = true).ToList();


                return View(bigViewModel);
            }
            else
            {
                Load_countries();
                Load_hobbies();
                Load_fruits();
                return View(bigViewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(BigViewModel bgvModel)
        {
            var country_model = bgvModel.CountryViewModel;
            var hobby_model = bgvModel.HobbyViewModel;
            var fruit_model = bgvModel.FruitViewModel;

            bool isValidModelForCountry = comman_Class.Validate_model(country_model);
            bool isValidModelForHobby = comman_Class.Validate_model(hobby_model);
            bool isValidModelForFruit = comman_Class.Validate_model(fruit_model);

            if (isValidModelForCountry && isValidModelForHobby && isValidModelForFruit)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Load_countries();
                Load_hobbies();
                Load_fruits();
                return View(bigViewModel);
            }
        }

        public ActionResult Get_selected_value_for_controls()
        {
            TempData["action_name"] = "Get_selected_value_for_controls";
            
            return RedirectToAction("Index");
        }

        private void Load_countries()
        {
            countries = new List<Countries>()
            {
                new Countries { Id=null, Name="--Select country--" },
                new Countries{ Id=1, Name="India" },
                new Countries{ Id=2, Name="US" },
                new Countries{ Id=3, Name="Japan" },
                new Countries{ Id=4, Name="PK" },
                new Countries{ Id=5, Name="China" }
            };

            bigViewModel.CountryViewModels = countries;
        }

        private void Load_hobbies()
        {
            hobbies = new List<Hobbies>()
            {
                new Hobbies{ Id=1, Name="Cricket" },
                new Hobbies{ Id=2, Name="Football" },
                new Hobbies{ Id=3, Name="Chess" },
                new Hobbies{ Id=4, Name="Hockey" },
                new Hobbies{ Id=5, Name="Tennis" }
            };

            bigViewModel.HobbyViewModels = hobbies;
        }

        private void Load_fruits()
        {
            //fruits = new List<Fruits>()
            //{
            //    new Fruits{ Id=1, Name="Apple", Checked = false },
            //    new Fruits{ Id=2, Name="Chiku", Checked = false },
            //    new Fruits{ Id=3, Name="Anar", Checked = false },
            //    new Fruits{ Id=4, Name="Jam", Checked = false },
            //    new Fruits{ Id=5, Name="Santara", Checked = false }
            //};

            fruits = new List<Fruits>()
            {
                new Fruits{ Id=1, Name="Apple", fruits = new List<Check_box>(){ new Check_box() { Checked = false } } },
                new Fruits{ Id=2, Name="Chiku", fruits = new List<Check_box>(){ new Check_box() { Checked = false } } },
                new Fruits{ Id=3, Name="Anar", fruits = new List<Check_box>(){ new Check_box() { Checked = false } } },
                new Fruits{ Id=4, Name="Jam", fruits = new List<Check_box>(){ new Check_box() { Checked = false } } },
                new Fruits{ Id=5, Name="Santara", fruits = new List<Check_box>(){ new Check_box() { Checked = false } } }
            };


            bigViewModel.FruitViewModels = fruits;
        }
    }

}