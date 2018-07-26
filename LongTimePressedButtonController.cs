using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class LongTimePressedButtonController : UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
//	[ Tooltip( "How long must pointer be down on this object to trigger a long press" ) ]
	public float durationThreshold = 1.0f;

	public UnityEvent onLongPress = new UnityEvent();

	public bool isPointerDown = false;
	public bool longPressTriggered = false;
	public float timePressStarted;

	private void Update() 
	{
		if (isPointerDown && !longPressTriggered) 
		{
			if (Time.time - timePressStarted > durationThreshold) 
			{
				longPressTriggered = true;
				onLongPress.Invoke();
			}
		}
	}
	public void OnPointerDown(PointerEventData eventData) 
	{
		timePressStarted = Time.time;
		isPointerDown = true;
		longPressTriggered = false;
	}

	public void OnPointerUp(PointerEventData eventData) 
	{
		isPointerDown = false;
	}


	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerDown = false;
	}
}
