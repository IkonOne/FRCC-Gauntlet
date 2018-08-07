using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour {
    public PlayerDef def;
    public LayerMask GroundLayers;
    public string GameCameraName = "GameCamera";

    CharacterController _controller;
    PlayerView _view;
    Vector2 _inputVec = Vector2.zero;
    bool _inputFireDown;
    bool _isShooting;
    Transform _gameCamera;
    Vector3 _moveVec = Vector3.zero;

	void Start () {
        _controller = GetComponent<CharacterController>();
        _view = GetComponent<PlayerView>();

        var health = GetComponent<Health>();
        health.OnDied += HandlePlayerDied;

        _gameCamera = GameObject.Find(GameCameraName).transform;
	}

    void Update()
    {
        UpdateInput();
        DoMove();
        DoLook();
        MoveToGround();
        UpdateAnimation();
    }

    void UpdateInput() {
        _inputVec = new Vector2(
            Input.GetAxisRaw(def.ControllerDef.HorizontalAxis),
            -Input.GetAxisRaw(def.ControllerDef.VerticalAxis)
        );

        // Rotate the _inputVec to be relative to the game camera because
        // the camera moves behind us, input forward might become back, left or right.
        var euler = transform.eulerAngles;
        euler.y = _gameCamera.eulerAngles.y;
        transform.eulerAngles = euler;

        _inputFireDown = Input.GetButton(def.ControllerDef.FireButton);

        //if(_inputVec.magnitude != 0.0f)
        //{
        //    Debug.Break();
        //}
    }

    void UpdateAnimation() {
        if(_inputVec.sqrMagnitude > 0) {
            _view.SetIsRunning(true);
        }
        else {
            _view.SetIsIdle(true);
        }

        _view.SetIsAttacking(_inputFireDown);
    }

    /// <summary>
    /// Move in the input direction.  Or don't.
    /// </summary>
    void DoMove() {
        if(_inputVec.sqrMagnitude > 0.0f) {
            _moveVec =
                transform.forward * _inputVec.y * def.speed +
                transform.right * _inputVec.x * def.speed;

            _moveVec.y = 0.0f;

            _controller.Move(_moveVec * Time.deltaTime);
        }
    }

    /// <summary>
    /// Look in the input direction
    /// </summary>
    void DoLook() {
        _view.transform.LookAt(transform.position + _moveVec);
        //if(_inputVec.sqrMagnitude > 0) {
        //    var angle = Vector2.SignedAngle(_inputVec, Vector2.up);
        //    var dAngle = Mathf.DeltaAngle(transform.eulerAngles.y, angle);
        //    transform.Rotate(Vector3.up, Mathf.LerpAngle(0, dAngle, Time.deltaTime / def.lookSmoothing));
        //}
    }

    /// <summary>
    /// We don't actually need gravity in this game.  So just see if there is something
    /// in the ground layer below us and move to it.
    /// </summary>
    void MoveToGround() {
        var ray = new Ray(transform.position, Vector3.down);
        var hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 100, GroundLayers)) {
            _controller.Move(Vector3.down * hit.distance);
        }
    }
  
    void HandlePlayerDied(Health health)
    {
        enabled = false;
        _view.enabled = false;

        Invoke("DestroyMe", 1.5f);  
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
