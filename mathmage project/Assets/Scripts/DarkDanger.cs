using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DarkDanger : MonoBehaviour
{
    [SerializeField] float radius = 5;
    [SerializeField] float speed = 2;
    [SerializeField] float updownSpeed = 1;
    ParticleSystem darkDanger;

    // Start is called before the first frame update
    void Start()
    {
        darkDanger = transform.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeNow = Time.realtimeSinceStartup;
        float newX = Mathf.Sin(timeNow * speed);
        float newY = Mathf.Cos(timeNow * updownSpeed) / 3;
        float newZ = Mathf.Cos(timeNow * speed);
        Vector3 newPosition = new Vector3(newX, (newY+0.5f), newZ) * radius;
        transform.position = newPosition;
        var main = darkDanger.main;
        main.startSpeed = newX;

    }
}
