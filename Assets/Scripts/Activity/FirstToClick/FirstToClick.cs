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

        public PlayerData(KeyCode code)
        {
            Key = code;
            Time = 0f;
            Clicked = false;
            ValidMove = false;
        }

        public void Reset()
        {
            Time = 0f;
            Clicked = false;
            ValidMove = false;
        }
    }
     
     public class FirstToClick : BaseActivity
     {
         [Header("Timer Parameters")]
         [SerializeField] private float _minimumWait;
         [SerializeField] private float _maximumWait;
         [SerializeField] private float _timeRange;
     
         private KeyCode _player1Key;
         private KeyCode _player2Key;

         private IEnumerator timer;

         private PlayerData _player1;
         private PlayerData _player2;
         private Loser _loser;
         
         private bool _isExecuting;
         private bool _isValidTimer;
         private float _stopwatchTime;
         private float _perfectTime;
     
         private void Awake()
         {
             this.gameObject.SetActive(false);
         }

         private void Start()
         {
             _player1Key = SceneManager.Instance.Player1Data.PrimaryKey;
             _player2Key = SceneManager.Instance.Player2Data.PrimaryKey;

             _player1 = new PlayerData(_player1Key);
             _player2 = new PlayerData(_player2Key);
         }
         
         public override void StartActivity()
         {
             SetParameters();
             StartCoroutine(Execute());
         }

         public override void EndActivity()
         {
             StopAllCoroutines();
         }

         private void Reset()
         {
             StopAllCoroutines();
             StartCoroutine(Execute());
         }

         private void SetParameters()
         {
             _perfectTime = Random.Range(_minimumWait, _maximumWait);
             var firstTimer = new WaitForSeconds(_perfectTime);
             var secondTimer = new WaitForSeconds(_timeRange);
             timer = StartTimer(firstTimer, secondTimer);
             _player1.Reset();
             _player2.Reset();
         }
     
         private void Update()
         {
             // if (Input.GetKeyDown(KeyCode.R)) Reset();
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
             _isExecuting = true;
             _isValidTimer = false;
             _stopwatchTime = 0;
             
             yield return firstTimer;
             Debug.Log("CLICK");
             _isValidTimer = true;
             
             yield return secondTimer;
             _isExecuting = false;
         }

         private void FinishGame()
         {
             GetWinner();
             _onFinish.Invoke(new ActivityData(_loser));
         }

         private void GetWinner()
         {
             var validPlayer1 = _player1.Clicked && _player1.ValidMove;
             var validPlayer2 = _player2.Clicked && _player2.ValidMove;
             
             // Check Player Validity
             if (!validPlayer1 && validPlayer2) _loser = Loser.Player1;
             if (!validPlayer2 && validPlayer1) _loser = Loser.Player2;
             if (!validPlayer2 && !validPlayer1) _loser = Loser.Both;
             if (!validPlayer1 || !validPlayer2) return;

             // Check Player Proximity
             var player1Time = _player1.Time - _perfectTime;
             var player2Time = _player2.Time - _perfectTime;
             if (player1Time < player2Time) _loser = Loser.Player2;
             else if (player2Time < player1Time) _loser = Loser.Player1;
             else _loser = Loser.Both;
         }
     }   
}