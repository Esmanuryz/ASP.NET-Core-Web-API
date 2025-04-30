using System.Collections.Generic;
using Net_React_odev_Nisan___2;


public static class fakedata
{
    //working or not for checkinh
    public static List<Product> Products { get; } = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 15000, Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Price = 250, Stock = 100 },
    };
}
