using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class ColumnsManager : MonoBehaviour
	{
		[SerializeField]
		private ColumnWithBalls[] _columns = new ColumnWithBalls[ConstantData.FieldSize];

		private bool _hasMovingBallsInPreviousFrame = false;
		
		public event Action OnBallMovementStopped;
		
		public ColumnWithBalls this[int index] => index < _columns.Length ? _columns[index] : null;

		private void OnValidate()
		{
			if(_columns.Length != ConstantData.FieldSize)
			{
				Array.Resize(ref _columns, ConstantData.FieldSize);
			}
		}

		private void FixedUpdate()
		{
			var hasMovingBalls = HasMovingBallsInColumns();
			// Если движение прекратилось
			if(!hasMovingBalls && _hasMovingBallsInPreviousFrame)
			{
				OnBallMovementStopped?.Invoke();
			}

			_hasMovingBallsInPreviousFrame = hasMovingBalls;
		}

		public bool HasMovingBallsInColumns()
		{
			foreach(var column in _columns)
			{
				if(column.HasMovingBallsInColumn())
				{
					return true;
				}
			}

			return false;
		}

		public bool FindMatch(out Vector2Int[] matchPositions)
		{
			matchPositions = new Vector2Int[ConstantData.FieldSize];
			return FindMatchInColumns(ref matchPositions) ||
				FindMatchInRows(ref matchPositions) ||
				FindMatchInDownToUpDiagonal(ref matchPositions) ||
				FindMatchInUpToDownDiagonal(ref matchPositions);
		}

		private bool FindMatchInColumns(ref Vector2Int[] matches)
		{
			for(int i = 0; i < ConstantData.FieldSize; i++)
			{
				var firstBall = _columns[i][0];
				if(firstBall == null)
				{
					continue;
				}
				
				bool hasMatch = true;
				for(int j = 0; j < ConstantData.FieldSize; j++)
				{
					var secondBall = _columns[i][j];
					if(secondBall == null || firstBall.BallVariant != secondBall.BallVariant)
					{
						hasMatch = false;
						break;
					}
					
					matches[j] = new Vector2Int(i, j);
				}

				if(hasMatch)
				{
					return true;
				}
			}
			
			return false;
		}

		private bool FindMatchInRows(ref Vector2Int[] matches)
		{
			for(int i = 0; i < ConstantData.FieldSize; i++)
			{
				var firstBall = _columns[0][i];
				if(firstBall == null)
				{
					continue;
				}

				bool hasMatch = true;
				for(int j = 0; j < ConstantData.FieldSize; j++)
				{
					var secondBall = _columns[j][i];
					if(secondBall == null || firstBall.BallVariant != secondBall.BallVariant)
					{
						hasMatch = false;
						break;
					}
					
					matches[j] = new Vector2Int(j, i);
				}

				if(hasMatch)
				{
					return true;
				}
			}
			
			return false;
		}

		private bool FindMatchInDownToUpDiagonal(ref Vector2Int[] matches)
		{
			var firstBall = _columns[0][0];
			if(firstBall == null)
			{
				return false;
			}

			bool hasMatch = true;
			for(int i = 0, j = 0; i < ConstantData.FieldSize && j < ConstantData.FieldSize; i++, j++)
			{
				var secondBall = _columns[i][j];
				if(secondBall == null || firstBall.BallVariant != secondBall.BallVariant)
				{
					hasMatch = false;
					break;
				}
				
				matches[i] = new Vector2Int(i, j);
			}
			
			return hasMatch;
		}

		private bool FindMatchInUpToDownDiagonal(ref Vector2Int[] matches)
		{
			var firstBall = _columns[0][ConstantData.FieldSize - 1];
			if(firstBall == null)
			{
				return false;
			}

			bool hasMatch = true;
			for(int i = 0, j = ConstantData.FieldSize - 1; i < ConstantData.FieldSize && j >= 0; i++, j--)
			{
				var secondBall = _columns[i][j];
				if(secondBall == null || firstBall.BallVariant != secondBall.BallVariant)
				{
					hasMatch = false;
					break;
				}
				
				matches[i] = new Vector2Int(i, j);
			}
			
			return hasMatch;
		}
	}
}