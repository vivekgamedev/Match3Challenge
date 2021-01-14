using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Controller
{
    public interface IGameController
    {
        GameState currentGameState { get; }
        Game currentGame { get; }
        
        void StartGame();
        void RestartGame();
        void HandleInput(float x, float y);
    }
}