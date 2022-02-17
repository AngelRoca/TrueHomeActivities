using Application.Dto;

namespace Application.Contracts
{
    public interface ICancelActivity
    {
        Task<CancelActivityResponse> CancelAsync(CancelActivityRequest activity);
    }
}
