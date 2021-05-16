using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform[] backGrounds;
    public float fParallax = 0.4f;
    public float layerFraction = 5f;
    public float fSmooth = 5f;
    Transform cam;
    Vector3 previousCamPos;
    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    private void Start()
    {
        previousCamPos = cam.position;
    }
    // Update is called once per frame
    void Update()
    {
        float fParrllaxX = (previousCamPos.x - cam.position.x) * fParallax; 
        for(int i = 0; i< backGrounds.Length;i++)
        {
            float fNewX = backGrounds[i].position.x + fParrllaxX * (1 + i * layerFraction);
            Vector3 newPos = new Vector3(fNewX, backGrounds[i].position.y, backGrounds[i].position.x);
            backGrounds[i].position = Vector3.Lerp(backGrounds[i].position, newPos,fSmooth*Time.deltaTime);
        }
        previousCamPos = cam.position;
    }
}
