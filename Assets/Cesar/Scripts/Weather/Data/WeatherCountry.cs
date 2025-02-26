using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct WeatherCountry
{
    [SerializeField] public string city;
    [SerializeField] public float countryLatitude;
    [SerializeField] public float countryLongitude;
}
