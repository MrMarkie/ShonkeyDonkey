using UnityEngine;
using System.Collections;

public class SwipeTargetCamera : MonoBehaviour {

    [SerializeField] private float m_Range = 2.0F;
    [SerializeField] private float m_Smoothing = 0.9F;
    [SerializeField] private float m_Azimuth = 0.0F;
    [SerializeField] private float m_Elevation = 0.0F;
    [SerializeField] private Vector3 m_Offset;
    [SerializeField] protected Transform m_Target;            // The target object to follow

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
       
        Quaternion rotation = Quaternion.Euler(m_Elevation, m_Azimuth, 0 );
        var axis = new Vector3(0.0f, 0.0f, -m_Range);

        Vector3 targetPosition = m_Target.position + m_Offset;
        Vector3 cameraPosition = targetPosition + rotation * axis;

        transform.position = Vector3.Lerp(currentPosition, cameraPosition, m_Smoothing);
        transform.rotation = Quaternion.LookRotation( Vector3.Normalize(targetPosition - cameraPosition), new Vector3(0.0f, 1.0f, 0.0f));
    }
}
