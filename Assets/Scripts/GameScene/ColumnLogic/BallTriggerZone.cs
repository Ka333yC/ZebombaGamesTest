using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
	public class BallTriggerZone : MonoBehaviour
	{
		public event Action<Ball> OnTriggerEnterEvent;
		public event Action<Ball> OnTriggerExitEvent;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				return;
			}
			
			OnTriggerEnterEvent?.Invoke(ball);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if(!other.gameObject.TryGetComponent<Ball>(out var ball))
			{
				return;
			}
			
			OnTriggerExitEvent?.Invoke(ball);
		}
	}
}