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

		nInputType = 0;
		nInputEvent = -1;
#if UNITY_EDITOR
#else
		if ("Desktop" != SystemInfo.deviceType.ToString())
		{
			nInputType = 1;
			nMoveSpeed = 100;
		}
#endif
        Debug.Log(string.Format("nInputType = {0} deviceType = {1}", nInputType , SystemInfo.deviceType.ToString()));
	}
	
	// Update is called once per frame
	void Update () {

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

            float rx = -Input.GetAxis("Mouse X") * nMoveSpeed * Time.deltaTime;    
            float ry = Input.GetAxis("Mouse Y") * nMoveSpeed * Time.deltaTime;
			Debug.Log(rx + " , " +  ry);
			if (!bIsMove)
			{
				if (Mathf.Abs(rx) > 0.2f || Mathf.Abs(ry) > 0.2f)
				{
					bIsMove = true;
				}
			}
			if(bIsMove)
			{
				vTarRotaion.y += rx;
				vTarRotaion.x += ry;
			}
		}

		vRotation = Vector3.MoveTowards(vRotation , vTarRotaion , 0.9f);
		camera.transform.localRotation = Quaternion.Euler(vRotation.x,vRotation.y,0);
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

		vLookPosition = camera.transform.forward;
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
