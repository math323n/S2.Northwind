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

            Orders = new ObservableCollection<Order>(orders);


        }

        public ObservableCollection<Order> Orders { get; set; }
    }
}
