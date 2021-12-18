using System;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField PlayernameInputField;

    public TMP_Text bestScoreMenuText;
    
    public string playerName;
    public int bestScore;

    public int bestScoreEver;
    public string bestPlayerEver;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        LoadBestScore();
        
        bestScoreMenuText.text = bestPlayerEver + "    " + bestScoreEver;
        
        DontDestroyOnLoad(Instance);
    }

    public void StartGame()
    {
        playerName = PlayernameInputField.text == "" ? "POLISH DUMPLING" : PlayernameInputField.text;

        SceneManager.LoadScene(1);
    }
    
    [Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScoreEver;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();

        data.bestPlayerName = playerName;
        data.bestScoreEver = bestScore;

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "bestScore.json", json);

    }
    
    public void LoadBestScore()
    {
        var path = Application.persistentDataPath + "bestScore.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);

            bestScoreEver = data.bestScoreEver;
            bestPlayerEver = data.bestPlayerName;
        }
        
        
    }
}
