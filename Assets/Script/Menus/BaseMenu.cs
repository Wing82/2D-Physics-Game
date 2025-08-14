using UnityEngine;

public enum MenuStates
{
    MainMenu,
    SettingsMenu,
    PauseMenu,
    GameOverMenu,
    InGame,
    CreditMenu,
    InstructionsMenu
}

public class BaseMenu : MonoBehaviour
{
    [HideInInspector]
    public MenuStates state;

    protected MenuController context;
    public virtual void Init(MenuController context) => this.context = context;

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void JumpBack() => context.JumpBack();

    public void SetNextMenu(MenuStates newState) => context.SetActiveState(newState);
}
