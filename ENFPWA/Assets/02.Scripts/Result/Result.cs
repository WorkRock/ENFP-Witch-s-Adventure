using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] private int Stage;
    [SerializeField] private int BStage;


    [Header("Result_UI")]
    public Text LastStage;
    public Text BestStage;
    public Text GetCoin;
    public Text GetExp;

    [Header("Result_GetCoin")]
    public int Result_GetCoin_TotalGetCoin;
    
    [Space(10f)]
    public int Result_GetCoin_BasicDefault;
    public int Result_GetCoin_BasicPlus;
    
    [Space(10f)]
    public int Result_GetCoin_EditDefault;
    public int Result_GetCoin_EditPlus;
    
    [Space(10f)]
    public int Result_GetCoin_BasicCor;
    public int Result_GetCoin_EditCor;
    
    [Space(10f)]
    public int Result_GetCoin_Max;


    [Header("Result_GetExp")]
    public int Result_GetExp_TotalGetExp;
    
    [Space(10f)]
    public int Result_GetExp_BasicDefault;
    public int Result_GetExp_BasicPlus;
    
    [Space(10f)]
    public int Result_GetExp_EditDefault;
    public int Result_GetExp_EditPlus;
    
    [Space(10f)]
    public int Result_GetExp_BasicCor;
    public int Result_GetExp_EditCor;
    
    [Space(10f)]
    public int Result_GetExp_Max;

    void Start()
    {
        Stage = ScoreManager.GetStage();
        BStage = ScoreManager.GetBStage();

        if(Stage > BStage)
        {
            BStage = Stage;
            ScoreManager.SetBStage(BStage);
        }

        Result_GetCoin_TotalGetCoin = Result_GetCoin_BasicDefault;
        Result_GetExp_TotalGetExp = Result_GetExp_BasicDefault;

        for(int i = 1; i <= Stage; i++)
        {
            totalGetCoinCal(i);
            totalGetExpCal(i);
        }
        
        if(Stage <= 1)
        {
            Result_GetCoin_TotalGetCoin = 0;
            Result_GetExp_TotalGetExp = 0;
        }

        LastStage.text = Stage + " Stage".ToString();
        BestStage.text = BStage + " Stage".ToString();
        GetCoin.text = "+ "+GetThousandCommaText(Result_GetCoin_TotalGetCoin).ToString();
        GetExp.text = "+ "+GetThousandCommaText(Result_GetExp_TotalGetExp).ToString();

        int Coin = ScoreManager.GetCoin();
        int Exp = ScoreManager.GetExp();

        Coin += Result_GetCoin_TotalGetCoin;
        Exp += Result_GetExp_TotalGetExp;

        ScoreManager.SetCoin(Coin);
        ScoreManager.SetExp(Exp);
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,##0}", data);
    }

    void totalGetCoinCal(int _Stage)
    {
        if(_Stage <= 1)
            return;
            
        
        Result_GetCoin_TotalGetCoin += ScoreManager.totalIntFormula(_Stage-1, Result_GetCoin_BasicPlus, Result_GetCoin_EditDefault
        , Result_GetCoin_EditPlus, Result_GetCoin_BasicCor, Result_GetCoin_EditCor);

        if(Result_GetCoin_TotalGetCoin >= Result_GetCoin_Max)
        {
            Result_GetCoin_TotalGetCoin += _Stage;
        }
    }

    void totalGetExpCal(int _Stage)
    {
        if(_Stage <= 1)
            return;
            

        Result_GetExp_TotalGetExp += ScoreManager.totalIntFormula(_Stage-1, Result_GetExp_BasicPlus, Result_GetExp_EditDefault
        , Result_GetExp_EditPlus, Result_GetExp_BasicCor, Result_GetExp_EditCor);

        if(Result_GetExp_TotalGetExp >= Result_GetExp_Max)
        {
            Result_GetExp_TotalGetExp += _Stage;
        }
    }

    public void GameStart()
    {
        ScoreManager.SetIsLobby(false);
        ScoreManager.SetStage(1);
        SceneManager.LoadScene("Ingame");
    }

    public void GoLobby()
    {
        ScoreManager.SetIsLobby(false);
        SceneManager.LoadScene("Lobby");
    }
}
