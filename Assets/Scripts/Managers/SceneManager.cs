using System;
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
    
    [SerializeField] private GameObject[] _activities;
    private int _activityIndex = 0;

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
        _activities[_activityIndex].SetActive(true);
    }

    private void SetParameters()
    {
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

    public void CallNextScene()
    {
        _activities[_activityIndex].SetActive(false);
        _activities[_activityIndex++].SetActive(true);
    }
}
