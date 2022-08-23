﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dragon : MonoBehaviour
{
    [Header("UI")]
    public Slider Dragon_HP_Bar;
    
    [Space(10f)]
    [Header("Effect")]
    public GameObject Explosion;
    public bool ExplosionOn;
    public float fdt_Explosion;

    //스테이지 값
    [Space(10f)]
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

   
    //스크립트 객체 생성
    [Space(10f)]
    [Header("Instances")]
    public Player_Move player;

    void OnEnable()
    {
        Stage = ScoreManager.GetStage();
        Debug.Log("Stage : " + Stage);

        Dragon_Total_HP = Dragon_HP_BasicDef;

        //활성화될 때마다 hp계산
        totalHPCal();
        Dragon_Now_HP = Dragon_Total_HP;

        Dragon_HP_Bar = GetComponentInChildren<Slider>();
        //활성화 될때 hp바 만땅
        Dragon_HP_Bar.value = 1.0f;

        Debug.Log($"Dragon_Total_HP : {Dragon_Total_HP}");
        Debug.Log($"Dragon_Now_HP : {Dragon_Now_HP}");

        ScoreManager.SetIsDragonDie(false);
    }

    void OnDisable()
    {
        //드래곤이 사라질 때마다 스테이지 값 + 1
        Stage++;
        ScoreManager.SetStage(Stage);

        ScoreManager.SetIsDragonDie(true);
    }

    // Update is called once per frame
    void Update()
    {
        //hp바 조정
        Dragon_HP_Bar.value = Dragon_Now_HP / Dragon_Total_HP;

        //hp <= 0 일시 드래곤 비활성화
        if (Dragon_Now_HP <= 0)
            gameObject.SetActive(false);

        OnExplosion();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player_Atk"))
        {
            collision.gameObject.SetActive(false);

            //폭발 효과
            if(!ExplosionOn)
            {
                Explosion.SetActive(true);
                ExplosionOn = true;
            }

            //체력 감소
            Dragon_Now_HP -= player.Player_Atk;
            Debug.Log($"Dragon_Now_HP : {Dragon_Now_HP}");
            //HP가 0보다 작아지면
            if (Dragon_Now_HP <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void totalHPCal()
    {
        if (Stage <= 1)
            return;

        Dragon_Total_HP = Dragon_HP_BasicDef + ScoreManager.totalFloatFormula(Stage - 1, Dragon_HP_BasicPlus, Dragon_HP_EditDef,
            Dragon_HP_EditPlus, Dragon_HP_BasicCorStage, Dragon_HP_EditCorStage);
        
    }

    void OnExplosion()
    {
        //폭발상태 true일때 시간을 계산해서 1초가 지나면 폭발 비활성화, 폭발상태 false로 변경
        if (ExplosionOn)
        {
            fdt_Explosion += Time.deltaTime;
            if (fdt_Explosion >= 1f)
            {
                Explosion.SetActive(false);
                ExplosionOn = false;
                fdt_Explosion = 0;
            }
        }
    }
}
