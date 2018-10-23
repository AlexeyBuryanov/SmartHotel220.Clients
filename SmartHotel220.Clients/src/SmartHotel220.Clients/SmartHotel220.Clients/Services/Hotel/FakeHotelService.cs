using System.Threading.Tasks;
using SmartHotel220.Clients.Core.Models;
using System.Collections.Generic;
using SmartHotel220.Clients.Core.Extensions;
using System.Linq;
using System;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Services.Hotel
{
    public class FakeHotelService : IHotelService
    {
        private static List<City> Cities = new List<City>
        {
            new City
            {
                Id = 1,
                Name = "Барселона",
                Country = "Испания",
            },
            new City
            {
                Id = 2,
                Name = "Севилья",
                Country = "Испания",
            },
            new City
            {
                Id = 3,
                Name = "Сиэтл",
                Country = "США"
            }
        };

        private static List<Models.Hotel> Hotels = new List<Models.Hotel>
        {
            new Models.Hotel
            {
                Id = 1,
                CityId = 1,
                Name = "Отель Secret Camp",
                Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_1.png" : "img_1",
                City = "Барселона, Испания",
                PricePerNight = 76,
                Price = 76, 
                Rating = 3,
                Latitude = 47.612081510010654,
                Longitude = -122.330555830464,
                CheckInTime = "12:00:00",
                CheckOutTime = "15:00:00",
                Services = new List<Service>
                {
                    new Service { Id = 1, Name = "Бесплатный Wi-fi" }
                },
                Rooms = new List<Room>
                {
                    new Room { RoomId = 1, Quantity = 1, RoomName = "Одноместный номер", RoomType = 1 }
                },
                Description = "Отель является правильным выбором для посетителей, которые ищут сочетание очарования и спокойствия. Завтрак шведский стол подается в лаундже на первом этаже, а также в нашем маленьком патио в летние месяцы. В отеле есть интернет и Wi-Fi."
            },
            new Models.Hotel
            {
                Id = 2,
                CityId = 2,
                Name = "Отель Призма",
                Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_2.png" : "img_2",
                City = "Севилья, Испания",
                PricePerNight = 161,
                Price = 161,
                Rating = 3,
                Latitude = 47.612081510010654,
                Longitude = -122.330555830464,
                CheckInTime = "12:00:00",
                CheckOutTime = "15:00:00",
                Services = new List<Service>
                {
                    new Service { Id = 1, Name = "Бесплатный Wi-fi" }
                },
                Rooms = new List<Room>
                {
                    new Room { RoomId = 1, Quantity = 1, RoomName = "Одноместный номер", RoomType = 1 }
                },
                Description = "Отель является правильным выбором для посетителей, которые ищут сочетание очарования и спокойствия. Завтрак шведский стол подается в лаундже на первом этаже, а также в нашем маленьком патио в летние месяцы. В отеле есть интернет и Wi-Fi."
            },
            new Models.Hotel
            {
                Id = 3,
                CityId = 3,
                Name = "Элитный Отель",
                Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_3.png" : "img_3",
                City = "Сиэтл, США",
                PricePerNight = 202,
                Price = 202,
                Rating = 4,
                Latitude = 47.612081510010654,
                Longitude = -122.330555830464,
                CheckInTime = "12:00:00",
                CheckOutTime = "15:00:00",
                Services = new List<Service>
                {
                    new Service { Id = 1, Name = "Бесплатный Wi-fi" }
                },
                Rooms = new List<Room>
                {
                    new Room { RoomId = 1, Quantity = 1, RoomName = "Одноместный номер", RoomType = 1 }
                },
                Description = "Отель является правильным выбором для посетителей, которые ищут сочетание очарования и спокойствия. Завтрак шведский стол подается в лаундже на первом этаже, а также в нашем маленьком патио в летние месяцы. В отеле есть интернет и Wi-Fi."
            }
        };

        private static List<Review> Reviews = new List<Review>
        {
            new Review
            {
                HotelId = 1,
                User = "Алексей Бурьянов",
                Message = "Отличный отель!",
                Room = "Одноместный номер",
                Date = DateTime.Today.AddDays(-1),
                Rating = 3
            },
            new Review
            {
                HotelId = 2,
                User = "Вася Пяточкин",
                Message = "Отличная концепция. Современный, не слишком чистый.",
                Room = "Двухместный номер",
                Date = DateTime.Today.AddDays(-2),
                Rating = 3
            },
            new Review
            {
                HotelId = 3,
                User = "Ирина Шевцова",
                Message = "Модный отель с лучшей лаундж зоной, которую я когда-либо видела. Уютное место с удобными кроватями.",
                Room = "Одноместный номер",
                Date = DateTime.Today.AddDays(-3),
                Rating = 4
            }
        };

        private static List<Service> HotelServices = new List<Service>
        {
            new Service
            {
                Id = 1,
                Name = "Бесплатный Wi-Fi"
            },
            new Service
            {
                Id = 2,
                Name = "Бассейн"
            },
            new Service
            {
                Id = 2,
                Name = "Кондиционер"
            }
        };

        private static List<Service> RoomServices = new List<Service>
        {
            new Service
            {
                Id = 1,
                Name = "Кондиционер"
            },
            new Service
            {
                Id = 2,
                Name = "Чистота и зелень вокруг"
            },
            new Service
            {
                Id = 3,
                Name = "Джакузи"
            }
        };

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            await Task.Delay(500);

            return Cities;
        }

        public async Task<IEnumerable<City>> GetCitiesByNameAsync(string name)
        {
            await Task.Delay(500);

            return Cities;
        }

        public async Task<Models.Hotel> GetHotelByIdAsync(int id)
        {
            await Task.Delay(500);

            return Hotels.FirstOrDefault(h => h.Id == id);
        }

        public async Task<IEnumerable<Models.Hotel>> GetMostVisitedAsync()
        {
            await Task.Delay(500);

            return Hotels;
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync(int id)
        {
            await Task.Delay(500);

            return Reviews.Where(r => r.HotelId == id).ToObservableCollection();
        }

        public async Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId)
        {
            await Task.Delay(500);

            return Hotels
                .Where(h => h.CityId == cityId);
        }

        public async Task<IEnumerable<Models.Hotel>> SearchAsync(int cityId, int rating, int minPrice, int maxPrice)
        {
            await Task.Delay(500);

            return Hotels
                .Where(h => h.CityId == cityId && h.Rating == rating && h.PricePerNight >= minPrice && h.PricePerNight < maxPrice)
                .ToObservableCollection();
        }

        public async Task<IEnumerable<Service>> GetHotelServicesAsync()
        {
            await Task.Delay(500);

            return HotelServices;
        }

        public async Task<IEnumerable<Service>> GetRoomServicesAsync()
        {
            await Task.Delay(500);

            return RoomServices;
        }
    }
}