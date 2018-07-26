#region Using
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using YeLib;
#endregion
[RequireComponent(typeof(ScrollRect))]
public class SliderBanner : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
	#region Declare Variables
	[Header("Main")]

	public GameObject FindArea;
	public bool IsHorizontal,IsVertical,IsCircle;
	public Button LeftOrUp,RightOrDown;

	public GameObject[] Member = new GameObject[5];
	[Tooltip("Member Name Format,e.g. SliderMember1,SliderMember2...SliderMember5")] 

	public string MemberName="SliderMember";
	public GameObject[] MemberObjectBase = new GameObject[20];
	public int ObjectTotal;
	public float SliderMemberDistance;
	[Header("Move")]
	[Tooltip("After Left/Right button clicked ,Slider Move Duration")] 
	public float MoveDuration;
	[Tooltip("Final Slider position adjust Duration")] 
	public float AdjustDuration;
	public int MiddleNumber=-1;
	private float[] EndValue=new float[7];
	private int Head = 0;
	private int Tail = 4;
	[Header("Drag")]
	public ScrollRect ScrollRectComponent;
	public GameObject Container;
	[Tooltip("it MUST be over 0.5f")] 
	public float MoveLimit=0.9f;
	private int ScrollPosition=0;
	private float PreSwapPosition=0f;
	[Header("Event")]
	public UnityEvent StartEvent = new UnityEvent();
	public UnityEvent BeginDrag = new UnityEvent();
	public UnityEvent Drag = new UnityEvent();
	public UnityEvent EndDrag = new UnityEvent();
    #endregion
    #region Init

	void Start ()
	{
		//Whether if ObjectTotal is even or not,we can get correct MiddleNumber by it;
		MiddleNumber = (ObjectTotal / 2 + ObjectTotal % 2)-1;
		for (int i = 0; i < 7; i++)
			EndValue [i] = SliderMemberDistance * (i - 3);
		for (int i = 0; i < 5; i++) 			
		{
			//The Prefab around MiddleNumber assign to Member;
			Member [i] = 
				(GameObject)(Instantiate(MemberObjectBase [MiddleNumber+i-2],Vector3.zero,Quaternion.identity,FindArea.transform));
            MemBerPositionInit(i);
		}
		StartCoroutine (StartEventInvoke ());
	}
    void MemBerPositionInit(int Number)
	{
		if (IsVertical)
		    Member [Number].transform.localPosition =new Vector3 (0f                   , EndValue [Number + 1], 0f) ;
		else if (IsHorizontal)
		    Member [Number].transform.localPosition =new Vector3 (EndValue [Number + 1], 0f                   , 0f) ;
	}
	IEnumerator StartEventInvoke()
	{
		yield return new WaitForSeconds (0.1f);
		StartEvent.Invoke();
	}
    #endregion 
    #region OnDrag
	void SliderMemberChange(int DragDirection,int TempChangeNumber,int HeadTail)
	{
		//Destroy old Member Content;
		Destroy (Member [HeadTail]);
		Member [HeadTail]                         =
			(GameObject)(Instantiate (MemberObjectBase [TempChangeNumber], Vector3.zero, Quaternion.identity, FindArea.transform));
		//The distance from MiddleNumber to Far Right/Left could be +2 or -2 depends on user DragDirection
		float NewPos=SliderMemberDistance * (ScrollPosition + 2 * DragDirection);
		Member [HeadTail].transform.localPosition = IsHorizontal == true ?
			new Vector3 (NewPos,0f    ,0f):
			new Vector3 (0f    ,NewPos,0f);
	}

	//DragDirection,Left:-1,Right:1;
	//Throw Far Right to Far Left,or opposite;
	void CommonScrollValueChange(int DragDirection)
	{
		MiddleNumber    = NumberCheck (MiddleNumber + DragDirection,ObjectTotal);
		ScrollPosition  = ScrollPosition+DragDirection;
		PreSwapPosition = PreSwapPosition+(-DragDirection)*SliderMemberDistance;
		//SliderMember Far Right;
		Head            = NumberCheck (Head + DragDirection,5);
		//SliderMember Far Left;
		Tail            = NumberCheck (Tail + DragDirection,5);
		int TempChangeNumber;
		if (DragDirection > 0) 
		{
			TempChangeNumber = MiddleNumber + 2 >= ObjectTotal ? MiddleNumber + 2 - ObjectTotal : MiddleNumber + 2;
			SliderMemberChange (DragDirection, TempChangeNumber, Tail);
		}
		else 
		{
		    TempChangeNumber = MiddleNumber     <  2           ? ObjectTotal - 2 + MiddleNumber  : MiddleNumber - 2;
			SliderMemberChange (DragDirection, TempChangeNumber, Head);
		}
	}

	public void ScrollValueChangeFun()
	{
		float X = IsHorizontal == true ? 
			      Container.transform.localPosition.x :
			      Container.transform.localPosition.y ;
		if (Mathf.Abs (X - PreSwapPosition) > SliderMemberDistance*MoveLimit) 
		{
			CommonScrollValueChange (X > PreSwapPosition ? -1 : 1);
		}
	}
	//If Number out of [0,CheckMax] then adjust it;
	int NumberCheck(int Number,int CheckMax)
	{
		if (Number < 0)
			return CheckMax-System.Math.Abs(Number);
		else if (Number > CheckMax-1)
			return Number-CheckMax;
		else 
			return Number;
	}

	//LeftRight,Left:-1,Right:1;
	public void LeftRightButtonOnClick(int LeftRight)
	{ 
		StartCoroutine(AvoidDoubleClick());
		float Px  = Container.transform.localPosition.x;
		float Py  = Container.transform.localPosition.y;
		float Dis = SliderMemberDistance;
		if (LeftRight == 1)
		    if (IsHorizontal)
			    Container.transform.DOLocalMove (new Vector3 (Px-Dis,0     ,0),MoveDuration);
		    else
			    Container.transform.DOLocalMove (new Vector3 (0     ,Py-Dis,0),MoveDuration);
		else
			if (IsHorizontal)
				Container.transform.DOLocalMove (new Vector3 (Px+Dis,0     ,0),MoveDuration);
			else
				Container.transform.DOLocalMove (new Vector3 (0     ,Py+Dis,0),MoveDuration);
	}

	IEnumerator AvoidDoubleClick()
	{
		LeftOrUp.interactable = false;
		RightOrDown.interactable = false;
		yield return new WaitForSeconds (MoveDuration);
		LeftOrUp.interactable = true;
		RightOrDown.interactable = true;
		//After LeftRightButton Move Overed,Adjust Scroll To Correct Position;
		AdjustScrollPosition ();
	}
	#endregion 
	#region Event
	public void OnBeginDrag(PointerEventData EventData)
	{
		BeginDrag.Invoke();
	}

	public void OnDrag(PointerEventData EventData)
	{
		Drag.Invoke();
	}

	public void OnEndDrag(PointerEventData EventData)
	{
		EndDrag.Invoke();
	}
    #endregion 
    #region Finalize
	int GetNearestMemberPosition()
	{
		int x = IsHorizontal == true ? 
			    (int)(Container.transform.localPosition.x) :
			    (int)(Container.transform.localPosition.y) ;
		int s = (int)(SliderMemberDistance);
		int t = x/s;
		int[] b = new int[3];
		b[0]=(t-1) * s;
		b[1]= t    * s;
		b[2]=(t+1) * s;
		int[] a = new int[3];
		a[0]=Mathf.Abs(x-b[0]);
		a[1]=Mathf.Abs(x-b[1]);
		a[2]=Mathf.Abs(x-b[2]);
		int Min=99999999;
		int Mini = -1;
		for (int i=0;i<3;i++)
			if (a[i]<Min)
			{
				Min=a[i];
				Mini=i;
			}
		return b[Mini];
	}

	public void AdjustScrollPosition()
	{
		ScrollRectComponent.horizontal = false;
		ScrollRectComponent.vertical   = false;
		if (IsHorizontal)
			Container.transform.DOLocalMove (new Vector3 (GetNearestMemberPosition(), 0f, 0f), AdjustDuration); 
		else
			Container.transform.DOLocalMove (new Vector3 (0f, GetNearestMemberPosition(), 0f), AdjustDuration); 
		//To avoid drag during adjust; 
		StartCoroutine (ScrollReset ());
	}
		
	IEnumerator ScrollReset()
	{
		yield return new WaitForSeconds (AdjustDuration);
		if (IsHorizontal)
		    ScrollRectComponent.horizontal = true;
		else
			ScrollRectComponent.vertical   = true;
	}
	#endregion 
}
