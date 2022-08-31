using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Wall : MonoBehaviour
{
    public float scroolSpeed;
    Material myMaterial;
    float newOffsetX;
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
        switch (SceneManager.GetActiveScene().name)
        {
            case "Lobby":
                newOffsetX = myMaterial.mainTextureOffset.x- 0.5f * Time.deltaTime;
                newOffset = new Vector2(newOffsetX, 0);

                myMaterial.mainTextureOffset = newOffset;
                break;
            case "Ingame":
                scroolSpeed = gameManager.Com_Obj_Speed;
                newOffsetX = myMaterial.mainTextureOffset.x - (scroolSpeed * 0.1f) * Time.deltaTime;
                newOffset = new Vector2(newOffsetX, 0);

                myMaterial.mainTextureOffset = newOffset;
                break;
        }
    }
}
