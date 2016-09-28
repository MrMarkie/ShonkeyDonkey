using UnityEngine;
using System.Collections;

public class CarryController : MonoBehaviour {

    [SerializeField] protected GameObject m_swipeCamera;
    [SerializeField] protected bool m_isCarrying;

    private float m_xPixelScale = 1.0f;
    private float m_yPixelScale = 1.0f;

    void Start ()
    {
        m_xPixelScale = Screen.width / 1280.0f;
        m_yPixelScale = Screen.height / 720.0f;
        m_isCarrying = false;         
    }
	
	void Update ()
    {
        Vector3 screenPoint = new Vector3(0.0f, 0.0f);
        bool touchDown = false;
                
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            screenPoint = Input.mousePosition;
            touchDown = true;            
        }
#else
  
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            screenPoint.x = touch.position.x;
            screenPoint.y = touch.position.y;
            touchDown = true;                
        }

#endif

        if ( screenPoint.y > 360.0f && touchDown==true )
        {
            m_isCarrying = true;
        }

        if ( touchDown==false )
        {
            m_isCarrying = false;
        }

        Plane intesectionPlane = new Plane(Camera.main.transform.forward, new Vector3(0.0f, 0.0f, 0.0f));

        if (m_isCarrying==true)
        {    
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);

            float rayDistance;

            if (intesectionPlane.Raycast(ray, out rayDistance))
            {
                transform.position = ray.GetPoint(rayDistance);               
            }
        }

        Rigidbody rigigBody = GetComponent<Rigidbody>();

        if (rigigBody!=null)
        {
            rigigBody.isKinematic = m_isCarrying;
        }
    }
}
