using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DepthLighting : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D hookLight;

    public float bright = 1f;
    public float dark = 0f;

    public float hookLightMin = 0f;
    public float hookLightMax = 2f;

    public float startDepth;
    public float fullDepth;

    void Update()
    {
        float current = FishingGearManager.Instance.currentDepth;

        if (current <= startDepth)
        {
            globalLight.intensity = bright;
            hookLight.intensity = hookLightMin;
        }
        else if (current >= fullDepth)
        {
            globalLight.intensity = dark;
            hookLight.intensity = hookLightMax;
        }
        else
        {
            float t = Mathf.InverseLerp(startDepth, fullDepth, current);

            globalLight.intensity = Mathf.Lerp(bright, dark, t);
            hookLight.intensity = Mathf.Lerp(hookLightMin, hookLightMax, t);
        }
    }
}