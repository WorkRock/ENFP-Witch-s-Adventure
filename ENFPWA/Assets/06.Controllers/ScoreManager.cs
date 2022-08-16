using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{   
    protected int PlayerLevel;
    protected int AtkUG;
    protected int Coin;
    protected int Exp;
    protected int BStage;
    protected int Stage;
    protected bool IsLobby; // 로비 화면인지를 체크
    protected bool IsSoundOn; // 소리가 켜져있는지 체크
    protected bool IsDragonDie; // 드래곤이 현재 사망상태인지 체크


    protected int PlayerHP;
    protected float PlayerTotalAtk;
    protected float ShieldCT;

    public ScoreManager()
    {
        // ScoreManager 초기화
    }


    // Get 메소드
    public static int GetPlayerLevel()
    {
        return GetInstance().PlayerLevel;
    }

    public static int GetAtkUG()
    {
        return GetInstance().AtkUG;
    }

    public static int GetCoin()
    {
        return GetInstance().Coin;
    }

    public static int GetExp()
    {
        return GetInstance().Exp;
    }

    public static int GetBStage()
    {
        return GetInstance().BStage;
    }

    public static int GetStage()
    {
        return GetInstance().Stage;
    }

    public static bool GetIsLobby()
    {
        return GetInstance().IsLobby;
    }

    public static bool GetIsSoundOn()
    {
        return GetInstance().IsSoundOn;
    }

    public static bool GetIsDragonDie()
    {
        return GetInstance().IsDragonDie;
    }

    public static int GetPlayerHP()
    {
        return GetInstance().PlayerHP;
    }

    public static float GetPlayerTotalAtk()
    {
        return GetInstance().PlayerTotalAtk;
    }

    public static float GetShieldCT()
    {
        return GetInstance().ShieldCT;
    }


    // Set 메소드
    public static void SetPlayerLevel(int _PlayerLevel)
    {
        GetInstance().PlayerLevel = _PlayerLevel;
        PlayerPrefs.SetInt("Level",_PlayerLevel);
        PlayerPrefs.Save();
    }

    public static void SetAtkUG(int _AtkUG)
    {
        GetInstance().AtkUG = _AtkUG;
        PlayerPrefs.SetInt("AtkUG",_AtkUG);
        PlayerPrefs.Save();
    }

    public static void SetCoin(int _Coin)
    {
        GetInstance().Coin = _Coin;
        PlayerPrefs.SetInt("Coin",_Coin);
        PlayerPrefs.Save();
    }

    public static void SetExp(int _Exp)
    {
        GetInstance().Exp = _Exp;
        PlayerPrefs.SetInt("Exp",_Exp);
        PlayerPrefs.Save();
    }

    public static void SetBStage(int _BStage)
    {
        GetInstance().BStage = _BStage;
        PlayerPrefs.SetInt("BStage",_BStage);
        PlayerPrefs.Save();
    }

    public static void SetStage(int _Stage)
    {
        GetInstance().Stage = _Stage;
    }

    public static void SetIsLobby(bool _IsLobby)
    {
        GetInstance().IsLobby = _IsLobby;
    }

    public static void SetIsSoundOn(bool _IsSoundOn)
    {
        GetInstance().IsSoundOn = _IsSoundOn;
    }

    public static void SetIsDragonDie(bool _IsDragonDie)
    {
        GetInstance().IsDragonDie = _IsDragonDie;
    }

    public static void SetPlayerHP(int _PlayerHP)
    {
        GetInstance().PlayerHP = _PlayerHP;
    }

    public static void SetPlayerTotalAtk(float _PlayerTotalAtk)
    {
        GetInstance().PlayerTotalAtk = _PlayerTotalAtk;
    }

    public static void SetShieldCT(float _ShieldCT)
    {
        GetInstance().ShieldCT = _ShieldCT;
    }

    public static int totalIntFormula(int nowStageLv, int BasicPlus, int EditDef, int EditPlus, int BasicCor, int EditCor)
    {
        int cal = 0;
        if(nowStageLv <= 1)
            return cal;

        cal = ((nowStageLv / BasicCor) * BasicPlus) + EditDef + ((nowStageLv / EditCor) * EditPlus);

        return cal;
    }

    public static float totalFloatFormula(int nowStageLv, float BasicPlus, float EditDef, float EditPlus, int BasicCor, int EditCor)
    {
        float cal = 0;
        if(nowStageLv <= 1)
            return cal;

        cal = ((nowStageLv / BasicCor) * BasicPlus) + EditDef + ((nowStageLv / EditCor) * EditPlus);

        return cal;
    }

}
