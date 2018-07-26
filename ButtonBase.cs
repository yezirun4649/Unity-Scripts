using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonBase : MonoBehaviour 
{
	public bool buttonHighlight;
	public Button button;
	public SceneChange other;
	public Vector3 Size = new Vector3 (1, 1, 1);
	void Start () 
	{
		buttonHighlight = false;
	}
	void Update()
	{
		if (buttonHighlight==false || other.SceneChanging==true)
		   button.transform.DOScale(Size, 0.2F);
	}
}
