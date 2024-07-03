using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    //Referencia al Box Collider de la paleta
    [SerializeField] private BoxCollider2D boxCollider;
    
    public bool isPlayer;
    public float velocity;
    public int dir;

    private void Update()
    {
        //Si somos el player movemos la paleta con las teclas direccionales
        if (isPlayer)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x - boxCollider.bounds.extents.x > GameManager.Instance.bottomLeft.x)
            {
                transform.position += Time.deltaTime * velocity * Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x + boxCollider.bounds.extents.x < GameManager.Instance.bottomRight.x)
            {
                transform.position += Time.deltaTime * velocity * Vector3.right;
            }
        }
        //Si no somos el player la paleta siempre sigue la posici�n en el eje X de la pelota
        //Asi si la pelota esta en un costado, la paleta le copiar� y tambi�n estar� en ese costado
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(GameManager.Instance.ball.position.x, transform.position.y, transform.position.z), 10f);
        }     
    }


    //Verificaci�n de la pelota sin f�sicas tal como lo hac�an en el motor de c�tedra
    //Uso boxCollider.bounds.center.x - boxCollider.bounds.extents.x para obtener los l�mites en los que tengo que verificar
    //OnDrawGysmos muestra estos l�mites en la pantalla para ayudar con el debugging
    internal bool CheckBallCollision(Vector3 position)
    {
        if (position.x > boxCollider.bounds.center.x - boxCollider.bounds.extents.x &&
            position.x < boxCollider.bounds.center.x + boxCollider.bounds.extents.x) {

            if (isPlayer)
            {
                if (position.y < boxCollider.bounds.center.y + boxCollider.bounds.extents.y) {
                    return true;
                }

            }

            else {
                if (position.y > boxCollider.bounds.center.y - boxCollider.bounds.extents.y)
                {
                    return true;
                }
            }
        }

        return false;
    }


    /* M�todo que dibuja en pantalla puntos en los corners de la paleta
     * DrawCube recibe 2 argumentos: una posici�n y un tama�o
     */
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3(boxCollider.bounds.center.x - boxCollider.bounds.extents.x,
                                    boxCollider.bounds.center.y + boxCollider.bounds.extents.y
                                    , 0), new Vector3(.1f, .1f, 1f));
        Gizmos.DrawCube(new Vector3(boxCollider.bounds.center.x + boxCollider.bounds.extents.x,
                                    boxCollider.bounds.center.y + boxCollider.bounds.extents.y
                                    , 0), new Vector3(.1f, .1f, 1f));
        Gizmos.DrawCube(new Vector3(boxCollider.bounds.center.x - boxCollider.bounds.extents.x,
                                    boxCollider.bounds.center.y - boxCollider.bounds.extents.y
                                    , 0), new Vector3(.1f, .1f, 1f));
        Gizmos.DrawCube(new Vector3(boxCollider.bounds.center.x + boxCollider.bounds.extents.x,
                                    boxCollider.bounds.center.y - boxCollider.bounds.extents.y
                                    , 0), new Vector3(.1f, .1f, 1f));
    }
}
