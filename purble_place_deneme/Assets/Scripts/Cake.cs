using UnityEngine;
using System.Collections.Generic;

public class Cake
{
    public List<CakeLayer> cakeLayers;

    public int currentCakeLayer = 0;
    public int sampleCakeLace ;
    public int currCakeLace ;

    public Cake()
    {
        cakeLayers = new List<CakeLayer>();
    }

    public void ChangeShape()
    {
        if (cakeLayers.Count <= currentCakeLayer)
        {
            cakeLayers.Add(new CakeLayer());
        }

        cakeLayers[currentCakeLayer].cakeShape =CakeLayer.CakeShape.Oval;
      //  Debug.LogError(cakeLayers[currentCakeLayer].cakeShape.ToString());

    }
    public void ChangeColor(int tür)
    {
            cakeLayers[currentCakeLayer].cakeColor = (CakeLayer.CakeColor)tür;
       
     
    }


    public void ChangeSauce(int tür)
    {
        cakeLayers[currentCakeLayer].cakeSauce = (CakeLayer.CakeSauce)tür;
    }

    public void ChangeTopping(int tür)
    {
        cakeLayers[currentCakeLayer].cakeTop = (CakeLayer.CakeTop)tür;
    }


    public void InitializeCake()
    {
        if (cakeLayers.Count == 0)
        {
            GenerateRandomCake(2);
        }
    }

    private void GenerateRandomCake(int layerCount)
    {
        for (int i = 0; i < layerCount; i++)
        {
            CakeLayer L = new CakeLayer();
            L.cakeShape = (CakeLayer.CakeShape.Oval);
          
            L.cakeColor = (CakeLayer.CakeColor)Random.Range(0, 3);
            L.cakeSauce = (CakeLayer.CakeSauce)Random.Range(0, 3);
            L.cakeTop = (CakeLayer.CakeTop)Random.Range(0, 3);

            L.ScaleFactor = new Vector3(0.66f, 1.3f,0.8f);

            L.RotationAngles = new Vector3(0f, 0f, 0f);

            cakeLayers.Add(L);
        }
    }

  
}
