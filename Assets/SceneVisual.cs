using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVisual : MonoBehaviour
{
    public Material skyBoxMat1, skyBoxMat2, skyBoxMat3, skyBoxMat4, skyBoxMat5, skyBoxMat6, skyBoxMat7, skyBoxMat8, skyBoxMat9;

    public float meters;

    private void Start()
    {
       
    }

    void Update()
    {
        meters = CarController.Current.distance * 4;

        if (meters >= 500 && meters < 1000)
        {
            RenderSettings.skybox = skyBoxMat1;
        }else if(meters >=1000 && meters < 1500)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 1500 && meters < 2000)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 2000 && meters < 2500)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 2500 && meters < 3000)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 3000 && meters < 3500)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 3500 && meters < 4000)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 4000 && meters < 4500)
        {
            RenderSettings.skybox = skyBoxMat2;
        }
        else if (meters >= 4500 && meters < 5000)
        {
            RenderSettings.skybox = skyBoxMat2;
        }else if(meters >= 5000)
        {
            RenderSettings.skybox = skyBoxMat5;
        }
    }
}
