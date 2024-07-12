using UnityEngine;

public class OnGroundPlayerController : PlayerController
{
    public bool onGround;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lookAhead != -1)
            {
                lookAhead = -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //animator.SetInteger permite avisarle al Animator que queremos cambiar de animación
            //Al cambiar a -1 la variable "velocity" del Animator, este transiciona hacia la animación de correr
            animator.SetInteger("velocity", -1);
            rb.velocity = new Vector2(velocity * Time.deltaTime * -1, rb.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //Al cambiar a 0 la variable "velocity" del Animator, este transiciona hacia la animación de idle
            animator.SetInteger("velocity", 0);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lookAhead != 1)
            {
                lookAhead = 1;

                //Invertimos la escala para que esté mirando hacia la derecha
                //Multiplicamos por -1 el eje de las X para que gire.
                //Puede hacerlo también con el flipx del componente que dibuja el sprite llamado "SpriteRenderer"
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("velocity", 1);
            rb.velocity = new Vector2(velocity * Time.deltaTime, rb.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetInteger("velocity", 0);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
