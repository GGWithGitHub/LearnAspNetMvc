
using Learn_mvc.Data.IdentityDbContextFolder;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Learn_mvc.Data.IdentityModels
{
    public class MyUserStore : UserStore<MyIdentityUser>
    {
        public MyUserStore(MyIdentityDbContext dbContext) : base(dbContext)
        {

        }
    }

}
