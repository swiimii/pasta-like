using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRoom : MonoBehaviour
{
    public Door doorToUnlock;

    public void Unlock()
    {
        doorToUnlock.locked = false;
    }
}
