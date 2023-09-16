using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeLayer
{
    public Vector3 ScaleFactor = Vector3.one;
    public Vector3 RotationAngles = Vector3.zero;

  
    public enum CakeShape
    {
        Oval,
    }

    public enum CakeColor
    {
        Yellow,
        Magenta,
        Blue
    }

    public enum CakeSauce
    {
        Purple,
        Red,
        Dark
    }

    public enum CakeTop
    {
        Heart,
        Star,
        Flower
    }

    public CakeShape cakeShape;
    public CakeColor cakeColor;
    public CakeSauce cakeSauce;
    public CakeTop cakeTop;
 

    
}
