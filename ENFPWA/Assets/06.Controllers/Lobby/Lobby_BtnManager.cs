using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby_BtnManager : MonoBehaviour
{
    public AudioSource LobbyBtnAudioSource;

    public GameObject UI_Lobby;


    public GameObject UI_Func;

    public GameObject Info;
    public GameObject Shop;
    public GameObject Credit;
    public GameObject Option;

    public GameObject OptionFunc;
    public GameObject Warning;
    public GameObject Reset;
    public GameObject QuitGame;




    private bool isSoundOn;
    private bool isInfo;
    private bool isShop;
    private bool isCredit;
    private bool isOption;


    private bool isReset = false;
    private bool isQuit = false;



    void Start()
    {
        isSoundOn = ScoreManager.GetIsSoundOn();
    }


    // InfoOn Btn
    public void InfoOn()
    {
        UI_Func.SetActive(true);
        isInfo = true;
        Info.SetActive(true);
    }

    // ShopOn Btn
    public void ShopOn()
    {
        UI_Func.SetActive(true);
        isShop = true;
        Shop.SetActive(true);
    }

    // CreditOn Btn
    public void CreditOn()
    {
        UI_Func.SetActive(true);
        isCredit = true;
        Credit.SetActive(true);
    }

    // OptionOn Btn
    public void OptionOn()
    {
        UI_Func.SetActive(true);
        isOption = true;
        Option.SetActive(true);
    }

    // Func Exit Btn
    public void FuncExit()
    {
        if (isInfo)
        {
            isInfo = false;
            Info.SetActive(false);
        }

        else if (isShop)
        {
            isShop = false;
            Shop.SetActive(false);
        }

        else if (isCredit)
        {
            isCredit = false;
            Credit.SetActive(false);
        }

        else if (isOption)
        {
            isOption = false;
            Option.SetActive(false);
        }

        UI_Func.SetActive(false);
    }

    public void SoundOnOff()
    {
        if(isSoundOn)
        {
            isSoundOn = false;
            ScoreManager.SetIsSoundOn(isSoundOn);
            LobbyBtnAudioSource.mute = true;
        }

        else
        {
            isSoundOn = true;
            ScoreManager.SetIsSoundOn(isSoundOn);
            LobbyBtnAudioSource.mute = false;
        }
    }



    // Account Reset
    public void ResetAccount()
    {
        if (isQuit || isReset)
            return;

        OptionFunc.SetActive(true);
        isReset = true;
        Warning.SetActive(true);
    }

    public void ResetAccount_Yes()
    {
        Warning.SetActive(false);
        Reset.SetActive(true);
    }

    public void ResetAccount_No()
    {
        OptionFunc.SetActive(false);
        Warning.SetActive(false);
        isReset = false;
    }

    public void ResetAccount_OK()
    {
        isReset = false;

        ScoreManager.SetPlayerLevel(1);
        ScoreManager.SetAtkUG(1);
        ScoreManager.SetCoin(0);
        ScoreManager.SetExp(0);
        ScoreManager.SetBStage(0);
        ScoreManager.SetIsLobby(false);

        SceneManager.LoadScene("Lobby");
    }


    // Quit Game
    public void Quit()
    {
        if (isReset || isQuit)
            return;

        OptionFunc.SetActive(true);
        isQuit = true;
        QuitGame.SetActive(true);
    }

    public void Quit_Yes()
    {
        isQuit = false;

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Quit_No()
    {
        OptionFunc.SetActive(false);
        isQuit = false;
        QuitGame.SetActive(false);
    }


    // GameStart Btn
    public void GameStart()
    {
        ScoreManager.SetIsLobby(false);
        ScoreManager.SetStage(1);
        SceneManager.LoadScene("Ingame");
    }
}
