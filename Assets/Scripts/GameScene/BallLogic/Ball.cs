using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Ball : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem _destroyParticle;
		
		private Rigidbody2D _rigidbody;

		public event Action OnMovementStopped;

		[field: SerializeField]
		public BallVariant BallVariant { get; private set; }
		[field: SerializeField]
		public int Score { get; private set; }

		public bool IsMoving { get; private set; }

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			bool isMovingInPreviousFrame = IsMoving;
			IsMoving = _rigidbody.velocity.magnitude > ConstantData.MinorVelocityMagnitude;
			if(isMovingInPreviousFrame && !IsMoving)
			{
				OnMovementStopped?.Invoke();
			}
		}

		public void EnablePhysics(bool enable)
		{
			_rigidbody.isKinematic = !enable;
		}

		public void DestroyBall()
		{
			_destroyParticle.gameObject.transform.parent = null;
			_destroyParticle.gameObject.SetActive(true);
			Destroy(gameObject);
			float totalDuration = _destroyParticle.main.duration + _destroyParticle.main.startLifetime.constantMax;
			Destroy(_destroyParticle.gameObject, totalDuration);
		}
	}
}