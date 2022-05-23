using CV_Ads_WebAPI.Contracts.DTOs.Request;
using CV_Ads_WebAPI.Data;
using CV_Ads_WebAPI.Domain.Constants;
using CV_Ads_WebAPI.Domain.Models;
using CV_Ads_WebAPI.Domain.Options;
using CV_Ads_WebAPI.Services;
using CV_Ads_WebAPI.Services.Interfaces;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class GetAdvertisementByEnvironmentTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly Mock<IFileStorageService> _fileStorageServiceMock;
        private readonly Mock<IStringLocalizer> _stringLocalizerMock;
        private readonly Mock<IOptions<AdvertisementEnvironmentDecisionOptions>> _advertisementEnvironmentDecisionOptionsMock;

        public GetAdvertisementByEnvironmentTests()
        {
            _connection = new InMemorySqliteConnection();
            _connection.Open();

            _fileStorageServiceMock = new Mock<IFileStorageService>();
            _stringLocalizerMock = new Mock<IStringLocalizer>();
            _advertisementEnvironmentDecisionOptionsMock = new Mock<IOptions<AdvertisementEnvironmentDecisionOptions>>();
        }

        [Fact]
        public async Task Should_return_null_If_there_are_no_suitable_advertisements()
        {
            var customer = new Customer("some login", "some password", "some first name", "some last name");
            var ad = new Advertisement
            {
                Id = Guid.NewGuid(),
                Status = AdvertisementStatus.Published,
                Name = "Some name",
                PictureExtension = ".jpeg",
                ViewsLimit = 10,
                CountryScope = "Ukraine",
                CityScope = "Kyiv",
                CustomerId = customer.Id,
                Customer = customer,
                TimePeriodLimits = new List<TimePeriodLimit>() { new TimePeriodLimit(19 * 60, 20 * 60) },
                HumanLimits = new List<HumanLimit>() { new HumanLimit(Gender.Male, 40, 50) },
                AdvertisementViews = new List<AdvertisementView>()
            };

            await using (var seedContext = CreateApplicationContext())
            {
                await seedContext.Database.EnsureCreatedAsync();
                seedContext.Advertisements.Add(ad);
                await seedContext.SaveChangesAsync();
            }

            await using var context = CreateApplicationContext();
            var sut = new AdvertisementService(
                context,
                _fileStorageServiceMock.Object, 
                _stringLocalizerMock.Object, 
                _advertisementEnvironmentDecisionOptionsMock.Object);

            var localTimeInMinutes = 14 * 60 + 20; // 14 hours 20 minutes
            var request = new GetAdvertisementByEnvironmentRequest()
            {
                Country = "Germany",
                City = "Berlin",
                TimeZoneOffset = default,
                Faces = new List<FaceRequest>
                {
                    new FaceRequest()
                    {
                        Gender = Gender.Male,
                        Age = 23
                    },
                    new FaceRequest()
                    {
                        Gender = Gender.Female,
                        Age = 42
                    }
                }

            };

            var adverstisementResponse =
                await sut.GetAdvertisementByEnvironmentAsync(localTimeInMinutes, request, default);

            adverstisementResponse.Should().BeNull();
        }
        
        [Fact]
        public async Task Should_return_the_most_relevant_advertisement_If_there_are_any_suitable_advertisements()
        {
            var smartDevice = new SmartDevice("login", "password");
            var customer = new Customer("some login", "some password", "some first name", "some last name");
            var firstAd = new Advertisement
            {
                Id = Guid.NewGuid(),
                Status = AdvertisementStatus.Published,
                Name = "First ad",
                PictureExtension = ".jpeg",
                ViewsLimit = 10,
                CountryScope = "Ukraine",
                CityScope = "Kyiv",
                CustomerId = customer.Id,
                Customer = customer,
                TimePeriodLimits = new List<TimePeriodLimit>() { new TimePeriodLimit(14 * 60, 20 * 60) },
                HumanLimits = new List<HumanLimit>() { new HumanLimit(Gender.Male, 40, 50) },
                AdvertisementViews = new List<AdvertisementView>()
            };
            var secondAd = new Advertisement
            {
                Id = Guid.NewGuid(),
                Status = AdvertisementStatus.Published,
                Name = "Second ad",
                PictureExtension = ".jpeg",
                ViewsLimit = 10,
                CountryScope = "Ukraine",
                CityScope = "Kyiv",
                CustomerId = customer.Id,
                Customer = customer,
                TimePeriodLimits = new List<TimePeriodLimit>() { new TimePeriodLimit(14 * 60, 20 * 60) },
                HumanLimits = new List<HumanLimit>() { new HumanLimit(Gender.Male, 20, 50) },
                AdvertisementViews = new List<AdvertisementView>()
            };


            await using (var seedContext = CreateApplicationContext())
            {
                await seedContext.Database.EnsureCreatedAsync();
                
                seedContext.SmartDevices.Add(smartDevice);
                seedContext.Advertisements.AddRange(firstAd, secondAd);
                await seedContext.SaveChangesAsync();
            }

            _advertisementEnvironmentDecisionOptionsMock.SetupGet(o => o.Value).Returns(
                new AdvertisementEnvironmentDecisionOptions() { AmountOfTargetAudienceWeight = 0.8f, AmountOfWorkRemainsWeight = 0.2f});
            await using var context = CreateApplicationContext();
            var sut = new AdvertisementService(
                context,
                _fileStorageServiceMock.Object, 
                _stringLocalizerMock.Object, 
                _advertisementEnvironmentDecisionOptionsMock.Object);

            var localTimeInMinutes = 14 * 60 + 20; // 14 hours 20 minutes
            var request = new GetAdvertisementByEnvironmentRequest()
            {
                Country = "Ukraine",
                City = "Kyiv",
                TimeZoneOffset = default,
                Faces = new List<FaceRequest>
                {
                    new FaceRequest()
                    {
                        Gender = Gender.Male,
                        Age = 23
                    },
                    new FaceRequest()
                    {
                        Gender = Gender.Male,
                        Age = 42
                    }
                }

            };
            var adverstisementResponse =
                await sut.GetAdvertisementByEnvironmentAsync(localTimeInMinutes, request, smartDevice.Id);

            adverstisementResponse.Should().NotBeNull();
            adverstisementResponse.Name.Should().BeEquivalentTo(secondAd.Name);
        }

        private ApplicationContext CreateApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(_connection)
                .Options;

            return new ApplicationContext(options);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
