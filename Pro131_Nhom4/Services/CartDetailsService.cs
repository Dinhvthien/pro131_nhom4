﻿using App_Shared.Model;
using App_Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;

namespace Pro131_Nhom4.Services
{
    public class CartDetailsService : ICartDetailsService
    {
        Mydb _context;
        public CartDetailsService()
        {
            _context = new Mydb();
        }
        public async Task<bool> CreateCartDetails(Guid accountId, Guid productId)
        {
            var cartDetail = await _context.Cartdetails.FirstOrDefaultAsync(p => p.AccountID == accountId && p.ProductID == productId);
            if (cartDetail == null)
            {

                CartDetails cartDetails = new CartDetails()
                {
                    Id = Guid.NewGuid(),
                    Quantity = 1,
                    ProductID = productId,
                    AccountID = accountId,
                };
                await _context.Cartdetails.AddAsync(cartDetails);
            }
            else
            {
                cartDetail.Quantity++;
                _context.Cartdetails.Update(cartDetail);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCartDetails(Guid id)
        {
            try
            {
                var del = _context.Cartdetails.Find(id);
                _context.Cartdetails.Remove(del);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<CartDetailsView>> GetAllCartDetails()
        {
            List<CartDetailsView> cartDetailsViews = new List<CartDetailsView>();
            cartDetailsViews = await (
                from a in _context.Cartdetails
                join b in _context.Products on a.ProductID equals b.Id
                join c in _context.Carts on a.AccountID equals c.UserID
                select new CartDetailsView()
                {
                    CartDetails = a,
                    Product = b,
                    Cart = c,
                }

                ).ToListAsync();
            return cartDetailsViews;
        }

        public async Task<List<CartDetailsView>> GetCartDetailsByAccountId(Guid id)
        {
            List<CartDetailsView> cartDetailsViews = new List<CartDetailsView>();
            cartDetailsViews = await (
                from a in _context.Cartdetails
                join b in _context.Products on a.ProductID equals b.Id
                join c in _context.Carts on a.AccountID equals c.UserID
                where a.AccountID == id
                select new CartDetailsView()
                {
                    CartDetails = a,
                    Product = b,
                    Cart = c,
                }

                ).ToListAsync();
            return cartDetailsViews;
        }

        public async Task<CartDetailsView> GetCartDetailsById(Guid accountId, Guid productId)
        {
            List<CartDetailsView> cartDetailsViews = new List<CartDetailsView>();
            cartDetailsViews = await (
                from a in _context.Cartdetails
                join b in _context.Products on a.ProductID equals b.Id
                join c in _context.Carts on a.AccountID equals c.UserID
                where a.AccountID == accountId
                select new CartDetailsView()
                {
                    CartDetails = a,
                    Product = b,
                    Cart = c,
                }).ToListAsync();
            return cartDetailsViews.AsQueryable().Where(p => p.CartDetails.Id == productId).FirstOrDefault();
        }

        public async Task<bool> Increase(Guid id)
        {
            try
            {
                var up = _context.Cartdetails.Find(id);
                up.Quantity++;
                _context.Cartdetails.Update(up);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Reduce(Guid id)
        {
            try
            {
                var up = _context.Cartdetails.Find(id);
                if (up.Quantity > 1)
                {
                    up.Quantity--;
                    _context.Cartdetails.Update(up);
                }
                else
                {
                    _context.Cartdetails.Remove(up);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
