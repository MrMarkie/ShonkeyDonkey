using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    [SerializeField]    protected GameObject m_gameFlow;  
    [SerializeField]    protected GameObject m_cameraInteractor;
    [SerializeField]    protected GameObject m_pickupInteractor;
    [SerializeField]    protected GameObject m_caroselInteractor;    

    enum InteractionState
    {
        kCamera,
        kPickup,
        kCarosel
    }

    private InteractionState m_interactionState = GameLogic.InteractionState.kCamera;

    void Start ()
    {
         m_interactionState  = GameLogic.InteractionState.kCamera;
    }
	
	void Update ()
    {
     
        UpdateGameInteractionState();
    }


    void UpdateGameInteractionState()
    {
        if (m_cameraInteractor!=null)
        {
            SwipeTargetCamera swipeInteractor = m_cameraInteractor.GetComponent<SwipeTargetCamera>();

            if ( swipeInteractor!=null )
            {
                swipeInteractor.enabled = m_interactionState == GameLogic.InteractionState.kCamera;
            }
        }

        if (m_pickupInteractor != null)
        {
            CarryController carryController = m_pickupInteractor.GetComponent<CarryController>();

            if (carryController != null)
            {
                carryController.enabled = m_interactionState == GameLogic.InteractionState.kPickup;
            }
        }
    }
}
