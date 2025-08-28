using UnityEngine;

public class PauseMenu : BaseMenu
{



    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.PauseMenu;
    }
}
