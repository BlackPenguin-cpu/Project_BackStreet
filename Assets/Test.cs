using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : Interaction
{
    protected override void Action()
    {
        SceneManager.LoadScene("TestScene");
    }
    protected override void Start()
    {
        base.Start();

    }

}