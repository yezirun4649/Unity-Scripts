using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class ServerDataReceiver : MonoBehaviour
{
	public string G1ListAPI = "http://52.68.173.184/index.php/game/g1ListAPI";
	public string G1StartAPI="http://52.68.173.184/index.php/game/g1StartAPI";
	public string G1ActAPI="http://52.68.173.184/index.php/game/g1ActAPI";
	public string G1LuckyAPI="http://52.68.173.184/index.php/game/g1LuckyAPI";
	public string G1EndAPI="http://52.68.173.184/index.php/game/g1EndAPI";
	public string LoginAPI="http://52.68.173.184/index.php/game/loginAPI";
	public string url;
	private string MainString,G1ActAPIJson,G1StartAPIJson,G1EndAPIJson,G1ListAPIJson;
	public int MainStringLength;
	public Text DebugText;

	public bool G1EndSwitch;

	public int nowTimePos;
	public Text TimeText;

	public float LastTime=-15f;
	void Start () 
	{
//		StartCoroutine (G1ListAPITest());
	}
	IEnumerator G1ListAPITest()
	{
		url=G1ListAPI;
		WWWForm sum = new WWWForm();
		sum.AddField ("uid","debac889bae1fd0c130b31a15d9d168af7d91307");
		sum.AddField ("params","{}");
		WWW ww2 = new WWW(url,sum);
		yield return ww2;
		G1ListAPIJson = ww2.text;
		nowTimePos=G1ListAPIJson.IndexOf ("nowTime");
		if (nowTimePos >= 0) 
		{
			string TempString = "";
			int QuoteCount = 0;
			for (int i = nowTimePos + 1; i < G1ListAPIJson.Length; i++) 
			{
				if (G1ListAPIJson [i] == '"') 
				{
					QuoteCount++;
					if (QuoteCount == 3)
						break;
				}
				if (QuoteCount == 2 && G1ListAPIJson [i] != '"')
					TempString += G1ListAPIJson [i];
			}
//		TempString.Remove (0, 1);
			TimeText.text = TempString;
		}
	}
	IEnumerator G1StartAPITest()
	{
		url=G1StartAPI;
		WWWForm sum = new WWWForm();
		sum.AddField ("uid","debac889bae1fd0c130b31a15d9d168af7d91307");
		sum.AddField ("params","{\"stageId\":1,\"partyNo\":1}");
		WWW ww2 = new WWW(url,sum);
		yield return ww2;
		G1StartAPIJson = ww2.text;
	}
	IEnumerator G1ActAPITest()
	{
		url=G1ActAPI;
		WWWForm sum = new WWWForm();
		sum.AddField ("uid","debac889bae1fd0c130b31a15d9d168af7d91307");
		sum.AddField ("params","{\"stageId\":1,\"castId\":1}");
		WWW ww2 = new WWW(url,sum);
		yield return ww2;
		G1ActAPIJson = ww2.text;
	}
	IEnumerator G1EndAPITest()
	{
		url=G1EndAPI;
		WWWForm sum = new WWWForm();
		sum.AddField ("uid","debac889bae1fd0c130b31a15d9d168af7d91307");
		sum.AddField ("params","{\"stageId\":1,\"stopFlag\":1}");
		WWW ww2 = new WWW(url,sum);
		yield return ww2;
		G1EndAPIJson = ww2.text;
	}
	IEnumerator Login ()
	{
		WWWForm sum = new WWWForm();
//		sum.AddField ("did", "allcharaprezenttestdeviceid");
//		sum.AddField ("params", "{\"playerName\":\"こんどー\", \"firstName\":\"太郎\", \"lastName\":\"近藤\",\"birthMonth\":3,\"birthDay\":13,\"os\":2}");
		sum.AddField ("uid","debac889bae1fd0c130b31a15d9d168af7d91307");
		sum.AddField ("params","{}");
//		Debug.Log (sum);
		WWW ww2 = new WWW(url,sum);
		yield return ww2;
//		Debug.Log (ww2.text);
		MainString = ww2.text;
		MainStringLength = MainString.Length;
		TempStringHandle ();
//		DebugText.text = MainString.Substring(0,500);
//		Debug.Log (MainString);
//		0, ww2.text.Length
//		string Temp = ww2.text;
//		JsonData TempJsonData = JsonMapper.ToObject(Temp);
//		Debug.Log ("status= " + TempJsonData ["status"]);
	}
	void TempStringHandle()
	{
		for (int i = 0; i < 2000; i++) 
		{
			DebugText.text += MainString [i];
			if (MainString [i] == '{' || MainString [i] == ',') 
			{
				DebugText.text += System.Environment.NewLine;
//				Debug.Log ("Finded!");
//				MainString.Insert (i, System.Environment.NewLine);
			}
		}
	}
	void StringHandle()
	{
/*		int Left,int Right
        int Now=Left;
		for (int i=Left+1;i<Right;i++)
		{
			if (MainString [i] == '{')
				StringHandle (i, Right);
			else if (MainString [i] == '}')
				JsonHandle (Left, i);
		}
*/
		int LeftNumber=-1;
		int[] LeftPosition=new int[10000];
		for (int i = 0; i < MainStringLength; i++) 
		{
			if (MainString [i] == '{') 
			{
				LeftNumber++;
				LeftPosition [LeftNumber] = i;
			} else if (MainString [i] == '}') 
			{
				JsonHandle(LeftPosition [LeftNumber],i);
				LeftNumber--;
			}
		}
	}
	void JsonHandle(int Left,int Right)
	{
//		string TempString;
	}

	void Update()
	{
		if (G1EndSwitch) 
		{
			StartCoroutine (G1EndAPITest ());
			G1EndSwitch = false;
		} else if (Time.time - LastTime > 1f) 
		{
			LastTime = Time.time;
			StartCoroutine (G1ListAPITest ());
		}
	}
}
