using UnityEngine;

namespace DefaultNamespace
{
	public class ColumnZone : MonoBehaviour
	{
		[field: SerializeField]
		public ColumnWithBalls Column { get; private set; }
	}
}