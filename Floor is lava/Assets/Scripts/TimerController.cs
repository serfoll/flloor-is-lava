using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour

{
	public static TimerController instance;
	[SerializeField]
	private TextMeshProUGUI timeCounterDoor;
	[SerializeField]
	private TextMeshProUGUI timeCounterWall;
    //public Text highScore;

	private TimeSpan timePlaying;
	private bool timerGoing;

	private float elapsedTime;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
        //highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
		timerGoing = false;
	}

	public void BeginTimer()
	{
		timerGoing = true;
		elapsedTime += Time.deltaTime;

		//if (timePlaying < PlayerPrefs.GetInt("HighScore", 0))
		// {
		//PlayerPrefs.SetInt("HighScore", number);
		//}

		DisplayTime(elapsedTime,false);
        //StartCoroutine(UpdateTimer());
	}

	public void EndTimer()
	{
		timerGoing = false;
	}

	void DisplayTime(float _timeToDisplay, bool _miSedondsDisplay)
	{
		float minutes = Mathf.FloorToInt(elapsedTime / 60);
		float seconds = Mathf.FloorToInt(elapsedTime % 60);
		float mSeonds = (_timeToDisplay % 1) * 1000;

		string timerText;

        if (_miSedondsDisplay)
			//Display milliseconds
			timerText = string.Format("{00:00}:{1:00}:{2:000}", minutes, seconds, mSeonds);
		else
			//No milliseconds
			timerText = string.Format("{00:00}:{1:00}", minutes, seconds,mSeonds);

		timeCounterDoor.text = timerText;
		timeCounterWall.text = timerText;
	}
}