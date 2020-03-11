using System;
using System.Collections.Generic;

namespace Entities
{
    public class Order
    {
        protected int orderID;
        protected DateTime orderDate;
        protected DateTime requiredDate;
        protected DateTime shippedDate;
        protected int shipVia;
        protected decimal freight;
        protected string shipName;
        protected string shipAddress;
        protected string shipCity;
        protected string shipRegion;
        protected string shipPostalCode;
        protected string shipCountry;
        protected List<OrderDetail> orderDetails;

        public Order(int orderID, DateTime orderDate, DateTime requiredDate, DateTime shippedDate,
            int shipVia, decimal freight, string shipName, string shipAddress, string shipCity,
            string shipRegion, string shipPostalCode, string shipCountry, List<OrderDetail> orderDetails)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            OrderDetails = orderDetails;
        }

        public virtual int OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
            }
        }

        public virtual DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
            }
        }

        public virtual DateTime RequiredDate
        {
            get
            {
                return requiredDate;
            }
            set
            {
                requiredDate = value;
            }
        }

        public virtual DateTime ShippedDate
        {
            get
            {
                return shippedDate;
            }
            set
            {
                shippedDate = value;
            }
        }

        public virtual int ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                shipVia = value;
            }
        }

        public virtual decimal Freight
        {
            get
            {
                return freight;
            }
            set
            {
                freight = value;
            }
        }

        public virtual string ShipName
        {
            get
            {
                return shipName;
            }
            set
            {
                shipName = value;
            }
        }

        public virtual string ShipAddress
        {
            get
            {
                return shipAddress;
            }
            set
            {
                shipAddress = value;
            }
        }

        public virtual string ShipCity
        {
            get
            {
                return shipCity;
            }
            set
            {
                shipCity = value;
            }
        }

        public virtual string ShipRegion
        {
            get
            {
                return shipRegion;
            }
            set
            {
                shipRegion = value;
            }
        }

        public virtual string ShipPostalCode
        {
            get
            {
                return shipPostalCode;
            }
            set
            {
                shipPostalCode = value;
            }
        }

        public virtual string ShipCountry
        {
            get
            {
                return shipCountry;
            }
            set
            {
                shipCountry = value;
            }
        }
        public virtual List<OrderDetail> OrderDetails
        {
            get
            {
                return orderDetails;
            }

            set
            {
                if(orderDetails != value)
                {
                    orderDetails = value;
                }
            }
        }
      
    }
}