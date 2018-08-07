using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "FRCC-Gauntlet/PlayerSelectorData")]
public class PlayerSelectorData : ScriptableObject {
    public List<PlayerDef> PlayerDefs;

    Queue<PlayerDef> _unusedPlayerDefs = new Queue<PlayerDef>();
    PlayerDef[] _indexedPlayerDefs = new PlayerDef[4];

    public void Reset() {
        _unusedPlayerDefs.Clear();

        for (int i = 0; i < 4; i++)
        {
            _unusedPlayerDefs.Enqueue(PlayerDefs[i]);
            _indexedPlayerDefs[i] = null;
        }
    }

    /// <summary>
    /// Gets the PlayerDef for the player at index playerIndex.
    /// </summary>
    /// <returns>The PlayerDef.  Returns null if the player is not registered.</returns>
    /// <param name="playerIndex">Player index.</param>
    public PlayerDef GetPlayerDef(int playerIndex) {
        PlayerDef def = _indexedPlayerDefs[playerIndex];
        return def;
    }

    public PlayerDef RegisterPlayer(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);


        if (_unusedPlayerDefs.Count == 0)
            return _indexedPlayerDefs[playerIndex];

        var def = _unusedPlayerDefs.Dequeue();
        _indexedPlayerDefs[playerIndex] = def;

        return def;
    }

    public void UnregisterPlayer(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        var def = _indexedPlayerDefs[playerIndex];
        if (def != null)
            _unusedPlayerDefs.Enqueue(def);

        _indexedPlayerDefs[playerIndex] = null;
    }

    public PlayerDef GetNext(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        if (_unusedPlayerDefs.Count == 0)
            return _indexedPlayerDefs[playerIndex];

        var next = _unusedPlayerDefs.Dequeue();
        _unusedPlayerDefs.Enqueue(_indexedPlayerDefs[playerIndex]);
        _indexedPlayerDefs[playerIndex] = next;

        return next;
    }
}