using System;
using _Scripts.Core.UICore.Page;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.MainMenuScene
{
	public class MainMenuView : BasePageView
	{
		[SerializeField]
		private Button _playButton;

		private void Start()
		{
			_playButton.onClick.AddListener(() => OpenGameScene().Forget());
		}

		private async UniTask OpenGameScene()
		{
			await SceneManager.LoadSceneAsync(SceneNames.GameSceneName);
		}
	}
}