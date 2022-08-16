using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Stage;
    //드래곤 연결
    [Header ("Dragons")]
    public GameObject[] Dragons;

    // Start is called before the first frame update
    void Start()
    {
        Stage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Stage = ScoreManager.GetStage();

        switch (Stage % 10)
        {
            //블루
            case 0:
                Dragons[0].SetActive(true);
                break;

            //그린
            case 1:
                Dragons[1].SetActive(true);
                break;

            //핑크
            case 2:
                Dragons[2].SetActive(true);
                break;

            //퍼플
            case 3:
                Dragons[3].SetActive(true);
                break;

            //레드
            case 4:
                Dragons[4].SetActive(true);
                break;

            //옐로우
            case 5:
                Dragons[5].SetActive(true);
                break;

            //블랙
            case 6:
                Dragons[6].SetActive(true);
                break;

            //블루
            case 7:
                Dragons[0].SetActive(true);
                break;

            //그린
            case 8:
                Dragons[1].SetActive(true);
                break;

            //핑크
            case 9:
                Dragons[2].SetActive(true);
                break;
        }
    }
}
