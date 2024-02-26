using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAdaptor;

namespace WebApiAdaptor.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/Orders
        [HttpGet]

        public object Get()
        {
            var queryString = Request.Query;
            var data = Orders.GetAllRecords().ToList();
            string sort = queryString["$orderby"];   //sorting     
            string filter = queryString["$filter"]; // filtering
 

            if (!string.IsNullOrEmpty(sort))
            {
                var sortConditions = sort.Split(',');

                var orderedData = data.OrderBy(x => 0); // Start with a stable sort

                foreach (var sortCondition in sortConditions)
                {
                    var sortParts = sortCondition.Trim().Split(' ');
                    var sortBy = sortParts[0];
                    var sortOrder = sortParts.Length > 1 && sortParts[1].ToLower() == "desc";

                    switch (sortBy)
                    {
                        case "OrderID":
                            orderedData = sortOrder ? orderedData.ThenByDescending(x => x.OrderID) : orderedData.ThenBy(x => x.OrderID);
                            break;
                        case "CustomerID":
                            orderedData = sortOrder ? orderedData.ThenByDescending(x => x.CustomerID) : orderedData.ThenBy(x => x.CustomerID);
                            break;
                        case "ShipCity":
                            orderedData = sortOrder ? orderedData.ThenByDescending(x => x.ShipCity) : orderedData.ThenBy(x => x.ShipCity);
                            break;
                    }
                }

                data = orderedData.ToList();
            }
            if (filter != null)
            {
                var filterfield="";
                var filtervalue = "";
                var filters = filter.Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var filterItem in filters)
                {
                    var filterParts = filterItem.Split('(', ')', '\'');
                    if (filterParts.Length != 9)
                    {
                        var filterValueParts = filterParts[1].Split();
                        filterfield = filterValueParts[0];
                        filtervalue = filterValueParts[2];
                    }
                    else
                    {
                      filterfield = filterParts[3];
                      filtervalue = filterParts[5];
                    }
                    switch (filterfield)
                    {
                        case "OrderID":

                            data = (from cust in data
                                    where cust.OrderID.ToString() == filtervalue.ToString()
                                    select cust).ToList();
                            break;
                        case "CustomerID":
                            data = (from cust in data
                                    where cust.CustomerID.ToLower().StartsWith(filtervalue.ToString())
                                    select cust).ToList();
                            break;
                        case "ShipCity":
                            data = (from cust in data
                                    where cust.ShipCity.ToLower().StartsWith(filtervalue.ToString())
                                    select cust).ToList();
                            break;
                    }
                }
            }

            int skip = Convert.ToInt32(queryString["$skip"]);
            int take = Convert.ToInt32(queryString["$top"]);
            return take != 0 ? new { Items = data.Skip(skip).Take(take).ToList(), Count = data.Count() } : new { Items = data, Count = data.Count() };
        }


        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public object Post([FromBody] Orders value)
        {
            Orders.GetAllRecords().Insert(0, value);
            var data = Orders.GetAllRecords().ToList();
            return data;

        }


        // PUT: api/Orders/5
        [HttpPut]
        public object Put(int id, [FromBody] Orders value)
        {
            var ord = value;
            Orders val = Orders.GetAllRecords().Where(or => or.OrderID == ord.OrderID).FirstOrDefault();
            val.OrderID = ord.OrderID;
            val.CustomerID = ord.CustomerID;
            val.ShipCity = ord.ShipCity;
            return value;
        }

        // DELETE: api/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            Orders.GetAllRecords().Remove(Orders.GetAllRecords().Where(or => or.OrderID == id).FirstOrDefault());
            return Ok(id);
        }
    }
}
