using Application.Dto;
using Application.Exceptions;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace TrueHomeActivities.Tests
{
    public class ReScheduleActivity_Should
    {
        private readonly Fixture _fixture;

        public ReScheduleActivity_Should()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ReScheduleActivity()
        {
            // Given
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var request = _fixture.Create<ReScheduleActivityRequest>();
            var activity = _fixture.Create<Activity>();
            activity.Id = request.ActivityId;
            activity.Status = ActivityStatus.Active;

            activitiesRepo.FindByIdAsync(request.ActivityId)
                .Returns(activity);

            // When
            var sut = new ReScheduleActivity(activitiesRepo);

            // Then
            var _ = await sut.ReScheduleAync(request);

            await activitiesRepo
                .ReceivedWithAnyArgs()
                .UpdateAsync(activity);
        }

        [Fact]
        public async Task NotReScheduleActivityWhenCancelledActivity()
        {
            // Given
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var request = _fixture.Create<ReScheduleActivityRequest>();
            var activity = _fixture.Create<Activity>();
            activity.Id = request.ActivityId;
            activity.Status = ActivityStatus.Cancelled;

            activitiesRepo.FindByIdAsync(request.ActivityId)
                .Returns(activity);

            var sut = new ReScheduleActivity(activitiesRepo);
            
            // When
            // Then
            await Assert.ThrowsAsync<CancelledActivityReScheduleException>(() => sut.ReScheduleAync(request));
        }
    }
}
