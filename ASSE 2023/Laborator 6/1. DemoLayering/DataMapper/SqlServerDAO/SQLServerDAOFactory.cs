using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    class SQLServerDAOFactory : IDAOFactory
    {
        public IProductDataServices ProducDataServices
        {
            get
            {
                return new SQLProductDataServices();
            }
        }

        public ICustomerDataServices CustomerDataServices
        {
            get
            {
                return new SQLCustomerDataServices();
            }
        }
    }
}
