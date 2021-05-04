using System.ComponentModel.DataAnnotations;

namespace SimpleLocalization.ViewModels.Home
{
    public class PersonViewModel
    {
        [Display(Name = "SecondaryKey")]
        public string Name { get; set; }

        [Display(Name = "AnotherKey")]
        public string LastName { get; set; }
    }
}
