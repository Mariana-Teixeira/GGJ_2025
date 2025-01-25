using UnityEngine;
using UnityEngine.Events;

public class ActivityManager : MonoBehaviour
{
    private UnityEvent<ActivityData> _onFinish;
    private ActivityData _activityData;

    public void FinishActivity(ActivityData data) => _onFinish.Invoke(_activityData);
}