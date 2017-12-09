using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSize : MonoBehaviour {
	public Slider p_slider;
	public void ch()
	{
		int a = (int)p_slider.value;
		switch (a) {
		case 1:
			SceneManager.LoadScene ("play");
			break;

		case 2:
			SceneManager.LoadScene ("play2");
			break;

		case 3:
			SceneManager.LoadScene ("play3");
			break;

		default:
			break;

		}

	}
}