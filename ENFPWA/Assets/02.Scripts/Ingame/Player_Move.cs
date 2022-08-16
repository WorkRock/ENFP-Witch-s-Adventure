using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //플레이어 함수(HP, ATK)
    [Header("Player HP")]
    public float Player_Total_HP;
    public float Player_Now_HP;

    [Space(10f)]
    [Header("Player Atk")]
    public float Player_Atk;

    [Space(10f)]
    [Header("Player Shield CT")]
    public float Player_ShieldCT;

    //스크립트 객체 생성
    [Space(10f)]
    [Header ("Instances")]
    public ObjectManeger objectManeger;

    //플레이어 위치 인덱스
    [Space(10f)]
    [Header ("Player Move")]
    public GameObject[] playerPos;
    private int minPos = 0;
    private int nowPos;
    private int maxPos = 2;

    //플레이어 쉴드
    [Space(10f)]
    [Header("Player Shield")]
    public CapsuleCollider2D capsuleCollider;
    public GameObject[] Shields;
 
    private int nowShieldNum;

    //쉴드 지속 시간
    public float curShieldDelay;
    public float maxShieldDelay;
    //쉴드 쿨타임
    public float curShieldCT;
    public float maxShieldCT;

    //쉴드 on/off 상태
    public bool isShieldOn;
       
    // Start is called before the first frame update
    void Start()
    {
        nowPos = 1;
        nowShieldNum = 0;
        curShieldCT = 0;
        curShieldDelay = 0;

        //HP 초기화
        Player_Total_HP = ScoreManager.GetPlayerHP();
        Player_Now_HP = Player_Total_HP;

        //공격력 초기화
        Player_Atk = ScoreManager.GetPlayerTotalAtk();

        //쉴드 쿨타임 초기화
        Player_ShieldCT = ScoreManager.GetShieldCT();

    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        MovePlayer();

        //쉴드 스왑
        SwapShield();

        //쉴드 On
        OnShield();

        //쉴드 Off
        OffShield();

        //쉴드 끊김 방지
        for(int i = 0; i < Shields.Length; i++)
        {
            if (Shields[i].activeSelf)
                Shields[i].transform.position = transform.position;
        }
    }

    //플레이어 이동 함수
    void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            float fHor = Input.GetAxisRaw("Horizontal");

            if (nowPos + fHor <= maxPos && nowPos + fHor >= minPos)
            {
                nowPos += (int)fHor;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, playerPos[nowPos].transform.position, 0.2f);
    }

    //쉴드 스왑 함수
    void SwapShield()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            float fVer = Input.GetAxisRaw("Vertical");

            nowShieldNum += (int)fVer;

            if (nowShieldNum > 3)
                nowShieldNum = 0;

            else if (nowShieldNum < 0)
                nowShieldNum = 3;
        }
    }

    //쉴드 On 함수
    void OnShield()
    {
        //쉴드 On(isShieldOn이 false이고, 현재 쉴드 쿨타임이 0일때 space를 누르면)
        if (isShieldOn == false && curShieldCT == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            isShieldOn = true;
            //플레이어 콜라이더 잠시 끄기
            capsuleCollider.enabled = false;

            switch (nowShieldNum)
            {
                //Pyro Shield
                case 0:
                    Shields[0].SetActive(true);
                    gameObject.tag = "PyroShield";
                    break;

                //Electro Shield
                case 1:
                    Shields[1].SetActive(true);
                    gameObject.tag = "ElectroShield";
                    break;

                //Ice Shield
                case 2:
                    Shields[2].SetActive(true);
                    gameObject.tag = "IceShield";
                    break;

                //Water Shield
                case 3:
                    Shields[3].SetActive(true);
                    gameObject.tag = "WaterShield";
                    break;
            }
        }
    }

    //쉴드 Off 함수
    void OffShield()
    {
        //현재 쉴드 지속시간에 시간을 누적해주고 max보다 커지면 끄기
        if (isShieldOn == true)
        {
            curShieldDelay += Time.deltaTime;
            if (curShieldDelay >= maxShieldDelay)
            {
                //쉴드 지속시간 다되면 쉴드 끄기
                for (int i = 0; i < 4; i++)
                    Shields[i].SetActive(false);

                isShieldOn = false;
                curShieldCT = maxShieldCT;
                curShieldDelay = 0;

                //플레이어 콜라이더 다시 켜기
                capsuleCollider.enabled = true;
                //태그명 플레이어로 다시 바꾸기
                gameObject.tag = "Player";

            }
        }

        //쉴드 off일땐 현재 shield 쿨타임에서 시간을 뺌
        else
        {
            curShieldCT -= Time.deltaTime;
            if (curShieldCT <= 0)
                curShieldCT = 0;
        }
    }
}
