using System;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents {
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class VisualPiece : MonoBehaviour
    {
        [SerializeField] private float _fallSpeed;
        
        private bool _isFalling;
        private Vector3 _targetPosition;
        private Transform _selfTransform;

        private void Awake()
        {
            _selfTransform = transform;
        }

        public void SetSprite(Sprite sprite) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }

        public void SetTargetPosition(Vector3 startPos, Vector3 endPos)
        {
            _selfTransform.localPosition = new Vector3(startPos.x, startPos.y, endPos.z);
            _targetPosition = endPos;
            _isFalling = true;
        }

        private void Update()
        {
            if (!_isFalling)
            {
                return;
            }

            var pos = _selfTransform.localPosition;
            pos.y -= _fallSpeed * Time.deltaTime;
            _selfTransform.localPosition = pos;

            if (pos.y > _targetPosition.y)
            {
                return;
            }
            
            _isFalling = false;
            _selfTransform.localPosition = _targetPosition;
        }
    }
}