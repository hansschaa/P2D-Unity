using UnityEngine;

public class RaycastOnGroundPlayerController : OnGroundPlayerController
{
    [SerializeField] private int raycastDistance;
    [SerializeField] private LayerMask layer;

    private void FixedUpdate()
    {
        // Raycast con dirección (0,-1) y distancia "raycastDistance" verificando la colisión con la capa de suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastDistance, layer);
        
        // Si toca suelo
        if (hit.collider != null)
        {
            onGround = true;
        }
        else
            onGround = false;
    }

    private void OnDrawGizmos()
    {
        // Dibujar una linea que permite hacer debug del raycast de la linea 11
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position  - Vector3.up* raycastDistance);
    }
}
