using _Scripts.Core.UICore.Animation;
using UnityEngine;

namespace _Scripts.Implementation.UIImplementation.Animations.PageAnimations.Alpha
{
	[CreateAssetMenu(fileName = "AnimationParameters", menuName = "Animation parameters/Page/Alpha animation parameters")]
	public class PageAlphaAnimatorParameters : ScriptableObject
	{
		[field: SerializeField]
		public AlphaAnimationParameter ShowAnimationParameter
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimationParameter MoveOutAnimationParameter
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimationParameter MoveInAnimationParameter
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimationParameter HideAnimationParameter
		{
			get;
			private set;
		}
	}
}