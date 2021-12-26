using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Chat talk;
    public virtual void Action()
    {
        if(talk != null && talk.TalkList.Length > 0)
        {
            TextManager.instance.GetChat(talk.TalkList);
        }
    }
}

[System.Serializable]
public class Chat
{
    public Talk[] TalkList;
}

[System.Serializable]
public class Talk
{
    public TalkData[] TextList;
}

[System.Serializable]
public class TalkData
{
    public Transform transform;

    [TextArea]
    public string Text;
}
