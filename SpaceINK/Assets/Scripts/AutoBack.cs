using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBack : MonoBehaviour
{

    public GameObject space;
    public GameObject stars1;
    public GameObject stars2;
    public GameObject stars3;
    public GameObject dust;
    public GameObject fog;

    private Vector2 vectorOffsetStep;
    private Vector2 vectorOffset;
    //скорость, с которой будет вращаться бекграунд. Чем больше число, тем медленнее будет он двигаться.
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        vectorOffsetStep = new Vector2 (Random.Range(3f, 3f), Random.Range(3f, 3f));
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        vectorOffset = vectorOffset + vectorOffsetStep * Time.fixedDeltaTime;
        space.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / scrollSpeed);
        stars1.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / (scrollSpeed /1.2f));
        stars2.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / (scrollSpeed / 1.6f));
        stars3.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / (scrollSpeed / 1.8f));
        dust.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / (scrollSpeed / 2.0f));
        fog.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", vectorOffset / (scrollSpeed / 2.2f));
    }
}
