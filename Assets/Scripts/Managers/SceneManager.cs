using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public struct PlayerKeys
{
    public KeyCode PrimaryKey;
    public KeyCode SecondaryKey;
    public KeyCode TertiaryKey;
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [Header("Interface Parameters")]
    [SerializeField] private GameObject _intermissionScreen;
    [SerializeField] private TMP_Text _activityTitle;
    [SerializeField] private float _transitionTime;
    
    [Header("Activity Parameters")]
    [SerializeField] private GameObject[] _activities;
    private int _activityIndex = 0;

    private WaitForSeconds _waitForSeconds;

    public PlayerKeys Player1Keys { get; private set; }
    public PlayerKeys Player2Keys { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("More than one 'SceneManagers' in Scene.");
        
        SetParameters();
    }

    private void Start()
    {
        StartCoroutine(WaitTimer(UpdateScreen, () =>
        {
           _activities[_activityIndex].SetActive(true);     
        }));
    }

    public void OnStartNextActivity()
    {
        StartCoroutine(WaitTimer(UpdateScreen, SwitchActivity));
    }

    private void SetParameters()
    {
        _intermissionScreen.SetActive(false);
        _activityTitle.text = "null";
        _waitForSeconds = new WaitForSeconds(_transitionTime);
        
        Player1Keys = new PlayerKeys
        {
            PrimaryKey = KeyCode.Q,
            SecondaryKey = KeyCode.W,
            TertiaryKey = KeyCode.E
        };

        Player2Keys = new PlayerKeys()
        {
            PrimaryKey = KeyCode.I,
            SecondaryKey = KeyCode.O,
            TertiaryKey = KeyCode.P
        };
    }

    private void SwitchActivity()
    {
        if (_activityIndex == 0)
        {
            _activities[_activityIndex].SetActive(true);
        }
        else
        {
            _activities[_activityIndex].SetActive(false);
            _activities[++_activityIndex].SetActive(true);
        }
    }

    private IEnumerator WaitTimer(Action onStartTimer, Action onEndTimer)
    {
        _intermissionScreen.SetActive(true);
        onStartTimer.Invoke();
        yield return _waitForSeconds;
        _intermissionScreen.SetActive(false);
        onEndTimer.Invoke();
    }

    private void UpdateScreen()
    {
        _activityTitle.text = _activities[_activityIndex].name;
    }
}
