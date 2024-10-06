using UnityEngine;

public class NoteObject : MonoBehaviour, IActivatable
{
    [SerializeField] Note note;
    [SerializeField] private new string name = "Üzenet";
    [SerializeField] protected string activationText = "Elolvasás";

    public string Name { get { return name; } }
    public string ActivationText { get { return activationText; } }

    public void Activate()
    {
        GameFunctions.PlayerActive = false;
        note.gameObject.SetActive(true);
    }

}
