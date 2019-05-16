using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeControllerScript : MonoBehaviour
{
    private int nScreenWidth, nScreenHeigh;
    private float _fScreenRatio;
    public float fScreenRatio
    {
        get
        {
            return _fScreenRatio;
        }
    }

    private void Awake()
    {
        nScreenWidth = Screen.width;
        nScreenHeigh = Screen.height;
        if (nScreenHeigh > nScreenWidth)
        {
            _fScreenRatio = nScreenHeigh * 1f / nScreenWidth * 1f;
        }
        else
        {
            _fScreenRatio = nScreenWidth * 1f / nScreenHeigh * 1f;
        }
    }
}
