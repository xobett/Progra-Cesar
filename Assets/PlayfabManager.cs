using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    //[Header("PLAYFAB SETTINGS")]
    //[SerializeField] private string titleID = "98349";
    //[SerializeField] private string secretKey = "O5R9WIRW6HTCKR3KH8Q8FW7G85SSRQJORCKCM5ADA9FUSB9KN7";

    //[Header("INPUT SETTINGS")]
    //[SerializeField] private TMP_InputField userNameInput;
    //[SerializeField] private TMP_InputField passwordInput;

    //void Start()
    //{
    //    if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
    //    {
    //        PlayFabSettings.TitleId = titleID;
    //        PlayFabSettings.DeveloperSecretKey = secretKey;
    //    }
    //}

    //private void RegisterUser()
    //{

    //    if (string.IsNullOrEmpty(userNameInput.text) || string.IsNullOrEmpty(passwordInput.text))
    //    {
    //        Debug.LogWarning("Alguno de los campos esta vacio");
    //        return;
    //    }
    //    var request = new RegisterPlayFabUserRequest()
    //    {
    //        DisplayName = userNameInput.text, //Nombre del jugador que aparece en base de datos.
    //        Username = userNameInput.text, // Es el usuario con el que se registra e inicia sesion el jugador.
    //        Password = passwordInput.text, //Contraseña del jugador.
    //        RequireBothUsernameAndEmail = false
    //    };

    //    PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, PlayFabErrorMessage);
    //}

    //private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    //{
    //    Debug.Log("USUARIO REGISTRADO CORRECTAMENTE");
    //}

    //private void PlayFabErrorMessage(PlayFabError error)
    //{
    //    Debug.LogWarning(error.ErrorMessage);
    //}


    //// Update is called once per frame
    //void Update()
    //{

    //}
}
