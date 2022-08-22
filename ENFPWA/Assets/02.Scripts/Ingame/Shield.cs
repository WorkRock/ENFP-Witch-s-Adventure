﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //스크립트 객체 생성
    [Header("Instances")]
    public ObjectManeger objectManeger;
    public GameManager gameManager;

    [Space(10f)]
    public Player_Move player;

    [Space(10f)]
    public Dragon dragon;

    //공격을 발사할 타겟 위치
    public GameObject targetPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //드래곤 공격에 맞았을 때
        if(collision.gameObject.tag.Equals("PyroBall") || collision.gameObject.tag.Equals("ElectroBall") ||
            collision.gameObject.tag.Equals("IceBall") || collision.gameObject.tag.Equals("WaterBall"))
        {
            //드래곤 공격과 쉴드 속성이 일치할 경우 체력 감소 X, 플레이어 공격 발사
            if ((collision.gameObject.tag.Equals("PyroBall") && gameObject.tag.Equals("PyroShield")) ||
                (collision.gameObject.tag.Equals("ElectroBall") && gameObject.tag.Equals("ElectroShield")) ||
                (collision.gameObject.tag.Equals("IceBall") && gameObject.tag.Equals("IceShield")) ||
                (collision.gameObject.tag.Equals("WaterBall") && gameObject.tag.Equals("WaterShield")))
            {
                //드래곤 공격 비활성화
                collision.gameObject.SetActive(false);

                //오브젝트 생성, 생성 위치 지정
                GameObject newPlayerAtk = objectManeger.MakeObj("Player_Atk");
                newPlayerAtk.transform.position = transform.position;

                //발사 방향 지정, 발사
                Vector3 dirVec = targetPos.transform.position - transform.position;
                newPlayerAtk.GetComponent<Rigidbody2D>().AddForce(dirVec * 1.5f, ForceMode2D.Impulse);
            }

            //아닌 경우 플레이어 체력 - 드래곤 공격력
            else
            {
                player.Player_Now_HP -= gameManager.Com_Obj_Atk;
                
            }
        }
    }
}
