using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSoundController : MonoBehaviour 
{
	public AudioClip[] Clips=new AudioClip[50];//
	public GameObject AudioDummy;
	public AudioSource Audio;
	public int ClipNumberCopy;
	void Start () 
	{
		AudioDummy = GameObject.FindWithTag("BGM");
		Audio = AudioDummy.GetComponent<AudioSource>();
	}
	public void ClipWantToPlayClipNumber(int ClipNumber)
	{
		ClipNumberCopy = ClipNumber;
		Audio.PlayOneShot(Clips[ClipNumber],1f);
	}
}
