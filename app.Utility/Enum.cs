﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Utility
{
    public enum ProductType
    {
        Raw_Product ='R',
        Finish_Product ='F',
    }

    public enum Unit
    {
        KG ,
        Pcs,
        Packet,
    }
    public enum VendorType
    {
        Supplier = 1,
        Customer = 2,
    }  
    public enum PaymentType
    {
        Credit=1,
        Cash=2,
        Special=3,
    }
    public enum PaymentMethod
    {
        Cash_Heand = 1,
        Bank = 2,
        Mobile_banking = 3,
    }
    public enum CustomerType
    {
        Dealer=1,
        Retail=2,
        Corporate=3,
        Individual = 4,
    }
    public enum StockType
    {
        PV = 1,
        SV = 2,
        PRV = 3,
        SRV = 4,
    }
    public enum OtherExpensesType
    {
        Rent =1,
        Repairs =2,
        Insurance =3,
        Rates_and_Taxes =4,
        Tax_Penalties = 5,
        Conveyance=6,
        Consumptions_of_Spares = 7,
        Salary= 8,
        Refresment = 9,
        Utility_Bill = 10,
       Others = 20,
    }
}
