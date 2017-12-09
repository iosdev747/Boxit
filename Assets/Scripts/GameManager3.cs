using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
	// b1r2
	public Text player1_textfield;
	public Text player2_textfield;
	public Text player3_textfield;
	public Text player4_textfield;
	public Text winner_textfield;
	private player_struct playerTurn;
	static int player1_score;
	static int player2_score;
	static int player3_score;
	static int player4_score;
	public Sprite Spirite_playerVertical;
	public Sprite Spirite_playerHorizontal;
	public Sprite Spirite_player2Vertical;
	public Sprite Spirite_player2Horizontal;
	public Sprite Spirite_player3Vertical;
	public Sprite Spirite_player3Horizontal;
	public Sprite Spirite_player4Vertical;
	public Sprite Spirite_player4Horizontal;
	public Sprite Spirite_playerBox;
	public Sprite Spirite_player2Box;
	public Sprite Spirite_player3Box;
	public Sprite Spirite_player4Box;
	int number_of_box;
	bool up;
	bool down;
	bool left;
	bool right;
	bool result;
	public enum player_struct
	{
		player1,
		player2,
		player3,
		player4
	}
	bool Check_Right(int x, int y)
	{
		result = false;
		up = CreateGameBoard.horizontal_line[x,y].CompareTag("is play");
		down = CreateGameBoard.horizontal_line[x, y + 1].CompareTag("is play");
		right = CreateGameBoard.vertical_line[x + 1, y].CompareTag("is play");
		if (up && down && right)
			result = true;
		return (result);
	}

	bool Check_Left(int x, int y)
	{
		result = false;
		up = CreateGameBoard.horizontal_line[x - 1, y].CompareTag("is play");
		down = CreateGameBoard.horizontal_line[x - 1, y + 1].CompareTag("is play");
		left = CreateGameBoard.vertical_line[x - 1, y].CompareTag("is play");
		if (up && down && left)
			result = true;
		return (result);
	}

	bool Check_Top(int x, int y)
	{
		result = false;
		up = CreateGameBoard.horizontal_line[x , y - 1].CompareTag("is play");
		left = CreateGameBoard.vertical_line[x , y - 1].CompareTag("is play");
		right = CreateGameBoard.vertical_line[x + 1, y -1].CompareTag("is play");
		if (up && right && left)
			result = true;
		return (result);
	}

	bool Check_Bottom(int x, int y)
	{
		result = false;
		down = CreateGameBoard.horizontal_line[x, y + 1].CompareTag("is play");
		left = CreateGameBoard.vertical_line[x, y ].CompareTag("is play");
		right = CreateGameBoard.vertical_line[x + 1, y].CompareTag("is play");
		if (down && right && left)
			result = true;
		return (result);
	}

	void Start()
	{
		number_of_box = CreateGameBoard.Height * CreateGameBoard.Width;
		player1_score = 0;
		player2_score = 0;
		player3_score = 0;
		player1_textfield.text = "Blue Player Score: " + player1_score;
		player2_textfield.text = "Red Player Score: " + player2_score;
		player3_textfield.text = "Green Player Score: " + player3_score;
		player4_textfield.text = "Yellow Player Score: " + player4_score;
		if (Random.Range(0f, 1f) <= 0.25f)
			playerTurn = player_struct.player1;
		else if (Random.Range(0f, 1f) > 0.25f && Random.Range(0f, 1f) <= 0.5f)
			playerTurn = player_struct.player2; 
		else if (Random.Range(0f, 1f) > 0.5f && Random.Range(0f, 1f) <= 0.75f)
			playerTurn = player_struct.player3; 
		else
			playerTurn = player_struct.player4; 

	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bool is_empty_vertical = false;
			bool is_empty_horizontal = false;
			bool extra_turn = false;
			float xCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			float yCor = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
			Vector2 origin = new Vector2(xCor, yCor);
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0);
			if (hit.collider != null)
			{
				is_empty_vertical = hit.transform.gameObject.tag.Equals("empty vertical");
				is_empty_horizontal = hit.transform.gameObject.tag.Equals("empty horizontal");
				if (is_empty_vertical || is_empty_horizontal)
				{
					int x_index = 0;
					int y_index = 0;
					for (int y = 0; y <= CreateGameBoard.Height; y++)
						for (int x = 0; x <= CreateGameBoard.Width; x++)
						{
							if (y != CreateGameBoard.Height && hit.transform == CreateGameBoard.vertical_line[x, y])
							{
								x_index = x;
								y_index = y;
							}
							else
								if (x != CreateGameBoard.Width && hit.transform == CreateGameBoard.horizontal_line[x, y])
								{
									x_index = x;
									y_index = y;
								}
						}


					//  P1




					if (this.playerTurn == player_struct.player1)
					{
						if (is_empty_horizontal)
						{
							hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_playerHorizontal;
							hit.transform.tag = "is play";
							if (y_index != 0)
							{
								if (Check_Top(x_index,y_index))
								{
									player1_score++;
									player1_textfield.text = "Blue Player Score: " + player1_score;
									CreateGameBoard.box_transform[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Spirite_playerBox;
									CreateGameBoard.box_transform[x_index, y_index - 1].tag = "mark box";
									extra_turn = true;
								}
							}
							if (y_index != CreateGameBoard.Height)
							{
								if (Check_Bottom(x_index, y_index))
								{
									player1_score++;
									player1_textfield.text = "Blue Player Score: " + player1_score;
									CreateGameBoard.box_transform[x_index, y_index ].GetComponent<SpriteRenderer>().sprite = Spirite_playerBox;
									CreateGameBoard.box_transform[x_index, y_index ].tag = "mark box";
									extra_turn = true;
								}
							}
						}
						else
							if (is_empty_vertical)
							{
								hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_playerVertical;
								hit.transform.tag = "is play";
								if (x_index != 0)
								{
									if (Check_Left(x_index, y_index))
									{
										player1_score++;
										player1_textfield.text = "Blue Player Score: " + player1_score;
										CreateGameBoard.box_transform[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_playerBox;
										CreateGameBoard.box_transform[x_index -1 , y_index].tag = "mark box";
										extra_turn = true;
									}
								}
								if (x_index != CreateGameBoard.Width)
								{
									if (Check_Right(x_index, y_index))
									{
										player1_score++;
										player1_textfield.text = "Blue Player Score: " + player1_score;
										CreateGameBoard.box_transform[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_playerBox;
										CreateGameBoard.box_transform[x_index, y_index].tag = "mark box";
										extra_turn = true;
									}
								}
							}
						if (!extra_turn)
							playerTurn = player_struct.player2;
					}






					//P2





					else if (this.playerTurn == player_struct.player2)
					{
						if (is_empty_horizontal)
						{
							hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player2Horizontal;
							hit.transform.tag = "is play";
							if (y_index != 0)
							{
								if (Check_Top(x_index, y_index))
								{
									player2_score++;
									player2_textfield.text = "Red Player Score: " + player2_score;
									CreateGameBoard.box_transform[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Spirite_player2Box;
									CreateGameBoard.box_transform[x_index, y_index - 1].tag = "mark box";
									extra_turn = true;
								}
							}
							if (y_index != CreateGameBoard.Height)
							{
								if (Check_Bottom(x_index, y_index))
								{
									player2_score++;
									player2_textfield.text = "Red Player Score: " + player2_score;
									CreateGameBoard.box_transform[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player2Box;
									CreateGameBoard.box_transform[x_index, y_index].tag = "mark box";
									extra_turn = true;
								}
							}
						}
						else
							if (is_empty_vertical)
							{
								hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player2Vertical;
								hit.transform.tag = "is play";
								if (x_index != 0)
								{
									if (Check_Left(x_index, y_index))
									{
										player2_score++;
										player2_textfield.text = "Red Player Score: " + player2_score;
										CreateGameBoard.box_transform[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player2Box;
										CreateGameBoard.box_transform[x_index - 1, y_index].tag = "mark box";
										extra_turn = true;
									}
								}
								if (x_index != CreateGameBoard.Width)
								{
									if (Check_Right(x_index, y_index))
									{
										player2_score++;
										player2_textfield.text = "Red Player Score: " + player2_score;
										CreateGameBoard.box_transform[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player2Box;
										CreateGameBoard.box_transform[x_index, y_index].tag = "mark box";
										extra_turn = true;
									}
								}
							}
						if (!extra_turn)
							playerTurn = player_struct.player3;
					}




					//P3




					else if (this.playerTurn == player_struct.player3)
					{
						if (is_empty_horizontal)
						{
							hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player3Horizontal;
							hit.transform.tag = "is play";
							if (y_index != 0)
							{
								if (Check_Top(x_index,y_index))
								{
									player3_score++;
									player3_textfield.text = "Green Player Score: " + player3_score;
									CreateGameBoard.box_transform[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Spirite_player3Box;
									CreateGameBoard.box_transform[x_index, y_index - 1].tag = "mark box";
									extra_turn = true;
								}
							}
							if (y_index != CreateGameBoard.Height)
							{
								if (Check_Bottom(x_index, y_index))
								{
									player3_score++;
									player3_textfield.text = "Green Player Score: " + player3_score;
									CreateGameBoard.box_transform[x_index, y_index ].GetComponent<SpriteRenderer>().sprite = Spirite_player3Box;
									CreateGameBoard.box_transform[x_index, y_index ].tag = "mark box";
									extra_turn = true;
								}
							}
						}
						else
							if (is_empty_vertical)
							{
								hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player3Vertical;
								hit.transform.tag = "is play";
								if (x_index != 0)
								{
									if (Check_Left(x_index, y_index))
									{
										player3_score++;
										player3_textfield.text = "Green Player Score: " + player3_score;
										CreateGameBoard.box_transform[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player3Box;
										CreateGameBoard.box_transform[x_index -1 , y_index].tag = "mark box";
										extra_turn = true;
									}
								}
								if (x_index != CreateGameBoard.Width)
								{
									if (Check_Right(x_index, y_index))
									{
										player3_score++;
										player3_textfield.text = "Green Player Score: " + player3_score;
										CreateGameBoard.box_transform[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player3Box;
										CreateGameBoard.box_transform[x_index, y_index].tag = "mark box";
										extra_turn = true;
									}
								}
							}
						if (!extra_turn)
							playerTurn = player_struct.player4;
					}







					//P4







					else if (this.playerTurn == player_struct.player4)
					{
						if (is_empty_horizontal)
						{
							hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player4Horizontal;
							hit.transform.tag = "is play";
							if (y_index != 0)
							{
								if (Check_Top(x_index,y_index))
								{
									player4_score++;
									player4_textfield.text = "Yellow Player Score: " + player4_score;
									CreateGameBoard.box_transform[x_index, y_index - 1].GetComponent<SpriteRenderer>().sprite = Spirite_player4Box;
									CreateGameBoard.box_transform[x_index, y_index - 1].tag = "mark box";
									extra_turn = true;
								}
							}
							if (y_index != CreateGameBoard.Height)
							{
								if (Check_Bottom(x_index, y_index))
								{
									player4_score++;
									player4_textfield.text = "Yellow Player Score: " + player4_score;
									CreateGameBoard.box_transform[x_index, y_index ].GetComponent<SpriteRenderer>().sprite = Spirite_player4Box;
									CreateGameBoard.box_transform[x_index, y_index ].tag = "mark box";
									extra_turn = true;
								}
							}
						}
						else
							if (is_empty_vertical)
							{
								hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = Spirite_player4Vertical;
								hit.transform.tag = "is play";
								if (x_index != 0)
								{
									if (Check_Left(x_index, y_index))
									{
										player4_score++;
										player4_textfield.text = "Yellow Player Score: " + player4_score;
										CreateGameBoard.box_transform[x_index - 1, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player4Box;
										CreateGameBoard.box_transform[x_index -1 , y_index].tag = "mark box";
										extra_turn = true;
									}
								}
								if (x_index != CreateGameBoard.Width)
								{
									if (Check_Right(x_index, y_index))
									{
										player4_score++;
										player4_textfield.text = "Yellow Player Score: " + player4_score;
										CreateGameBoard.box_transform[x_index, y_index].GetComponent<SpriteRenderer>().sprite = Spirite_player4Box;
										CreateGameBoard.box_transform[x_index, y_index].tag = "mark box";
										extra_turn = true;
									}
								}
							}
						if (!extra_turn)
							playerTurn = player_struct.player1;
					}









				}
			}
		}
		if (player1_score + player2_score + player3_score + player4_score == number_of_box) {
			if ((player1_score == player2_score) && (player1_score == player3_score) && (player1_score == player4_score))
				winner_textfield.text = "GAME DRAW";
			else {
				if ((player1_score > player2_score) && (player1_score > player3_score) && (player1_score > player4_score))
					winner_textfield.text = "BLUE PLAYER WIN";
				else if ((player2_score > player1_score) && (player2_score > player3_score) && (player2_score > player4_score))
					winner_textfield.text = "RED PLAYER WIN";
				else if ((player3_score > player1_score) && (player3_score > player2_score) && (player3_score > player4_score))
					winner_textfield.text = "GREEN PLAYER WIN";
				else 
					winner_textfield.text = "YELLOW PLAYER WIN";
			}
		}
	}
}