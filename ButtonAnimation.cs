using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class ButtonAnimation : MonoBehaviour,IPointerEnterHandler
{
	public Button button;
	public ButtonBase other;
	public Vector3 Size;
	//public AudioClip Click;
	//public GameObject AudioDummy;
	//public AudioSource Audio;
	void Start()
	{
		//AudioDummy = GameObject.FindWithTag("BGM");
		//Audio = AudioDummy.GetComponent<AudioSource>();
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		//Audio.PlayOneShot(Click, 1F);
		if (other.buttonHighlight == false) 
		{
			other.buttonHighlight = true;
			button.transform.DOScale (Size, 0.5F).SetEase(Ease.InOutBack,10);
		}
	}
}
