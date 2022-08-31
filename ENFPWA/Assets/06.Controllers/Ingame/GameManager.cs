using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Space(10f)]
    [Header("Special Attack")]
    public Slider Special_Atk_Bar;
    public GameObject Special_Logo_None;
    public GameObject Special_Logo_Charged;
    public GameObject Special_Btn_None;
    public GameObject Special_Btn_Charged;
    public int Special_Stack_Total = 10;
    public int Special_Stack_Now;

    [Space(10f)]
    [Header("Alert")]
    private bool isSpawn;
    public GameObject AlertLine_Left;
    public GameObject AlertLine_Center;
    public GameObject AlertLine_Right;
    public float fdt_Alert;
    public bool AlertOn;

    [Space(10f)]
    [Header("Dragon Animation")]
    public static bool isAtk;
    public static bool isCenter_Atk;
    public float fdt_Atk;

    [Space(10f)]
    [Header("UI")]
    public GameObject Ready_Img;
    public GameObject Go_Img;
    [SerializeField] private bool ReadyEnd;
    [SerializeField] private bool GoEnd;

    [Space(10f)]
    [Header("Stage")]
    public Text StageText;
    public int Stage;
    public bool isDragonDie;

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
    [SerializeField] private float fdt_Start;
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
        isSpawn = false;
        Special_Stack_Now = 0;
        Special_Atk_Bar.value = 0f;

        //스테이지 및 스테이지 텍스트 초기화
        Stage = ScoreManager.GetStage();
        StageText.text = "STAGE " + Stage.ToString();

        Com_Obj_Speed = Com_Obj_Speed_BasicDef;
        Com_Obj_Atk = Com_Obj_Atk_BasicDef;
        Com_Obj_Delay = Com_Obj_Delay_BasicDef;

        //게임 시작 후 3초 후 드래곤 스폰
        Invoke("SpawnDragon", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        ReadyGo();

        //필살기
        SpecialAtk();

        //스테이지 및 스테이지 텍스트 업데이트
        Stage = ScoreManager.GetStage();
        StageText.text = "STAGE " + ScoreManager.GetStage().ToString();

        if (Stage <= 1)
            Stage = 1;

        isDragonDie = ScoreManager.GetIsDragonDie();

        //드래곤 사망 true일때 3초 후 드래곤 스폰
        if (isDragonDie)
            Invoke("SpawnDragon", 3f);
          
        //드래곤 사망 false일때
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

            //공격 상태 true일 때 fdt_Atk에 시간 누적
            CheckAtk();

            if (AlertLine_Left.activeSelf || AlertLine_Center.activeSelf || AlertLine_Right.activeSelf)
            {
                fdt_Alert += Time.deltaTime;
                if (fdt_Alert >= 0.5f)
                {
                    OffAlert();
                    fdt_Alert = 0;
                }         
            }
        }  
    }

    //오브젝트 스폰 함수
    void SpawnObjects()
    {
        fdt += Time.deltaTime;

        if(fdt >= (Com_Obj_Delay - 0.5f) && !isSpawn)
        {
            //1. 장애물
            //랜덤 위치
            ranPos_Obs = Random.Range(0, 3);
            //랜덤 드래곤볼
            ranBall = Random.Range(0, 4);
            //공격 경고라인
            switch (ranPos_Obs)
            {
                case 0:
                    ranPos_Ball = Random.Range(1, 3);
                    break;
                case 1:
                    while (true)
                    {
                        ranPos_Ball = Random.Range(0, 3);
                        if (ranPos_Ball != 1)
                            break;
                    }
                    break;
                case 2:
                    ranPos_Ball = Random.Range(0, 2);
                    break;
            }

            switch (ranPos_Ball)
            {
                case 0:
                    AlertLine_Left.SetActive(true);
                    break;
                case 1:
                    AlertLine_Center.SetActive(true);
                    break;
                case 2:
                    AlertLine_Right.SetActive(true);
                    break;
            }
            isSpawn = true;
        }


        if (fdt >= Com_Obj_Delay)
        {
            switch (ranPos_Obs)
            {
                case 0:
                    //장애물 발사
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = SpawnPoints[0].position;

                    dirVec_Obs = player.playerPos[0].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;
                case 1:
                    

                    //장애물 발사
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = SpawnPoints[1].position;

                    dirVec_Obs = player.playerPos[1].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;
                case 2:
                    

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
                    SoundManager.instance.PlayAudio_03("IG_PyroAtk");
                    newDragonBall = objectManeger.MakeObj("PyroBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Electro Ball
                case 1:
                    SoundManager.instance.PlayAudio_03("IG_ElectroAtk");
                    newDragonBall = objectManeger.MakeObj("ElectroBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Ice Ball
                case 2:
                    SoundManager.instance.PlayAudio_03("IG_IceAtk");
                    newDragonBall = objectManeger.MakeObj("IceBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                //Water Ball
                case 3:
                    SoundManager.instance.PlayAudio_03("IG_WaterAtk");
                    newDragonBall = objectManeger.MakeObj("WaterBall");
                    newDragonBall.transform.position = SpawnPoints[ranPos_Ball].position;

                    dirVec_Ball = player.playerPos[ranPos_Ball].transform.position - newDragonBall.transform.position;
                    newDragonBall.GetComponent<Rigidbody2D>().AddForce(dirVec_Ball * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

            }

            //스폰 딜레이 초기화
            fdt = 0;

            //오브젝트 스폰될 때 isAtk를 true로 만들어서 dragon스크립트에서 접근
            isAtk = true;
            if (ranPos_Ball == 1)
                isCenter_Atk = true;

            isSpawn = false;
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

    void ReadyGo()
    {
        if(!startEnd)
        {
            //레디 고 이미지 표출을 위한 시간 계산
            fdt_Start += Time.deltaTime;
            
            if (fdt_Start >= 1f && !ReadyEnd)
            {
                SoundManager.instance.PlayAudio_01("IG_Ready");
                Ready_Img.SetActive(true);
                ReadyEnd = true;
            }
                
            if (fdt_Start >= 2.8f && ReadyEnd && !GoEnd)
            {
                Ready_Img.SetActive(false);
                Go_Img.SetActive(true);
                SoundManager.instance.PlayAudio_01("IG_Go");
                GoEnd = true;
            }
            if (fdt_Start >= 3.5f)
            {
                Go_Img.SetActive(false);
                startEnd = true;
                fdt_Start = 0;
            }
        }
    }

    void OffAlert()
    {
        AlertLine_Left.SetActive(false);
        AlertLine_Center.SetActive(false);
        AlertLine_Right.SetActive(false);
        AlertOn = false;
    }

    //필살기 함수
    public void SpecialAtk()
    {
        if (Special_Stack_Now >= Special_Stack_Total)
        {
            Special_Logo_None.SetActive(false);
            Special_Btn_None.SetActive(false);
            Special_Logo_Charged.SetActive(true);
            Special_Btn_Charged.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Special_Stack_Now = 0;
                Special_Atk_Bar.value = 0;

                for (int i = 0; i < 30; i++)
                {
                    newDragonBall.SetActive(false);
                    newObstacle.SetActive(false);
                }

                for(int i = 0; i < Dragons.Length; i++)
                {
                   if(Dragons[i].activeSelf)
                    {
                        Dragons[i].GetComponent<Dragon>().Dragon_Now_HP -= 100;
                    }
                }

                Special_Logo_None.SetActive(true);
                Special_Btn_None.SetActive(true);
                Special_Logo_Charged.SetActive(false);
                Special_Btn_Charged.SetActive(false);
            }
        }
    }

    //공격상태 체크
    void CheckAtk()
    {
        if (isAtk)
        {
            fdt_Atk += Time.deltaTime;
            //0.7초 지나면 다시 원래대로 초기화
            if (fdt_Atk >= 0.7f)
            {
                isAtk = false;
                isCenter_Atk = false;
                fdt_Atk = 0;
            }
        }
    }
}
