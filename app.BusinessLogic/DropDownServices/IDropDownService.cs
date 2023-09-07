using app.EntityModel.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Services.DropDownServices
{
    public interface IDropDownService
    {
        Task<IEnumerable<DropDownViewmodel>> vendorlist(int vendortype);
        Task<IEnumerable<DropDownViewmodel>> productlist();
        Task<Product> sigleproduct(long id);
    }
}
