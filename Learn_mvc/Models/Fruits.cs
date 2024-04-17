using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Learn_mvc.Models
{
    public class Fruits
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public bool Checked { get; set; }

        [CheckBoxAtLeastOneValidation(ErrorMessage = "Select at least 1 fruits")]
        public List<Check_box> fruits { get; set; }
    }

    public class Check_box
    {
        public bool Checked { get; set; }
    }
}