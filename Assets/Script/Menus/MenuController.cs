using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public BaseMenu[] allMenus; // Array of all menus in the game

    public MenuStates initState = MenuStates.CarChase; // Initial state of the menu system

    public BaseMenu CurrentState => currentState; // Property to get the current active menu state
    private BaseMenu currentState; // Reference to the currently active menu state

    Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>(); // Dictionary to map MenuStates to BaseMenu instances
    Stack<MenuStates> menuStack = new Stack<MenuStates>(); // Stack to manage the history of menu states

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (allMenus.Length <= 0)
        {
            allMenus = gameObject.GetComponentsInChildren<BaseMenu>(true);
        }

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;
            menu.Init(this);

            if (menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        SetActiveState(initState); // Set the initial active state of the menu system

        //GameManager.Instance.SetMenuController(this); // Register this MenuController with the GameManager
    }

    public void JumpBack()
    {
        // In this instance - we should probably log the error
        if (menuStack.Count <= 0) return;

        menuStack.Pop();
        SetActiveState(menuStack.Peek(), true);
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        // If we don't have an active menu then we can't set the new state
        if (!menuDictionary.ContainsKey(newState)) return;

        // If we are already in the menu - exit the function
        if (currentState == menuDictionary[newState]) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        //if (!isJumpingBack) menuStack.Push(newState);

        //if (!isJumpingBack)
        //{
        //    if (menuStack.Count > 0 && menuStack.Contains(newState))
        //    {
        //        //remove everything above the new state
        //        while (menuStack.Peek() != newState)
        //        {
        //            menuStack.Pop();
        //        }
        //        //we don't need to push the new state because it is already on the stack
        //        return;
        //    }
        //    menuStack.Push(newState);
        //}

        if (!isJumpingBack)
        {
            if (menuStack.Count > 0 && menuStack.Contains(newState))
            {
                List<MenuStates> oldStates = new List<MenuStates>();
                //remove everything above the new state
                while (menuStack.Peek() != newState)
                {
                    oldStates.Add(menuStack.Pop());
                }

                //pop the new state as we need to re-add it to the top of the stack
                menuStack.Pop();

                //we need to re-add the old states back to the stack
                for (int i = oldStates.Count - 1; i >= 0; i--)
                {
                    menuStack.Push(oldStates[i]);
                }

                menuStack.Push(newState);
                //we don't need to push the new state because it is already on the stack
                return;
            }
            menuStack.Push(newState);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
