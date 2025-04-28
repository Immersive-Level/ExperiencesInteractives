
    public interface IMiniGame
    {
        void StartGame();
        void EndGame();
        string GetGameName();
    }

namespace SlotMachine.Core
{
    public interface ISlotMachine
    {
        void Initialize();
        void Spin();
        void Stop();
        bool CheckWin();
        int CurrentCredits { get; }
        void AddCredits(int amount);
    }
}

