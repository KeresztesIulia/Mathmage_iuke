using UnityEngine;
using UnityEngine.UI;

public class F5UpButton : ButtonScript
{
    [SerializeField] Vector3 floatPosition;
    [SerializeField] Rigidbody player;
    [SerializeField] float floatTime = 5;

    [SerializeField] GameObject[] toggleEnabled;

    float startTime;
    bool isFloating = false;
    Vector3 playerStartPosition;
    float elapsedTime
    {
        get
        {
            return Time.time - startTime;
        }
    }
    public override void OnClick()
    {
        playerStartPosition = player.position;
        startTime = Time.time;
        isFloating = true;

        foreach (GameObject go in toggleEnabled)
        {
            go.SetActive(false);
        }
        GetComponent<Image>().enabled = false;

    }

    private void Update()
    {
        if (elapsedTime > floatTime && isFloating)
        {
            isFloating = false;
            GameFunctions.PlayerActive = true;
            transform.parent.gameObject.SetActive(false);
            foreach (GameObject go in toggleEnabled)
            {
                go.SetActive(true);
            }
            GetComponent<Image>().enabled = true;
        }
        if (isFloating)
        {
            player.position = Vector3.Lerp(playerStartPosition, floatPosition, elapsedTime / floatTime);
        }
    }
}
