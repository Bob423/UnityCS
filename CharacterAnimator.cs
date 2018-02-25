using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

	public Direction direction;
	private const int ROWS = 4;
	public int numColumns; // The number of sprites in a row
	public int frameDelay; // The framerate of this object
	public bool notIdle;

	int countFrames;
	int currentSprite;
	Sprite[] sprites;
	SpriteRenderer spriteRenderer;
	string spriteSheetName;

	// Use this for initialization
	void Start () {
		// Get sprite renderer object from GameObject
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		// Get name of sprite sheet from current sprite's name.
		spriteSheetName = spriteRenderer.sprite.name.Trim(new System.Char[] {'_', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

		// Load all sprites in sprite sheet into an array.
		sprites = Resources.LoadAll<Sprite>(spriteSheetName);

		countFrames = 0;
		currentSprite = 0;
	}
	
	// Update is called once per frame
	void Update () {


		// Check Direction
		if(notIdle) {

			if(countFrames == frameDelay) {
				switch(direction) {

					case Direction.Down:
						animate(0);
						break;
					case Direction.Left:
						animate(1);
						break;
					case Direction.Right:
						animate(2);
						break;
					case Direction.Up:
						animate(3);
						break;
				}

				countFrames = 0;
			}

			countFrames++;
		} else {
			countFrames = 0;
			currentSprite = 0;

			// Get idle sprites
			switch(direction) {

				case Direction.Down:
					spriteRenderer.sprite = sprites[0];
					break;
				case Direction.Left:
					spriteRenderer.sprite = sprites[numColumns];
					break;
				case Direction.Right:
					spriteRenderer.sprite = sprites[numColumns * 2];
					break;
				case Direction.Up:
					spriteRenderer.sprite = sprites[numColumns * 3];
					break;
			}
		}

	}

	// Move to next frame
	public void animate(int row) {
		currentSprite++;

		// If sprite has reached the end of the row
		if(currentSprite >= (row + 1) * numColumns) {
			currentSprite = row * numColumns;
		}

		// If sprite is on a previous row.
		if(currentSprite < row * numColumns) {
			currentSprite = row * numColumns;
		}

		spriteRenderer.sprite = sprites[currentSprite];
	}

	public enum Direction {
		Down, Left, Right, Up
	}

}
