using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("CutScene")]
    public GameObject Cutscene;

    //스크립트 객체 생성
    [Space(10f)]
    [Header("Instances")]
    public ObjectManeger objectManager;
    public GameManager gameManager;
    public Player_Move player;
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
                //컷씬 활성화
                Cutscene.SetActive(true);
                Time.timeScale = 0.1f;
                Invoke("OffCutScene", 0.03f);

                //1~3번중 랜덤 패링 음성 재생
                int ranParrySound = Random.Range(1, 4);
                SoundManager.instance.PlayAudio_04("IG_Parrying_0" + ranParrySound);
                //쉴드 튕길때 소리 재생
                SoundManager.instance.PlayAudio_05("IG_PlayerParry");

                //드래곤 공격 비활성화
                collision.gameObject.SetActive(false);

                //오브젝트 생성, 생성 위치 지정
                GameObject newPlayerAtk = objectManager.MakeObj("Player_Atk");
                newPlayerAtk.transform.position = transform.position;

                //반사 위치에 따라 공격 기울기 조정
                //왼쪽 패링
                if (collision.gameObject.transform.position.x < 0)
                {
                    newPlayerAtk.transform.rotation = Quaternion.Euler(0, 0, 150f);
                }

                //오른쪽 패링
                else if (collision.gameObject.transform.position.x > 0)
                {
                    newPlayerAtk.transform.rotation = Quaternion.Euler(0, 0, 210f);
                }

                //가운데 패링
                else
                    newPlayerAtk.transform.rotation = Quaternion.Euler(0, 0, 180f);

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

    void OffCutScene()
    {
        Cutscene.SetActive(false);
        Time.timeScale = 1;
    }
}
