using UnityEngine;

public class GroundCheckController : MonoBehaviour
{
    [SerializeField] OnGroundPlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            playerController.onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            playerController.onGround = false;
        }
    }
}
