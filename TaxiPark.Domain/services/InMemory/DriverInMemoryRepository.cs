using System.Xml.Linq;
using TaxiPark.Domain.Data;
using TaxiPark.Domain.model;
using TaxiPark.Domain.services;

namespace TaxiPark.Domain.Services.InMemory;

public class DriverInMemoryRepository : IDriverRepository
{
    private List<Driver> _drivers;
    private List<Car> _cars;
    private List<DriverCar> _driverCars;
    private List<Trip> _trips;

    public DriverInMemoryRepository()
    {
        _drivers = DataSeeder.Drivers;
        _cars = DataSeeder.Cars;
        _driverCars = DataSeeder.DriverCars;
        _trips = DataSeeder.Trips;
    }

    public Task<Driver> Add(Driver entity)
    {
        _drivers.Add(entity);
        return Task.FromResult(entity);
    }

    public async Task<bool> Delete(int key)
    {
        var driver = await Get(key);
        if (driver != null)
        {
            _drivers.Remove(driver);
            return true;
        }
        return false;
    }

    public Task<Driver?> Get(int key) =>
        Task.FromResult(_drivers.FirstOrDefault(d => d.Id == key));

    public Task<IList<Driver>> GetAll() =>
        Task.FromResult((IList<Driver>)_drivers);

    public Task<(Driver driver, Car car)?> GetDriverWithCar(int driverId)
    {
        var driverCar = _driverCars.FirstOrDefault(dc => dc.DriverId == driverId);
        if (driverCar == null) return Task.FromResult<(Driver, Car)?>(null);

        var driver = _drivers.FirstOrDefault(d => d.Id == driverId);
        var car = _cars.FirstOrDefault(c => c.Id == driverCar.CarId);

        return Task.FromResult(driver != null && car != null ? (driver, car) : null);
    }

    public Task<IList<(Driver driver, int tripCount)>> GetTop5DriversByTripCount()
    {
        var topDrivers = _trips
            .GroupBy(t => t.CarId)
            .Select(g => (new Driver { Id = g.Key }, g.Count()))
            .OrderByDescending(g => g.Item2)
            .Take(5)
            .ToList();

        return Task.FromResult((IList<(Driver, int)>)topDrivers);
    }

    public Task<IList<(Driver driver, int tripCount, double avgTravelTime, int maxTravelTime)>> GetDriverTripStatistics()
    {
        var statistics = _trips
            .GroupBy(t => t.CarId)
            .Select(g => (
                driver: new Driver { Id = g.Key },
                tripCount: g.Count(),
                avgTravelTime: g.Average(t => t.TravelTime ?? 0),
                maxTravelTime: g.Max(t => t.TravelTime ?? 0)))
            .ToList();

        return Task.FromResult((IList<(Driver, int, double, int)>)statistics);
    }
}
