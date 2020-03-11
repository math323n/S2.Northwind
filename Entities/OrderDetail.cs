using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class OrderDetail
    {
        protected int orderID;
        protected decimal unitPrice;
        protected int quantity;
        protected float discount;

        public OrderDetail(int orderID, decimal unitPrice, int quantity, float discount)
        {
            OrderID = orderID;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
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

        public virtual decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                unitPrice = value;
            }
        }

        public virtual int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                (bool isValid, string errorMessage) = ValidateIsNegative(value);
                if(!isValid)
                {
                    throw new ArgumentException(nameof(Quantity), errorMessage);
                }
                if(value != quantity)
                {
                    quantity = value;
                }
            }
        }

        public virtual float Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
            }
        }

        public static (bool, string) ValidateIsNegative(int number)
        {
            if(number <= 0)
            {
                return (false, "Error, number below or equal to 0.");
                
            }
            else
            {
                return (true, string.Empty);
            }
        }
    }
}
