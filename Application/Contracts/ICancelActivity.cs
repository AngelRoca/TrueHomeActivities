using Application.Dto;

namespace Application.Contracts
{
    public interface ICancelActivity
    {
        Task<CancelActivityResponse> Cancel(CancelActivityRequest activity);
    }
}
