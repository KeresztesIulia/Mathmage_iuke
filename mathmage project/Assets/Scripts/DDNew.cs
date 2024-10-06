
using UnityEngine;

public class DDNew : MonoBehaviour
{
    [SerializeField] int rotationSpeed;
    static float speedDivisor = 3000;

    Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Vector2 offset = material.GetTextureOffset("_MainTex");
        offset.x += rotationSpeed / speedDivisor;
        material.SetTextureOffset("_MainTex", offset);
    }
}
