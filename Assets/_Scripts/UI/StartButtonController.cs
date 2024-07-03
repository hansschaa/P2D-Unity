using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    /* ID de la escena a la cual queremos ir, usted puede usar tambien un string que sería el nombre de la escena
     * Para ver los IDS, en el editor de Unity, ir a File->Build Settings->parte superior "Scenes in build"
     * Si no ve su escena, debe agregarla con el botón en la esquina inferior del mismo panel que dice "Add Open Scenes"
     */
    [SerializeField] int sceneID;

    /*Método que colocamos en el componente Button para que sea ejecutado cuando hacemos click en el*/
    public void ChangeScene()
    {
        //Scene Manager es una clase de la biblioteca UnityEngine.SceneManagement
        //Permite manejar scenas, hacer scenas de carga, carga asincrónica, etc.
        SceneManager.LoadScene(sceneID);
    }
}
