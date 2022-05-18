using AutoDealer_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutoDealer_Service
{
    public class CarRepository
    {
        private static List<Car> cars;

        public CarRepository()
        {
            cars = new List<Car> { new Car { Id = 1, Brand = "BMW", Model = "3 Series", Year = 2020, Price = 45000 } };
        }

        public async Task<List<Car>> GetCars()
        {
            return await Task.Run(() => cars);
        }

        public async Task<Car> GetCar(int id)
        {
            return await Task.Run(() => cars.FirstOrDefault(f => f.Id == id));
        }

        public async void AddCar(Car car)
        {
            cars.Add(car);
        }
        

    }
}