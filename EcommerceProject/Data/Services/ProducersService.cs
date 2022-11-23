using EcommerceProject.Data.Base;
using EcommerceProject.Models;

namespace EcommerceProject.Data.Services
{
    public class ProducersService:EntityBaseRepository<Producer>, IProducerServices
    {
        public ProducersService(AppDbContext context):base(context)
        {

        }
    }
}
