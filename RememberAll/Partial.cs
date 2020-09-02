using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RememberAll
{
    partial class Users
    {
        public int Count { get; set; }
    }
    partial class Orders 
    {
        public decimal Price
        {
            get
            {
                RememberAllEntities db = new RememberAllEntities();
                decimal price = 0;
                foreach (var item in db.OrderItems.Where(x=>x.Orders.EmergencyMaintenancesID!=null).ToList())
                {
                    if (item.OrderID==ID)
                    {
                        if (item.UnitPrice.HasValue)
                        {
                            price += Convert.ToDecimal(item.UnitPrice);
                        }
                    }
                }
                return price;
            }
        }
    }
}
