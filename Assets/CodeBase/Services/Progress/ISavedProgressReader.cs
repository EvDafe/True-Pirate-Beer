using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}