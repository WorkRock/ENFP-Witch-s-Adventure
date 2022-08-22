using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private int Level;
    private int AtkUG;
    private int Coin;
    private bool isLobby;
    private bool isLobbyDelay;
    private bool isLobbyUIOn;
    private bool isLobbySpriteDelay;
    private int LobbySpriteDir;
    [SerializeField] private float fdt;

    [Header("UI_MainLobby")]
    public GameObject LobbyIntro;
    public Text PressAnyKey;
    public Image BlackBG;
    public Image ToothlessLogo;

    public Image UI_LobbySprite;
    public GameObject UI_Lobby;
    public Slider Main_ExpSlider;
    public Text Main_Level;
    public Text Main_Coin;
    
    [Header("UI_Option")]
    public GameObject CheckBtn;
    private bool isSoundOn;

    [Header("UI_Info")]
    public GameObject Info_AtkUG_01;
    public GameObject Info_AtkUG_02;
    public GameObject Info_AtkUG_03;
    public Text Info_Level;
    public Text Info_HP;
    public Text Info_Atk;
    public Text Info_AtkUG;
    public Text Info_AtkUG_DMG;
    public Text Info_AtkUG_CT;
    public Text Info_Exp;


    [Header("Player_HP")]
    public int Player_HP_TotalHP;

    [Space(10f)]
    public int Player_HP_BasicDefault;
    public int Player_HP_BasicPlus;

    [Space(10f)]
    public int Player_HP_EditDefault;
    public int Player_HP_EditPlus;
    
    [Space(10f)]
    public int Player_HP_BasicCor;
    public int Player_HP_EditCor;
    
    [Space(10f)]
    public int Player_HP_Max;

    [Header("Player_Atk")]
    public float Player_Atk_TotalPlusUG;
    public int Player_Atk_TotalAtk;
    
    [Space(10f)]
    public int Player_Atk_BasicDefault;
    public int Player_Atk_BasicPlus;
    
    [Space(10f)]
    public int Player_Atk_EditDefault;
    public int Player_Atk_EditPlus;
    
    [Space(10f)]
    public int Player_Atk_BasicCor;
    public int Player_Atk_EditCor;
    
    [Space(10f)]
    public int Player_Atk_Max;


    [Header("Player_Exp")]
    public int Player_Exp_CurExp;
    public int Player_Exp_TotalExp;
    
    [Space(10f)]
    public int Player_Exp_BasicDefault;
    public int Player_Exp_BasicPlus;
    
    [Space(10f)]
    public int Player_Exp_EditDefault;
    public int Player_Exp_EditPlus;
    
    [Space(10f)]
    public int Player_Exp_BasicCor;
    public int Player_Exp_EditCor;
    
    [Space(10f)]
    public int Player_Exp_Max;



    [Header("UI_Shop")]
    public Button Upgrade;
    public Text Upgrade_Text;
    public GameObject Shop_FirstUG;
    public GameObject Shop_SecondUG;
    public Slider Shop_FirstUGSlider;
    public Slider Shop_SecondUGSlider;
    public Text Shop_AtkUG;
    public Text Shop_NeedCoin;
    public Text Shop_UGNow;
    public Text Shop_UGNext;
    public Text Shop_UGCT;
    [SerializeField] private int Shop_FirstUGLevelMax;
    [SerializeField] private int Shop_SecondUGLevelMax;


    [Header("UG_DMG")]
    public float UG_DMG_TotalAtk;
    public float UG_DMG_TotalNext;
    
    [Space(10f)]
    public float UG_DMG_BasicDefault;
    public float UG_DMG_BasicPlus;
    
    [Space(10f)]
    public float UG_DMG_EditDefault;
    public float UG_DMG_EditPlus;
    
    [Space(10f)]
    public int UG_DMG_BasicCor;
    public int UG_DMG_EditCor;
    
    [Space(10f)]
    public float UG_DMG_Max;
    

    [Header("UG_CT")]
    public float UG_CT_TotalCT;
    
    [Space(10f)]
    public float UG_CT_BasicDefault;
    public float UG_CT_BasicPlus;
    
    [Space(10f)]
    public float UG_CT_EditDefault;
    public float UG_CT_EditPlus;
    
    [Space(10f)]
    public int UG_CT_BasicCor;
    public int UG_CT_EditCor;
    
    [Space(10f)]
    public float UG_CT_Max;


    [Header("UG_NeedCoin")]
    public int UG_NeedCoin_TotalNeedCoin;
    
    [Space(10f)]
    public int UG_NeedCoin_BasicDefault;
    public int UG_NeedCoin_BasicPlus;
    
    [Space(10f)]
    public int UG_NeedCoin_EditDefault;
    public int UG_NeedCoin_EditPlus;
    
    [Space(10f)]
    public int UG_NeedCoin_BasicCor;
    public int UG_NeedCoin_EditCor;
    
    [Space(10f)]
    public int UG_NeedCoin_Max;

    Color color;


    void Start()
    {
        LobbyIntro.SetActive(true);
        color = PressAnyKey.color;

        isLobbySpriteDelay = true;
        LobbySpriteDir = 1;
        isLobby = false;
        isLobbyUIOn = false;
        isLobbyDelay = true;

    
        Level = ScoreManager.GetPlayerLevel();
        if(Level <= 1)
        {   
            Level = 1;
            ScoreManager.SetPlayerLevel(1);
        }

        AtkUG = ScoreManager.GetAtkUG();
        
        if(AtkUG <= 1)
        {
            AtkUG = 1;
            ScoreManager.SetAtkUG(1);
        }   

        Coin = ScoreManager.GetCoin();


        Player_HP_TotalHP = Player_HP_BasicDefault;
        Player_Atk_TotalAtk = Player_Atk_BasicDefault;
        Player_Exp_TotalExp = Player_Exp_BasicDefault;
        
        Player_Exp_CurExp = ScoreManager.GetExp();

        UG_DMG_TotalAtk = UG_DMG_BasicDefault;
        UG_DMG_TotalNext = UG_DMG_BasicDefault + UG_DMG_BasicPlus;
        UG_CT_TotalCT = UG_CT_BasicDefault;
        UG_NeedCoin_TotalNeedCoin = UG_NeedCoin_BasicDefault;

        Player_Atk_TotalPlusUG = Player_Atk_TotalAtk * UG_DMG_TotalAtk;
        

        totalHPCal();
        totalAtkCal();
        totalExpCal();
        totalUGAtkCal();
        totalUGCTCal();
        totalUGNeedCoinCal();

        ScoreManager.SetPlayerHP(Player_HP_TotalHP);
        ScoreManager.SetPlayerTotalAtk(Player_Atk_TotalPlusUG);
        ScoreManager.SetShieldCT(UG_CT_TotalCT);

        MarkLevel();
        Shop_MarkLevel();
    }

    void Update()
    {
        isLobby = ScoreManager.GetIsLobby();

        if(isLobbyDelay & !isLobby)
            fdt += Time.deltaTime;        
        
        color.a += (float)(LobbySpriteDir / (float)20);

        PressAnyKey.color = color;

        if(isLobbySpriteDelay) 
        {
            isLobbySpriteDelay = false;
            StartCoroutine(LobbySpriteMove());
        }

        if(fdt > 4.5)
        {
            fdt = 0;
            isLobbyDelay = false;
            isLobby = true;
            ScoreManager.SetIsLobby(isLobby);
        }

        CheatMode();

        if(Input.anyKeyDown)
        {   
            if(isLobbyDelay)
                return;
            
            isLobbyDelay = true;
            SoundManager.instance.PlayAudio_01("Btn_Normal");
            LobbyIntro.SetActive(false);
            isLobbyUIOn = true;
            isLobby = true;
            ScoreManager.SetIsLobby(isLobby);
        }


        if(isLobbyUIOn) UI_Lobby.SetActive(true);
        else UI_Lobby.SetActive(false);

        isSoundOn = ScoreManager.GetIsSoundOn();

        if(isSoundOn)
        {
            CheckBtn.SetActive(true);
        }
            
        else
        {
            CheckBtn.SetActive(false);
        }

        // 테스트를 위한 치트모드
        

        LevelEdit();
        
        Main_Level.text = Level.ToString();
        Main_Coin.text = GetThousandCommaText(Coin).ToString();

        Info_Level.text = Level.ToString();
        Info_HP.text = Player_HP_TotalHP.ToString();
        Info_Atk.text =  Player_Atk_TotalAtk + " x " + (UG_DMG_TotalAtk * 100) + "% (" + Player_Atk_TotalPlusUG.ToString("F2") + ")".ToString();
        Info_AtkUG.text =  AtkUG.ToString();
        Info_Exp.text = ("<b><color=green>"+ Player_Exp_CurExp + "</color></b>" +" / " + Player_Exp_TotalExp).ToString();
        Info_AtkUG_DMG.text = ((UG_DMG_TotalAtk * 100) + "%").ToString();
        Info_AtkUG_CT.text = UG_CT_TotalCT.ToString();

        Shop_AtkUG.text = AtkUG.ToString();

        if(UG_NeedCoin_TotalNeedCoin > Coin)
        {
            Shop_NeedCoin.text = ("<b><color=red>"+ GetThousandCommaText(UG_NeedCoin_TotalNeedCoin) + "</color></b>").ToString();
            Upgrade.interactable = false;
            Upgrade_Text.text = "<color=grey>Upgrade</color>";
        }

        else
        {
            if(AtkUG >= 40) 
            {
                Info_AtkUG.text = "<color=green>Max</color>";
                Shop_AtkUG.text = "<color=green>Max Level</color>";
                Shop_NeedCoin.text = "<color=green>Max Level</color>";
                Upgrade.interactable = false;
                Upgrade_Text.text = "<color=grey>Upgrade</color>";
            }
            else 
            {
                Shop_NeedCoin.text = GetThousandCommaText(UG_NeedCoin_TotalNeedCoin).ToString();
                Upgrade.interactable = true;
                Upgrade_Text.text = "Upgrade";
            }            
        }
        
        Shop_UGNow.text = ((UG_DMG_TotalAtk * 100) + "%").ToString();
        Shop_UGNext.text = ((UG_DMG_TotalNext * 100) + "%").ToString();
        Shop_UGCT.text = UG_CT_TotalCT.ToString();
    }

    IEnumerator LobbySpriteMove()
    {
        yield return new WaitForSeconds(1.0f);
        UI_LobbySprite.transform.rotation = Quaternion.Euler(0, 0, LobbySpriteDir*5);
        
        LobbySpriteDir *= (-1);
        isLobbySpriteDelay = true;
    }

    IEnumerator LobbyDelay()
    {
        yield return new WaitForSeconds(3.0f);
    }


    void CheatMode()
    {
        if(!Input.GetKeyDown(KeyCode.T))
            return;
        
        Coin += 10000;
        ScoreManager.SetCoin(Coin);

        ScoreManager.SetExp(Player_Exp_CurExp + 10);
        MarkLevel();
    }

    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,##0}", data);
    }

    void MarkLevel()
    {
        Main_ExpSlider.value = (float)Player_Exp_CurExp / Player_Exp_TotalExp;
    }

    void LevelEdit()
    {
        Player_Exp_CurExp = ScoreManager.GetExp();

        if(Player_Exp_CurExp >= Player_Exp_TotalExp)
        {
            Player_Exp_CurExp -= Player_Exp_TotalExp;

            Level++;
            ScoreManager.SetPlayerLevel(Level);
            ScoreManager.SetExp(Player_Exp_CurExp);

            totalExpCal();
            LevelEdit();
        }

        else
        {            
            if(Player_Exp_CurExp < 0)
                Player_Exp_CurExp = 0;
            
            ScoreManager.SetExp(Player_Exp_CurExp);

            totalAtkCal();
            totalHPCal();
            MarkLevel();

            ScoreManager.SetPlayerTotalAtk(Player_HP_TotalHP);
            ScoreManager.SetPlayerTotalAtk(Player_Atk_TotalPlusUG);
        }
    }

    public void UpgradeBtn()
    {
        if(AtkUG >= 40)
            return;

        if(Coin > UG_NeedCoin_TotalNeedCoin)
        {
            Coin -= UG_NeedCoin_TotalNeedCoin;
            
            ScoreManager.SetCoin(Coin);
            
            AtkUG++;
            ScoreManager.SetAtkUG(AtkUG);

            totalUGAtkCal();
            totalUGCTCal();
            totalUGNeedCoinCal();
            Shop_MarkLevel();

            ScoreManager.SetPlayerTotalAtk(Player_Atk_TotalPlusUG);
            ScoreManager.SetShieldCT(UG_CT_TotalCT);
        }
    }

    void Shop_MarkLevel()
    {
        if(AtkUG < Shop_FirstUGLevelMax)
        {
            Info_AtkUG_01.SetActive(true);
            Info_AtkUG_02.SetActive(false);
            Info_AtkUG_03.SetActive(false);

            Shop_FirstUG.SetActive(true);
            Shop_SecondUG.SetActive(false);
            
            if(AtkUG != 1)
                Shop_FirstUGSlider.value = (float)AtkUG / Shop_FirstUGLevelMax;
            else
                Shop_FirstUGSlider.value = 0;
        }

        else if(AtkUG < Shop_SecondUGLevelMax)
        {
            Info_AtkUG_01.SetActive(false);
            Info_AtkUG_02.SetActive(true);
            Info_AtkUG_03.SetActive(false);

            Shop_FirstUG.SetActive(false);
            Shop_SecondUG.SetActive(true);

            if((AtkUG-Shop_FirstUGLevelMax) == 0)
                Shop_SecondUGSlider.value = 0;
            else
                Shop_SecondUGSlider.value = (float)(AtkUG-Shop_FirstUGLevelMax) / (Shop_SecondUGLevelMax-Shop_FirstUGLevelMax);
        }

        else
        {
            Info_AtkUG_01.SetActive(false);
            Info_AtkUG_02.SetActive(false);
            Info_AtkUG_03.SetActive(true);

            Shop_FirstUG.SetActive(false);
            Shop_SecondUG.SetActive(true);
            Shop_SecondUGSlider.value = 1;
        }
    }


    void totalHPCal()
    {
        if(Level <= 1)
            return;
        
        Player_HP_TotalHP = Player_HP_BasicDefault +
        ScoreManager.totalIntFormula(Level-1, Player_HP_BasicPlus, Player_HP_EditDefault
        , Player_HP_EditPlus, Player_HP_BasicCor, Player_HP_EditCor);

        ScoreManager.SetPlayerHP(Player_HP_TotalHP);
        if(Player_HP_TotalHP >= Player_HP_Max)
        {
            Player_HP_TotalHP = Player_HP_Max;
        }
    }

    void totalAtkCal()
    {
        if(Level <= 1)
            return;
        
        Player_Atk_TotalAtk = Player_Atk_BasicDefault +
        ScoreManager.totalIntFormula(Level-1, Player_Atk_BasicPlus, Player_Atk_EditDefault
        , Player_Atk_EditPlus, Player_Atk_BasicCor, Player_Atk_EditCor);

        Player_Atk_TotalPlusUG = Player_Atk_TotalAtk * UG_DMG_TotalAtk;

        if(Player_Atk_TotalAtk >= Player_Atk_Max)
        {
            Player_Atk_TotalAtk = Player_Atk_Max;
        }
    }

    void totalExpCal()
    {
        if(Level <= 1)
            return;
        
        Player_Exp_TotalExp = Player_Exp_BasicDefault +
        ScoreManager.totalIntFormula(Level-1, Player_Exp_BasicPlus, Player_Exp_EditDefault
        , Player_Exp_EditPlus, Player_Exp_BasicCor, Player_Exp_EditCor);

        if(Player_Exp_TotalExp >= Player_Exp_Max)
        {
            Player_Exp_TotalExp = Player_Exp_Max;
        }
    }



    void totalUGAtkCal()
    {
        if(AtkUG <= 1)
            return;
        
        UG_DMG_TotalAtk = UG_DMG_BasicDefault +
        ScoreManager.totalFloatFormula(AtkUG - 1, UG_DMG_BasicPlus, UG_DMG_EditDefault
        , UG_DMG_EditPlus, UG_DMG_BasicCor, UG_DMG_EditCor);

        UG_DMG_TotalNext = UG_DMG_BasicDefault +
        ScoreManager.totalFloatFormula(AtkUG, UG_DMG_BasicPlus, UG_DMG_EditDefault
        , UG_DMG_EditPlus, UG_DMG_BasicCor, UG_DMG_EditCor);

        if(UG_DMG_TotalAtk >= UG_DMG_Max)
        {
            UG_DMG_TotalAtk = UG_DMG_Max;
        }

        if(UG_DMG_TotalNext >= UG_DMG_Max)
        {
            UG_DMG_TotalNext = UG_DMG_Max;
        }
    }

    void totalUGCTCal()
    {
        if(AtkUG <= 1)
            return;
        
        UG_CT_TotalCT = UG_CT_BasicDefault +
        ScoreManager.totalFloatFormula(AtkUG, UG_CT_BasicPlus, UG_CT_EditDefault
        , UG_CT_EditPlus, UG_CT_BasicCor, UG_CT_EditCor);

        if(UG_CT_TotalCT <= UG_CT_Max)
        {
            UG_CT_TotalCT = UG_CT_Max;
        }
    }

    void totalUGNeedCoinCal()
    {
        if(AtkUG <= 1)
            return;
        
        UG_NeedCoin_TotalNeedCoin = UG_NeedCoin_BasicDefault +
        ScoreManager.totalIntFormula(AtkUG-1, UG_NeedCoin_BasicPlus, UG_NeedCoin_EditDefault
        , UG_NeedCoin_EditPlus, UG_NeedCoin_BasicCor, UG_NeedCoin_EditCor);

        if(UG_NeedCoin_TotalNeedCoin >= UG_NeedCoin_Max)
        {
            UG_NeedCoin_TotalNeedCoin = UG_NeedCoin_Max;
        }
    }
}
