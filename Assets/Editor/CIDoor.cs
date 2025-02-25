using Codice.Client.Common.GameUI;
using Profe;
using UnityEditor;
using UnityEngine; // Contiene todo lo que necesitamos para modificar el editor de unity

#if UNITY_EDITOR // Indica que solo se compila si estas en el editor de unity

[CustomEditor(typeof(Door))] // Este script es de custom editor, y modificara los scripts de tipo Door
public class CIDoor : Editor
{
    private Door _door; // Esta referencia la vamos a usar para nosotros poder escribir las variables que existen en el script de puerta
    private string descripcion;

    private SerializedProperty tipoDePuerta;

    private SerializedProperty eventoActivado;

    private SerializedProperty key;

    private SerializedProperty keys;

    private SerializedProperty slideDoorSpeed;
    private SerializedProperty isAutomatic;
    private SerializedProperty doorIsUp;
    private SerializedProperty slideDownPos;
    private SerializedProperty slideUpPos;

    private SerializedProperty inventoryHandler;

    private void OnEnable() // OnEnable se ejecuta cuando se activa un objeto en la escena // Se activa, al tener ese script en el inspector, exclusivamente de el objeto seleccionado
    {
        _door = (Door)target;

        tipoDePuerta = serializedObject.FindProperty("tipoDePuerta");

        eventoActivado = serializedObject.FindProperty("eventoActivado");

        key = serializedObject.FindProperty("key");

        keys = serializedObject.FindProperty("keys");

        slideDoorSpeed = serializedObject.FindProperty("slideDoorSpeed");
        isAutomatic = serializedObject.FindProperty("isAutomatic");
        doorIsUp = serializedObject.FindProperty("doorIsUp");
        slideDownPos = serializedObject.FindProperty("slideDownPos");
        slideUpPos = serializedObject.FindProperty("slideUpPos");

    }

    public override void OnInspectorGUI() // Este metodo sobreescribe TODOS los valores de el inspector 
    {
        serializedObject.UpdateIfRequiredOrScript();

        _door.tipoDePuerta = (TipoDePuerta)EditorGUILayout.EnumPopup("Door Type", _door.tipoDePuerta);
        EditorGUILayout.LabelField(descripcion);

        switch (_door.tipoDePuerta)
        {
            case TipoDePuerta.DeLlave:
                {
                    MakeSpace();
                    descripcion = "Esta puerta requiere de una llave para abrirse. Arrastra un objeto de tipo Item al campo";
                    _door.key = EditorGUILayout.ObjectField("Llave requerida", _door.key, typeof(SOItem), false) as SOItem;
                    break;
                }

            case TipoDePuerta.Evento:
                {
                    MakeSpace();
                    descripcion = "Esta puerta requiere que se cumpla un evento para poderse abrir";
                    _door.eventoActivado = EditorGUILayout.Toggle("Se puede abrir?", _door.eventoActivado);
                    break;
                }

            case TipoDePuerta.MultiplesLlaves:
                {
                    MakeSpace();
                    descripcion = "Esta puerta requiere de multiples llaves para abrirse. Indica en el arreglo cuantas llaves necesita y cuales";
                    EditorGUILayout.PropertyField(keys, new GUIContent("Keys needed to unlock door"));
                    break;
                }

            case TipoDePuerta.Automatica:
                {
                    MakeSpace();
                    descripcion = "Esta puerta se abre cuando te acercas";
                    EditorGUILayout.PropertyField(isAutomatic, new GUIContent("Door is automatic"));
                    MakeSpace();
                    EditorGUILayout.PropertyField(slideDoorSpeed, new GUIContent("Slide door speed"));
                    break;
                }

            case TipoDePuerta.Normal:
                {
                    descripcion = "Esta puerta se abre manualmente";
                    break;
                }

        }

        serializedObject.ApplyModifiedProperties();
    }

    public void MakeSpace()
    {
        EditorGUILayout.Space(5);
    }

}

#endif