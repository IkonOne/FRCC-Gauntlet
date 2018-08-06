using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "FRCC-Gauntlet/PlayerSelectorData")]
public class PlayerSelectorData : ScriptableObject {
    public List<PlayerDef> PlayerDefs;

    int[] _indexedPlayerDefs = { -1, -1, -1, -1 }; // Indexed by player index

    public void Reset() {
        _indexedPlayerDefs = new int[] { -1, -1, -1, -1 };
    }

    /// <summary>
    /// Gets the PlayerDef for the player at index playerIndex.
    /// </summary>
    /// <returns>The PlayerDef.  Returns null if the player is not registered.</returns>
    /// <param name="playerIndex">Player index.</param>
    public PlayerDef GetPlayerDef(int playerIndex) {
        PlayerDef def = null;
        if (_indexedPlayerDefs[playerIndex] != -1)
            def = PlayerDefs[_indexedPlayerDefs[playerIndex]];

        return def;
    }

    public PlayerDef RegisterPlayer(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        // find the first unused PlayerDef
        var defIdx = 0;
        while(EnumerableContains(_indexedPlayerDefs, defIdx)) {
            defIdx = WrapPlayerDefIndex(defIdx + 1);
        }

        _indexedPlayerDefs[playerIndex] = defIdx;
        return PlayerDefs[defIdx];
    }

    public void UnregisterPlayer(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        _indexedPlayerDefs[playerIndex] = -1;
    }

    public PlayerDef GetNext(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        var defIdx = _indexedPlayerDefs[playerIndex];
        if (EnumerableContains(_indexedPlayerDefs, -1))
        {
            do
            {
                defIdx = WrapPlayerDefIndex(_indexedPlayerDefs[playerIndex] + 1);
            } while (EnumerableContains(_indexedPlayerDefs, defIdx)) ;
        }

        _indexedPlayerDefs[playerIndex] = defIdx;
        return PlayerDefs[defIdx];
    }

    public PlayerDef GetPrevious(int playerIndex) {
        Debug.Assert(playerIndex >= 0 && playerIndex < 4);

        var defIdx = _indexedPlayerDefs[playerIndex];
        if (EnumerableContains(_indexedPlayerDefs, -1))
        {
            do
            {
                defIdx = WrapPlayerDefIndex(defIdx - 1);
            } while (EnumerableContains(_indexedPlayerDefs, defIdx)) ;
        }

        _indexedPlayerDefs[playerIndex] = defIdx;
        return PlayerDefs[defIdx];
    }

    private int WrapPlayerDefIndex(int index) {
        while (index < 0) index += PlayerDefs.Count;
        while (index >= PlayerDefs.Count) index -= PlayerDefs.Count;
        return index;
    }

    private bool EnumerableContains<T>(IEnumerable<T> enumerable, T obj) {
        foreach (var item in enumerable)
        {
            if (item.Equals(obj))
                return true;
        }

        return false;
    }
}