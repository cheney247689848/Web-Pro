using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
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
		private int nMoveSpeed = 100;
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

#if UNITY_EDITOR
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

            float rx = -Input.GetAxis("Mouse X") * nMoveSpeed * Time.deltaTime;    
            float ry = Input.GetAxis("Mouse Y") * nMoveSpeed * Time.deltaTime;    			
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
#else

		if (Input.touchCount > 0)
		{
			AppendText(Input.GetTouch(0).phase.ToString());
			if (Input.GetTouch(0).phase == TouchPhase.Began){

				bIsMove = false;
			}else if(Input.GetTouch(0).phase == TouchPhase.Ended){

				if (!bIsMove)
				{
					AppendText("Press----------------------------");
					GameObject obj;
					if (Press(Input.mousePosition , out obj))
					{
						if (delegatePress != null)
						{
							delegatePress(obj);
						}
					}
				}
			}else if(Input.GetTouch(0).phase == TouchPhase.Moved){

				float rx = -Input.GetAxis("Horizontal") * nMoveSpeed * Time.deltaTime;    
				float ry = Input.GetAxis("Vertical") * nMoveSpeed * Time.deltaTime;    			
				Debug.Log(rx + " , " +  ry);
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
			
		}
#endif

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
