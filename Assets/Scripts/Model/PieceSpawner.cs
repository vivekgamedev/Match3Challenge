namespace Tactile.TactileMatch3Challenge.Model {

	public class PieceSpawner : IPieceSpawner {
		
		public int CreateBasicPiece() {
			return UnityEngine.Random.Range(0, 5);
		}

		public int CreatePowerPiece()
		{
			return UnityEngine.Random.Range(5, 7);
		}
	}

}