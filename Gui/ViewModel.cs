using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Gui
{
   public class ViewModel
    {


        public ViewModel()
        {
            List<Order> orders = Repository.GetAllOrders();
            List<Order> ordersDetails = Repository.GetAllOrderDetails();

            Orders = new ObservableCollection<Order>(orders);
            OrderDetails = new ObservableCollection<Order>(ordersDetails);


        }

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Order> OrderDetails { get; set; }
    }
}
