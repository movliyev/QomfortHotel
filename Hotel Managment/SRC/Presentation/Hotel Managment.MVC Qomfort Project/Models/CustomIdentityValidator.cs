using Microsoft.AspNetCore.Identity;

namespace Hotel_Managment.MVC_Qomfort_Project.Models
{
    public class CustomIdentityValidator:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Password must be at least {length} characters long"
            };
        }
    }
}
