using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour 
{
	[Header("TimeCountDown")]
	public bool IsCountDown;
	public TimeDisplay StartTime,EndTime;
	public Text CountHour,CountMinute,CountSecond;
	public int Hour, Minute, Second,RemainSecond;
	public float PreChangeTime;
	void Start () 
	{
		GetRemainTime ();
	}

	void GetRemainTime()
	{
		int StartSecond = StartTime.Time.Day * 24 * 60 * 60 + StartTime.Time.Hour * 60 * 60 + StartTime.Time.Minute * 60 + StartTime.Time.Second;
		int EndSecond   =   EndTime.Time.Day * 24 * 60 * 60 +   EndTime.Time.Hour * 60 * 60 +   EndTime.Time.Minute * 60 +   EndTime.Time.Second;
		RemainSecond = EndSecond - StartSecond;
		SecondChangeToHMS (RemainSecond);
	}

	void SecondChangeToHMS(int s)
	{
		Hour    = s / 3600;
		s       = s % 3600;
		Minute  = s / 60;
		Second  = s % 60;
	}

	void CountDownUpdate()
	{
		PreChangeTime = Time.time;
		RemainSecond--;
		SecondChangeToHMS (RemainSecond);
		CountHour.text = "" + Hour;
		CountMinute.text = "" + Minute;
		CountSecond.text = "" + Second;
	}

	void Update () 
	{
		if (IsCountDown && Time.time-PreChangeTime>=1f) 
			CountDownUpdate ();
	}
}

