﻿using DataAccess;
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
            Repository repository = new Repository();
            
            List<Order> orders = repository.GetAllOrders();
            Orders = new ObservableCollection<Order>(orders);



        }

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<OrderDetail> OrderDetails { get; set; }
    }
}