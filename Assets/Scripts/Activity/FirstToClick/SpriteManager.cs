using System;
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

        private void Awake()
        {
            _player1Renderer.sprite = _player1Sprites.Idle;
            _player2Renderer.sprite = _player2Sprites.Idle;
        }

        public void ChangeToShout()
        {
            _player1Renderer.sprite = _player1Sprites.Shouting;
            _player2Renderer.sprite = _player2Sprites.Shouting;
        }
    }
}