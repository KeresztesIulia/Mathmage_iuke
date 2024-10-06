using UnityEngine;


public class F1TeleportVisualizer : MonoBehaviour
{
    [SerializeField] DropSpot r;
    [SerializeField] GameObject tooBigText;

    [Header("Images")]
    [SerializeField] GameObject circleImage;
    [SerializeField] GameObject horizontalLineImage;
    [SerializeField] GameObject verticalLineImage;
    [SerializeField] GameObject cardioidImage;
    [SerializeField] GameObject cardioidInvertedImage;
    [SerializeField] GameObject spiralImage;
    [SerializeField] GameObject spiralInvertedImage;
    [SerializeField] GameObject lemniscateImage;

    bool tooBig;
    GameFunctions.PolarCurve actualCurve;

    public GameFunctions.PolarCurve ActualCurve
    {
        get { return actualCurve; }
    }

    public bool TooBig
    {
        get { return tooBig; }
    }

    private void Update()
    {
        actualCurve = GameFunctions.PolarCurve.None;
        if (r.IsEmpty)
        {
            tooBig = false;
        }
        else if (r.ContainsDraggable)
        {
            switch (r.actualValue)
            {
                case "circleH":
                case "circleV":
                    CheckCircle();
                    break;
                case "lineH":
                    CheckLine("h");
                    break;
                case "lineV":
                    CheckLine("v");
                    break;
                case "cardioid":
                    CheckCardioid();
                    break;
                case "spiral":
                    CheckSpiral();
                    break;
                case "lemniscate":
                    CheckLemniscate();
                    break;
            }
        }
        else
        {
            try
            {
                float radius = float.Parse(r.actualValue);
                TooBigRange(radius, -3, 3, false);
                if (radius != 0) actualCurve = GameFunctions.PolarCurve.Circle;
            }
            catch
            {
                tooBig = false;
            }
        }
        ShowImage();
        tooBigText.SetActive(tooBig);
    }

    

    void TooBigRange(float number, float min, float max, bool canBeZero)
    {
        if (number < min || number > max) tooBig = true;
        else tooBig = false;
        if (!canBeZero && number == 0) tooBig = false;
    }

    private void CheckCircle()
    {
        try
        {
            float radius = float.Parse(r.ActualDraggable.GetComponentInChildren<DropSpot>().actualValue);
            TooBigRange(radius, -2, 2, false);
            if (radius != 0) actualCurve = GameFunctions.PolarCurve.Circle;
        }
        catch
        {
            tooBig = false;
        }
    }

    private void CheckLine(string lineType)
    {
        try
        {
            float position = float.Parse(r.ActualDraggable.GetComponentInChildren<DropSpot>().actualValue);
            TooBigRange(position, -3, 3, false);
            if (position != 0)
                if (lineType == "h") actualCurve = GameFunctions.PolarCurve.HorizontalLine;
                else actualCurve = GameFunctions.PolarCurve.VerticalLine;
        }
        catch
        {
            tooBig = false;
        }
    }

    private void CheckCardioid()
    {
        try
        {
            float size = float.Parse(r.ActualDraggable.GetComponentsInChildren<DropSpot>()[0].actualValue);
            bool correct = r.ActualDraggable.GetComponentsInChildren<DropSpot>()[1].ValueIsCorrect;
            if (correct)
            {
                TooBigRange(size, -2, 2, false);
                if (size < 0) actualCurve = GameFunctions.PolarCurve.InvertedCardioid;
                if (size > 0) actualCurve = GameFunctions.PolarCurve.Cardioid;
            }
            else
            {
                tooBig = false;
            }
        }
        catch
        {
            tooBig = false;
        }
    }

    private void CheckSpiral()
    {
        tooBig = false;
        try
        {
            float size = float.Parse(r.ActualDraggable.GetComponentInChildren<DropSpot>().actualValue);
            if (size < 0) actualCurve = GameFunctions.PolarCurve.InvertedSpiral;
            if (size > 0) actualCurve = GameFunctions.PolarCurve.Spiral;
        }
        catch
        {
            return;
        }
        
    }

    private void CheckLemniscate()
    {
        try
        {
            float size = float.Parse(r.ActualDraggable.GetComponentsInChildren<DropSpot>()[0].actualValue);
            bool correct = r.ActualDraggable.GetComponentsInChildren<DropSpot>()[1].ValueIsCorrect;
            if (correct)
            {
                TooBigRange(size, -3, 3, false);
                if (size != 0) actualCurve = GameFunctions.PolarCurve.Lemniscate;
            }
            else
            {
                tooBig = false;
            }
        }
        catch
        {
            tooBig = false;
        }

    }

    void ShowImage()
    {
        switch (actualCurve)
        {
            case GameFunctions.PolarCurve.None:
                DisableAllImages(); break;
            case GameFunctions.PolarCurve.Circle:
                circleImage.SetActive(true); break;
            case GameFunctions.PolarCurve.Spiral:
                spiralImage.SetActive(true); break;
            case GameFunctions.PolarCurve.InvertedSpiral:
                spiralInvertedImage.SetActive(true); break;
            case GameFunctions.PolarCurve.Cardioid:
                cardioidImage.SetActive(true); break;
            case GameFunctions.PolarCurve.InvertedCardioid:
                cardioidInvertedImage.SetActive(true); break;
            case GameFunctions.PolarCurve.HorizontalLine:
                horizontalLineImage.SetActive(true); break;
            case GameFunctions.PolarCurve.VerticalLine:
                verticalLineImage.SetActive(true); break;
            case GameFunctions.PolarCurve.Lemniscate:
                lemniscateImage.SetActive(true); break;
        }
    }

    void DisableAllImages()
    {
        circleImage.SetActive(false);
        horizontalLineImage.SetActive(false);
        verticalLineImage.SetActive(false);
        cardioidImage.SetActive(false);
        cardioidInvertedImage.SetActive(false);
        spiralImage.SetActive(false);
        spiralInvertedImage.SetActive(false);
        lemniscateImage.SetActive(false);
    }
}
