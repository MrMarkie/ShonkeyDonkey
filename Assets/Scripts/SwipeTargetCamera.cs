using UnityEngine;
using System.Collections;

public class SwipeTargetCamera : MonoBehaviour {

    [SerializeField] private float m_range = 2.0f;
    [SerializeField] private float m_smoothing = 0.9f;
    [SerializeField] private float m_azimuth = 0.0f;
    [SerializeField] private float m_elevation = 0.0f;
    [SerializeField] private float m_sensitivity = 1.0f;
    [SerializeField] private float m_swipeDecay = 0.9f;
    [SerializeField] private float m_minElevation = -45.0f;
    [SerializeField] private float m_maxElevation = 45.0f;    
    [SerializeField] private Vector3 m_targetOffset;
    [SerializeField] protected Transform m_targetTransform;

    private float m_xPixelScale =1.0f;
    private float m_yPixelScale =1.0f;
    private bool m_lastTouchDown =false;
    private float m_lastTouchPosX;
    private float m_lastTouchPosY;
    private float m_elevationSpeed = 0.0f;
    private float m_azimuthSpeed = 0.0f;
    
    void Awake()
    {
        m_xPixelScale = Screen.width / 1280.0f;
        m_yPixelScale = Screen.height / 720.0f;
        m_lastTouchDown = false;
    }

    void Start ()
    {
	
	}

    void Update()
    {
        float touchPosX = 0.0f;
        float touchPosY = 0.0f;
        bool touchDown = false;

#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            touchPosX = (Input.mousePosition.x / m_xPixelScale);
            touchPosY = (Input.mousePosition.y / m_yPixelScale);
            touchDown = true;
        }

#else
        touchPosX = (Input.mousePosition.x / m_xPixelScale);
        touchPosY = (Input.mousePosition.y / m_yPixelScale);           

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosX = (touch.position.x / m_xPixelScale);
            touchPosY = (touch.position.y / m_yPixelScale);
            touchDown = true;
        }

#endif

        if (touchDown == true && m_lastTouchDown==true)
        {
            m_azimuthSpeed = (touchPosX - m_lastTouchPosX) * m_sensitivity;
            m_elevationSpeed = -(touchPosY - m_lastTouchPosY) * m_sensitivity;           
        }

        if ( touchDown==false && m_lastTouchDown==true )
        {
            m_elevationSpeed = 0.0f;
        }

        m_azimuth += m_azimuthSpeed;
        m_elevation += m_elevationSpeed;

        m_azimuthSpeed = m_azimuthSpeed * m_swipeDecay;

        if (m_elevation > m_maxElevation)
        {
            m_elevation = m_maxElevation;
        }
        else if (m_elevation < m_minElevation)
        {
            m_elevation = m_minElevation;
        }

        if (m_azimuth > 360.0f)
        {
            m_azimuth = m_azimuth-360.0f;
        }
        else if (m_azimuth < 0.0f)
        {
            m_azimuth = 360.0f + m_azimuth;
        }

        m_lastTouchDown = touchDown;
        m_lastTouchPosX = touchPosX;
        m_lastTouchPosY = touchPosY;
        
        Vector3 currentPosition = transform.position;       
        Quaternion rotation = Quaternion.Euler(m_elevation, m_azimuth, 0);

        var axis = new Vector3(0.0f, 0.0f, -m_range);

        Vector3 targetPosition = m_targetTransform.position + m_targetOffset;
        Vector3 cameraPosition = targetPosition + rotation * axis;

        transform.position = Vector3.Lerp(currentPosition, cameraPosition, m_smoothing);
        transform.rotation = Quaternion.LookRotation( Vector3.Normalize(targetPosition - cameraPosition), new Vector3(0.0f, 1.0f, 0.0f));
    }
}
