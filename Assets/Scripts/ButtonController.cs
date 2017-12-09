using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	public void change(string scenename)
	{
		SceneManager.LoadScene (scenename);
	}
	public void quitgame()
	{
		Application.Quit ();
	}
}
