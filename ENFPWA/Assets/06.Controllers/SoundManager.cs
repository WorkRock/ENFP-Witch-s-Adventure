using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    /* // 싱글톤 //
     * instance라는 변수를 static으로 선언을 하여 다른 오브젝트 안의 스크립트에서도 instance를 불러올 수 있게 합니다 
     */
    public static SoundManager instance = null; 

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }


    [System.Serializable]
    public struct AudioType
    {
        public string name;
        public AudioClip audio; 
    }

    // Inspector 에표시할 사운드 목록
    public AudioType[] BGMList; // BGM 목록

    public AudioType[] AudioList; // 효과음 목록

    private AudioSource BGM;
    private AudioSource audioSource_01;
    private AudioSource audioSource_02;
    private AudioSource audioSource_03;
    private AudioSource audioSource_04;
    private AudioSource audioSource_05;

    private string NowBGMname="";

    private string NowAudioname="";


    private bool isLobby;
    private bool isSoundOn;

    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        audioSource_01 = gameObject.AddComponent<AudioSource>();
        audioSource_02 = gameObject.AddComponent<AudioSource>();
        audioSource_03 = gameObject.AddComponent<AudioSource>();
        audioSource_04 = gameObject.AddComponent<AudioSource>();
        audioSource_05 = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
    }

    void Update()
    {
        isLobby = ScoreManager.GetIsLobby();
        isSoundOn = ScoreManager.GetIsSoundOn();

        if(!isSoundOn) 
        {
            SoundOff();
            return;
        }

        switch(SceneManager.GetActiveScene().name)
        {
            case "Lobby":
            if(isLobby)
            {
                SoundOn();
                BGM.volume = 1.0f;
                PlayBGM("Lobby");
            }
            else
            {
                SoundOff();
            }
            break;

            case "Ingame":
            SoundOn();
            PlayBGM("Ingame");
            BGM.volume = 0.5f;
            break;

            case "Result":
            PlayBGM("Result");
            BGM.volume = 1.0f;
            break;
        }
    }
    
    public void PlayBGM(string name)
    {
        if (NowBGMname.Equals(name)) return;

        for (int i = 0; i < BGMList.Length; ++i)
            if (BGMList[i].name.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
    }

    public void PlayAudio_01(string name)
    {
        if (NowAudioname.Equals(name)) return;

        for (int i = 0; i < AudioList.Length; ++i)
            if (AudioList[i].name.Equals(name))
            {
                audioSource_01.clip = AudioList[i].audio;
                audioSource_01.Play();
                NowAudioname = name;
            }
    }

     public void PlayAudio_02(string name)
    {
        for (int i = 0; i < AudioList.Length; ++i)
            if (AudioList[i].name.Equals(name))
            {
                audioSource_02.clip = AudioList[i].audio;
                audioSource_02.Play();
            }
    }

     public void PlayAudio_03(string name)
    {
        for (int i = 0; i < AudioList.Length; ++i)
            if (AudioList[i].name.Equals(name))
            {
                audioSource_03.clip = AudioList[i].audio;
                audioSource_03.Play();
            }
    }

     public void PlayAudio_04(string name)
    {
        for (int i = 0; i < AudioList.Length; ++i)
            if (AudioList[i].name.Equals(name))
            {
                audioSource_04.clip = AudioList[i].audio;
                audioSource_04.Play();
            }
    }

     public void PlayAudio_05(string name)
    {
        for (int i = 0; i < AudioList.Length; ++i)
            if (AudioList[i].name.Equals(name))
            {
                audioSource_05.clip = AudioList[i].audio;
                audioSource_05.Play();
            }
    }

    void SoundOn()
    {
        BGM.mute = false;
        audioSource_01.mute = false;
        audioSource_02.mute = false;
        audioSource_03.mute = false;
        audioSource_04.mute = false;
        audioSource_05.mute = false;
    }

    void SoundOff()
    {
        BGM.mute = true;
        audioSource_01.mute = true;
        audioSource_02.mute = true;
        audioSource_03.mute = true;
        audioSource_04.mute = true;
        audioSource_05.mute = true;
    }

}
