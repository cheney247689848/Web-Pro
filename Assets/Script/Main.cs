using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
public class Main : MonoBehaviour {

	private PwContainer pwContainer;
	CameraControler cameraControler;

	// Use this for initialization
	void Start () {
		
		Debug.Log(" --- Main Start---");
        //Application.targetFrameRate = 60;
		pwContainer = new PwContainer();
		cameraControler = this.gameObject.GetComponent<CameraControler>();
		
		//A
		int nForward = 0;
		int nRankMax = 9;
		int nColMax = 16;
		string strData = "5,x,6,7,8,9,10,11,12,13,14,15,16,17,18,19|"
		+ "5,6,7,8,9,x,10,11,12,13,14,15,16,17,18,19|"
		+ "4,5,6,7,8,9,10,11,12,x,14,15,16,17,x,19|"
		+ "4,5,6,7,x,9,10,11,12,13,14,15,x,17,18,19|"
		+ "4,5,6,7,8,9,10,11,12,13,14,x,16,17,18,19|"
		+ "4,5,6,7,8,9,10,x,x,13,x,15,16,17,18,19|"
		+ "4,5,x,7,8,9,10,11,12,13,14,15,x,17,18,19|"
		+ "4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19|"
		+ "4,5,6,7,8,9,x,11,12,13,14,15,16,17,x,19";
		pwContainer.Creat(nForward ,nRankMax , nColMax , strData , GameObject.Find("pwContainerA").transform);
		
		//B
		nForward = 1;
		nRankMax = 9;
		nColMax = 22;
		strData = "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42|"
		+ "21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42";
		pwContainer.Creat(nForward ,nRankMax , nColMax , strData , GameObject.Find("pwContainerB").transform);

		//C
		nForward = 2;
		nRankMax = 9;
		nColMax = 16;
		strData = "32,33,34,35,36,x,37,38,39,x,40,41,42,43,44|"
		+ "32,33,34,35,36,37,38,39,40,41,42,43,44|"
		+ "31,32,33,34,35,36,37,38,x,39,40,41,42,43,44,45|"
		+ "31,32,33,34,35,36,37,38,39,40,41,42,43,44,45|"
		+ "31,32,33,34,35,36,37,38,39,x,40,41,42,43,44,45|"
		+ "31,32,33,34,35,36,37,38,39,40,41,42,43,44,45|"
		+ "31,32,33,34,35,36,37,38,39,x,40,41,42,43,44,45|"
		+ "31,32,33,34,35,x,36,37,38,39,40,41,42,43,44,45|"
		+ "31,32,33,34,35,36,37,38,39,40,41,x,42,43,44,45";
		pwContainer.Creat(nForward ,nRankMax , nColMax , strData , GameObject.Find("pwContainerC").transform);

		//D
		nForward = 3;
		nRankMax = 9;
		nColMax = 3;
		strData = "47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48";
		pwContainer.Creat(nForward ,nRankMax , nColMax , strData , GameObject.Find("pwContainerD").transform);


		//E
		nForward = 4;
		nRankMax = 9;
		nColMax = 3;
		strData = "47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48|"
		+ "46,47,48";
		pwContainer.Creat(nForward ,nRankMax , nColMax , strData , GameObject.Find("pwContainerE").transform);
		

		//Event
		cameraControler.delegatePress = new DelegatePress(ShowTip);

		//UI
		Button btn_A = GameObject.Find ("Canvas/Btn_A").GetComponent<Button>();
		btn_A.onClick.AddListener (OnClick_A);
		Button btn_B = GameObject.Find ("Canvas/Btn_B").GetComponent<Button>();
		btn_B.onClick.AddListener (OnClick_B);
		Button btn_C = GameObject.Find ("Canvas/Btn_C").GetComponent<Button>();
		btn_C.onClick.AddListener (OnClick_C);
		Button btn_D = GameObject.Find ("Canvas/Btn_D").GetComponent<Button>();
		btn_D.onClick.AddListener (OnClick_D);
		Button btn_E = GameObject.Find ("Canvas/Btn_E").GetComponent<Button>();
		btn_E.onClick.AddListener (OnClick_E);
	}

	 void OnClick_A ()
     {
        //  Debug.Log ("click!");
		 cameraControler.LookAt(GameObject.Find("Ap").transform.position);
     }

	 void OnClick_B ()
     {
        //  Debug.Log ("click!");
		 cameraControler.LookAt(GameObject.Find("Bp").transform.position);
     }

	 void OnClick_C ()
     {
        //  Debug.Log ("click!");
		 cameraControler.LookAt(GameObject.Find("Cp").transform.position);
     }

	 void OnClick_D ()
     {
        //  Debug.Log ("click!");
		 cameraControler.LookAt(GameObject.Find("Dp").transform.position);
     }

	 void OnClick_E ()
     {
        //  Debug.Log ("click!");
		 cameraControler.LookAt(GameObject.Find("Ep").transform.position);
     }
	
	// Update is called once per frame
	void Update () {
	

	}

	void ShowTip(GameObject obj){

		Pw p =  pwContainer.GetPwKey(obj);
		if (p != null)
		{
			
			Debug.Log(p.ToString());
 			object[] args = new object[]{p.nColumn};
    		// Application.ExternalCall("showdetail",args);
			// showdetail(p.nColumn);

		}else
		{
			Debug.LogError("Error cant not find the Obj");
		}
	}

	[DllImport("__Internal")]
    private static extern void showdetail(int arg);
}
