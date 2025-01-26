using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SceneManager))]
public class AnimationManager : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;
    
    [Header("Animation Parameters")]
    [SerializeField] private Transform _leftOpenPosition;
    [SerializeField] private Transform _rightOpenPosition;
    [SerializeField] private Transform _closedPosition;
    [SerializeField] private Ease _transitionEase;

    [Header("Events")]
    [SerializeField] private UnityEvent _onStartTransitionPlay;
    [SerializeField] private UnityEvent _onStartTransitionComplete;
    [SerializeField] private UnityEvent _onEndTransitionPlay;
    [SerializeField] private UnityEvent _onEndTransitionComplete;

    public void PlayAnimation(float time)
    {
        _leftDoor.DOMove(_closedPosition.position, time).SetEase(_transitionEase);
        _rightDoor.DOMove(_closedPosition.position, time).SetEase(_transitionEase)
            .OnPlay(_onStartTransitionPlay.Invoke)
            .OnComplete(_onStartTransitionComplete.Invoke);
    }

    public void RewindAnimation(float time)
    {
        _leftDoor.DOMove(_leftOpenPosition.position, time).SetEase(_transitionEase);
        _rightDoor.DOMove(_rightOpenPosition.position, time).SetEase(_transitionEase)
            .OnPlay(_onEndTransitionPlay.Invoke)
            .OnComplete(_onEndTransitionComplete.Invoke);
    }
}
