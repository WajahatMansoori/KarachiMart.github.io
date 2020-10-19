using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models.Home
{
    public class HomeIndexViewModel
    {
        //This class is responsible for Back-End to Front-End connectivity
        //i.e Product image adn alla the details, which was added by the admin will display to user in Front-End

        public List<Product> ListOfProducts { get; set; }
        public static EcommerceDBEntities context = new EcommerceDBEntities();
        
        public  HomeIndexViewModel CreateModel()
        {
            return new HomeIndexViewModel
            {
                ListOfProducts = context.Products.ToList()
                
            };
        }

    }

}