using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDay;
    public float startTime;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColour;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColour;
    public AnimationCurve moonIntensity;

    [Header("Other")]
    public AnimationCurve lightIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    void Start()
    {
        timeRate = 1.0f / fullDay;
        time = startTime;
    }

    void Update()
    {
        time += timeRate * Time.deltaTime;

        if(time >= 1.0f)
        {
            time = 0.0f;
        }

        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        sun.color = sunColour.Evaluate(time);
        moon.color = moonColour.Evaluate(time);

        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(false);
        else if(sun.intensity > 0 && sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(true);
        
        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(false);
        else if(moon.intensity > 0 && moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(true);

        RenderSettings.ambientIntensity = lightIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }
}


