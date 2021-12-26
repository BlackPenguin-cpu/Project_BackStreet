using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : Singleton<TextManager>
{
    private Queue<TalkData> talkdatalist = new Queue<TalkData>();
    private List<Talk> talks = new List<Talk>();
    private TalkData nowtalkdata = null;
    private int textingcount;
    public GameObject obj;
    public TextMeshPro tmpro;
    public Player player;
    public void GetChat(Talk[] talks)
    {
        this.talks = new List<Talk>();
        this.talks.AddRange(talks);
        player.movable = false;
        this.talkdatalist = new Queue<TalkData>();
        foreach(TalkData talkdata in this.talks[0].TextList)
        {
            talkdatalist.Enqueue(talkdata);
        }
        TextUpdate();
    }

    IEnumerator TextJinHaeng()
    {
        textingcount = 0;
        string s = nowtalkdata.Text;
        if(nowtalkdata.transform != null)
        {
            obj.transform.SetParent(nowtalkdata.transform);
            obj.transform.localPosition = new Vector3(0, 0.711f, 0);
        }
        while (textingcount < nowtalkdata.Text.Length)
        {
            tmpro.text = s.Substring(0, textingcount);
            textingcount++;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
    void Update()
    {
        if(nowtalkdata != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (textingcount == nowtalkdata.Text.Length)
                {
                    nowtalkdata = null;
                }
                else
                {
                    textingcount = nowtalkdata.Text.Length;
                    tmpro.text = nowtalkdata.Text;
                }
            }
        }
    }

    public void TextUpdate()
    {
        if(nowtalkdata == null)
        {
            if(talkdatalist.Count > 0)
            {
                nowtalkdata = talkdatalist.Dequeue();
                StartCoroutine(TextJinHaeng());
            }
            else
            {
                obj.SetActive(false);
                player.movable = true;
            }
        }
    }
}
