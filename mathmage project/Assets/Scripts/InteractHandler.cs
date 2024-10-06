using TMPro;
using UnityEngine;

public sealed class InteractHandler : MonoBehaviour
{
    IInteractable interactable = null;
    bool activatable;
    bool reactable;

    [SerializeField] Camera playerCamera;
    
    [SerializeField] TMP_Text nameText;
    [SerializeField] GameObject activateObject;
    [SerializeField] TMP_Text activateText;
    [SerializeField] GameObject reactText;

    public IInteractable Interactable
    {
        set
        {
            if (interactable == value) return;
            interactable = value;
            if (interactable != null)
            {
                ShowName();
                if (interactable is IActivatable)
                {
                    activatable = true;
                    ShowActivate(true);
                }
                else
                {
                    ShowActivate(false);
                }
                if (interactable is IReactable)
                {
                    reactable = true;
                    ShowReact(true);
                }
                else
                {
                    ShowReact(false);
                }
            }
            else
            {
                activatable = false;
                reactable = false;
                ShowNothing();
            }
        }
    }

    void Update()
    {
        if (!GameFunctions.PlayerActive || !GameFunctions.GameIsActive) return;
        FindInteractable();
        if (activatable && Input.GetKeyDown(KeyCode.E))
        {
            ((IActivatable)interactable).Activate();
            Interactable = null;
        }
        if (reactable && Input.GetKeyDown(KeyCode.R))
        {
            ((IReactable)interactable).React();
        }

    }

    void FindInteractable()
    {
        Ray interacter = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitData;
        Debug.DrawRay(interacter.origin, interacter.direction * 4); // DEBUG!!!

        if (Physics.Raycast(interacter, out hitData, 4))
        {
            IInteractable interactable = hitData.collider.GetComponent<IInteractable>();
            if (interactable == null)
            {
                interactable = hitData.collider.GetComponentInParent<IInteractable>();
            }
            Interactable = interactable;
        }
        else
        {
            Interactable = null;
        }
    }

    void ShowName()
    {
        nameText.text = interactable.Name;
        nameText.gameObject.SetActive(true);
    }

    void ShowActivate(bool show)
    {
        if (show)
        {
            string text = ((IActivatable)interactable).ActivationText;
            activateText.text = text == "" ? "Megnézés" : text;
        }
        activateObject.SetActive(show);
    }

    void ShowReact(bool show)
    {
        reactText.SetActive(show);
    }

    void ShowNothing()
    {
        nameText.gameObject.SetActive(false);
        reactText.SetActive(false);
        activateObject.SetActive(false);
    }
}
