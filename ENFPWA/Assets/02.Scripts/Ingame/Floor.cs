using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
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
        float newOffsetY = myMaterial.mainTextureOffset.y + scroolSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;
    }
}
