using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class ColumnWithBalls : MonoBehaviour
{
	private readonly List<Ball> _ballsInColumn = new List<Ball>();

	[SerializeField]
	private BallTriggerZone _columnTriggerZone;

	public int BallsCount => _ballsInColumn.Count;
	
	public Ball this[int index] => index < _ballsInColumn.Count ? _ballsInColumn[index] : null;

	private void Awake()
	{
		_columnTriggerZone.OnTriggerEnterEvent += OnTriggerZoneEnterEvent;
		_columnTriggerZone.OnTriggerExitEvent += OnTriggerZoneExitEvent;
	}

	public bool HasMovingBallsInColumn()
	{
		foreach(var ball in _ballsInColumn)
		{
			if(ball.IsMoving)
			{
				return true;
			}
		}

		return false;
	}

	public bool HasSpaceForNewBall()
	{
		return _ballsInColumn.Count < ConstantData.FieldSize;
	}

	private void OnTriggerZoneEnterEvent(Ball ball)
	{
		_ballsInColumn.Add(ball);
	}

	private void OnTriggerZoneExitEvent(Ball ball)
	{
		_ballsInColumn.Remove(ball);
	}
}