using UnityEngine;

public class Door : MonoBehaviour, IActivatable
{
    [SerializeField] new Collider collider;
    [SerializeField] protected int openingDirection;
    protected string activationText = "Kinyitás";
    new string name = "Ajtó";
    protected int actualDirection;
    

    protected float openStartTime = -1;

    public string Name { get { return name; } }
    public string ActivationText { get { return activationText; } }

    protected float elapsedTime {  get { return Time.time - openStartTime; } }

    protected virtual void Start()
    {
        if (openingDirection < 0)
        {
            actualDirection = -1;
        }
        else
        {
            actualDirection = 1;
        }
    }
    protected virtual void Update()
    {
        
        if (elapsedTime < 1)
        {
            transform.Rotate(-actualDirection * Vector3.up * 90 * Time.deltaTime, Space.Self);   
        }
        else
        {
            if (collider != null && collider.isTrigger) collider.isTrigger = false;
        }
        
    }
    public virtual void Activate()
    {
        if (elapsedTime > 1)
        {
            openStartTime = Time.time;
            actualDirection = -actualDirection;
            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
        
    }
}
