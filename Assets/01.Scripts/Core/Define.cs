using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType{
    None,
    Health,
    Ammo,
    Coin
}
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
