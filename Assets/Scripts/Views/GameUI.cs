using Tactile.TactileMatch3Challenge.Model;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _topPanel;
        
        [SerializeField] 
        private TMP_Text _movesText;

        [SerializeField] 
        private Image _targetImage;
        
        [SerializeField] 
        private TMP_Text _targetText;

        [SerializeField]
        private RectTransform _gameOverPanel;
        
        [SerializeField] 
        private TMP_Text _gameOverText;

        private PieceTypeDatabase _pieceTypeDatabase;

        public void Initialize(PieceTypeDatabase pieceTypeDatabase)
        {
            _pieceTypeDatabase = pieceTypeDatabase;
            _topPanel.gameObject.SetActive(true);
            _gameOverPanel.gameObject.SetActive(false);
        }
        
        public void UpdateUI(Game game)
        {
            _movesText.text = $"Moves Used : {game.MovesUsed}/{game.LevelObjective.MaximumMoves}";
            _targetImage.sprite = _pieceTypeDatabase.GetSpriteForPieceType(game.LevelObjective.Type);
            _targetText.text = $"{game.TargetAchieved}/{game.LevelObjective.Target}";
        }
        
        public void ChangeGameOverState(bool isGameOver)
        {
            _topPanel.gameObject.SetActive(!isGameOver);
            _gameOverPanel.gameObject.SetActive(isGameOver);
        }

        public void UpdateGameOverText(string gameOverText)
        {
            _gameOverText.text = gameOverText;
        }
    }
}
