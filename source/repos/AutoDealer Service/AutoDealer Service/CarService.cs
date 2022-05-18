using AutoDealer_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AutoDealer_Service
{
    public class CarService
    {
        private readonly CarRepository carRepository;

        public CarService()
        {
            carRepository = new CarRepository();
        }

        public async Task<List<Car>> GetCars()
        {
            return await carRepository.GetCars();
        }
    }
}