using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Controller
{
    public class GameController : MonoBehaviour, IGameController
    {
        #region Views
        
        [SerializeField] 
        private GameUI _gameUI;
        
        [SerializeField]
        private BoardRenderer _boardRenderer;
        #endregion

        #region Models
        
        [SerializeField]
        private PieceTypeDatabase _pieceTypeDatabase;

        public GameState currentGameState { get; private set; }
        public Game currentGame { get; private set; }

        private readonly int[,] boardDefinition = {
            {3, 3, 1, 2, 3, 3},
            {2, 2, 1, 2, 3, 3},
            {1, 1, 0, 0, 2, 2},
            {2, 2, 0, 0, 1, 1},
            {1, 1, 2, 2, 1, 1},
            {1, 1, 2, 2, 1, 1},
        };
        
        #endregion

        public void StartGame()
        {
            currentGame = new Game();
            currentGame.Initialize(LevelObjective.CreateDefaultLevel());
            _gameUI.Initialize(_pieceTypeDatabase);
            _gameUI.UpdateUI(currentGame);
            CreateBoard();
        }

        public void RestartGame()
        {
            currentGame.ResetLevel();
            _gameUI.UpdateUI(currentGame);
            _gameUI.ChangeGameOverState(false);
            CreateBoard();
        }

        public void HandleInput(float x, float y)
        {
            var result = _boardRenderer.OnClicked(x, y);
            if (result == null || result.connected.Count <= 1)
            {
                return;
            }
            
            currentGame.IncrementMoveCount();

            foreach (var piece in result.connected)
            {
                if (piece.type == currentGame.LevelObjective.Type)
                {
                    currentGame.IncrementTargetAchieved();
                }
            }

            if (currentGame.TargetAchieved >= currentGame.LevelObjective.Target || 
                currentGame.MovesUsed >= currentGame.LevelObjective.MaximumMoves)
            {
                currentGameState = GameState.GameOver;
                _gameUI.ChangeGameOverState(true);
                _boardRenderer.ResetBoard();
                var gameOverText = currentGame.TargetAchieved >= currentGame.LevelObjective.Target ? "You Won! Game Over!" : "You Lost! Game Over!";
                _gameUI.UpdateGameOverText(gameOverText);
            }
            else
            {
                _gameUI.UpdateUI(currentGame);
            }
        }

        private void CreateBoard()
        {
            currentGameState = GameState.InGame;
            var pieceSpawner = new PieceSpawner();
            var board = Board.Create(boardDefinition, pieceSpawner);
            _boardRenderer.Initialize(board);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || currentGameState != GameState.InGame)
            {
                return;
            }
            
            HandleInput(Input.mousePosition.x, Input.mousePosition.y);
        }
    }    
}

