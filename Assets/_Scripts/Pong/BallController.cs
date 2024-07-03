using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float vel;
    public Vector3 dir;

    /* Puede usar Headers para segmentar las variables en el inspector de Unity
     * Prueba a cambiar el nombre y observa como en el editor de Unity cambia el título
     */
    [Header("Gameobjects")]
    [SerializeField] GameObject[] paddles;

    /*TIP: Colocar Region ayuda a mantener su código ordenado, establece regiones que cumplen con
     * una característica en especial, por ejemplo acá coloco todos los metodos que tienen que ver
     * con callbacks de Unity
    */
    #region Monobehaviour callbacks


    /* Método heredado de MonoBehaviour 
     * Se ejecuta luego del método Reset https://docs.unity3d.com/Manual/ExecutionOrder.html
     */
    void Start()
    {
        SetVelocity();
    }

    /* Método heredado de MonoBehaviour 
     * OnDrawGizmos es útil para hacer debugging de lo que se está programando/diseñando
     *No es necesario que ejecute la aplicación pues OnDrawGizmos se llama en el editor
     *Los objetos no forman parte del juego, son solo "guías" que nos ayudan a desarrollar
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

    /* Método heredado de MonoBehaviour 
     * Similar al método Update del game engine de cátedra
     * Este es llamado en cada frame, existe una variante llamada FixedUpdate que 
     * es ejecutado con un ratio menor, es útil cuando se trabaja con físicas
     */
    void Update()
    {
        CheckWallCollision();
        CheckPaddleCollision();


        transform.position += Time.deltaTime * vel * dir;
    }
    #endregion

    # region Métodos de clase
    
    /*Método que verifica si la pelota tocó los bordes del viewport*/
    public void CheckWallCollision() {
        //Verificación con los bordes horizontales
        if (transform.position.x > GameManager.Instance.topRight.x || transform.position.x < GameManager.Instance.topLeft.x)
        {
            dir.x *= -1;
            transform.position += Time.deltaTime * vel * dir;
        }
        
        //Verificación con los bordes verticales
        if (transform.position.y > GameManager.Instance.topRight.y || transform.position.y < GameManager.Instance.bottomLeft.y)
        {
            //Si dir es negativo, significa que la pelota bajaba y toca el borde inferior
            if (dir.y < 0)
            {
                GameManager.Instance.RemoveLife();
                StartCoroutine(Reset());
            }
            //Si dir es positivo, significa que la pelota subía y toca el borde superior
            else
            {
                GameManager.Instance.AddPoints();
                StartCoroutine(Reset());
            }

            dir.y *= -1;
            transform.position += Time.deltaTime * vel * dir;
        }
    }

    /*Una corrutina es un método que contiene lapsos de tiempo
    Por ejemplo, en este método "yield return new WaitForSeconds(1);" espera 1 segundo antes de seguir con la siguiente instrucción
    Su símil al motor de cátedra sería el Thread.Sleep() pero usted no puede usar esta instrucción en Unity
    Unity controla el hilo principal donde se ejecutan métodos como el Update, Awake, Start, etc...
    */
    private IEnumerator Reset()
    {
        transform.position = Vector3.zero;
        dir = Vector3.zero;
        yield return new WaitForSeconds(1);
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //Método que verifica colisión entre la pelota y las paletas
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

    //Método que cambia la velocidad de la pelota
    public void SetVelocity()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        dir = new Vector2(xDirection, yDirection).normalized;
    }
    #endregion
}
