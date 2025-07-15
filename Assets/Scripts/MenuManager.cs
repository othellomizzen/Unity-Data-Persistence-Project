using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;
    public int highscore;
    public string highscoreHolder;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);

        LoadHighscores();
    }

    [System.Serializable]
    class SaveData
    {
        public string highscoreHolder;
        public int highscore;
    }

    public void LoadLevel()
    {
        playerName = GrabPlayerName();
        if (playerName == "")
        {
            Debug.Log("Please type a name!");
        }
        else
        {
            SceneManager.LoadScene("main");
        }
            
    }

    public void LoadHighscores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highscore = data.highscore;
            highscoreHolder = data.highscoreHolder;
        }
    }

    public void SaveHighscores()
    {
        SaveData data = new SaveData(); 
        data.highscore = highscore;
        data.highscoreHolder = highscoreHolder;
        string json = JsonUtility.ToJson(data); 
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private string GrabPlayerName()
    {
        TMP_InputField nameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        return nameInput.text;
    }
}
