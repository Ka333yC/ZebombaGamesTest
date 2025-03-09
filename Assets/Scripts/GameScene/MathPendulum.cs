using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class MathPendulum : MonoBehaviour
	{
		private readonly RaycastHit2D[] _cachedRaycastHits = new RaycastHit2D[10];
		
		[Header("Pendulum movement")]
		[SerializeField]
		private Vector3 _amplitude;
		[SerializeField]
		private float _duration;
		[SerializeField]
		private AnimationCurve _curve;
		
		[Header("Ball logic")]
		[SerializeField]
		private BallFactory _ballFactory;
		[SerializeField]
		private Transform _ballSpawnPoint;

		private Tween _movementTween;
		private Ball _ballOnPendulum;

		public bool HasBall => _ballOnPendulum != null;

		private void Awake()
		{
			SwingToLeftFromIdle();
		}

		public bool TryDropBall()
		{
			if(_ballOnPendulum == null || !CanDropBall())
			{
				return false;
			}

			_ballOnPendulum.transform.parent = null;
			_ballOnPendulum.EnablePhysics(true);
			_ballOnPendulum = null;
			return true;
		}

		public void CreateBall()
		{
			var randomBallVariant = (BallVariant)Random.Range((int)BallVariant.Variant1, 
				(int)BallVariant.Variant3 + 1);
			var ball = _ballFactory.CreateBall(randomBallVariant);
			ball.EnablePhysics(false);
			ball.transform.position = _ballSpawnPoint.position;
			ball.transform.parent = transform;
			_ballOnPendulum = ball;
		}

		public void StopMovement()
		{
			_movementTween?.Kill();
		}

		private void SwingToLeftFromIdle()
		{
			var destinationRotation = transform.rotation.eulerAngles - _amplitude / 2;
			var rotationAnimation = transform
				.DORotate(destinationRotation, _duration / 2)
				.SetEase(_curve);
			rotationAnimation.onComplete += SwingToRight;
			_movementTween = rotationAnimation;
		}

		private void SwingToLeft()
		{
			var destinationRotation = transform.rotation.eulerAngles - _amplitude;
			var rotationAnimation = transform
				.DORotate(destinationRotation, _duration)
				.SetEase(_curve);
			rotationAnimation.onComplete += SwingToRight;
			_movementTween = rotationAnimation;
		}

		private void SwingToRight()
		{
			var destinationRotation = transform.rotation.eulerAngles + _amplitude;
			var rotationAnimation = transform
				.DORotate(destinationRotation, _duration)
				.SetEase(_curve);
			rotationAnimation.onComplete += SwingToLeft;
			_movementTween = rotationAnimation;
		}

		private bool CanDropBall()
		{
			var hitsCount = Physics2D.CircleCastNonAlloc(_ballSpawnPoint.position, 0.1f, 
				Vector2.zero, _cachedRaycastHits);
			for(int i = 0; i < hitsCount; i++)
			{
				if(_cachedRaycastHits[i].transform.TryGetComponent<ColumnZone>(out var columnZone) && 
				   !columnZone.Column.HasSpaceForNewBall())
				{
					return false;
				}
			}

			return true;
		}
	}
}