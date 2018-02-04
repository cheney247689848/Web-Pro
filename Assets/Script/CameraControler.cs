﻿using UnityEngine;
using System.Collections;
public delegate void DelegatePress(GameObject obj); //定义点击事件
public class CameraControler : MonoBehaviour {

	
	public DelegatePress delegatePress;
	private Camera camera;
	private Vector3 vRotation;
	private Vector3 vTarRotaion;
	private bool bIsMove;

	#if UNITY_EDITOR
		private int nMoveSpeed = 200;
	#else
		private int nMoveSpeed = 150;
	#endif

	// Use this for initialization
	void Start () {
	
		camera = Camera.main;
		vRotation = new Vector3(0 , 180 , 0);
		vTarRotaion = vRotation;
		bIsMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown(0))
		{
			bIsMove = false;
		}else if(Input.GetMouseButtonUp(0)){
			
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
		}else if(Input.GetMouseButton(0)){

            float rx = -Input.GetAxis("Mouse X") * 200 * Time.deltaTime;    
            float ry = Input.GetAxis("Mouse Y") * 200 * Time.deltaTime;    			
			// Debug.Log(rx + " , " +  ry);
			if (!bIsMove)
			{
				if (Mathf.Abs(rx) > 1 || Mathf.Abs(ry) > 1)
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
}