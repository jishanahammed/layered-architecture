using System;
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
    public enum CustomerType
    {
        Dealer=1,
        Retail=2,
        Corporate=3,
    }
    public enum StockType
    {
        PV = 1,
        SV = 2,
        PRV = 3,
        SRV = 4,
    }
}
