using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameFlow : MonoBehaviour {

    enum GameState
    {
        kLoadMenu,
        kMenuLoading,
        kMenu,
        kLoadGame,
        kGameLoading,
        kGame,
        kDefault = kLoadMenu
    }

    private GameFlow.GameState m_state = GameFlow.GameState.kDefault;

    void Start ()
    {
        m_state = GameFlow.GameState.kDefault;

        Scene mainScene = SceneManager.GetSceneByName("main");

        SceneManager.SetActiveScene( mainScene );

    }
	
	void Update ()
    {        
        switch ( m_state )
        {
            case GameFlow.GameState.kLoadMenu:
            {               
                 SceneManager.LoadScene("menu", LoadSceneMode.Additive);
                m_state = GameFlow.GameState.kMenuLoading;
            }
            break;

            case GameFlow.GameState.kMenuLoading:
            {
                Scene menuScene = SceneManager.GetSceneByName("menu");

                if ( menuScene!=null )
                {
                    if ( menuScene.isLoaded )
                    {
                        m_state = GameFlow.GameState.kMenu;
                    }
                }          
            }
            break;

            case GameFlow.GameState.kMenu:
            {
                bool touchDown = false;

#if UNITY_EDITOR
                if (Input.GetMouseButton(0))
                {
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

                if ( touchDown==true )
                {
                    SceneManager.UnloadScene("menu");
                    m_state = GameFlow.GameState.kLoadGame;
                }
            }
            break;

            case GameFlow.GameState.kLoadGame:
            {
                SceneManager.LoadScene("game", LoadSceneMode.Additive);
                m_state = GameFlow.GameState.kGameLoading;
            }
            break;

            case GameFlow.GameState.kGameLoading:
            {
                Scene gameScene = SceneManager.GetSceneByName("game");

                if (gameScene != null )
                {
                    if (gameScene.isLoaded )
                    {
                        m_state = GameFlow.GameState.kGame;
                    }
                }          
            }
            break;

            case GameFlow.GameState.kGame:
            {
                Scene gameScene = SceneManager.GetSceneByName("game");
             
                if ( Input.GetKey( KeyCode.Escape ) )
                {
                    // Player quit back to menu
                    SceneManager.UnloadScene("game");
                    m_state = GameFlow.GameState.kLoadMenu;
                }
            }
            break;
        }
       
    }
}
