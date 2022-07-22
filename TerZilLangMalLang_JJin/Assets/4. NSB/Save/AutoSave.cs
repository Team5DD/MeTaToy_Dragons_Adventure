using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class AutoSave : MonoBehaviour
{
    // ---싱글톤으로 선언--- 
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    public static AutoSave _instance;
    public static AutoSave instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(AutoSave)) as AutoSave;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public GameObject[] char_Prefeb;
    public GameObject char_Prefeb_Choice;
    public GameObject save_char_Img;
    public string save_char_Name;
    public int save_nft_Number;
    public int save_StageClear;
    public bool isTitleSkip = false;
    public string TicketRandNum;

    public class GameData
    {
        public bool isClear_1 = false;
        public bool isClear_2 = false;
        public bool isClear_3 = false;
        public bool isClear_4 = false;
        public bool isClear_5 = false;
    }

    // --- 게임 데이터 파일이름 설정 ---
    public string GameDataFileName = "DragonsData.json";

    // "원하는 이름(영문).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // 게임이 시작되면 자동으로 실행되도록
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "DragonsData.json");
        LoadGameData();
        SaveGameData();
    }

    private void Awake()
    {
        _instance = this;
    }


    // 저장된 게임 불러오기 //Application.persistentDataPath
    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath + GameDataFileName);

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            print("불러오기 성공");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        // 저장된 게임이 없다면
        else
        {
            print("새로운 파일 생성");
            _gameData = new GameData();
        }
    }

    // 게임 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Path.Combine(Application.persistentDataPath + GameDataFileName);

        // 이미 저장된 파일이 있다면 덮어쓰기
        File.WriteAllText(filePath, ToJsonData);

        // 올바르게 저장됐는지 확인 (자유롭게 변형)
        print("저장완료");
        print("2는 " + gameData.isClear_2);
        print("3는 " + gameData.isClear_3);
        print("4는 " + gameData.isClear_4);
        print(filePath);
    }

    void Update()
    {
        DontDestroyOnLoad(this);
        print("1" + gameData.isClear_1);
        print("2" + gameData.isClear_2);
        print("3" + gameData.isClear_3);
        print("4" + gameData.isClear_4);
        print("5" + gameData.isClear_5);
    }

    // 게임을 종료하면 자동저장되도록
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveGameData();
        }
    }

}