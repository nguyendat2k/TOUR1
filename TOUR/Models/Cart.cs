using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOUR.Models
{
    public class CartItem
    {
        public Tour tour { get; set; }
        public int quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Product_Cart(Tour tour, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s.tour.MaTour == tour.MaTour);
            if (item == null)
                items.Add(new CartItem
                {
                    tour = tour,
                    quantity = _quan
                });
            else
                item.quantity += _quan;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s.quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s.quantity * s.tour.Gia);
            return (decimal)total;
        }
        public void Update_quantity(string id, int _new_quan)
        {
            var item = items.Find(s => s.tour.MaTour == id);
            if (item != null)
                //item._quantity = _new_quan;

                item.quantity = _new_quan;
        }

        public void Remove_CartItem(string id)
        {
            items.RemoveAll(s => s.tour.MaTour == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}