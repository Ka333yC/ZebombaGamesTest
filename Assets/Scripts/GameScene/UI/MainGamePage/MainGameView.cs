using System;
using _Scripts.Core.UICore.Page;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI.MainGamePage
{
	public class MainGameView : BasePageView
	{
		[SerializeField]
		private TextMeshProUGUI _scoreText;
		[SerializeField]
		private Button _dropBallButton;
		
		[Inject]
		private GameController _gameController;
		
		private void Start()
		{
			ChangeTotalScore(_gameController.TotalScore);
			_gameController.OnTotalScoreChanged += ChangeTotalScore;
			
			_dropBallButton.onClick.AddListener(_gameController.DropBall);
		}

		private void ChangeTotalScore(int score)
		{
			_scoreText.text = $"Total score: {score}";
		}
	}
}