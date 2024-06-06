using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-100)]
public class LogScript : MonoBehaviour
{
    [SerializeField] TooltipedItem prefabEntry;

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        //if (type != LogType.Error) return;
        //if (stackTrace.Length < 10) return;
            var instance = Instantiate(prefabEntry, transform);

        instance.SetText(logString, stackTrace);
        //instance.gameObject.SetActive(true);
        //instance.transform.SetAsFirstSibling();
        //output = logString;
        //stack = stackTrace;
    }
}
