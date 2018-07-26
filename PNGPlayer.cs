using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Please set your file name as "PNG_Name_0,PNG_Name_1,PNG_Name_2".
public class PNGPlayer : MonoBehaviour 
{
	[Header("Main")]
	public float PreChangeTime;
	private float SixtyFPS = 1f / 60f;
	private int LoadedNumber = 0;

	public RawImage PNG_Player;
	public bool BeginSwitch;
	public string PNG_Name;
	public int PNG_Number;

	[Header("PNG_Type")]
	public bool IsEnd;
    public GameObject AnimationPlayer;
	public float Delay;

	string LoadName = PNG_Name;

	IEnumerator DelayToOver()
	{
       yield return new WaitForSeconds(Delay);
       AnimationPlayer.SetActive(false);
	}

    void ImagePlayer()
	{
		if (IsReadyForNextFrame()) 
		{
            GetLoadName();
            CheckIsPlayerOver();
			EndOfFrame();
		}
	}

	bool IsReadyForNextFrame()
	{
		return (Time.time - PreChangeTime > SixtyFPS);
	}

	void GetLoadName()
	{
		LoadName = PNG_Name;
		if (LoadedNumber<10)
			LoadName += "0";
		if (LoadedNumber<100)
			LoadName += "0";
		LoadName += LoadedNumber.ToString ();
		LoadedNumber += 1;
	}

	void CheckIsPlayerOver()
	{
		if (LoadedNumber >= PNG_Number) 
		{
			IsEnd = true;
			BeginSwitch = false;
			StartCoroutine(DelayToOver());
			return;
		}
	}
    
    void EndOfFrame()
	{
        PNG_Player.texture = Resources.Load (LoadName) as Texture;
		PreChangeTime = Time.time;
	}

	void Update () 
	{
		if (BeginSwitch) 
		{
            ImagePlayer();
		}
	}
}
