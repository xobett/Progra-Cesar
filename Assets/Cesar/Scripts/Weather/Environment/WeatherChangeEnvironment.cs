using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherChangeEnvironment : MonoBehaviour
{
    [SerializeField] private VolumeProfile volumeProfile;
    [SerializeField] private float colorTransitionSpeed;

    void Start()
    {
        StartCoroutine(ChangeWeatherColor());
    }

    void Update()
    {

    }

    public Color SetWeatherColor(float actualTemp)
    {
        Color weatherColor = Color.clear;

        switch (actualTemp)
        {
            case var color when actualTemp <= 0:
                {
                    weatherColor = new Color(0, 110, 255);
                    break;
                }

            case var color when actualTemp <= 10:
                {
                    weatherColor = Color.cyan;
                    break;
                }

            case var color when actualTemp <= 20:
                {
                    weatherColor = new Color(255, 232, 0);
                    break;
                }

            case var color when actualTemp <= 30:
                {
                    weatherColor = new Color(255, 163, 0);
                    break;
                }
        }

        return weatherColor;
    }

    private IEnumerator ChangeWeatherColor()
    {
        yield return new WaitForSeconds(3);

        volumeProfile.TryGet<Bloom>(out var bloom);

        //Color newColor = SetWeatherColor();

        float time = 0;

        while (time <1)
        {
            bloom.tint.value = Color.Lerp(bloom.tint.value, Color.cyan, time);
            time += Time.deltaTime * colorTransitionSpeed;
            yield return null;
        }
        bloom.tint.value = Color.cyan;
        Debug.Log("Color changed");
    }
}
