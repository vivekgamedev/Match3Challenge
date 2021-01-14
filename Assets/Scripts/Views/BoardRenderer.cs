using System.Collections.Generic;
using System.Linq;
using Tactile.TactileMatch3Challenge.Model;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents {

	public class BoardRenderer : MonoBehaviour {
		
		[SerializeField] private PieceTypeDatabase pieceTypeDatabase;
		[SerializeField] private VisualPiece visualPiecePrefab;

		private readonly Dictionary<Piece, VisualPiece> pieceMapper = new Dictionary<Piece, VisualPiece>();
		
		private Board board;
		
		public void Initialize(Board board) {
			this.board = board;

			CenterCamera();
			CreateVisualPiecesFromBoardState();
		}

		private void CenterCamera() {
			Camera.main.transform.position = new Vector3((board.Width-1)*0.5f,-(board.Height-1)*0.5f);
		}

		private void CreateVisualPiecesFromBoardState() {
			DestroyVisualPieces();
			
			foreach (var pieceInfo in board.IteratePieces())
			{
				var visualPiece = CreateVisualPiece(pieceInfo.piece);
				visualPiece.transform.localPosition = LogicPosToVisualPos(pieceInfo.pos.x, pieceInfo.pos.y);
			}
		}

		private void CreateVisualPiecesFromResolveResult(ResolveResult result)
		{
			if (result == null)
			{
				return;
			}
			
			foreach (var piece in result.connected.Where(piece => pieceMapper.ContainsKey(piece)))
			{
				Destroy(pieceMapper[piece].gameObject);
				pieceMapper.Remove(piece);
			}
			
			foreach (var change in result.changes)
			{
				var visualPiece = change.Value.WasCreated ? CreateVisualPiece(change.Key) : pieceMapper[change.Key];
				visualPiece.SetTargetPosition(LogicPosToVisualPos(result.changes[change.Key].FromPos), LogicPosToVisualPos(result.changes[change.Key].ToPos));
			}
		}

		public Vector3 LogicPosToVisualPos(float x,float y) { 
			return new Vector3(x, -y, -y);
		}

		public Vector3 LogicPosToVisualPos(BoardPos pos)
		{
			return new Vector3(pos.x, -pos.y, -pos.y);
		}

		private BoardPos ScreenPosToLogicPos(float x, float y) { 
			
			var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x,y,-Camera.main.transform.position.z));
			var boardSpace = transform.InverseTransformPoint(worldPos);

			return new BoardPos() {
				x = Mathf.RoundToInt(boardSpace.x),
				y = -Mathf.RoundToInt(boardSpace.y)
			};

		}

		private VisualPiece CreateVisualPiece(Piece piece) {
			
			var pieceObject = Instantiate(visualPiecePrefab, transform, true);
			pieceMapper[piece] = pieceObject;
			var sprite = pieceTypeDatabase.GetSpriteForPieceType(piece.type);
			pieceObject.SetSprite(sprite);
			return pieceObject;
			
		}

		private void DestroyVisualPieces() {
			foreach (var visualPiece in GetComponentsInChildren<VisualPiece>()) {
				Object.Destroy(visualPiece.gameObject);
			}
			pieceMapper.Clear();
		}

		public void ResetBoard()
		{
			board.RemoveAllPieces();
			DestroyVisualPieces();
		}

		public ResolveResult OnClicked(float x, float y)
		{
			var pos = ScreenPosToLogicPos(x, y);

			if (board.IsWithinBounds(pos.x, pos.y))
			{
				var result = board.Resolve(pos.x, pos.y);
				CreateVisualPiecesFromResolveResult(result);
				return result;
			}

			return null;
		}
	}
}
