using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(WeatherAPI))]
public class CIWeatherAPI : Editor
{
    //Propiedad principal.
    private SerializedProperty countries;

    //Propiedades secundarias.
    private SerializedProperty country;
    private SerializedProperty countryLatitude;
    private SerializedProperty countryLongitude;

    private SerializedProperty weatherData;

    private SerializedProperty weatherCity;
    private SerializedProperty weatherDescription;
    private SerializedProperty actualTemp;
    private SerializedProperty windSpeed;

    private SerializedProperty weatherIsDisplayed;

    //Arreglo de booleanos para crear los foldouts.
    bool[] weatherFoldouts;

    private void OnEnable()
    {
        //Cada que se selecciona el script, se consigue la propiedad principal de este, el arreglo de paises, donde contiene toda la demas informacion.
        countries = serializedObject.FindProperty("countries");
        //El tamaño del arreglo se crea a base del tamaño del arreglo de paises.
        weatherFoldouts = new bool[countries.arraySize];

    }

    public override void OnInspectorGUI()
    {
        //Por cada pais en el arreglo de paises registrados, se imprime la informacion de cada uno.
        for (int i = 0; i < countries.arraySize; i++)
        {
            //Agarra las propiedades serializadas del pais imprimiendose, primero sus datos principales de nombre, latitud y longitud.
            country = countries.GetArrayElementAtIndex(i).FindPropertyRelative("country");
            countryLatitude = countries.GetArrayElementAtIndex(i).FindPropertyRelative("countryLatitude");
            countryLongitude = countries.GetArrayElementAtIndex(i).FindPropertyRelative("countryLongitude");

            //Despues, agarra la propiedad serializada del struct de datos de clima del pais.
            weatherData = countries.GetArrayElementAtIndex(i).FindPropertyRelative("weatherData");

            //Referenciando la propiedad seriaizada anterior, agarra los datos dentro de ella.
            weatherCity = weatherData.FindPropertyRelative("city");
            weatherDescription = weatherData.FindPropertyRelative("weatherDescription");
            actualTemp = weatherData.FindPropertyRelative("actualTemp");
            windSpeed = weatherData.FindPropertyRelative("windSpeed");

            //Agarra por ultimo un booleano que evalua si se consiguio la informacion actual del pais imprimiendose.
            weatherIsDisplayed = countries.GetArrayElementAtIndex(i).FindPropertyRelative("weatherIsDisplayed");
            
            //Imprime la el nombre, latitud y longitud del pais imprimiendose.
            EditorGUILayout.PropertyField(country, new GUIContent("Country Name"));
            EditorGUILayout.PropertyField(countryLatitude, new GUIContent("Country Latitude"));
            EditorGUILayout.PropertyField(countryLongitude, new GUIContent("Country Longitude"));

            //Se crea un foldout del pais imprimiendose, de lo contrario si se creara solo uno, todos los demas paises serian interactuados a la misma vez.
            weatherFoldouts[i] = EditorGUILayout.Foldout(weatherFoldouts[i], "Weather Info");

            if (weatherFoldouts[i])
            {
                //Si se consiguio la informacion de clima del pais imprimiendose, se imprimiran los datos de este.
                if (weatherIsDisplayed.boolValue)
                {
                    EditorGUILayout.LabelField("City", weatherCity.stringValue);
                    EditorGUILayout.LabelField("Description", weatherDescription.stringValue);
                    EditorGUILayout.LabelField("Temperature", actualTemp.floatValue.ToString());
                    EditorGUILayout.LabelField("Wind Speed", windSpeed.floatValue.ToString());
                }
                else
                {
                    //Si no se consiguio la informacion del clima del pais imprimiendose, se imprime un aviso que se intente en un minuto.
                    EditorGUILayout.LabelField("Try again in a minute.");
                }

                //Para mejor visibilidad, se agrega espacio al abrir el foldout.
                MakeSpace();
            }

            //Se agrega espacio para mejor visibilidad.
            MakeSpace();
        }
    }

    private void MakeSpace()
    {
        EditorGUILayout.Space(10);
    }
    
}

#endif
