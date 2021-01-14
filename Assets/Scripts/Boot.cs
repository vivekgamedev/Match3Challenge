using Tactile.TactileMatch3Challenge.Controller;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge {
	
	public class Boot : MonoBehaviour {
		
		[SerializeField] private GameController _gameController;
		
		void Start () 
		{
			_gameController.StartGame();
		}
	}
}
