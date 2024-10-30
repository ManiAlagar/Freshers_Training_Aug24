using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Net;
using System.Web.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookstoreApplication.Repository.Implementation
{
    public class OrderRepository: IOrderRepository
    {
        private readonly BookDBContext db;
        public readonly IHttpContextAccessor httpContextAccessor;
        private readonly DapperContext _context;

        public OrderRepository(BookDBContext _dbContext, IHttpContextAccessor httpContextAccessor, DapperContext _context)
        {
            this.db = _dbContext;
            this.httpContextAccessor = httpContextAccessor;
            this._context = _context;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                var roleId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var query = "select Orders.*,users.Username,OrderStatus.[status] from orders join Users on orders.userid=users.userid join OrderStatus on OrderStatus.Id=Orders.StatusId";
                using (var connection = _context.CreateConnection())
                {
                    var orders = await connection.QueryAsync<Order>(query);
                    if (roleId == "1")//customers
                    {
                        return orders.Where(a => a.UserId == Convert.ToInt32(userId)).ToList();
                    }
                    else//admin
                    {
                        return orders.ToList();
                    }
                }
                
                //if (roleId == "1")//customers
                //{
                //    return await db.Orders.Where(a => a.UserId == Convert.ToInt32(userId)).ToListAsync();
                //}
                //else//admin
                //{
                //    return await db.Orders.ToListAsync();
                //}

            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

        }

    




        public async Task<int> AddOrder(string Address)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            int UserId = Convert.ToInt32(userId);
            using (var connection = _context.CreateConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@Address", Address);
                queryParameters.Add("@UserId", UserId);

                try
                {
                    var res = await connection.QueryAsync<int>(
                    "AddOrder",
                    queryParameters,
                    commandType: CommandType.StoredProcedure);
                    return 1;
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            var Order = await db.Orders.FindAsync(id);
            return Order;
        }

        public async Task<int> DeleteOrder(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            int UserId = Convert.ToInt32(userId);
            using (var connection = _context.CreateConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@id", id);
                queryParameters.Add("@UserId", UserId);

                try
                {
                    var res = await connection.QueryAsync<int>(
                    "DeleteOrder",
                    queryParameters,
                    commandType: CommandType.StoredProcedure);
                    return 1;
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }

        public async Task<int> UpdateOrder(int OrderId, int StatusId)
        {
            using (var connection = _context.CreateConnection())
            {
                var queryParameters = new DynamicParameters();
                queryParameters.Add("@orderId", OrderId);
                queryParameters.Add("@statusId", StatusId);

                try
                {
                    var res = await connection.QueryAsync<int>(
                    "updateOrder",
                    queryParameters,
                    commandType: CommandType.StoredProcedure);
                    return 1;
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }

        public async Task<IEnumerable<Order>> GetBooksFromOrder(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            using (var connection = _context.CreateConnection())
            {
                var queryParameters = new DynamicParameters();
                //queryParameters.Add("@userid", userId);
                queryParameters.Add("@id", id);

                try
                {
                    var res = await connection.QueryAsync<Order>(
                    "GetBooksFromOrder",
                    queryParameters,
                    commandType: CommandType.StoredProcedure);
                    return res;
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

            }
        }
    }
}














