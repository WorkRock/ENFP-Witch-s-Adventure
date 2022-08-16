using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private int AtkUG;
    private int Coin;

    [Header("UI_Shop")]
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



    // Start is called before the first frame update
    void Start()
    {
        AtkUG = ScoreManager.GetAtkUG();

        if(AtkUG <= 1)
        {
            AtkUG = 1;
            ScoreManager.SetAtkUG(1);
        }  
    }

    // Update is called once per frame
    void Update()
    {

    }

        // Upgrade Btn
    void UpgradeBtn()
    {
        int coin = ScoreManager.GetCoin();

        //if(coin > )
    }

    void totalUGAtkCal()
    {
        if(AtkUG <= 1)
            return;
        
        UG_DMG_TotalAtk = UG_DMG_BasicDefault +
        ScoreManager.totalFloatFormula(AtkUG - 1, UG_DMG_BasicPlus, UG_DMG_EditDefault
        , UG_DMG_EditPlus, UG_DMG_BasicCor, UG_DMG_EditCor);

        if(UG_DMG_TotalAtk >= UG_DMG_Max)
        {
            UG_DMG_TotalAtk = UG_DMG_Max;
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
}
