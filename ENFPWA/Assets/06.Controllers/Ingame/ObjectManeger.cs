using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManeger : MonoBehaviour
{
    //생성할 프리팹 모두 연결
    [Header("Prefabs To Generate")]
    public GameObject ElectroBallPref;
    public GameObject PyroBallPref;
    public GameObject IceBallPref;
    public GameObject WaterBallPref;

    [Space(10f)]
    public GameObject ObstaclePref;

    [Space(10f)]
    public GameObject Player_AtkPref;

    //오브젝트 풀 생성(public X)
    GameObject[] ElectroBall_Pool;
    GameObject[] PyroBall_Pool;
    GameObject[] IceBall_Pool;
    GameObject[] WaterBall_Pool;

    GameObject[] Obstacle_Pool;
    GameObject[] Player_Atk_Pool;

    //타겟풀 생성
    GameObject[] TargetPool;

    void Awake()
    {
        //생성할 풀의 크기 설정
        ElectroBall_Pool = new GameObject[20];
        PyroBall_Pool = new GameObject[20];
        IceBall_Pool = new GameObject[20];
        WaterBall_Pool = new GameObject[20];

        Obstacle_Pool = new GameObject[20];

        Player_Atk_Pool = new GameObject[20];

        //생성함수 호출
        Generate();
    }

    void Generate()
    {
        //풀의 크기만큼 생성만 해두고, 비활성화
        for(int i = 0; i < ElectroBall_Pool.Length; i++)
        {
            ElectroBall_Pool[i] = Instantiate(ElectroBallPref);
            ElectroBall_Pool[i].SetActive(false);
        }

        for(int i = 0; i < PyroBall_Pool.Length; i++)
        {
            PyroBall_Pool[i] = Instantiate(PyroBallPref);
            PyroBall_Pool[i].SetActive(false);
        }

        for(int i = 0; i < IceBall_Pool.Length; i++)
        {
            IceBall_Pool[i] = Instantiate(IceBallPref);
            IceBall_Pool[i].SetActive(false);
        }

        for(int i = 0; i < WaterBall_Pool.Length; i++)
        {
            WaterBall_Pool[i] = Instantiate(WaterBallPref);
            WaterBall_Pool[i].SetActive(false);
        }

        for(int i = 0; i < Obstacle_Pool.Length; i++)
        {
            Obstacle_Pool[i] = Instantiate(ObstaclePref);
            Obstacle_Pool[i].SetActive(false);
        }

        for(int i = 0; i < Player_Atk_Pool.Length; i++)
        {
            Player_Atk_Pool[i] = Instantiate(Player_AtkPref);
            Player_Atk_Pool[i].SetActive(false);
        }
    }
    
    //외부에서 오브젝트 풀에 접근할 함수
    public GameObject MakeObj(string type)
    {
        //매개변수 타입에 따라 타겟풀 설정
        switch (type)
        {
            case "PyroBall":
                TargetPool = PyroBall_Pool;
                break;

            case "ElectroBall":
                TargetPool = ElectroBall_Pool;
                break;

            case "WaterBall":
                TargetPool = WaterBall_Pool;
                break;

            case "IceBall":
                TargetPool = IceBall_Pool;
                break;

            case "Obstacle":
                TargetPool = Obstacle_Pool;
                break;

            case "Player_Atk":
                TargetPool = Player_Atk_Pool;
                break;
        }

        //타겟풀을 검사해서 비활성화 상태이면, 활성화 시킨 후, 리턴
        for(int i = 0; i < TargetPool.Length; i++)
        {
            if(!TargetPool[i].activeSelf)
            {
                TargetPool[i].SetActive(true);
                return TargetPool[i];
            } 
        }

        //모두 활성화 상태이면 널 리턴
        return null;
    }
}
