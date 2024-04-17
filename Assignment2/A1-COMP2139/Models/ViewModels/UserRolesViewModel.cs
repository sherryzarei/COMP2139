namespace A1_COMP2139.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
