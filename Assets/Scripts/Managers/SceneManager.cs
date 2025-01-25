using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _activities;

    private void Start()
    {
        _activities[0].SetActive(true);
    }
}
