using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [Header("Settings")]
    
    public float scrollSpeed = 3f;
    public bool isCloud = false;

    [Header("References")]
    public GameObject scrollObject;


    private MeshRenderer soMesh;

    // Start is called before the first frame update
    void Start()
    {
        soMesh = scrollObject.GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCloud)
            soMesh.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
        else
        {
            Vector3 thisVec = transform.position;
            thisVec.x -= scrollSpeed * Time.deltaTime;

            if (thisVec.x < -11f)
            {
                thisVec = new Vector3(11, Random.Range(0, 5));
            }

            transform.position = thisVec;
        }
    }
}
