using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}