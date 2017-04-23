namespace AutoRent.Migrations
{
    using AutoRent.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoRentContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutoRentContext context)
        {
            var customer = new List<Customer>
        {
            new Customer { firstName = "Carson",   lastName = "Alexander",   middleName = "Coleman", passportDetails = "W459270Q0", phoneNumber = "72534", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Meredith", lastName = "Alonso",      middleName = "Jarvis",  passportDetails = "I009479I8", phoneNumber = "64753", discountPercentage = Convert.ToDecimal(0.04)},
            new Customer { firstName = "Arturo",   lastName = "Anand",       middleName = "Kurt",    passportDetails = "Q500000Y2", phoneNumber = "53532", discountPercentage = Convert.ToDecimal(0.03)},
            new Customer { firstName = "Gytis",    lastName = "Barzdukas",   middleName = "Darius",  passportDetails = "Q4R6564Y7", phoneNumber = "46475", discountPercentage = Convert.ToDecimal(0.02)},
            new Customer { firstName = "Yan",      lastName = "Li",          middleName = "Cole",    passportDetails = "U900000R1", phoneNumber = "46575", discountPercentage = Convert.ToDecimal(0.1)},
            new Customer { firstName = "Kim",      lastName = "Li",          middleName = "Duncan",  passportDetails = "E200000I2", phoneNumber = "35465", discountPercentage = Convert.ToDecimal(0.02)},
            new Customer { firstName = "Peggy",    lastName = "Justice",     middleName = "Daniel",  passportDetails = "W400298U6", phoneNumber = "35465", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Laura",    lastName = "Norman",      middleName = "Blake",   passportDetails = "Q298765R4", phoneNumber = "24534", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Nino",     lastName = "Olivetto",    middleName = "Elisha",  passportDetails = "T454545R5", phoneNumber = "24354", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Kim",      lastName = "Abercrombie", middleName = "Earl",    passportDetails = "T870088U8", phoneNumber = "64738", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Fadi",     lastName = "Fakhouri",    middleName = "Houston", passportDetails = "T622660I8", phoneNumber = "45637", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Candace",  lastName = "Kapoor",      middleName = "Cordell", passportDetails = "Y200020U8", phoneNumber = "93647", discountPercentage = Convert.ToDecimal(0.00)},
            new Customer { firstName = "Roger",    lastName = "Zheng",       middleName = "Braxton", passportDetails = "Y600080I8", phoneNumber = "23456", discountPercentage = Convert.ToDecimal(0.00)},

        };

            customer.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();

            var car = new List<Car>
        {
            new Car { brand = "Audi A3 Sedan", totalValue = 1200000, rentPrice = 3000, isTaken = false},
            new Car { brand = "Audi A4", totalValue = 1200000, rentPrice = 3000, isTaken = false},
            new Car { brand = "Audi Q6", totalValue = 1200000, rentPrice = 3000, isTaken = false},
            new Car { brand = "Audi Q6", totalValue = 1200000, rentPrice = 3000, isTaken = false},
            new Car { brand = "Citroen C4", totalValue = 1200000, rentPrice = 2000, isTaken = false},
            new Car { brand = "Ford Fiesta", totalValue = 1200000, rentPrice = 1500, isTaken = false},
            new Car { brand = "Ford Focus", totalValue = 1200000, rentPrice = 1770, isTaken = false},
            new Car { brand = "Ford Focus", totalValue = 1200000, rentPrice = 2800, isTaken = false},
            new Car { brand = "Ford Kuga", totalValue = 1200000, rentPrice = 3330, isTaken = false},
            new Car { brand = "Kia Cee'd", totalValue = 1200000, rentPrice = 3500, isTaken = false},
            new Car { brand = "Kia Sportage", totalValue = 1200000, rentPrice = 4500, isTaken = false},
            new Car { brand = "Kia Spotrage", totalValue = 1200000, rentPrice = 4500, isTaken = false},
            new Car { brand = "Kia Ruo", totalValue = 1200000, rentPrice = 1400, isTaken = false},
            new Car { brand = "Mazda 3", totalValue = 1200000, rentPrice = 2600, isTaken = false},
            new Car { brand = "Mazda 6", totalValue = 1200000, rentPrice = 2700, isTaken = false},
            new Car { brand = "Mazda 6", totalValue = 1200000, rentPrice = 2740, isTaken = false},
            new Car { brand = "Suzuki SX4", totalValue = 1200000, rentPrice = 2000, isTaken = false},
            new Car { brand = "Suzuki SX4", totalValue = 1200000, rentPrice = 3000, isTaken = false},
            new Car { brand = "Suzuki Jimny", totalValue = 1200000, rentPrice = 2000, isTaken = false},
            new Car { brand = "Suzuki Jimny", totalValue = 1200000, rentPrice = 2000, isTaken = false},
            new Car { brand = "Volkswagen Tiguan", totalValue = 1200000, rentPrice = 3300, isTaken = false},
            new Car { brand = "Volkswagen Polo", totalValue = 1200000, rentPrice = 3400, isTaken = false},
            new Car { brand = "Volkswagen Touareg", totalValue = 1200000, rentPrice = 3600, isTaken = false},
        };

            car.ForEach(s => context.Cars.Add(s));
            context.SaveChanges();
        }
    }
}
