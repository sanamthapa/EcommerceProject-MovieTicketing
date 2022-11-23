using EcommerceProject.Data.Base;
using EcommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Data.Services
{
    public class ActorsService :EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context) { }
       
      
    }
}
