using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float vel;
    public Vector3 dir;

    /* Puede usar Headers para segmentar las variables en el inspector de Unity
     * Prueba a cambiar el nombre y observa como en el editor de Unity cambia el t�tulo
     */
    [Header("Gameobjects")]
    [SerializeField] GameObject[] paddles;

    /*TIP: Colocar Region ayuda a mantener su c�digo ordenado, establece regiones que cumplen con
     * una caracter�stica en especial, por ejemplo ac� coloco todos los metodos que tienen que ver
     * con callbacks de Unity
    */
    #region Monobehaviour callbacks


    /* M�todo heredado de MonoBehaviour 
     * Se ejecuta luego del m�todo Reset https://docs.unity3d.com/Manual/ExecutionOrder.html
     */
    void Start()
    {
        SetVelocity();
    }

    /* M�todo heredado de MonoBehaviour 
     * OnDrawGizmos es �til para hacer debugging de lo que se est� programando/dise�ando
     *No es necesario que ejecute la aplicaci�n pues OnDrawGizmos se llama en el editor
     *Los objetos no forman parte del juego, son solo "gu�as" que nos ayudan a desarrollar
     */
    void OnDrawGizmos()
    {
        //Top-left corner
        Gizmos.color = Color.black;
        Gizmos.DrawCube(Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)), new Vector3(1f, 1f, 1f));

        //Top-right corner
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)), new Vector3(1f, 1f, 1f));

        //Bottom-left corner
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)), new Vector3(1f, 1f, 1f));

        //Botom-right corner
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)), new Vector3(1f, 1f, 1f));
        
    }

    /* M�todo heredado de MonoBehaviour 
     * Similar al m�todo Update del game engine de c�tedra
     * Este es llamado en cada frame, existe una variante llamada FixedUpdate que 
     * es ejecutado con un ratio menor, es �til cuando se trabaja con f�sicas
     */
    void Update()
    {
        CheckWallCollision();
        CheckPaddleCollision();


        transform.position += Time.deltaTime * vel * dir;
    }
    #endregion

    # region M�todos de clase
    
    /*M�todo que verifica si la pelota toc� los bordes del viewport*/
    public void CheckWallCollision() {
        //Verificaci�n con los bordes horizontales
        if (transform.position.x > GameManager.Instance.topRight.x || transform.position.x < GameManager.Instance.topLeft.x)
        {
            dir.x *= -1;
            transform.position += Time.deltaTime * vel * dir;
        }
        
        //Verificaci�n con los bordes verticales
        if (transform.position.y > GameManager.Instance.topRight.y || transform.position.y < GameManager.Instance.bottomLeft.y)
        {
            //Si dir es negativo, significa que la pelota bajaba y toca el borde inferior
            if (dir.y < 0)
            {
                GameManager.Instance.RemoveLife();
                StartCoroutine(Reset());
            }
            //Si dir es positivo, significa que la pelota sub�a y toca el borde superior
            else
            {
                GameManager.Instance.AddPoints();
                StartCoroutine(Reset());
            }

            dir.y *= -1;
            transform.position += Time.deltaTime * vel * dir;
        }
    }

    /*Una corrutina es un m�todo que contiene lapsos de tiempo
    Por ejemplo, en este m�todo "yield return new WaitForSeconds(1);" espera 1 segundo antes de seguir con la siguiente instrucci�n
    Su s�mil al motor de c�tedra ser�a el Thread.Sleep() pero usted no puede usar esta instrucci�n en Unity
    Unity controla el hilo principal donde se ejecutan m�todos como el Update, Awake, Start, etc...
    */
    private IEnumerator Reset()
    {
        transform.position = Vector3.zero;
        dir = Vector3.zero;
        yield return new WaitForSeconds(1);
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //M�todo que verifica colisi�n entre la pelota y las paletas
    private void CheckPaddleCollision()
    {
        foreach (GameObject paddle in paddles) {
            bool isColliding = paddle.GetComponent<PaddleController>().CheckBallCollision(transform.position);
            if (isColliding) {
                dir.y *= -1;
                transform.position += Time.deltaTime * vel * dir;
            }

        }
    }

    //M�todo que cambia la velocidad de la pelota
    public void SetVelocity()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        dir = new Vector2(xDirection, yDirection).normalized;
    }
    #endregion
}
