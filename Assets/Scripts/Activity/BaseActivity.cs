using UnityEngine;
using UnityEngine.Events;

public abstract class BaseActivity : MonoBehaviour, IActivity
{
    [SerializeField] protected UnityEvent<ActivityData> _onFinish;
    
    public abstract void StartActivity();
    public abstract void EndActivity();
}