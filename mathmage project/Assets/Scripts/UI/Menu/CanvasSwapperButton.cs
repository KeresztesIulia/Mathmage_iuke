using UnityEngine;

public class CanvasSwapperButton : ButtonScript
{
    [SerializeField] protected Canvas from;
    [SerializeField] protected Canvas to;

    public override void OnClick()
    {
        GameFunctions.SwapCanvas(from, to);
    }
}
