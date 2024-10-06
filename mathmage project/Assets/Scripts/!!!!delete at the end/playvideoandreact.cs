using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playvideoandreact : MonoBehaviour, IActivatable
{

    [SerializeField] PlayableDirector director;

    public string ActivationText
    {
        get
        {
            return "play video";
        }
    }

    public string Name
    {
        get
        {
            return "video test";
        }
    }

    public void Activate()
    {
        GameFunctions.GameIsActive = false;
        GameFunctions.PlayerActive = false;
        director.Play();
    }
}
