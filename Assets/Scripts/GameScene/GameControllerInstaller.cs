using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
	public class GameControllerInstaller : MonoInstaller
	{
		[SerializeField]
		private GameController _gameController;
		
		public override void InstallBindings()
		{
			Container
				.Bind<GameController>()
				.FromInstance(_gameController)
				.AsSingle();
		}
	}
}