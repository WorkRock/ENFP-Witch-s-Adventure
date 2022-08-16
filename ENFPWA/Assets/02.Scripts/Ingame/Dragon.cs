using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    //스테이지 값
    public int Stage;

    [Space(10f)]
    [Header("Dragon HP")]
    public float Dragon_Total_HP;
    public float Dragon_Now_HP;

    [Space(5f)]
    public float Dragon_HP_BasicDef;             //100
    public float Dragon_HP_BasicPlus;            //15
    [Space(5f)]
    public float Dragon_HP_EditDef;              //0
    public float Dragon_HP_EditPlus;             //40
    [Space(5f)]
    public int Dragon_HP_BasicCorStage;        //1
    public int Dragon_HP_EditCorStage;         //10
    [Space(5f)]
    public float Dragon_HP_Max;                  //6000

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

    //스크립트 객체 생성
    [Space(10f)]
    [Header("Instances")]
    public Player_Move player;

    public ObjectManeger objectManeger;

    [Space(10f)]
    [Header("Spawn Fdt")]
    public float fdt;

    [Space(10f)]
    [Header("SpawnPoints")]
    public GameObject Spawn_L;
    public GameObject Spawn_M;
    public GameObject Spawn_R;

    private GameObject newObstacle;
    private GameObject newDragonBall;

    // Start is called before the first frame update
    void Start()
    { 
        //시작 시엔 체력, 스피드, 공격력, 딜레이 모두 디폴트값으로 초기화
        Dragon_Total_HP = Dragon_HP_BasicDef;
        Dragon_Now_HP = Dragon_Total_HP;

        Com_Obj_Speed = Com_Obj_Speed_BasicDef;
        Com_Obj_Atk = Com_Obj_Atk_BasicDef;
        Com_Obj_Delay = Com_Obj_Delay_BasicDef;
    }

    void OnEnable()
    {
        //드래곤이 활성화 될때마다(스테이지가 바뀔때마다) ComObj 스피드, 공격력, 딜레이 계산 후 갱신
        //스피드
        Com_Obj_Speed = Com_Obj_Speed_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Speed_BasicPlus, Com_Obj_Speed_EditDef,
            Com_Obj_Speed_EditPlus, Com_Obj_Speed_BasicCorStage, Com_Obj_Speed_EditCorStage);

        //공격력
        Com_Obj_Atk = Com_Obj_Atk_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Atk_BasicPlus, Com_Obj_Atk_EditDef,
            Com_Obj_Atk_EditPlus, Com_Obj_Atk_BasicCorStage, Com_Obj_Atk_EditCorStage);

        //딜레이
        Com_Obj_Delay = Com_Obj_Delay_BasicDef + ScoreManager.totalFloatFormula(Stage, Com_Obj_Delay_BasicPlus, Com_Obj_Delay_EditDef,
            Com_Obj_Delay_EditPlus, Com_Obj_Delay_BasicCorStage, Com_Obj_Delay_EditCorStage);
    }

    void OnDisable()
    {
        //드래곤이 사라질 때마다 스테이지 값 + 1
        Stage++;
    }

    // Update is called once per frame
    void Update()
    {
        fdt += Time.deltaTime;

        if(fdt >= Com_Obj_Delay)
        {
            //1. 장애물
            //랜덤 위치
            int ranPos_Obs = Random.Range(0, 3);
               
            switch (ranPos_Obs)
            {
                case 0:
                    //오브젝트 생성, 위치 지정
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = Spawn_L.transform.position;
                    //발사 방향 지정, 발사
                    Vector3 dirVec_Obs_L = player.playerPos[0].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs_L * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                case 1:
                    //오브젝트 생성, 위치 지정
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = Spawn_M.transform.position;
                    //발사 방향 지정, 발사
                    Vector3 dirVec_Obs_M = player.playerPos[1].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs_M * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;

                case 2:
                    //오브젝트 생성, 위치 지정
                    newObstacle = objectManeger.MakeObj("Obstacle");
                    newObstacle.transform.position = Spawn_R.transform.position;
                    //발사 방향 지정, 발사
                    Vector3 dirVec_Obs_R = player.playerPos[2].transform.position - newObstacle.transform.position;
                    newObstacle.GetComponent<Rigidbody2D>().AddForce(dirVec_Obs_R * Com_Obj_Speed * 0.1f, ForceMode2D.Impulse);
                    break;
            }

            
            //2. 드래곤 공격(랜덤 발사)
            //랜덤 위치
            int ranPos_Ball = Random.Range(0, 3);
            //장애물과 드래곤볼 위치가 같으면 생성X
            if (ranPos_Ball == ranPos_Obs)
                return;

            //위치가 다르면 생성 o
            else
            {
                switch (ranPos_Ball)
                {
                    case 0:
                        //랜덤한 드래곤 볼
                        int ranBall = Random.Range(0, 4);

                        switch (ranBall)
                        {
                            //Pyro Ball
                            case 0:
                                newDragonBall = objectManeger.MakeObj("PyroBall");
                                newDragonBall.transform.position = Spawn_L.transform.position;

                                //Vector3 dirVec_Ball = player.
                                break;

                            //Electro Ball
                            case 1:
                                break;

                            //Ice Ball
                            case 2:
                                break;

                            //Water Ball
                            case 3:
                                break;

                        }
                        break;
                    case 1:
                        break;
                    case 2:
                        break;

                }
                /*
                //랜덤한 드래곤 볼
                int ranBall = Random.Range(0, 4);

                switch (ranBall)
                {
                    //Pyro Ball
                    case 0:
                        newDragonBall = objectManeger.MakeObj("PyroBall");
                        newDragonBall.transform.position = transform.position;

                        //Vector3 dirVec_Ball = player.
                        break;

                    //Electro Ball
                    case 1:
                        break;

                    //Ice Ball
                    case 2:
                        break;

                    //Water Ball
                    case 3:
                        break;

                }
                */
            }
            
            

            //스폰 딜레이 초기화
            fdt = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player_Atk"))
        {
            collision.gameObject.SetActive(false);

            //체력 감소
            Dragon_Now_HP -= player.Player_Atk;

            //HP가 0보다 작아지면
            if(Dragon_Now_HP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
