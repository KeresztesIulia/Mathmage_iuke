using UnityEngine;

public class Closeable : MonoBehaviour
{
    protected virtual void Close()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
}
