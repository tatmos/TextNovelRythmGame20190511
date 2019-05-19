using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{

	public Text text;

	double nextEventTime = 0;

	private int flip = 0;

	List<string> eventData = new List<string>();
	public List<AudioClip> clipData = new List<AudioClip>();
	private AudioSource[] audioSources = new AudioSource[2];


	// 文字の出現回数をカウント
	public static int CountChar(string s, char c)
	{
		return s.Length - s.Replace(c.ToString(), "").Length;
	}

	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			GameObject child = new GameObject("Player");
			child.transform.parent = gameObject.transform;
			audioSources[i] = child.AddComponent<AudioSource>();
		}

		nextEventTime = AudioSettings.dspTime + 0.0f;

		eventData.Add("はじまりは、勝手に、進む1");
		eventData.Add("次の段落も、勝手に、進む2");
		eventData.Add("次の段落も、勝手に、進む3");
		eventData.Add("次の段落も、勝手に、進む4");
		eventData.Add("次の段落も、勝手に、進む5");
		eventData.Add("次の段落も、勝手に、進む6");
		eventData.Add("次の段落も、勝手に、進む7");
		eventData.Add("次の段落も、勝手に、進む8");
		eventData.Add("ラストは繰り返し");
		eventData.Add("倒した！");
	}

	int eventNo = -1;

	int lastCount = 0;

	// Update is called once per frame
	void Update()
	{      
		if (nextEventTime < AudioSettings.dspTime)
		{
			if (eventData.Count > eventNo)
			{
				bool nextFlag = false;

				if (eventNo == 8)
				{
					//  8回目だけ何回か繰り返す
					lastCount++;
					if (lastCount > 3)
					{
						nextFlag = true;
					}
				}
				else
				{
					nextFlag = true;
				}

				if (eventNo >= 0)    //  初回は何もしない
				{
					//  テキスト表示
					text.text += "\n";
					text.text += eventData[eventNo];
				} else {
					eventNo++;
					return;
				}

				if (eventNo == 9)
				{
					eventNo++; return;  //  配列外にして終わり
				}

				//  音は次のイベントの音を先行して再生させる。（遅延して再生）
				audioSources[flip].clip = clipData[eventNo + (nextFlag ? 1 : 0)]; //  次のクリップを指定
				nextEventTime += clipData[eventNo].length;          //  今のクリップの時間を指定
				audioSources[flip].PlayScheduled(nextEventTime);    //  予約再生

				flip = 1 - flip;

				if(nextFlag)eventNo++;
			}


		}
	}
}
