using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference PleaseMusic { get; private set; }
    [field: Header("SnotClick")]
    [field: SerializeField] public EventReference SnotClick { get; private set; }
    [field: Header("SnotPop")]
    [field: SerializeField] public EventReference SnotPop { get; private set; }
    [field: Header("BathTimeYell")]
    [field: SerializeField] public EventReference BathTimeYell { get; private set; }
    [field: Header("SiblingNo")]
    [field: SerializeField] public EventReference SiblingNo { get; private set; }
    [field: Header("MillionareCorrect")]
    [field: SerializeField] public EventReference MillionareCorrect { get; private set; }
    [field: Header("MillionareWrong")]
    [field: SerializeField] public EventReference MillionareWrong { get; private set; }
    [field: Header("BubbleGunPow")]
    [field: SerializeField] public EventReference BubbleGunPow { get; private set; }
    [field: Header("NannyHelp")]
    [field: SerializeField] public EventReference NannyHelp { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) Debug.LogError("Singleton with multiple Instances.");
        Instance = this;
    }
}
