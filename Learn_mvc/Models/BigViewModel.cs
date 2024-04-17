using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class BigViewModel
    {
        public Student StudentViewModel { get; set; }
        public College CollegeViewModel { get; set; }
        public Countries CountryViewModel { get; set; }
        public Hobbies HobbyViewModel { get; set; }
        public Fruits FruitViewModel { get; set; }
        public User UserViewModel { get; set; }

        public IEnumerable<Student> StudentViewModels { get; set; }
        public List<Countries> CountryViewModels { get; set; }
        public List<Hobbies> HobbyViewModels { get; set; }
        public List<Fruits> FruitViewModels { get; set; }
    }
}