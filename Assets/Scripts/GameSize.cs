using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSize : MonoBehaviour {
	public Slider w_slider;
	public Slider h_slider;
	void Update(){
		PlayerPrefs.SetInt ("width", (int)w_slider.value);
		PlayerPrefs.SetInt ("height", (int)h_slider.value);
	}
}