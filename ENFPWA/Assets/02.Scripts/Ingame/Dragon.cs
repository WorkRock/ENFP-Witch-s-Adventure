using System.Collections;
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

    [Space(10f)]
    [Header("Animation")]
    public GameObject Eye_Idle;
    public GameObject Eye_Hit;
    public bool isHit;
    public float fdt_Hit;

    [Space(5f)]
    public GameObject Eye_Atk;
    public GameObject Mouth_Atk;
    public GameObject Mouth_Idle;

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
    public GameManager gameManager;

    void OnEnable()
    {
        //다시 활성화될 때 첫타격시 폭발효과가 씹히는 것 방지하기 위해 조건을 다 초기화
        Explosion.SetActive(false);
        ExplosionOn = false;
        fdt_Explosion = 0;

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
        //다시 활성화될 때 x자눈으로 활성화 되는거 막기 위해 비활성화될 때 isHit를 false로 초기화
        isHit = false;
      
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
        {
            SoundManager.instance.PlayAudio_05("IG_DragonDie");
            Invoke("DelayDisable", 0.5f);
        }
           
        //폭발 효과
        OnExplosion();

        //피격 애니메이션
        HitAnim();

        //공격 애니메이션
        AtkAnim();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player_Atk"))
        {
            collision.gameObject.SetActive(false);

            //필살기 스택 +1
            gameManager.Special_Stack_Now++;
            gameManager.Special_Atk_Bar.value += 0.1f;
            
            //isHit를 true로 바꿔주고 애니메이션 변경은 update에서 한다. 필살기 맞아도 애니메이션 변경 해줘야하므로
            isHit = true;

            //폭발 효과
            if(!ExplosionOn)
            {
                Explosion.SetActive(true);
                ExplosionOn = true;
                SoundManager.instance.PlayAudio_02("IG_Explosion");
            }

            //체력 감소
            Dragon_Now_HP -= player.Player_Atk;
            Debug.Log($"Dragon_Now_HP : {Dragon_Now_HP}");
            //HP가 0보다 작아지면
            if (Dragon_Now_HP <= 0)
            {
                Invoke("DelayDisable", 0.5f);
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

    void HitAnim()
    {
        if (isHit)
        {
            //애니메이션 변경
            Eye_Idle.SetActive(false);
            Eye_Hit.SetActive(true);

            //isHit가 true이면 시간을 누적해서 n초 이상이면 애니메이션 원래대로
            fdt_Hit += Time.deltaTime;
            if (fdt_Hit >= 0.7f)
            {
                Eye_Idle.SetActive(true);
                Eye_Hit.SetActive(false);
                isHit = false;
                fdt_Hit = 0;
            }
        }
    }

    void AtkAnim()
    {
        if (GameManager.isAtk && !isHit)
        {
            Eye_Atk.SetActive(true);
            Eye_Idle.SetActive(false);

            if (GameManager.isCenter_Atk)
            {
                Mouth_Atk.SetActive(true);
                Mouth_Idle.SetActive(false);
            }

            //공격 중이라도 맞으면 피격애니메이션 재생
            HitAnim();
        }

        else if (!GameManager.isAtk && !isHit)
        {
            Eye_Atk.SetActive(false);
            Eye_Idle.SetActive(true);
            Mouth_Atk.SetActive(false);
            Mouth_Idle.SetActive(true);
        }

        else if (GameManager.isAtk && isHit)
        {
            Eye_Atk.SetActive(false);
            Eye_Idle.SetActive(false);
            Mouth_Atk.SetActive(false);
            Mouth_Idle.SetActive(false);

            HitAnim();
        }
    }

    void DelayDisable()
    {
        gameObject.SetActive(false);
    }
}
