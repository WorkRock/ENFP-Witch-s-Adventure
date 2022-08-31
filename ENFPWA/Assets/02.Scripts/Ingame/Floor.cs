using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
    public float scroolSpeed;
    Material myMaterial;
    float newOffsetY;
    Vector2 newOffset;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Lobby":
                newOffsetY = myMaterial.mainTextureOffset.y + 0.5f * Time.deltaTime;
                newOffset = new Vector2(0, newOffsetY);

                myMaterial.mainTextureOffset = newOffset;
                break;
            case "Ingame":
                scroolSpeed = gameManager.Com_Obj_Speed;
                newOffsetY = myMaterial.mainTextureOffset.y + (scroolSpeed*0.1f) * Time.deltaTime;
                newOffset = new Vector2(0, newOffsetY);

                myMaterial.mainTextureOffset = newOffset;
                break;
        }

    }
}
