using UnityEngine;

public class MyDebuggerr : MonoBehaviour
{
    void Start()
    {
        var rec = FindFirstObjectByType<PlayerInputRecorder>();
        Debug.Log($"MyDebuggerr::start '{rec}'");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            Debug.Log($"MyDebuggerr::Update key J '{FindFirstObjectByType<PlayerInputRecorder>()}'");

        }
    }
}
