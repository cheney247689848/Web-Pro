using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pw{

	public int nId; // ID
	public string strDec; //描述
	public int nForward; //A B C D E [0 - 4]
	public int nRank; //行数
	public int nColumn; //数列
	public GameObject obj;
	public bool bActive;

	override public string ToString(){
		return string.Format("nId = {0} ,  nForward = {1} , nRank = {2} , nColumn = {3} , strDec = {4} ,bActive = {5}" ,
		nId , nForward , nRank , nColumn ,strDec , bActive);
	}
}

public class PwContainer {

	public Dictionary<GameObject , Pw> disPws = new Dictionary<GameObject, Pw>();

	public void Creat(int nForward , int nRankMax , int nColMax , string strData ,Transform parent){

		//解析str
		string[] strRank = strData.Split('|');
		for (int l = 0; l < strRank.Length; l++)
		{
			if (l > nRankMax - 1)
			{
				Debug.LogError("out rank max");
				return;
			}
			string[] spw = strRank[l].Split(',');
			for (int i = 0; i < spw.Length; i++)
			{
				if (i > nColMax - 1)
				{
					Debug.LogError("out col max");
					// return;
				}else{
					int num = -1;
					if (int.TryParse(spw[i] , out num))
					{
						//creat
						Pw p = new Pw();
						p.nId = num;
						p.bActive = true;
						p.nForward = nForward;
						p.nRank = l;
						p.nColumn = i;
						p.strDec = "详细信息";

						p.obj = GetPwObj();
						p.obj.transform.SetParent(parent);
						p.obj.transform.localRotation = Quaternion.Euler(0,0,0);
						p.obj.transform.localScale = new Vector3(0.7f,1,0.2f);
						p.obj.transform.localPosition = new Vector3(- 0.8f * i,  1.2f * l , 0);
						disPws.Add(p.obj , p);
					}
				}
			}
		}
	}

	public Pw GetPwKey(GameObject obj){

		Pw key;
		if (disPws.TryGetValue(obj , out key))
		{
			return key;
		}
		return null;
	}

	private GameObject GetPwObj(){

		return GameObject.Instantiate(GameObject.Find("paizi"));
		// return null;
	}
}
