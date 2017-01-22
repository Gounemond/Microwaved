using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGUIManager : MonoBehaviour
{
    public Animator[] playerGUIPressA;
    public Animator[] playerGUIPressX;

    // Singleton implementation
    protected static MenuGUIManager _instance;
    public static MenuGUIManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(MenuGUIManager)) as MenuGUIManager;
            return _instance;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KeyAPressed(int playerIndex)
    {
        playerGUIPressA[playerIndex].SetTrigger("Pressed");
    }

    public void KeyACompleted(int playerIndex)
    {
        playerGUIPressA[playerIndex].SetBool("Completed", true);
    }

    public void KeyXPressed(int playerIndex)
    {
        playerGUIPressX[playerIndex].SetTrigger("Pressed");
    }

    public void KeyXCompleted(int playerIndex)
    {
        playerGUIPressX[playerIndex].SetBool("Completed", true);
    }

    public void KeyXAppear(int playerIndex)
    {
        playerGUIPressX[playerIndex].SetBool("Completed", false);
    }
}
