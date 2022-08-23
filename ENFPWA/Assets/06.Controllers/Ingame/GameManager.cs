using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Stage;
    public bool isDragonDie;

    [Space(10f)]
    [Header("UI")]
    public GameObject Ready_Img;
    public GameObject Go_Img;

    [Space(10f)]
    [Header("Instances")]
    public ObjectManeger objectManeger;
    public Player_Move player;
    
    //드래곤 연결
    [Header ("Dragons")]
    public GameObject[] Dragons;

    [Space(10f)]
    [Header("Com Obj Speed")]
    public float Com_Obj_Speed;

    [Space(5f)]
    public float Com_Obj_Speed_BasicDef;        //5
    public float Com_Obj_Speed_BasicPlus;       //0
    [Space(5f)]
    public float Com_Obj_Speed_EditDef;         //0
    public float Com_Obj_Speed_EditPlus;        //1.5
    [Space(5f)]
    public int Com_Obj_Speed_BasicCorStage;   //1
    public int Com_Obj_Speed_EditCorStage;    //10
    [Space(5f)]
    public float Com_Obj_Speed_Max;             //15

    [Space(10f)]
    [Header("Com Obj Atk")]
    public float Com_Obj_Atk;

    [Space(5f)]
    public float Com_Obj_Atk_BasicDef;          //40
    public float Com_Obj_Atk_BasicPlus;         //2.5
    [Space(5f)]
    public float Com_Obj_Atk_EditDef;           //0
    public float Com_Obj_Atk_EditPlus;          //20
    [Space(5f)]
    public int Com_Obj_Atk_BasicCorStage;     //1
    public int Com_Obj_Atk_EditCorStage;      //10
    [Space(5f)]
    public float Com_Obj_Atk_Max;               //99999

    [Space(10f)]
    [Header("Com Obj Delay")]
    public float Com_Obj_Delay;

    [Space(5f)]
    public float Com_Obj_Delay_BasicDef;        //6
    public float Com_Obj_Delay_BasicPlus;       //-0.1
    [Space(5f)]
    public float Com_Obj_Delay_EditDef;         //0
    public float Com_Obj_Delay_EditPlus;        //-0.5
    [Space(5f)]
    public int Com_Obj_Delay_BasicCorStage;   //1
    public int Com_Obj_Delay_EditCorStage;    //10
    [Space(5f)]
    public float Com_Obj_Delay_Max;             //1

    [Space(10f)]
    [Header("Fdt")]
    public float fdt_Start;
    private bool startEnd = false;
    public float fdt;

    [Space(10f)]
    [Header("SpawnPoints")]
    public Transform[] SpawnPoints;

    private GameObject newObstacle;
    private GameObject newDragonBall;

    //장애물, 드래곤볼의 랜덤 생성 위치
    private int ranPos_Obs;
    private int ranPos_Ball;
    //랜덤 드래곤볼
    private int ranBall;
    //생성되는 오브젝트들의 방향
    private Vector3 dirVec_Obs;
    private Vector3 dirVec_Ball;

    // Start is called before the first frame update
    void Start()
    {
        Stage = ScoreManager.GetStage();

        Com_Obj_Speed = Com_Obj_Speed_BasicDef;
        Com_Obj_Atk = Com_Obj_Atk_BasicDef;
        Com_Obj_Delay = Com_Obj_Delay_BasicDef;

        //게임 시작 후 3초 후 드래곤 스폰
        Invoke("SpawnDragon", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //레디 고 이미지 표출을 위한 시간 계산
        fdt_Start += Time.deltaTime;

        if (fdt_Start >= 1f && startEnd == false)
            Ready_Img.SetActive(true);
        if (fdt_Start >= 2.5f && startEnd == false)
        {
            Ready_Img.SetActive(false);
            Go_Img.SetActive(true);
        }
        if (fdt_Start >= 3.2f && startEnd == false)
        {
            Go_Img.SetActive(false);
            fdt_Start = 0;
            startEnd = true;
        }


        Stage = ScoreManager.GetStage();
        if (Stage <= 1)
            Stage = 1;

        isDragonDie = ScoreManager.GetIsDragonDie();

        //드래곤 사망 true일때 3초 후 드래곤 스폰
        if (isDragonDie)
            Invoke("SpawnDragon", 3f);

        else
        {
            //스피드
            Com_Obj_Speed = Com_Obj_Speed_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Speed_BasicPlus, Com_Obj_Speed_EditDef,
                Com_Obj_Speed_EditPlus, Com_Obj_Speed_BasicCorStage, Com_Obj_Speed_EditCorStage);

            //공격력
            Com_Obj_Atk = Com_Obj_Atk_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Atk_BasicPlus, Com_Obj_Atk_EditDef,
                Com_Obj_Atk_EditPlus, Com_Obj_Atk_BasicCorStage, Com_Obj_Atk_EditCorStage);

            //딜레이
            Com_Obj_Delay = Com_Obj_Delay_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Delay_BasicPlus, Com_Obj_Delay_EditDef,
                Com_Obj_Delay_EditPlus, Com_Obj_Delay_BasicCorStage, Com_Obj_Delay_EditCorStage);

            SpawnObjects();
        }
       
    }

    //오브젝트 스폰 함수
    void SpawnObjects()
    {
        fdt += Time.deltaTime;

        if (fdt >= Com_Obj_Delay)
        {
            //1. 장애물
            //랜덤 위치
            ranPos_Obs = Random.Range(0, 3);
            //랜덤 드래곤볼
            ranBall = Random.Range(0, 4);

            switch (ranPos_Obs)
            {
                case 0:
                    ranPos_Ball = Random.Range(1, 3);

                    //장애물 발사
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = SpawnPoints[0].position;

                    dirVec_Obs = player.playerPos[0].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;
                case 1:
                    while (true)
                    {
                        ranPos_Ball = Random.Range(0, 3);
                        if (ranPos_Ball != 1)
                            break;
                    }

                    //장애물 발사
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = SpawnPoints[1].position;

                    dirVec_Obs = player.playerPos[1].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;
                case 2:
                    ranPos_Ball = Random.Range(0, 2);

                    //장애물 발사
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = SpawnPoints[2].position;

                    dirVec_Obs = player.playerPos[2].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

            }

            //2. 드래곤 공격(랜덤 발사)
            switch (ranBall)
            {
                //Pyro Ball
                case 0:
                    newDragonBall = objectManeger.MakeObj("PyroBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Electro Ball
                case 1:
                    newDragonBall = objectManeger.MakeObj("ElectroBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Ice Ball
                case 2:
                    newDragonBall = objectManeger.MakeObj("IceBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Water Ball
                case 3:
                    newDragonBall = objectManeger.MakeObj("WaterBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

            }

            //스폰 딜레이 초기화
            fdt = 0;
        }
    }

    void SpawnDragon()
    {
        switch (Stage % 10)
        {
            //블루
            case 1:
                Dragons[0].SetActive(true);
                break;

            //그린
            case 2:
                Dragons[1].SetActive(true);
                break;

            //핑크
            case 3:
                Dragons[2].SetActive(true);
                break;

            //퍼플
            case 4:
                Dragons[3].SetActive(true);
                break;

            //레드
            case 5:
                Dragons[4].SetActive(true);
                break;

            //옐로우
            case 6:
                Dragons[5].SetActive(true);
                break;

            //블랙
            case 7:
                Dragons[6].SetActive(true);
                break;

            //블루
            case 8:
                Dragons[0].SetActive(true);
                break;

            //그린
            case 9:
                Dragons[1].SetActive(true);
                break;

            //핑크
            case 0:
                Dragons[2].SetActive(true);
                break;
        }
    }
}
