
using UnityEngine;

public class F3PutBackBook : RiddleObject
{
    [SerializeField] Vector3 originalPosition;
    [SerializeField] Vector3 originalRotation;

    public void ResetTransform()
    {
        SetTransform(originalPosition, originalRotation);
    }

    public void SetTransform(Vector3 position, Vector3 rotation)
    {
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
