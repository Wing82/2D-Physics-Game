using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : BaseMenu
{
    public Button carMainMenu;
    public Button birdMainMenu;
    public Button backBtn;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.LevelSelectMenu;

        if (carMainMenu) carMainMenu.onClick.AddListener(() => SceneManager.LoadScene("CarTitleScreen"));
        if (birdMainMenu) birdMainMenu.onClick.AddListener(() =>SceneManager.LoadScene("BirdTitleScreen"));
        if (backBtn) backBtn.onClick.AddListener(JumpBack);
    }
}
