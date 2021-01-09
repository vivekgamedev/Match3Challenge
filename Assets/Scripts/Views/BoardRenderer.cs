using Tactile.TactileMatch3Challenge.Model;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents {

	public class BoardRenderer : MonoBehaviour {
		
		[SerializeField] private PieceTypeDatabase pieceTypeDatabase;
		[SerializeField] private VisualPiece visualPiecePrefab;
		
		private Board board;
		
		public void Initialize(Board board) {
			this.board = board;

			CenterCamera();
			CreateVisualPiecesFromBoardState(null);
		}

		private void CenterCamera() {
			Camera.main.transform.position = new Vector3((board.Width-1)*0.5f,-(board.Height-1)*0.5f);
		}

		private void CreateVisualPiecesFromBoardState(ResolveResult result) {
			DestroyVisualPieces();

			foreach (var pieceInfo in board.IteratePieces())
			{
				var visualPiece = CreateVisualPiece(pieceInfo.piece);

				if (result != null && result.changes.ContainsKey(pieceInfo.piece))
				{
					visualPiece.SetTargetPosition(LogicPosToVisualPos(result.changes[pieceInfo.piece].FromPos), LogicPosToVisualPos(result.changes[pieceInfo.piece].ToPos));
				}
				else
				{
					visualPiece.transform.localPosition = LogicPosToVisualPos(pieceInfo.pos.x, pieceInfo.pos.y);
				}
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
			var sprite = pieceTypeDatabase.GetSpriteForPieceType(piece.type);
			pieceObject.SetSprite(sprite);
			return pieceObject;
			
		}

		private void DestroyVisualPieces() {
			foreach (var visualPiece in GetComponentsInChildren<VisualPiece>()) {
				Object.Destroy(visualPiece.gameObject);
			}
		}

		private void Update() {
			
			if (Input.GetMouseButtonDown(0)) {

				var pos = ScreenPosToLogicPos(Input.mousePosition.x, Input.mousePosition.y);

				if (board.IsWithinBounds(pos.x, pos.y)) {
					var result = board.Resolve(pos.x, pos.y);
					CreateVisualPiecesFromBoardState(result);
				}
			}
		}
		
	}

}
