//----------------------------------------------------------
//File:            SliderMemberGradient.cs
//Name:            Ye Zirun
//Last Update Date:2018/01/15
//Desc:            A sub Function of Slider Banner.
//ToDo:            Without cover.png ,use shader or something else to make a cover.
//BugList:         2018/01/15 Cover Render Order Bug Fixed.
//----------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SliderMemberGradient : MonoBehaviour 
{
	public SliderBanner Other;
	public bool FadeSwitch,CoverSwitch,CircleSwitch;
	public string CoverName;
	public void MemberSizeAndColorChange()
	{
		float Dis = Other.SliderMemberDistance;
		float Cx = Other.IsHorizontal == true ? 
			       Other.Container.transform.localPosition.x :
			       Other.Container.transform.localPosition.y;
		for (int i=0;i<5;i++)
		{
			float Mx = Other.IsHorizontal == true ? 
				       Other.Member [i].transform.localPosition.x :
				       Other.Member [i].transform.localPosition.y;
			float x = Mx + Cx;
			if (CircleSwitch)
			{
				Other.Member [i].transform.localRotation=
				    Quaternion.Euler(new Vector3(-15f*x/Dis,30f*Mathf.Abs (x)/Dis,0f));
			}
			if ((x < Dis / 2) && (x > -Dis / 2)) 
			{
				x = Mathf.Abs (x);
				Other.Member [i].transform.localScale = Vector3.one * (-x / Dis + 1);
				if (FadeSwitch)
					(Other.Member [i].GetComponent (typeof(CanvasGroup)) as CanvasGroup).alpha = (-x / Dis + 1);
				if (CoverSwitch)
					(Other.Member [i].transform.Find(CoverName).gameObject.
					 GetComponent(typeof(Image)) as Image).color= new Color(0f,0f,0f,1.6f*x / Dis);
			    
			} else 
			{
				Other.Member [i].transform.localScale = Vector3.one * 0.5f;
				if (FadeSwitch)
				   (Other.Member [i].GetComponent (typeof(CanvasGroup)) as CanvasGroup).alpha = 0.5f;
				if (CoverSwitch)
					(Other.Member [i].transform.Find(CoverName).gameObject.
					 GetComponent(typeof(Image)) as Image).color= new Color(0f,0f,0f,0.8f);
			}
		}
	}
}
