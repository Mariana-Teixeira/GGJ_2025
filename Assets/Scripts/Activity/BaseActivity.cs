using UnityEngine;
using UnityEngine.Events;

public abstract class BaseActivity : MonoBehaviour, IActivity
{
    [SerializeField] protected UnityEvent<ActivityData> _onFinish;
    [SerializeField] protected string _activityName;
    [SerializeField] protected string _activityInstruction;

    public string ActivityName => _activityName;
    public string ActivityInstruction => _activityInstruction;
    
    public abstract void StartActivity();
    public abstract void EndActivity();
}