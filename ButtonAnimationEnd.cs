using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonAnimationEnd : MonoBehaviour , IPointerExitHandler
{
	public Button button;
	public ButtonBase other;
	public Vector3 Size=new Vector3(1f, 1f, 1f);
	public void OnPointerExit(PointerEventData eventData)
	{
		other.buttonHighlight = false;
		button.transform.DOScale(Size, 0.2F).SetEase(Ease.InOutBack,10);
	}
}
