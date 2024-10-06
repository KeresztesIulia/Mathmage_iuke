
using UnityEngine;

public class F3WindowShutters : RiddleObject
{
    [SerializeField] Transform leftShutter;
    [SerializeField] Transform rightShutter;
    [SerializeField] Transform step;
    [SerializeField] Vector3 stepStartPosition;

    F3OpenWindowRiddle openRiddle;

    protected override void Start()
    {
        base.Start();
        openRiddle = riddle.GetComponent<F3OpenWindowRiddle>();
        if (openRiddle == null) throw new System.Exception("Not the right riddle!");
    }

    private void Update()
    {
        //if (openRiddle.ValueChanged) - why doesn't it work
        //{
        float a = openRiddle.A;
        float b = openRiddle.B;
        float length = Mathf.Abs(b - a);
        float shutterScale = Mathf.Clamp01(1 - 0.5f * length);
        leftShutter.localScale = new Vector3(shutterScale, 1, 1);
        rightShutter.localScale = new Vector3(shutterScale, 1, 1);

        //step.localPosition = stepStartPosition;
        //step.Translate(new Vector3(-Mathf.Min(a, b) * 0.13f, 0, 0), Space.Self); //DEBUG!!! not a debug code, but needs to be changed in final with the correct directions and sizes!

        //step.localScale = new Vector3(length, 1, 1);
        //}
    }
}