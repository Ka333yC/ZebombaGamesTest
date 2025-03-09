using System;
using System.Linq;
using _Scripts.Core.UICore;
using _Scripts.Core.UICore.Page;
using DefaultNamespace.UI.LosePage;
using DefaultNamespace.UI.MainGamePage;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DefaultNamespace
{
	public class GameController : MonoBehaviour
	{
		[SerializeField]
		private ColumnsManager _columnsManager;
		[SerializeField]
		private MathPendulum _pendulum;
		
		[Inject]
		private ViewFactory _viewFactory;
		[Inject]
		private PageViewStack _viewStack;
		
		private int _totalScore;
		
		public event Action<int> OnTotalScoreChanged;

		public int TotalScore
		{
			get => _totalScore;

			private set
			{
				_totalScore = value;
				OnTotalScoreChanged?.Invoke(_totalScore);
			}
		}

		private void Awake()
		{
			_pendulum.CreateBall();
			_columnsManager.OnBallMovementStopped += BallMovementStopped;
			_columnsManager.OnBallMovementStopped += CheckIsGameOver;
		}

		private void Start()
		{
			var mainGameView = _viewFactory.CreatePage<MainGameView>();
			_viewStack.OpenView(mainGameView);
		}

		public void DropBall()
		{
			_pendulum.TryDropBall();
		}

		private void BallMovementStopped()
		{
			if(!_pendulum.HasBall)
			{
				_pendulum.CreateBall();
			}

			DestroyMatch();
			CheckIsGameOver();
		}

		private void DestroyMatch()
		{
			if(!_columnsManager.FindMatch(out var matchPositions))
			{
				return;
			}

			foreach(var matchPosition in matchPositions.Reverse())
			{
				var ball = _columnsManager[matchPosition.x][matchPosition.y];
				TotalScore += ball.Score;
				ball.DestroyBall();
			}
		}

		private void CheckIsGameOver()
		{
			bool isGameOver = true;
			for(int i = 0; i < ConstantData.FieldSize; i++)
			{
				if(_columnsManager[i].BallsCount != ConstantData.FieldSize)
				{
					isGameOver = false;
					break;
				}
			}

			if(isGameOver)
			{
				_pendulum.StopMovement();
				var loseView = _viewFactory.CreatePage<LoseView>();
				_viewStack.OpenView(loseView);
			}
		}
	}
}