using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [Header("PLAYFAB SETTINGS")]
    //String donde se almacena la el ID del proyecto de PlayFab a ingresar.
    [SerializeField] private string titleID = "98349";
    //String donde se almacena la llave secreta del proyecto de PlayFab a ingresar.
    [SerializeField] private string secretKey = "O5R9WIRW6HTCKR3KH8Q8FW7G85SSRQJORCKCM5ADA9FUSB9KN7";

    [Header("CREATE ACCOUNT INPUT SETTINGS")]
    //Text Input para almacenar los datos de creacion de usuario.
    [SerializeField] private TMP_InputField createUserNameInput;
    //Text Input para almacenar los datos de creacion de contraseña.
    [SerializeField] private TMP_InputField createPasswordInput;

    [Header("LOG IN ACCOUNT INPUT SETTINGS")]
    //Text Input para almacenar los datos de ingreso de usuario.
    [SerializeField] private TMP_InputField logInUserNameInput;
    //Text Input para almacenar los datos de ingreso de contraseña.
    [SerializeField] private TMP_InputField logInPasswordInput;

    [Header("PANEL SETTINGS")]
    [SerializeField] private GameObject userPanels;

    [Header("HUD DISPLAY SETTINGS")]
    [SerializeField] private TextMeshProUGUI usernameDisplay;
    [SerializeField] private Image uiImage;

    void Start()
    {
        //Si los ajustes de PlayFab del proyecto son nulos, se asigna el ID del titulo y la llave del proyecto a los ajustes.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        {
            PlayFabSettings.TitleId = titleID;
            PlayFabSettings.DeveloperSecretKey = secretKey;
        }
    }

    public void RegisterUser()
    {
        //Evita seguir si no hay informacion en los inputs de crear usuario
        if (string.IsNullOrEmpty(createUserNameInput.text) || string.IsNullOrEmpty(createPasswordInput.text))
        {
            Debug.LogWarning("Alguno de los campos esta vacio");
            return;
        }

        //Crea una variable donde almacena una solicitud para crear un usuario.
        var request = new RegisterPlayFabUserRequest()
        {
            DisplayName = createUserNameInput.text, //Nombre del jugador que aparece en base de datos.
            Username = createUserNameInput.text, // Es el usuario con el que se registra e inicia sesion el jugador.
            Password = createPasswordInput.text, //Contraseña del jugador.
            RequireBothUsernameAndEmail = false //No requiere email del jugador.
        };

        //Se comunica con PlayFab y manda la solicitud para crear un usuario, dando como parametros dos acciones donde se almacenaran los resultados.
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, PlayFabErrorMessage);
    }

    public void LogInUser()
    {
        //Evita seguir si no hay informacion en los inputs de log in de usuario.
        if (string.IsNullOrEmpty(logInUserNameInput.text) || string.IsNullOrEmpty(logInPasswordInput.text))
        {
            Debug.LogWarning("Alguno de los campos esta vacio");
            return;
        }

        //Crea una variable donde almacena una solicitud para ingresar a un usuario.
        var logInRequest = new LoginWithPlayFabRequest()
        {
            Username = logInUserNameInput.text, // Es el usuario con el que se registra e inicia sesion el jugador.
            Password = logInPasswordInput.text, //Contraseña del jugador.
        };

        //Se comunica con PlayFab y manda la solicitud para ingresar a un usuario, dando como parametros dos acciones donde se almacenaran los resultados.
        PlayFabClientAPI.LoginWithPlayFab(logInRequest, OnLogInResult, PlayFabErrorMessage);
    }

    //Metodo a donde se regresa el resultado del ingreso del jugador a PlayFab. 
    private void OnLogInResult(LoginResult logInResult)
    {
        Debug.Log("USUARIO INGRESO CORRECTAMENTE");

        GetPlayerProfile();
    }

    public void GetPlayerProfile()
    {
        //Crea una variable donde se almacena una solicitud para obtener los datos del jugador.
        var request = new GetPlayerProfileRequest()
        {
            //Permite al jugador obtener el nombre de usuario y URL del avatar del usuario.
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true,
                ShowAvatarUrl = true
            }
        };

        //Se comunica con PlayFab y manda la solicitud para obtener los datos del jugador que inicio sesion.
        PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfile, PlayFabErrorMessage);
    } 

    private void OnGetPlayerProfile(GetPlayerProfileResult result)
    {
        //Desactiva los paneles de ingreso y creacion de cuenta.
        userPanels.SetActive(false);

        //Actualiza el texto del HUD con el nombre del usuario.
        usernameDisplay.text = $"Username : {result.PlayerProfile.DisplayName}";

        //Obtiene el URL de la imagen del avatar del jugador.
        string avatarUrl = result.PlayerProfile.AvatarUrl;
        StartCoroutine(ShowAvatar(avatarUrl));
    }


    //Metodo a donde se regresa el resultado del registro del jugador a PlayFab.
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("USUARIO REGISTRADO CORRECTAMENTE");
    }

    //Metodo a donde se regresa un error de PlayFab.
    private void PlayFabErrorMessage(PlayFabError error)
    {
        Debug.LogWarning(error.ErrorMessage);
    }

    private IEnumerator ShowAvatar(string avatarUrl)
    {
        //Se crea una solicitud a la web donde se consigue una textura.
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(avatarUrl);

        //Se espera hasta que se obtenga la respuesta de la solicitud.
        yield return webRequest.SendWebRequest();

        //Si la solicitud fue un exito
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            //La informacion descargada de la web manejada por el download handler, se convierte a un handler que descarga la informacion a forma de textura.
            //Posteriormente, la textura descargada se almacena en una Textura 2D.
            Texture2D avatarTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            //A causa de que una Textura2D no se puede convertir a una Sprite, se crea una Sprite a base de la Textura.
            Sprite avatarImage = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2());
            //Se asigna a la imagen del UI la sprite creada.
            uiImage.sprite = avatarImage;
            //Para evitar alargamiento de la imagen, se activa una propiedad que trata de preservar el aspecto original.
            uiImage.preserveAspect = true;
        }
        else
        {
            //Si la solicitud fue un error, muestra la informacion del error.
            Debug.Log(webRequest.error);
        }
    }
}
