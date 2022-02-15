using Application.Contracts;
using Application.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.Services
{
    public class ScheduleActivity : IScheduleActivity
    {
        private readonly IPropertiesRespository _propertiesRepo;
        private readonly IActivitiesRespository _activitiesRepo;

        public ScheduleActivity(
            IPropertiesRespository propertiesRepo, 
            IActivitiesRespository activitiesRepo)
        {
            _propertiesRepo = propertiesRepo;
            _activitiesRepo = activitiesRepo;
        }

        public async Task<ScheduledActivity> ScheduleAsync(ScheduleActivityRequest request)
        {
            await ValidatePropertyDisabled(request);
            await ValidateAnotherActivityAtTheSameTimeForThisProperty(request);

            var insertedActivity = await _activitiesRepo.InsertAsync(BuildActivity(request));

            var response = new ScheduledActivity
            {
                ActivityId = insertedActivity.Id.ToString(),
                Success = true
            };

            return response;
        }

        private async Task ValidatePropertyDisabled(ScheduleActivityRequest request)
        {
            var property = await _propertiesRepo.FindByIdAsync(request.PropertyId);

            if (property.Status == PropertyStatus.Disabled || request.Schedule >= property?.DisabledAt)
            {
                throw new PropertyDisabledException();
            }
        }

        private async Task ValidateAnotherActivityAtTheSameTimeForThisProperty(ScheduleActivityRequest request)
        {
            var activities = await _activitiesRepo.FindAllByPropertyIdAsync(request.PropertyId);

            var sameTimeActivity = activities.Any(a =>
                a.PropertyId.Equals(request.PropertyId) &&
                a.Schedule.Year == request.Schedule.Year &&
                a.Schedule.Month == request.Schedule.Month &&
                a.Schedule.Day == request.Schedule.Day &&
                a.Schedule.Hour == request.Schedule.Hour
                );

            if (sameTimeActivity)
            {
                throw new OtherActivityAtTheSameTimeException();
            }
        }

        private Activity BuildActivity(ScheduleActivityRequest request)
        {
            return new Activity
            {
                Id = Guid.NewGuid(),
                PropertyId = request.PropertyId,
                Title = request.Title,
                Schedule = request.Schedule,
                Status = ActivityStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
