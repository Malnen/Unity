using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSlave : MonoBehaviour
{
    #region Singletone
    public static CoroutineSlave slave;
    private void Awake()
    {
        slave = this;
    }
    #endregion
}
