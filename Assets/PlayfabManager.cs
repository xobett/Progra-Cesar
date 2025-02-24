using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ProfilesModels;

public class PlayfabManager : MonoBehaviour
{
    [Header("PLAYFAB SETTINGS")]
    [SerializeField] private string titleID = "98349";
    [SerializeField] private string secretKey = "O5R9WIRW6HTCKR3KH8Q8FW7G85SSRQJORCKCM5ADA9FUSB9KN7";

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        {
            PlayFabSettings.TitleId = titleID;
            PlayFabSettings.DeveloperSecretKey = secretKey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
