using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcPlayground.Helpers;

namespace MvcPlayground.Models
{
    [TypeConverter(typeof(PascalCaseWordSplittingEnumConverter))]
    public enum Color
    {
        Red,
        Green,
        Blue,
        BrightRed,
        BrightGreen,
        BrightBlue,
    }

    public class Person
    {
        [Display(Name = "Favorite fruit", Order = 200)]
        [AutoComplete("AutocompleteTextBox", "Autocomplete")] 
        public string Fruit { get; set; }
        [Display(Name = "Fullname", Order = 100)]
        public string Name { get; set; }

        
        [Display(Order = 300)]
        [DataType("Date")]
        public DateTime Birthdate { get; set; }

       // [UIHint("EnumDropdown")]
       // public Color FavoriteColor { get; set; }
    }
}