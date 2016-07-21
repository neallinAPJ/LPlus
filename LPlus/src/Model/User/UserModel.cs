
using System.ComponentModel.DataAnnotations;


namespace Model.User
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Required]
        public string Account { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string EncryptPassword { get; set; }
        public string Adress { get; set; }
        public bool RememberMe { get; set; }
        public string Pictrue { get; set; }
        public string Phone { get; set; }
    }
}
