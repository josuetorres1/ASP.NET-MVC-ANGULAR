using System.Collections.Generic;
using Yangaroo.Core.Models;

namespace Yangaroo.Core.Repositories
{
    public interface ICupCakeRepository
    {
        void Create(CupCake cupcake);

        IEnumerable<CupCake> GetAll();

        void Update(CupCake cupcake);

        void Delete(CupCake cupcake);
    }
}
