using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectController : MonoBehaviour {
    public bool startJoined = false;

    [Range(0, 3)]
    public int PlayerIndex;
    public PlayerSelectorData PlayerSelector;
    public GameObject Platform;
    public GameObject ButtonJoin;
    public GameObject ButtonLeave;
    public GameObject ButtonNext;

    PlayerDef _currentDef;
    GameObject _characterPreview;

    private void Start()
    {
        if(startJoined) {
            PlayerSelector.Reset();
            OnJoinClicked();
        }
    }

    public void OnJoinClicked() {
        ButtonJoin.SetActive(false);
        ButtonLeave.SetActive(true);
        ButtonNext.SetActive(true);
        Platform.SetActive(true);

        var playerDef = PlayerSelector.RegisterPlayer(PlayerIndex);
        UpdateCurrentDef(playerDef);
    }

    public void OnLeaveClicked() {
        ButtonJoin.SetActive(true);
        ButtonLeave.SetActive(false);
        ButtonNext.SetActive(false);
        Platform.SetActive(false);

        PlayerSelector.UnregisterPlayer(PlayerIndex);
    }

    public void OnNextClicked() {
        var nextDef = PlayerSelector.GetNext(PlayerIndex);
        UpdateCurrentDef(nextDef);
    }

    private void UpdateCurrentDef(PlayerDef next) {
        if (_characterPreview != null)
        {
            Destroy(_characterPreview);
            _characterPreview = null;
        }

        _currentDef = next;

        if(_currentDef != null) {
            _characterPreview = Instantiate(_currentDef.UIPreviewPrefab);
            _characterPreview.transform.parent = Platform.transform;
            _characterPreview.transform.localPosition = Vector3.zero;
            _characterPreview.transform.localRotation = Quaternion.identity;
        }
    }
}
