﻿using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.Entity;

namespace HKL_Juice.Routes
{
    public class JuiceRoute : NancyModule
    {
        public JuiceRoute(ApplicationDbContext dbContext)
        {
            Get("/", parameters =>
            {
                var products = dbContext.Product
                   .Select(p => new
                   {
                       productId = p.productId,
                       categoryId = p.categoryId,
                       productName = p.productName,
                       price = p.price,
                       descript = p.descript,
                       imgUrl = p.imgUrl,
                       OrderDetails = p.Category.categoryName
                   }).ToList();

                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(products);

                return View["juice.cshtml", json];
            }
            );
            Get("/{numberTable}", parameters =>
            {
                var numberTable = parameters.numberTable;
                var script = $@"
                    <script>
                        localStorage.setItem('tableNumber', '{numberTable}');
            
                        window.location.href = '/';
                    </script>";
                return script;
            });
        }
    }
}