using UnityEngine;

public enum MenuStates
{
    MainMenu,
    LevelSelectMenu,
    CarLevelMenu,
    SettingsMenu,
    PauseMenu,
    GameOverMenu,
    CarChase,
    CreditMenu,
    InstructionsMenu,
    ContinueMenu,
    BirdCannon,
    BirdLevelMenu
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
