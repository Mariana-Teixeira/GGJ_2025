using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Activity.FirstToClick
{
    [Serializable]
    public struct PlayerData
    {
        public KeyCode Key;
        public float Time;
        public bool Clicked;
        public bool ValidMove;
    }
     
     public class FirstToClick : MonoBehaviour
     {
         [Header("Timer Parameters")]
         [SerializeField] private float _minimumWait;
         [SerializeField] private float _maximumWait;
         [SerializeField] private float _timeRange;
         [SerializeField] private AudioClip _executionClip;
     
         [Header("Player Parameters")]
         [SerializeField] private KeyCode _player1Key;
         [SerializeField] private KeyCode _player2Key;

         private IEnumerator execution;
         private IEnumerator timer;

         private PlayerData _player1;
         private PlayerData _player2;
         
         private ActivityManager _activityManager;
         private bool _isExecuting;
         private bool _isValidTimer;
         private float _stopwatchTime;
         private float _perfectTime;
     
         private void Awake()
         {
             _activityManager = GetComponent<ActivityManager>();
             _activityManager.gameObject.SetActive(false);
         }

         private void OnEnable()
         {
             SetParameters();
             StartCoroutine(execution);
         }

         private void OnDisable()
         {
             StopAllCoroutines();
         }

         private void Reset()
         {
             StopAllCoroutines();
             StartCoroutine(execution);
         }

         private void SetParameters()
         {
             _perfectTime = Random.Range(_minimumWait, _maximumWait);
             var firstTimer = new WaitForSeconds(_perfectTime);
             var secondTimer = new WaitForSeconds(_timeRange);
             timer = StartTimer(firstTimer, secondTimer);
             execution = Execute();
             
             _player1 = new PlayerData
             {
                 Key = _player1Key,
                 Time = 0,
                 Clicked = false,
                 ValidMove = false
             };
             
             _player2 = new PlayerData
             {
                 Key = _player2Key,
                 Time = 0,
                 Clicked = false,
                 ValidMove = false
             };
         }
     
         private void Update()
         {
             if (Input.GetKeyDown(KeyCode.R)) Reset();
             if (!_isExecuting) return;
             
             _stopwatchTime += Time.deltaTime;
             CheckForClick(ref _player1);
             CheckForClick(ref _player2);

             if (!_player1.Clicked || !_player2.Clicked) return;
             _isExecuting = false;
             StopAllCoroutines();
             FinishGame();
         }
     
         private void CheckForClick(ref PlayerData player)
         {
             if (player.Clicked) return;
             if (!Input.GetKeyDown(player.Key)) return;
             player.ValidMove = _isValidTimer;
             player.Time = _stopwatchTime;
             player.Clicked = true;
         }

         private IEnumerator Execute()
         {
             SetParameters();
             yield return StartCoroutine(timer);
             FinishGame();
         }
     
         private IEnumerator StartTimer(WaitForSeconds firstTimer, WaitForSeconds secondTimer)
         {
             Debug.Log("...");
             _isExecuting = true;
             _isValidTimer = false;
             _stopwatchTime = 0;
             
             yield return firstTimer;
             Debug.Log("CLICK");
             _isValidTimer = true;
             
             yield return secondTimer;
             Debug.Log("Time Run Out!");
             _isExecuting = false;
         }

         private void FinishGame()
         {
             GetWinner();
             _activityManager.FinishActivity(new ActivityData());
         }

         private void GetWinner()
         {
             var validPlayer1 = _player1.Clicked && _player1.ValidMove;
             var validPlayer2 = _player2.Clicked && _player2.ValidMove;
             
             // Check Player Validity
             if (!validPlayer1 && validPlayer2) Debug.Log("Player 2 Win!");
             if (!validPlayer2 && validPlayer1) Debug.Log("Player 1 Win!");
             if (!validPlayer2 && !validPlayer1) Debug.Log("Both Players Lost!");
             if (!validPlayer1 || !validPlayer2) return;

             // Check Player Proximity
             var player1Time = _player1.Time - _perfectTime;
             var player2Time = _player2.Time - _perfectTime;
             if (player1Time < player2Time) Debug.Log("Player 1 Wins!");
             else if (player2Time < player1Time) Debug.Log("Player 2 Wins!");
             else Debug.Log("Both Players Lost!");
         }
     }   
}