using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "BallFactory", menuName = "Ball factory")]
	public class BallFactory : ScriptableObject
	{
		[SerializeField]
		private List<Ball> _ballPrefabs = new List<Ball>();

		public Ball CreateBall(BallVariant ballVariant)
		{
			var ballPrefab = _ballPrefabs.Find((b) => b.BallVariant == ballVariant);
			if(ballPrefab == null)
			{
				throw new ArgumentException("Unknown ball variant");
			}
			
			var result = Instantiate(ballPrefab);
			return result;
		}
	}
}