using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
public delegate void DelegatePress(GameObject obj); //定义点击事件
public class CameraControler : MonoBehaviour {

	
	public DelegatePress delegatePress;
	private Camera camera;
	private Vector3 vRotation;
	private Vector3 vTarRotaion;
	private bool bIsMove;
	private Vector3 vLookPosition;
	private Vector3 vTarLookPosition;
	private Vector3 vVarietyPosition;
	private Vector3 vStartPosition;
	private Vector3 vLastPosition;
	private Vector3 vSceneTouchPosition;
	private bool bIsLook;
	private int nMoveSpeed = 200;
	private int nInputType;
	private int nInputEvent;

	// Use this for initialization
	void Start () {
	
		camera = Camera.main;
		vRotation = new Vector3(0 , 180 , 0);
		vTarRotaion = vRotation;
		bIsMove = false;
		bIsLook = false;
		vLookPosition = camera.transform.forward;

		nInputType = 0;
		nInputEvent = -1;
#if UNITY_EDITOR
#else
		if ("Desktop" != SystemInfo.deviceType.ToString())
		{
			nInputType = 1;
			nMoveSpeed = 20;
		}
#endif
        Debug.Log(string.Format("nInputType = {0} deviceType = {1}", nInputType , SystemInfo.deviceType.ToString()));

		LookAt(GameObject.Find("Ap").transform.position);
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			LookAt(GameObject.Find("Ap").transform.position);
			
			
		}else if(Input.GetKeyDown(KeyCode.Alpha2)){

			LookAt(GameObject.Find("Bp").transform.position);

		}else if(Input.GetKeyDown(KeyCode.Alpha2)){


		}else if(Input.GetKeyDown(KeyCode.Alpha2)){


		}else if(Input.GetKeyDown(KeyCode.Alpha2)){


		}

		if (bIsLook)
		{
			if (Vector3.Distance(vLookPosition , vTarLookPosition) > 1)
			{	
				vLookPosition = Vector3.MoveTowards(vLookPosition , vTarLookPosition , 0.5f);
				camera.transform.LookAt(vLookPosition);
			}else{
				
				bIsLook = false;
				vRotation = new Vector3(camera.transform.localRotation.eulerAngles.x, camera.transform.localRotation.eulerAngles.y , 0);
				vTarRotaion = vRotation;
				vTarLookPosition = vLookPosition;
				vLastPosition = vLookPosition;
			}
			return;
		}

		nInputEvent = -1;
		if (0 == nInputType)
		{
			if (Input.GetMouseButtonDown(0)){
				nInputEvent = 0;
			}else if(Input.GetMouseButtonUp(0)){
				nInputEvent = 1;
			}else if(Input.GetMouseButton(0)){
				nInputEvent = 2;
			}
		}else if(1 == nInputType){

			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began){
					nInputEvent = 0;
				}else if(Input.GetTouch(0).phase == TouchPhase.Ended){
					nInputEvent = 1;					
				}else if(Input.GetTouch(0).phase == TouchPhase.Moved){
					nInputEvent = 2;
				}
			}
		}

		if (0 == nInputEvent)
		{
			bIsMove = false;
			vSceneTouchPosition = new Vector3(Input.mousePosition.x , Input.mousePosition.y , 10);
			vStartPosition = camera.ScreenToWorldPoint(vSceneTouchPosition);
			Debug.Log("start : " + vStartPosition);
			// GameObject.Find("pLook").transform.position = p;Debug.Log(p);

		}else if(1 == nInputEvent){
			
			if (!bIsMove)
			{
				GameObject obj;
				if (Press(Input.mousePosition , out obj))
				{
					if (delegatePress != null)
					{
						delegatePress(obj);
					}
				}
			}
		}else if(2 == nInputEvent){

			Vector3 vt = new Vector3(Input.mousePosition.x , Input.mousePosition.y , 10);
			if (Vector3.Distance(vSceneTouchPosition , vt) > 0.5f)
			{
				vSceneTouchPosition = vt;
				Vector3 v = camera.ScreenToWorldPoint(vSceneTouchPosition);
				Debug.Log("move : " + v);
				vVarietyPosition = vStartPosition - v;
				vStartPosition = v;
				Debug.Log(vVarietyPosition);
				vTarLookPosition += vVarietyPosition;
				Debug.Log(vLookPosition + " , " + vTarLookPosition);
			}

			vLookPosition = Vector3.MoveTowards(vLookPosition , vTarLookPosition , 0.2f);
			camera.transform.LookAt(vLookPosition);
			
			// if (Vector3.Distance(vLookPosition , vTarLookPosition) > 0.5f)
			// {	
			// 	vLookPosition = Vector3.MoveTowards(vLookPosition , vTarLookPosition , 0.5f);
			// 	camera.transform.LookAt(vLookPosition);
			// }

            // float rx = -Input.GetAxis("Mouse X") * nMoveSpeed * Time.deltaTime;    
            // float ry = Input.GetAxis("Mouse Y") * nMoveSpeed * Time.deltaTime;
			// // Debug.Log(rx + " , " +  ry);
			// if (!bIsMove)
			// {
			// 	if (Mathf.Abs(rx) > 0.2f || Mathf.Abs(ry) > 0.2f)
			// 	{
			// 		bIsMove = true;
			// 	}
			// }
			// if(bIsMove)
			// {
			// 	vTarRotaion.y += rx;
			// 	vTarRotaion.x += ry;

			// 	vRotation = Vector3.MoveTowards(vRotation , vTarRotaion , 0.9f);
			// 	camera.transform.localRotation = Quaternion.Euler(vRotation.x,vRotation.y,0);
			// 	vLookPosition = camera.transform.forward;
			// 	Debug.Log(vLookPosition);
			// }
		}

		// vRotation = Vector3.MoveTowards(vRotation , vTarRotaion , 0.9f);
		// camera.transform.localRotation = Quaternion.Euler(vRotation.x,vRotation.y,0);
	}

	bool Press(Vector3 mousePosition , out GameObject obj){

		Ray ray = camera.ScreenPointToRay(mousePosition);
		RaycastHit hit;  
		obj = null;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))  
		{  
			// 如果射线与平面碰撞，打印碰撞物体信息  
			Debug.Log("hit: " + hit.collider.name);  
			// 在场景视图中绘制射线 
			Debug.DrawLine(ray.origin, hit.point, Color.red);
			obj = hit.collider.gameObject;
			return true;
		}
		return false;
	}

	public void LookAt(Vector3 pos){

		bIsLook = true;
		vTarLookPosition = pos;
	}

	private StringBuilder builder = new StringBuilder();
	private int nMaxLine;
	public void AppendText(string str){

		nMaxLine --;
		if (nMaxLine <= 0)
		{
			nMaxLine = 20;
			builder = new StringBuilder();
		}

		builder.AppendLine(str);
		Text t = GameObject.Find("Text").GetComponent<Text>();
		t.text = builder.ToString();
	}
}
