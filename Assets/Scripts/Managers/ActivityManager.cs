using UnityEngine;
using UnityEngine.Events;

public class ActivityManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<ActivityData> _onFinish;

    public void FinishActivity(ActivityData data) => _onFinish.Invoke(data);
}