using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonObject : Singleton<SingletonObject>
{
    protected override void Awake()
    {
        isNotSingle = true;
        base.Awake();
    }
}
