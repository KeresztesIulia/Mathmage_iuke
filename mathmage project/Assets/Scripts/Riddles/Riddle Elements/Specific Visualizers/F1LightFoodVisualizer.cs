using UnityEngine;

public class F1LightFoodVisualizer : MonoBehaviour, IRiddleVisualizer
{
    [SerializeField] GameObject lightEffect;
    [SerializeField] RectTransform lightCircle;
    RectTransform circleBorder;
    RectTransform colorCircle;
    Vector2 startPosition;
    const float unitSize = 22.5f;
    const float borderWidth = 3;

    [Header("Dropspots")]
    [SerializeField] DropSpot xa;
    [SerializeField] DropSpot xb_cos;
    [SerializeField] DropSpot xcos_b;
    [SerializeField] DropSpot ya;
    [SerializeField] DropSpot yb_sin;
    [SerializeField] DropSpot ysin_b;

    string XA = "";
    string XB_COS = "";
    string XCOS_B = "";
    string YA = "";
    string YB_SIN = "";
    string YSIN_B = "";

    public bool CanBeVisualized
    {
        get
        {
            if (((XB_COS == "cos" || XCOS_B == "cos") && (YB_SIN == "sin" || YSIN_B == "sin")) &&
                ((isPI(XA) || isPI(XA, true) || !xa.ContainsDraggable) && (isPI(YA) || isPI(YA, true) || !ya.ContainsDraggable)) &&
                ((isPI(XB_COS) || isPI(XB_COS, true) || !xb_cos.ContainsDraggable) || (isPI(XCOS_B) || isPI(XCOS_B, true) || !xcos_b.ContainsDraggable)) &&
                ((isPI(YB_SIN) || isPI(YB_SIN, true) || !yb_sin.ContainsDraggable) || (isPI(YSIN_B) || isPI(YSIN_B, true) || !ysin_b.ContainsDraggable)))
            {
                return true;
            }
            return false;
        }
    }

    bool shouldUpdate
    {
        get
        {
            if (XA != xa.actualValue && !dotOrMinus(xa.actualValue))
            {
                XA = xa.actualValue;
                return true;
            }
            if (XB_COS != xb_cos.actualValue && !dotOrMinus(xb_cos.actualValue))
            {
                XB_COS = xb_cos.actualValue;
                return true;
            }
            if (XCOS_B != xcos_b.actualValue && !dotOrMinus(xcos_b.actualValue))
            {
                XCOS_B = xcos_b.actualValue;
                return true;
            }
            if (YA != ya.actualValue && !dotOrMinus(ya.actualValue))
            {
                YA = ya.actualValue;
                return true;
            }
            if (YB_SIN != yb_sin.actualValue && !dotOrMinus(yb_sin.actualValue))
            {
                YB_SIN = yb_sin.actualValue;
                return true;
            }
            if (YSIN_B != ysin_b.actualValue && !dotOrMinus(ysin_b.actualValue))
            {
                YSIN_B = ysin_b.actualValue;
                return true;
            }
            return false;
        }
    }

    bool isPI(string value, bool twopi = false)
    {
        if (twopi)
        {
            if (value == "2pi" || value == "6.28") return true;
        }
        else
        {
            if (value == "pi" || value == "3.14") return true;
        }
        return false;
    }

    bool dotOrMinus(string value)
    {
        return (value == "." || value == "-");
    }

    float Numberize(string value)
    {
        if (value == "2pi") return 6.48f;
        if (value == "pi") return 3.14f;
        return float.Parse(value);
    }

    public void UpdateVisuals()
    {
        if (CanBeVisualized)
        {
            SetCenter();
            bool setActive = ChangeSize();
            lightCircle.gameObject.SetActive(setActive);
            lightEffect.SetActive(setActive);
        }
        else
        {
            lightCircle.gameObject.SetActive(false);
            lightEffect.SetActive(false);
        }
    }

    void SetCenter()
    {
        float centerX = Numberize(XA);
        float centerY = Numberize(YA);
        Vector3 newPosition = startPosition;
        newPosition.x += centerX * unitSize;
        newPosition.y += centerY * unitSize;
        lightCircle.anchoredPosition = newPosition;
    }
    bool ChangeSize()
    {
        float radiusX;
        float radiusY;
        if (XB_COS == "cos")
        {
            radiusX = Numberize(XCOS_B);
        }
        else
        {
            radiusX = Numberize(XB_COS);
        }
        if (YB_SIN == "sin")
        {
            radiusY = Numberize(YSIN_B);
        }
        else
        {
            radiusY = Numberize(YB_SIN);
        }

        if (radiusX <= 0 || radiusY <= 0)
        {
            return false;

        }
        circleBorder.sizeDelta = new Vector2(2 * radiusX * unitSize, 2 * radiusY * unitSize);
        colorCircle.sizeDelta = new Vector2(2 * radiusX * unitSize - borderWidth, 2 * radiusY * unitSize - borderWidth);
        return true;
    }

    void Start()
    {
        if (lightCircle == null)
        {
            throw new System.Exception();
        }
        circleBorder = lightCircle.GetChild(0).GetComponent<RectTransform>();
        colorCircle = lightCircle.GetChild(1).GetComponent<RectTransform>();
        startPosition = lightCircle.anchoredPosition;
    }

    void Update()
    {
        if (shouldUpdate)
        {
            UpdateVisuals();
        }   
    }
}
