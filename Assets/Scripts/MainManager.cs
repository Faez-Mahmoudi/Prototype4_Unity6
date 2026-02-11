using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance{get; private set;}
    public int bestWave;
    public int bestPoint;
    public bool isGameActive;

    // Save our data
    [System.Serializable]
    class SaveData
    {
        public int b_wave;
        public int b_point;
    }

    void Awake()
    {
       if (Instance != null)
       {
            Destroy(gameObject);
            return;
       }

       Instance = this;
       DontDestroyOnLoad(gameObject); 
       isGameActive = true;
       LoadScore();
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.b_wave = bestWave;
        data.b_point = bestPoint;

        string json  = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestWave = data.b_wave;
            bestPoint = data.b_point;
        }
        else
        {
            bestWave = 1;
            bestPoint = 0;
        }
    }
}
