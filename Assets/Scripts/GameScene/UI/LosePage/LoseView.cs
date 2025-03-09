using System;
using _Scripts.Core.UICore.Page;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI.LosePage
{
	public class LoseView : BasePageView
	{
		[SerializeField]
		private Button _restartButton;
		[SerializeField]
		private Button _toMainMenuButton;
		[SerializeField]
		private TextMeshProUGUI _scoreText;
		
		[Inject]
		private GameController _gameController;

		private void Start()
		{
			_restartButton.onClick.AddListener(() => RestartGame().Forget());
			_toMainMenuButton.onClick.AddListener(() => OpenMainMenu().Forget());
			_scoreText.text = $"Total score: {_gameController.TotalScore}";
		}

		private async UniTask RestartGame()
		{
			await SceneManager.LoadSceneAsync(SceneNames.GameSceneName);
		}

		private async UniTask OpenMainMenu()
		{
			await SceneManager.LoadSceneAsync(SceneNames.MainMenuSceneName);
		}
	}
}