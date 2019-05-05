using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private float speed= 2.0f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float cameraPositionY;


    private const float FPS_UPDATE_INTERVAL = 0.5f;
    private float fpsAccum = 0;
    private int fpsFrames = 0;
    private float fpsTimeLeft = FPS_UPDATE_INTERVAL;
    private float fps = 0;

    private void Awake()
    {
        //if (!target) target = FindObjectOfType<Character>().transform;
    }

    private void FixedUpdate()
    {
        if(target)
        {
            Vector3 position = target.position + new Vector3(0, cameraPositionY, 0);
            position.z = -15.0f;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }  
    }
    private void Update()
    {

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
    }


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(5, 5, 500, 500));
        GUILayout.Label("FPS: " + fps.ToString("f1"));
        GUILayout.EndArea();
    }
}


