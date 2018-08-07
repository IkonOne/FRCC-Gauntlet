using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveragePlayerPosition : MonoBehaviour {
    [Range(0.001f, 2f)]
    public float smoothing = 0.4f;

    public List<Transform> PlayerTransforms {
        get { return _playerTransforms; }
    }
    private List<Transform> _playerTransforms = new List<Transform>();
    private Stack<Transform> _toRemove = new Stack<Transform>();

    private void LateUpdate()
    {
        Vector3 averagePosition = Vector3.zero;
        for (int i = 0; i < _playerTransforms.Count; i++)
        {
            var playerTransform = _playerTransforms[i];
            if (playerTransform != null)
            {
                averagePosition += _playerTransforms[i].position;
            }
            else {
                _toRemove.Push(playerTransform);
            }
        }

        while(_toRemove.Count > 0) {
            _playerTransforms.Remove(_toRemove.Pop());
        }

        transform.position = Vector3.LerpUnclamped(
            transform.position,
            averagePosition / _playerTransforms.Count,
            Time.deltaTime / smoothing
        );
    }
}
