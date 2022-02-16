using Application.Contracts;
using Application.Dto;
using Application.Services;
using Application.Services.ListActivitiesFilters;
using AutoFixture;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TrueHomeActivities.Tests
{
    public class ListActivities_Should
    {
        private readonly Fixture _fixture;

        public ListActivities_Should()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ReturnAListOfActivities()
        {
            // Given
            var activities = _fixture.CreateMany<Activity>().ToList();

            var filter = new DefaultListActivityFilter();

            var activitiesRepo = Substitute.For<IActivitiesRespository>();
            activitiesRepo.FindAllByPredicateAsync(filter.Predicate)
                .ReturnsForAnyArgs(activities);

            var request = _fixture.Create<ListActiviesRequest>();
            var sut = new ListActivities(activitiesRepo);

            // When
            var _ = await sut.GetListAsync(request);

            // Then
            await activitiesRepo
                .ReceivedWithAnyArgs()
                .FindAllByPredicateAsync(filter.Predicate);
        }

        //[Fact]
        public async Task ReturnAListOfActivitiesFilteredByStatus()
        {
            // Given
            var activities = _fixture.CreateMany<Activity>().ToList();

            var request = _fixture.Create<ListActiviesRequest>();
            request.StartDate = null;
            request.EndDate = null;
            request.Status = ActivityStatus.Done;

            IListActivityFilter filter = new StatusListActivityFilter(request.Status.Value);

            var activitiesRepo = Substitute.For<IActivitiesRespository>();
            activitiesRepo.FindAllByPredicateAsync(filter.Predicate)
                .ReturnsForAnyArgs(activities);

            var sut = new ListActivities(activitiesRepo);

            // When
            var _ = await sut.GetListAsync(request);

            // Then
            await activitiesRepo
                .Received()
                .FindAllByPredicateAsync(filter.Predicate);
        }

        //[Fact]
        public async Task ReturnAListOfActivitiesFilteredByDateRange()
        {
            // Given
            var activities = _fixture.CreateMany<Activity>().ToList();

            var request = _fixture.Create<ListActiviesRequest>();
            request.StartDate = DateTime.Now.AddDays(-2);
            request.EndDate = DateTime.Now.AddDays(7);
            request.Status = null;

            var filter = ListActivityFilterFactory.GetFilter(request);

            var activitiesRepo = Substitute.For<IActivitiesRespository>();
            activitiesRepo.FindAllByPredicateAsync(filter.Predicate)
                .Returns(activities);

            var sut = new ListActivities(activitiesRepo);

            // When
            var _ = await sut.GetListAsync(request);

            // Then
            await activitiesRepo
                .Received()
                .FindAllByPredicateAsync(filter.Predicate);
        }
    }
}
