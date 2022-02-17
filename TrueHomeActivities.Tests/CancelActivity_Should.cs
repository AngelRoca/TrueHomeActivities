using Application.Contracts;
using Application.Dto;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace TrueHomeActivities.Tests
{
    public class CancelActivity_Should
    {
        private readonly Fixture _fixture;

        public CancelActivity_Should()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CancelActivity()
        {
            // Given
            var activitiesRepo = Substitute.For<IActivitiesRespository>();

            var activity = _fixture.Create<Activity>();
            activity.Status = ActivityStatus.Active;

            var request = _fixture.Create<CancelActivityRequest>();

            activitiesRepo
                .FindByIdAsync(request.ActivityId)
                .Returns(activity);

            // When
            ICancelActivity sut = new CancelActivity(activitiesRepo);

            var _ = await sut.CancelAsync(request);

            // Then
            await activitiesRepo
                .ReceivedWithAnyArgs()
                .UpdateAsync(activity);
        }
    }
}
