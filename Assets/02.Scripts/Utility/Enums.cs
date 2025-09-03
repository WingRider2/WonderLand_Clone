using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 타입에대한 enum입니다.
/// </summary>
public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

/// <summary>
/// 사운드 종료에대한 enum입니다.
/// </summary>
public enum SoundType
{
    None,
    landSound,
    walkSound,
    dropSound,
    jumpSound,
    BGM,
    roomRotateSound,
    clearPuzzleSound,
    openPuzzleSound,
    openDoorSound,
    closeDoorSound,
    bookSound,
    breakSound,
    returnSound,
    eatSound,
    grabSound,
    throwSound
}