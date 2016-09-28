using UnityEngine;
using System.Collections;

public class CarryController : MonoBehaviour {

    [SerializeField] protected GameObject m_swipeCamera;
    [SerializeField] protected bool m_isCarrying;
    
    void Start ()
    {
      
	}
	
	void Update ()
    {
        Vector3 screenPoint = new Vector3(0.0f, 0.0f);
      
        m_isCarrying = false;
        
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            screenPoint = Input.mousePosition;
            m_isCarrying = true;
        }

#else
  
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            screenPoint.x = touch.position.x;
            screenPoint.y = touch.position.y;
            m_isCarrying = true;
        }

#endif
        
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

    }
}
