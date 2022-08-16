using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float scroolSpeed = 0.5f;
    Material myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetX = myMaterial.mainTextureOffset.x - scroolSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffsetX, 0);

        myMaterial.mainTextureOffset = newOffset;
    }
}
