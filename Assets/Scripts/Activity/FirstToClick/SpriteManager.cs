using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Activity.FirstToClick
{
    [Serializable]
    public struct PlayerSprites
    {
        public Sprite Idle;
        public Sprite Shouting;
    }
    
    public class SpriteManager : MonoBehaviour
    {
        [Header("Sprite Parameters")]
        [SerializeField] private SpriteRenderer _player1Renderer;
        [SerializeField] private PlayerSprites _player1Sprites;
        [Space(10)]
        [SerializeField] private SpriteRenderer _player2Renderer;
        [SerializeField] private PlayerSprites _player2Sprites;
        
        [Header("Text Parameters")]
        [SerializeField] private TMP_Text _bathTimeLine;
        [SerializeField] private TMP_Text _player1Line;
        [SerializeField] private TMP_Text _player2Line;
        [SerializeField] private float _popSpeed = 0.2f;
        [SerializeField] private Ease _popEase;

        private void Awake()
        {
            Reset();
        }

        public void Reset()
        {
            _player1Renderer.sprite = _player1Sprites.Idle;
            _player2Renderer.sprite = _player2Sprites.Idle;
            _bathTimeLine.transform.localScale = Vector3.zero;
            _player1Line.transform.localScale = Vector3.zero;
            _player2Line.transform.localScale = Vector3.zero;
        }

        public void ChangeToBath()
        {
            _bathTimeLine.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
        }

        public void Player1Shout()
        {
            _player1Renderer.sprite = _player1Sprites.Shouting;
            _player1Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
        }

        public void Player2Shout()
        {
            _player2Renderer.sprite = _player2Sprites.Shouting;
            _player2Line.transform.DOScale(Vector3.one, _popSpeed).SetEase(_popEase);
        }
    }
}