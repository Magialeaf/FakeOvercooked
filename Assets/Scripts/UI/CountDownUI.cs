using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    // 挂载的文本发生的发低烧 
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private GameObject numberTextObject;
    private int preNumber = -1;

    private Animator anim;
    private const string IS_SHAKE = "IsShake";

    private void Start()
    {
        GameManager.Instance.onStateChanged += GameManager_OnStateChanged;
        anim = numberTextObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsCountDownToStart())
        {
            int nowNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
            numberText.text = nowNumber.ToString();
            if (nowNumber != preNumber)
            {
                SoundManager.Instance.PlayCountDownSound();
                anim.SetTrigger(IS_SHAKE);
                preNumber = nowNumber;
            }
        }
    }

    private void GameManager_OnStateChanged(object render, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStart())
        {
            numberText.gameObject.SetActive(true);
        }
        else
        {
            numberText.gameObject.SetActive(false);
        }
    }
}
