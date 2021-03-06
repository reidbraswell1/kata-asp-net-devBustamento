﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Itb.Shared;
using System.Threading.Tasks;

namespace Itb.Repositories 
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn; 
        }

        public int CreateProduct(Product prod)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("INSERT INTO product (Name) VALUES (@Name)", prod);
            }
        }

        public int DeleteProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("DELETE FROM product WHERE ProductId = @Id", new { id });
            }
        }

        public int UpdateProduct(Product prod)
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.Execute("UPDATE product SET Name = @Name WHERE ProductId = @id", prod);
            }
        }

        public Task<Product> GetProduct(int id) 
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.QueryFirstAsync<Product>("SELECT *, ProductId as Id FROM Product WHERE ProductId = @Id", new { id });
            }
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            using (var conn = _conn)
            {
                conn.Open();
                return conn.QueryAsync<Product>("SELECT *, ProductId as Id FROM Product");
            }
        }
    }
}
