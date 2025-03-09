using _Scripts.Core.UICore;
using _Scripts.Core.UICore.Page;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.MainMenuScene
{
	public class MainMenuStartPoint : MonoBehaviour
	{
		[Inject]
		private ViewFactory _viewFactory;
		[Inject]
		private PageViewStack _viewStack;

		private void Start()
		{
			var mainMenu = _viewFactory.CreatePage<MainMenuView>();
			_viewStack.OpenView(mainMenu);
		}
	}
}