using UnityEngine;

public class InvokeActivityFinish : MonoBehaviour
{
    private ActivityManager _activityManager;
    
    private void Start()
    {
        _activityManager = GetComponent<ActivityManager>();
    }

    public void CallFinishActivity()
    {
        _activityManager.FinishActivity(new ActivityData());
    }
}
