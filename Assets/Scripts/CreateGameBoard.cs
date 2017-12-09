using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CreateGameBoard : MonoBehaviour
{
	public Transform vertical;
	public static Transform[,] vertical_line;
	public Transform horizontal;
	public static Transform[,] horizontal_line;
	public Transform box;
	public static Transform[,] box_transform;
	public static int Width;
	public static int Height;
	public Camera mainCam;


void Start()
{
	Width = PlayerPrefs.GetInt("width");
	Height = PlayerPrefs.GetInt("height");
	CreateBoard(Width,Height);
}

void CreateBoard (int Width, int Height)
	{
		float vertical_Width = vertical.GetComponent<SpriteRenderer> ().bounds.size.x;
		float vertical_Height = vertical.GetComponent<SpriteRenderer> ().bounds.size.y;	
		vertical_line = new Transform[Width + 1, Height];

		float horizontal_Width = horizontal.GetComponent<SpriteRenderer> ().bounds.size.x;
		float horizontal_Height = horizontal.GetComponent<SpriteRenderer> ().bounds.size.y;
		horizontal_line = new Transform[Width, Height + 1];

		float box_Width = box.GetComponent<SpriteRenderer> ().bounds.size.x;
		float box_Height = box.GetComponent<SpriteRenderer> ().bounds.size.y;

		box_transform = new Transform[Width, Height];

		float COORD_Y = mainCam.ScreenToWorldPoint (new Vector3 (0, Screen.height / 2 , 0f)).y;
		COORD_Y = COORD_Y + (Height * vertical_Height + (Height + 1) * horizontal_Height) / 2;

		for (int y = 0; y <= Height; y++) {
			float COORD_X = mainCam.ScreenToWorldPoint (new Vector3 (Screen.width / 2, 0 , 0f)).x;
			COORD_X = COORD_X - (Width * horizontal_Width + (Width + 1) * vertical_Width) / 2;

			for (int x = 0; x <= Width; x++) {
				Vector3 center_of_Box = new Vector3 (COORD_X, COORD_Y, 0f);
				Vector3 center_of_Vertical = new Vector3 (COORD_X - horizontal_Width / 2 - vertical_Width / 2, COORD_Y, 0f);
				Vector3 center_of_Horizontal = new Vector3 (COORD_X, COORD_Y + horizontal_Height / 2 + vertical_Height / 2, 0f);

				if (x != Width && y != Height)
					box_transform [x, y] = Instantiate (box, center_of_Box, Quaternion.identity) as Transform;
				if (y != Height)
					vertical_line [x, y] = Instantiate (vertical, center_of_Vertical, Quaternion.identity) as Transform;
				if (x != Width)
					horizontal_line [x, y] = Instantiate (horizontal, center_of_Horizontal, Quaternion.identity) as Transform;
				COORD_X += box_Width + vertical_Width;
			}
			COORD_Y -= box_Height + horizontal_Height;
		}
	}
}