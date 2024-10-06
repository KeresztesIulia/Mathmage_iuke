
using UnityEngine;

public class skipIntroScript : SceneLoader
{
    public string sceneName = "test_stuff";

    void Update()
    {
        if (Input.GetAxis("Cancel") != 0) LoadGameScene(sceneName);
    }
}
