using UnityEngine;


namespace Profe
{
    public class CameraController : MonoBehaviour
    {

        [Header("Camera Settings")]
        [SerializeField] private Transform player; // Objeto a rotar
        [SerializeField] private float mouseSensitivity; // Sensibilidad del mouse
        [SerializeField] private float smoothnes; // Desfase
        [SerializeField] private float minAngleY; // Minima rotacion en vertical
        [SerializeField] private float maxAngleY; // Maxima rotacion en vertical

        private Vector2 camVelocity; // Velocidad de la camara
        private Vector2 smoothVelocity; // Velocidad de los "ojos"

        [Header("Camera Movement")]
        [SerializeField] bool moveHead; // Si est� activo si sucede el movimiento de la cabeza

        [SerializeField] private float walkingSpeed; // La velocidad de la cabeza al caminar
        [SerializeField] private float runningSpeed; // La velocidad de la cabeza al correr

        [SerializeField] private float amplitude; // Que tanto se mueve
        [SerializeField] private float frequency; // Con que frecuencia se mueve
        [SerializeField] private float resetPosSpeed; // Cuanto tarda en regresar a su posicion cuando dejas de moverte

        private Vector3 startPos; // Almacena la posicion original de la cabeza

        private MovementController movementController; // Necesitamos esta referencia para saber si el personaje se est� moviendo o no

        private void Start()
        {
            movementController = FindObjectOfType<MovementController>();

            if (player == null)
            {
                player = transform.parent;
            }
            else
            {
                Debug.Log("No hay jugador");
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        private void Update()
        {
            RotateCamera();

            if (!moveHead) return;
            BlobMove();
            ResetPosition();
        }

        #region Camera Rotation

        private void RotateCamera()
        {
            Vector2 rawFrameVelocity = Vector2.Scale(MousePos(), Vector2.one * mouseSensitivity); // Consigue hacia donde muevo el mouse y lo multiplica por la sensibilidad ((5,8) * ((1,1) * 2) => (5,8) * (2,2) => (10,16)
            smoothVelocity = Vector2.Lerp(smoothVelocity, rawFrameVelocity, 1 / smoothnes); // Mueve de (0,0) a (10,16) en 1/smoothnes tiempo
            camVelocity += smoothVelocity; // camVelocity es el vector final donde almaceno hacia donde voy a voltear
            camVelocity.y = Mathf.Clamp(camVelocity.y, minAngleY, maxAngleY); // Le digo a camVelocity que solo puedo mirar hacia arriba y abajo hasta x angulo

            transform.localRotation = Quaternion.AngleAxis(-camVelocity.y, Vector3.right); // Roto la c�mara hacia arriba y abajo
            player.localRotation = Quaternion.AngleAxis(camVelocity.x, Vector3.up); //Roto la capsula o player hacia los lados
        }

        private Vector2 MousePos()
        {
            return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

        #endregion



        #region Camera Movement

        private void BlobMove()
        {
            if (!movementController.IsMoving())
            {
                return;
            }

            if (movementController.IsMoving() && !movementController.IsRunning())
            {
                transform.localPosition += FootStepMotion();
            }
            else if (movementController.IsMoving() && movementController.IsRunning())
            {
                transform.localPosition += RunningFootStepMotion();
            }
        }

        private void ResetPosition()
        {
            if (transform.localPosition == startPos) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, resetPosSpeed * Time.deltaTime);
        }

        private Vector3 FootStepMotion()
        {
            Vector3 pos = Vector3.zero;
            pos.y = Mathf.Sin(Time.time * frequency) * amplitude * walkingSpeed;
            pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2 * walkingSpeed;
            return pos;
        }

        private Vector3 RunningFootStepMotion()
        {
            Vector3 pos = Vector3.zero;
            pos.y = Mathf.Sin(Time.time * frequency) * amplitude * runningSpeed;
            pos.x = Mathf.Cos(Time.time * frequency / 2) * amplitude * 2 * runningSpeed;
            return pos;
        }

        #endregion

    }

}