using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAdaptor
{
    public class Orders
    {
        public static List<Orders> order = new List<Orders>();
        public Orders()
        {

        }
        public Orders(int OrderID, string CustomerId,string ShipCity)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.ShipCity = ShipCity;
        }
        public static List<Orders> GetAllRecords()
        {
            if (order.Count() == 0)
            {
                int code = 10000;
                for (int i = 1; i < 10; i++)
                {
                    order.Add(new Orders(code + 1, "ALFKI", "Berlin"));
                    order.Add(new Orders(code + 2, "ANATR", "Madrid"));
                    order.Add(new Orders(code + 3, "ANTON", "Cholchester"));
                    order.Add(new Orders(code + 4, "BLONP", "Marseille"));
                    order.Add(new Orders(code + 5, "BOLID", "Tsawassen"));
                    code += 5;
                }
            }
            return order;
        }

        public int? OrderID { get; set; }
        public string CustomerID { get; set; }

        public string ShipCity { get; set; }

    }
}