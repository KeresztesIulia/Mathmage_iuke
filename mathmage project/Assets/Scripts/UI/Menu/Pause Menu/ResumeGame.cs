using UnityEngine;

public class ResumeGame : ButtonScript
{
    [SerializeField] GameObject pauseMenu;
    public override void OnClick()
    {
        GameFunctions.PlayerActive = true;
        pauseMenu.SetActive(false);
    }
}
