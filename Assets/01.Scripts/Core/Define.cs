using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            _mainCam ??= Camera.main;
            return _mainCam;
        }
    }
    
}
