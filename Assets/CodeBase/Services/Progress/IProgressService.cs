using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public interface IProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}