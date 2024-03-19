using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAdaptor.Server.Models;

namespace WebApiAdaptor.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/Orders
        [HttpGet]
        // Action to retrieve orders
        public object Get()
        {
            var queryString = Request.Query;
            var data = OrdersDetails.GetAllRecords().ToList();

            string? sort = queryString["$orderby"];   // Get sorting parameter  
            string? filter = queryString["$filter"]; // // Get filtering parameter

            //Peform sort operation
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

                data = [.. orderedData];
            }
            if (filter != null)
            {
                var filters = filter.Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var filterItem in filters)
                {
                    if (filterItem.Contains("substringof"))
                    {
                        // Performing Search operation

                        var searchParts = filterItem.Split('(', ')', '\'');
                        var searchValue = searchParts[3];

                        // Apply the search value to all searchable fields
                        data = data.Where(cust =>
                                cust != null &&
                                ((cust.OrderID?.ToString()?.Contains(searchValue) ?? false) ||
                                (cust.CustomerID?.ToLower()?.Contains(searchValue) ?? false) ||
                                (cust.ShipCity?.ToLower()?.Contains(searchValue) ?? false))).ToList();
                    }
                    else
                    {
                        // Performing filter operation

                        var filterfield = "";
                        var filtervalue = "";
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
                                data = data.Where(cust => cust != null && cust.OrderID?.ToString() == filtervalue.ToString()).ToList();
                                break;
                            case "CustomerID":
                                data = data.Where(cust => cust != null && cust.CustomerID?.ToLower().StartsWith(filtervalue.ToString()) == true).ToList();
                                break;
                            case "ShipCity":
                                data = data.Where(cust => cust != null && cust.ShipCity?.ToLower().StartsWith(filtervalue.ToString()) == true).ToList();
                                break;
                                // Add more cases for other searchable fields if needed
                        }

                    }
                }
            }
            //Perform page operation

            int skip = Convert.ToInt32(queryString["$skip"]);
            int take = Convert.ToInt32(queryString["$top"]);
            int TotalRecordsCount = data.Count;

            return take != 0 ? new { Items = data.Skip(skip).Take(take).ToList(), Count = TotalRecordsCount } : new { Items = data, Count = TotalRecordsCount };
        }


        // POST: api/Orders
        [HttpPost]
        /// <summary>
        /// Inserts a new data item into the data collection.
        /// </summary>
        /// <param name="value">It holds new record detail which is need to be inserted.</param>
        /// <returns>Returns void</returns>
        public void Post([FromBody] OrdersDetails newRecord)
        {
            // Insert a new record into the OrdersDetails model
            OrdersDetails.GetAllRecords().Insert(0, newRecord);
        }

        // PUT: api/Orders/5
        [HttpPut]
        /// <summary>
        /// Update a existing data item from the data collection.
        /// </summary>
        /// <param name="order">It holds updated record detail which is need to be updated.</param>
        /// <returns>Returns void</returns>
        public void Put(int id, [FromBody] OrdersDetails order)
        {
            // Find the existing order by ID
            var existingOrder = OrdersDetails.GetAllRecords().FirstOrDefault(o => o.OrderID == id);
            if (existingOrder != null)
            {
                // If the order exists, update its properties
                existingOrder.OrderID = order.OrderID;
                existingOrder.CustomerID = order.CustomerID;
                existingOrder.ShipCity = order.ShipCity;
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        /// <summary>
        /// Remove a specific data item from the data collection.
        /// </summary>
        /// <param name="id">It holds specific record detail id which is need to be removed.</param>
        /// <returns>Returns void</returns>
        public void Delete(int id)
        {
            // Find the order to remove by ID
            var orderToRemove = OrdersDetails.GetAllRecords().FirstOrDefault(order => order.OrderID == id);
            // If the order exists, remove it
            if (orderToRemove != null)
            {
                OrdersDetails.GetAllRecords().Remove(orderToRemove);
            }
        }
    }
}