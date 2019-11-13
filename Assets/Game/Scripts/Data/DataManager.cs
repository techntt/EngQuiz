using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    public Data data;
    public void ReadQuestionFromFileData(int file_id)
    {        
        TextAsset asset = Resources.Load<TextAsset>(Const.DATA_FILE + file_id);
        if (asset == null)
            return;
        //Debug.Log("Data: " + asset.text);
        data = JsonConvert.DeserializeObject<Data>(asset.text);
        data.Init();
    }
}


public class Data
{
    public Question[] quest { get; set; }

    private Dictionary<int, Question> questDict;
    public void Init()
    {
        questDict = new Dictionary<int, Question>();
        foreach (Question q in quest)
            if (questDict.ContainsKey(q.id))
                questDict[q.id] = q;
            else
                questDict.Add(q.id, q);
    }
    public Question GetQuestionById(int id)
    {
        Question question = null;
        return question;
    }
}
public class Question
{
    public int id { get; set; }
    public int type { get; set; }
    public string question { get; set; }
    public Answer[] answers { get; set; }
    public string correct { get; set; }
}

public class Answer
{
    public string id { get; set; }
    public string content { get; set; }
}
