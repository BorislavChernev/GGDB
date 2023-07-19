using Microsoft.AspNetCore.Identity;

namespace GoodGameDatabase.Data.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }
    }
}