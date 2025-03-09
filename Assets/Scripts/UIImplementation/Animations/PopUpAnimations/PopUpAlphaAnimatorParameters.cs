using _Scripts.Core.UICore.Animation;
using UnityEngine;

namespace _Scripts.Implementation.UIImplementation.Animations.PopUpAnimations
{
	[CreateAssetMenu(fileName = "AnimationParameters", menuName = "Animation parameters/Pop up/Alpha animation parameters")]
	public class PopUpAlphaAnimatorParameters : ScriptableObject
	{
		[field: SerializeField]
		public AlphaAnimationParameter ShowAnimationParameter
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