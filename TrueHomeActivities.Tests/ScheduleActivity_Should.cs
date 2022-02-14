using Application.Contracts;
using Application.Dto;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TrueHomeActivities.Tests
{
    public class ScheduleActivity_Should
    {
        private readonly Fixture _fixture;

        public ScheduleActivity_Should()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ScheduleAnActivity()
        {
            // Given
            var request = _fixture.Create<ScheduleActivityRequest>();
            request.PropertyId = Guid.NewGuid();
            request.Schedule = DateTime.UtcNow.AddDays(2);

            var activity = _fixture.Create<Activity>();

            var activitiesRepo = Substitute.For<IActivitiesRespository>();
            activitiesRepo.InsertAsync(activity)
                .ReturnsForAnyArgs(activity);

            var propertiesRepo = Substitute.For<IPropertiesRespository>();
            var property = _fixture.Create<Property>();
            property.Status = PropertyStatus.Active;
            property.DisabledAt = DateTime.UtcNow.AddDays(5);

            propertiesRepo
                .FindByIdAsync(request.PropertyId)
                .Returns(property);

            var sut = new ScheduleActivity(propertiesRepo, activitiesRepo);

            // When
            var _ = await sut.ScheduleAsync(request);

            //Then
            await activitiesRepo.ReceivedWithAnyArgs().InsertAsync(activity);
        }

        [Fact]
        public async Task NotScheduleActivityWhenPropertyIsDisabled()
        {
            // Given
            var propertiesRepo = Substitute.For<IPropertiesRespository>();
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var request = _fixture.Create<ScheduleActivityRequest>();
            request.PropertyId = _fixture.Create<Guid>();

            var property = _fixture.Create<Property>();
            property.Status = PropertyStatus.Disabled;

            propertiesRepo
                .FindByIdAsync(request.PropertyId)
                .Returns(property);

            IScheduleActivity sut = new ScheduleActivity(propertiesRepo, activitiesRepo);

            // When
            // Then
            await Assert.ThrowsAsync<PropertyDisabledException>(() => sut.ScheduleAsync(request));
        }

        [Fact]
        public async Task NotScheduleActivityWhenPropertyDisabledAtIsPassed()
        {
            // Given
            var propertiesRepo = Substitute.For<IPropertiesRespository>();
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var request = _fixture.Create<ScheduleActivityRequest>();
            request.PropertyId = _fixture.Create<Guid>();
            request.Schedule = DateTime.UtcNow.AddDays(1);

            var property = _fixture.Create<Property>();
            property.Status = PropertyStatus.Active;
            property.DisabledAt = DateTime.UtcNow.AddDays(-1);

            propertiesRepo
                .FindByIdAsync(request.PropertyId)
                .Returns(property);

            IScheduleActivity sut = new ScheduleActivity(propertiesRepo, activitiesRepo);

            // When
            // Then
            await Assert.ThrowsAsync<PropertyDisabledException>(() => sut.ScheduleAsync(request));
        }

        [Fact]
        public async Task NotScheduleActivityWhenOtherActivityAtTheSameDateAndHour()
        {
            // Given
            var propertiesRepo = Substitute.For<IPropertiesRespository>();
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var request = _fixture.Create<ScheduleActivityRequest>();
            request.PropertyId = _fixture.Create<Guid>();
            request.Schedule = new DateTime(2022, 2, 28, 12, 0, 0);

            var property = _fixture.Create<Property>();
            property.Status = PropertyStatus.Active;
            property.DisabledAt = DateTime.UtcNow.AddYears(1);

            propertiesRepo
                .FindByIdAsync(request.PropertyId)
                .Returns(property);

            var activities = _fixture.CreateMany<Activity>().ToList();
            activities.First().PropertyId = request.PropertyId;
            activities.First().Schedule = new DateTime(2022, 2, 28, 12, 0, 0); ;

            activitiesRepo
                .FindAllByPropertyIdAsync(request.PropertyId)
                .Returns(activities);

            IScheduleActivity sut = new ScheduleActivity(propertiesRepo, activitiesRepo);

            // When
            // Then
            await Assert.ThrowsAsync<OtherActivityAtTheSameTimeException>(() => sut.ScheduleAsync(request));
        }
    }
}