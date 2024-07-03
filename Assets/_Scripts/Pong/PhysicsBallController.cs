using System.Collections;
using UnityEngine;

public class PhysicsBallController : MonoBehaviour
{
    public float vel;
    public Vector3 dir;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Dirección inicial aleatoria
        SetVelocity();
    }

    public void SetVelocity() {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float yDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        dir = new Vector2(xDirection, yDirection).normalized;
        rb.velocity = Time.deltaTime * vel * dir;
    }

    public IEnumerator ResetBall()
    {
        //Colocamos la pelota en el centro e igualamos su velocidad a 0
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        dir = Vector3.zero;

        //Esperamos 1 segundo
        yield return new WaitForSeconds(1);

        //Se da una velocidad aleatoria a la pelota
        SetVelocity();
    }

    /* Callback que se llama cuando un elemento que posee rigidbody2D y collider2D colisiona con otro que tiene su collider
     * marcado Is Trigger = true
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Top"))
        {
            GameManager.Instance.AddPoints();
            StartCoroutine(ResetBall());
        }
        else if (collision.CompareTag("Bottom")) {
            GameManager.Instance.RemoveLife();
            StartCoroutine(ResetBall());
        }

    }

    /* Callback que se llama cuando un elemento que posee rigidbody2D y collider2D sale de la colisión de otro gameobject
     * que tiene marcado Is Trigger = true
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    /* Callback que se llama cuando un elemento que posee rigidbody2D y collider2D se mantiene dentro de un gameobject
     * que tiene marcado Is Trigger = true
     */
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    /* Callback que se llama cuando un elemento que posee rigidbody2D y collider2D toca al collider del gameobject
     * que tiene marcado Is Trigger = true
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.SpawnParticles(transform.position);
    }
}
