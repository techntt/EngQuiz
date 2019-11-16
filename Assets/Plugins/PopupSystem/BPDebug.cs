using UnityEngine;
using System.Collections;
public class BPDebug : MonoBehaviour
{
    private string logMessage = "";
    private Rect logPosition;
    private int lineCount = 0;

    private bool enableLog = false;  

    private static BPDebug mInstance;

    public static BPDebug Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("ScreenLogger").AddComponent<BPDebug>();
            }

            return mInstance;
        }
    }

    public static void LogMessage(string msg, bool error = false)
    {
        BPDebug.LogMessage(msg, false, error);
    }

    public static void LogMessage(string msg, bool clearScreen, bool error = false)
    {
        BPDebug instane = BPDebug.Instance;
        
        if (error) Debug.LogError(msg);
        else Debug.Log(msg);

        if (!instane.enableLog)
            return;

        if (clearScreen)
        {
            instane.logMessage = msg;
        }
        else
        {
            instane.lineCount++;
            if (instane.lineCount == 50)
            {
                instane.lineCount = 0;
                instane.logMessage = "";
            }

            instane.logMessage += "\n" + msg;
        }
    }

    // Use this for initialization
    void Start()
    {
        logPosition = new Rect(10, 10, Screen.width - 10, Screen.height - 10);
    }

    void OnGUI()
    {
#if UNITY_EDITOR
        GUI.skin.label.fontSize = 20;
#else
            GUI.skin.label.fontSize = 10;
#endif
        GUI.Label(logPosition, logMessage);
    }
}
