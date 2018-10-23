﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Services.Suggestion
{
    public class FakeSuggestionService : ISuggestionService
    {
        public async Task<ObservableCollection<Models.Suggestion>> GetSuggestionsAsync(double latitude, double longitude)
        {
            if (latitude == 0 || longitude == 0)
            {
                return null;
            }

            await Task.Delay(500);

            return new ObservableCollection<Models.Suggestion>
            {
                new Models.Suggestion { Name = "Солёный циплёнок", Description = "Loren ipsum dolor sit amet, consectetur adipisicing elit.", Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_1.png" : "img_1", Rating = 4, Votes = 81, SuggestionType = Models.SuggestionType.Restaurant, Latitude = 47.5743905f, Longitude = -122.4023376f },
                new Models.Suggestion { Name = "Осенний клуб", Description = "Loren ipsum dolor sit amet, consectetur adipisicing elit.", Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_2.png" : "img_2", Rating = 4, Votes = 66, SuggestionType = Models.SuggestionType.Event, Latitude = 47.5790791f, Longitude = -122.4136163f },
                new Models.Suggestion { Name = "Велосипедный гонщик", Description = "Loren ipsum dolor sit amet, consectetur adipisicing elit.", Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_3.png" : "img_3", Rating = 5, Votes = 22, SuggestionType = Models.SuggestionType.Event, Latitude = 47.5766275f, Longitude = -122.4217906f },
                new Models.Suggestion { Name = "Конференция разработчиков ПО", Description = "Loren ipsum dolor sit amet, consectetur adipisicing elit.", Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_1.png" : "img_1", Rating = 4, Votes = 17, SuggestionType = Models.SuggestionType.Event, Latitude = 47.5743905f, Longitude = -122.4023376f },
                new Models.Suggestion { Name = "Весенний клуб", Description = "Loren ipsum dolor sit amet, consectetur adipisicing elit.", Picture = Device.RuntimePlatform == Device.UWP ? "Assets/img_2.png" : "img_2", Rating = 5, Votes = 132, SuggestionType = Models.SuggestionType.Restaurant, Latitude = 47.5743905f, Longitude = -122.4023376f }
            };
        }
    }
}