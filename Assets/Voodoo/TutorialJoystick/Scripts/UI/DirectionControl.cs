using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine;


namespace Voodoo.Visual.UI
{
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}
	
	public class DirectionControl : MonoBehaviour
	{
		[Header("Authorized directions")]
		public bool up = true;
		public bool down = true;
		public bool left = true;
		public bool right = true;

		[Header("Starting Direction")]
		public Direction direction;

		[Tooltip("If true, will randomly choose direction among authorized")]
		public bool isRandom;

		[Header("Translation Duration")]
		public float timeTranslation;

		[Header("Translation Distance")]
		public float distanceTranslation = 50f;
		
		
		// Private
		private bool movingOut = true;
		private float timer;
		private float tmpDeltaTime;
		private Vector2 translationGoal;

		private int index;
		private List<Direction> directions;
		private List<Direction> randomDirections;
		
		
		
		void Start()
		{
			directions = new List<Direction>();

			if (up)
			{
				directions.Add(Direction.Up);
			}
			
			if (left)
			{
				directions.Add(Direction.Left);
			}
			
			if (down)
			{
				directions.Add(Direction.Down);
			}

			
			if (right)
			{
				directions.Add(Direction.Right);
			}

			index = 0;
			
			translationGoal = Vector2.zero;

			DefineDirection();

		}
	

		protected void Update()
		{
			if (directions.Count ==0)
			{
				return;
			}
			
			if (tmpDeltaTime > 1)
			{
				if (movingOut == false)
				{
					ChangeDirection();
				}
				
				movingOut = !movingOut;
				
				timer = Time.time;
			}

			tmpDeltaTime = (Time.time - timer) / timeTranslation;
			
			
			if (movingOut)
			{
				transform.localPosition = Vector2.Lerp(Vector2.zero, translationGoal*distanceTranslation, tmpDeltaTime);
			}
			else
			{
				transform.localPosition = Vector2.Lerp(translationGoal*distanceTranslation, Vector2.zero, tmpDeltaTime);
			}


		}


		void ChangeDirection()
		{
			if (isRandom)
			{
				randomDirections = new List<Direction>(directions);

				try
				{
					randomDirections.Remove(direction);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}



				if (randomDirections.Count == 0)
				{
					randomDirections.Add(direction);
				}

				direction = randomDirections[UnityEngine.Random.Range(0, randomDirections.Count)];

			}
			else
			{
				index++;

				if (index > directions.Count - 1)
				{
					index = 0;
				}

				direction = directions[index];

			}

			DefineDirection();

		}

		void DefineDirection()
		{
			switch (direction)
			{
				case Direction.Up:
					translationGoal = Vector2.up;
					break;

				case Direction.Down:
					translationGoal = Vector2.down;
					break;

				case Direction.Left:
					translationGoal = Vector2.left;
					break;

				case Direction.Right:
					translationGoal = Vector2.right;
					break;
			}
		}
		
	}
	
	
	
}

