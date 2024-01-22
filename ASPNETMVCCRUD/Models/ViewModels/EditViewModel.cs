namespace ASPNETMVCCRUD.Models.ViewModels
{
    public class EditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int Salary { get; set; }

        public string Department { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
