using System.ComponentModel.DataAnnotations;

namespace UserApi.Dto
{
    public class RegisterModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
