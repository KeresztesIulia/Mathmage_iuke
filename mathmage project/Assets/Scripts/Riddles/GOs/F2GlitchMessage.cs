using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F2GlitchMessage : Closeable
{
    object response = null;

    public object Response
    {
        get { return response; }
    }

    private void OnEnable()
    {
        response = null;
    }

    protected override void Close()
    {
        response = new object();
        base.Close();
    }


}
