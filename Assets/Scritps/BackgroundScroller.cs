using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;
    void Start()
    {
        myMaterial = gameObject.GetComponent<Renderer>().material;
        offSet = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
