using UnityEngine;

public class PauseMenu : Closeable
{
    [SerializeField] GameObject[] popups;
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject popup in popups)
            {
                popup.SetActive(false);
            }
            Close();
            //GameFunctions.PlayerActive = true;
        }
    }
}
