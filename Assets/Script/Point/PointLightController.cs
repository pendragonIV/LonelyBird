
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PointLightController : MonoBehaviour
{
    [SerializeField]
    private Light2D light2D;
    private float falloffValue;
    private bool isMax;
    private static FieldInfo m_FalloffField = typeof(Light2D).GetField("m_FalloffIntensity", BindingFlags.NonPublic | BindingFlags.Instance);

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        isMax = false;
        falloffValue = 0.7f;
        SetFalloff(0.7f);
    }

    void Update()
    {
        if (!isMax)
        {
            falloffValue += 0.2f * Time.deltaTime;
            if (falloffValue >= 0.8f) isMax = true;
            SetFalloff(falloffValue);
        }
        else
        {
            falloffValue -= 0.2f * Time.deltaTime;
            if (falloffValue <= 0.6f) isMax = false;
            SetFalloff(falloffValue);
        }


    }

    public void SetFalloff(float falloff)
    {
        m_FalloffField.SetValue(light2D, falloff);
    }
}
