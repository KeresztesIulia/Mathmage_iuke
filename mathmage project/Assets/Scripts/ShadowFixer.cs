using UnityEngine;

[ExecuteInEditMode]
public class ShadowFixer : MonoBehaviour
{
    public float setBias = -0.33f;

    // Update is called once per frame
    void Update ()
    {
        GetComponent<Light>().shadowBias = setBias;
    }
}