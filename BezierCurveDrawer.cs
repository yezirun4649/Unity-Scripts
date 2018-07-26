using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveDrawer : MonoBehaviour 
{
	public Vector3[] BezierCurveP = new Vector3[4];
	public float CurbeMoveTime;
	public int FPSNeed=60;
	public float FPS;
	public Vector3 BezierCurveResult;
	public GameObject Effect;
	public float T,TempT;
	public float[] T0=new float[61]; 
	public float[] T1=new float[61];
	public float[] T2=new float[61];
	public float[] T3=new float[61];
	public float Timer=0;
	public int i,Flames;
	public bool x,Begin;
	public float TempFloat;
	public Vector3 FinalSize;
	void Start () 
	{
		FPS = CurbeMoveTime / FPSNeed;
		Flames = 0;
		for (i = 0; i <= 61; i++) 
		{
			T = (float)i / (float)FPSNeed;
			T0 [i] = (1 - T) * (1 - T) * (1 - T);
			T3 [i] = T * T * T;
			T1[i] = 3*T * (1 - T) * (1 - T);
			T2[i] = 3*T * (1 - T) * T;
		}
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		for (i = 0; i < 3; i++)
			Gizmos.DrawLine (BezierCurveP [i], BezierCurveP [i + 1]);
	}
	public void EffectCurveActive()
	{
		this.gameObject.SetActive (true);
	}
	public void EffectCurveBegin()
	{
		Begin = true;
	}
	public void EndVector(Vector3 End)
	{
		BezierCurveP [3] = End;
	}
	public void EffectCurveEnd()
	{
		Begin = false;
	}
	void Update () 
	{
		if (!Begin)
			return;
		if (x) 
		{
			x = false;
			Flames = 0;
			Timer = 0;
		}
		if (Flames <= FPSNeed*100000)
		{
			Timer += Time.deltaTime;
			if (Timer > FPS) 
			{
				Timer = 0;
				BezierCurveResult =
				   T0 [Flames] * BezierCurveP [0] +
				T1 [Flames] * BezierCurveP [1] +
				T2 [Flames] * BezierCurveP [2] +
				T3 [Flames] * BezierCurveP [3];
				Effect.transform.position = BezierCurveResult;
				Flames++;
				if (Flames < FPSNeed / 2)
					TempFloat = 1.2f + (float)Flames * 2 / FPSNeed;
				else
					TempFloat = 1.2f + (float)(FPSNeed - Flames) * 2 / FPSNeed;
				Effect.transform.localScale = new Vector3 (TempFloat* 1.2f, TempFloat* 1.2f, TempFloat* 1.2f);
			}
		} else 
		{
//			Effect.SetActive (false);
//			Effect.transform.position = BezierCurveP [0];
			Effect.transform.localScale=FinalSize;
			Flames = 0;
			Begin = false;
		}
	}
}
