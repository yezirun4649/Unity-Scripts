using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CsvReader : MonoBehaviour 
{
	public TextAsset CSV;
	public bool TestSwitch;
	public Text DisplayText;
	public string[,] Result = new string[1000, 20]; 
	public int Lines=0;
	public bool CSVIsReaded=false;
	void Start () 
	{
        DealWithCSVData();
		if (TestSwitch) CheckCSVData();
	}
	void DealWithCSVData()
	{
		string[] lineArray = CSV.text.Split("\r"[0]);
		foreach (string s in lineArray)
		{
			string[] temp = s.Split (',');
			for (int j=0;j<temp.Length;j++)
				Result [Lines,j] = temp[j];
			if (Lines>0)
			    Result [Lines,0]=Result [Lines,0].Trim('\n');
			Lines++;
		}
		CSVIsReaded=true;
	}
	void CheckCSVData()
	{
		for (int i=0;i<Lines;i++)
		    for (int j=0;j<6;j++)
		        DisplayText.text+=Result [i,j]+" ";
			DisplayText.text+="\n";
	}
}
