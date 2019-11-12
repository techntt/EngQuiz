using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    public TextAsset[] data;

    public void ReadQuestionFromFileData(int file_id)
    {        
        TextAsset asset = Resources.Load<TextAsset>(Const.DATA_FILE + file_id);
        if (asset == null)
            return;
        //Debug.Log("Data: " + asset.text);
        Data data = JsonConvert.DeserializeObject<Data>(asset.text);

    }
}


public class Data
{
    public Question[] quest { get; set; }
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
