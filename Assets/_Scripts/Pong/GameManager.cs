using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*Investigar: Patrón Singleton*/
    public static GameManager Instance;

    //Corners del viewport, la clase Vector2 es como la clase PointF del motor de cátedra
    [Header("Corners")]
    public Vector2 topLeft;
    public Vector2 topRight;
    public Vector2 bottomLeft;
    public Vector2 bottomRight;

    //Variables de juego
    public int lifes;
    public int points;

    //Referencia al prefab (Gameobject prefabricado)
    public GameObject particles;

    //Referencia a la pelota que está en la escena de juego
    public Transform ball;

    /*On Enable es llamado cuando el gameobject se activa*/
    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        print("On Enable");
    }

    /*On Disable es llamado cuando el gameobject se desactiva*/
    private void OnDisable()
    {
        print("On Disable");
    }

    /*Awake es antes de Start incluso antes de que la escena se ejecute*/
    private void Awake()
    {
        print("On Awake");
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);
    }

    /*On destroy es llamado cuando destruyes el gameobject o el componente (en este caso, este script)*/
    private void OnDestroy()
    {
        print("On Destroy");
    }

    // Start is called before the first frame update
    void Start()
    {
        print("Start");
        SetCorners();

        HUDManager.instance.UpdateLifesText(lifes);
        HUDManager.instance.UpdatePointsText(points);
    }

    // Update is called once per frame
    void Update()
    {
        //print("Update");
    }

    /*Corners guardados en variables de tipo Vector2 para limitar el movimiento de la pelota
    * El método ViewportToWorldPoint transforma las coordenadas del viewport en coordenadas de mundo
    */
    public void SetCorners()
    {
        topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
    }

    /*Remover una vida y llamar al Manager de UI para que actualice el texto en pantalla*/
    internal void RemoveLife()
    {
        lifes-=1;
        HUDManager.instance.UpdateLifesText(lifes);
    }

    /*Agregar puntos y llamar al Manager de UI para que actualice el texto en pantalla*/
    internal void AddPoints()
    {
        points++;
        HUDManager.instance.UpdatePointsText(points);
    }

    /*Este método hace spawn de las partículas, usted puede ver en la carpeta Prefabs que hay un objeto creado
     * En Unity, tiene la opción de preconfigurar gameobjects e instanciarlos en tiempo de ejecución
     */
    public void SpawnParticles(Vector3 position) {

        //Instantiate permite instanciar gameobjects en el mundo con una posición y rotación
        //Use Quaternion.identity, esta es la matriz identidad para la rotación, rotación = 0 en todos los ejes.
        GameObject particlesInstance = Instantiate(particles, position, Quaternion.identity);

        //Destruir el objeto de las partículas en 2 segundos
        Destroy(particlesInstance, 2 );
    }
}
