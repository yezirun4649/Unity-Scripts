using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class AutoSlideIn : MonoBehaviour 
{
	public Vector3 OriginPoint,EndPoint;
	public float SlideTime;
	public GameObject Main;
	public bool TestSwitch;

	public bool[] Status;
	public Image Cover;
	public OOPartChange OOPartChangePart5;

	public Image[] MainImage;
	public GameObject Dialog;
	public float DialogAppearTime;
	void Start () 
	{
		int i;
		Main.transform.DOMove (EndPoint, SlideTime);
		if (Status [1])
			StartCoroutine (AutoFadeOut ());
		else if (Status [2])
			i = 1;
        else
		    StartCoroutine(SlideOut());
	}
	IEnumerator SlideOut()
	{
		yield return new WaitForSeconds (SlideTime*2);
		Main.transform.DOScaleY (0f, SlideTime/5);
		if (Status[0]) 
			StartCoroutine (AutoEnd());
	}
	IEnumerator AutoEnd()
	{
		yield return new WaitForSeconds (SlideTime/5);
		Cover.DOFade (0f, 2f);
		OOPartChangePart5.PartChangeButtonOnClick ();
	}
	IEnumerator AutoFadeOut()
	{
		yield return new WaitForSeconds (1f+SlideTime);
		for (int i=0;i<4;i++)
			MainImage[i].DOFade (0f, SlideTime / 5);
		StartCoroutine (AutoDialogAppear ());
	}
	IEnumerator AutoDialogAppear()
	{
		yield return new WaitForSeconds (SlideTime / 5);
		Dialog.transform.localScale = Vector3.zero;
		Dialog.SetActive (true);
		Dialog.transform.DOScale (new Vector3 (1f, 1f, 1f), DialogAppearTime);
	}
	void Update()
	{
		if (TestSwitch)
		{
			Main.transform.DOMove (EndPoint, 0.01f);
			//TestSwitch = false;
		}
	}

}
//-6 4.25