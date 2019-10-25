using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private float speed= 2.0f;
    private GameObject target;
    [SerializeField]
    private float cameraPositionY;
    [SerializeField]
    private float cameraPositionZ;

    public float defaultPositionZ; 
    private GameObject controlPanel;
    private GameObject gameOverPanel; 

    public float dieTime = 8;
    private float myTime = 0;


    private const float FPS_UPDATE_INTERVAL = 0.5f;
    private float fpsAccum = 0;
    private int fpsFrames = 0;
    private float fpsTimeLeft = FPS_UPDATE_INTERVAL;
    private float fps = 0;

    public float time = 0;

    private void Awake()
    {
        //if (!target) target = FindObjectOfType<Character>().transform;
        controlPanel = GameObject.Find("Canvas").gameObject.transform.Find("PanelControl").gameObject;
        gameOverPanel = GameObject.Find("Canvas").gameObject.transform.Find("GameOverPanel").gameObject;
        target = GameObject.FindGameObjectWithTag("Player");
        //
    }

    private void FixedUpdate()
    {
        if(target)
        {
            Vector3 position = target.transform.position + target.transform.up * target.GetComponent<Rigidbody2D>().velocity.magnitude * 0.8f + new Vector3(0, cameraPositionY, -15);
            if (target.GetComponent<PlayerShip>().shipState == Unit.State.Die)
            {
                controlPanel.SetActive(false);
                if (myTime <= dieTime)
                {
                    myTime += Time.deltaTime;
                    this.GetComponent<Camera>().orthographicSize = cameraPositionZ - (myTime * 4);
                }
                else
                {
                    gameOverPanel.GetComponent<MoneyEndGame>().UpdateMoneyEnd();
                    gameOverPanel.SetActive(true);
                    Time.timeScale = 0;
                }
                this.transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0, cameraPositionY, -15), speed * Time.deltaTime * 2);
            }
            else
            {
                //отдаляем или приближаем камеру в зависимосьт от скорости коробля
                cameraPositionZ = Mathf.Lerp(cameraPositionZ, defaultPositionZ + (target.GetComponent<Rigidbody2D>().velocity.magnitude * 0.5f), Time.deltaTime);
                this.GetComponent<Camera>().orthographicSize = cameraPositionZ;

                this.transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
            } 
        }  
    }
    private void Update()
    {
        /*
        fpsTimeLeft -= Time.deltaTime;
        fpsAccum += Time.timeScale / Time.deltaTime;
        fpsFrames++;

        if (fpsTimeLeft <= 0)
        {
            fps = fpsAccum / fpsFrames;
            fpsTimeLeft = FPS_UPDATE_INTERVAL;
            fpsAccum = 0;
            fpsFrames = 0;
        }
        time += Time.deltaTime;
        */
    }


    void OnGUI()
    {
        /*
        GUILayout.BeginArea(new Rect(5, 5, 500, 500));
        GUILayout.Label("FPS: " + fps.ToString("f1"));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(5, 20, 500, 500));
        GUILayout.Label("Time: " + time.ToString("f1"));
        GUILayout.EndArea();
        */
    }
}


