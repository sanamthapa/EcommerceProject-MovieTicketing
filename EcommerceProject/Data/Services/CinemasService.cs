using EcommerceProject.Data.Base;
using EcommerceProject.Models;

namespace EcommerceProject.Data.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}
