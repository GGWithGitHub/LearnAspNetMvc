using Learn_mvc.Data.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.IdentityDbContextFolder
{
    public class MyIdentityDbContext : IdentityDbContext<MyIdentityUser>
    {
        public MyIdentityDbContext() : base("IdentityDb", throwIfV1Schema: false)
        {
        }

        public static MyIdentityDbContext Create()
        {
            return new MyIdentityDbContext();
        }
    }
}
