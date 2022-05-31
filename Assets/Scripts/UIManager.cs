using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance = null;

    public Text textPerc;
    public Text textWin;
    public Text coins;
    public Image progressBar;
    public Image blockPanel;
    public GameObject restartBtn;
    public GameObject claimBtn;
    public Animator coinHolder;
    public string dumpValue;
    private int basePlaces = 3;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void RestartGameOnAppRuntime()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ShowHideEndGameUI(bool show)
    {
        if (GameManager.instance.outcome == outcome.victory) {
            restartBtn.SetActive(show);
            blockPanel.enabled = show;
            textWin.gameObject.SetActive(show);
            textWin.text = "YOU WIN!!";
            textWin.GetComponent<RandomTextColor>().AnimateColor();
            textPerc.gameObject.SetActive(show);
            textPerc.text = "COMPLETED 100.0%";
            claimBtn.SetActive(show);
        } else {
            restartBtn.SetActive(show);
            blockPanel.enabled = show;
            textWin.gameObject.SetActive(show);
            textWin.text = "YOU LOSE";
            textWin.GetComponent<RandomTextColor>().ApplyLosingColor();
            textPerc.gameObject.SetActive(show);
            textPerc.text = dumpValue == "NaN" ? "COMPLETED " + "100.0%" : "COMPLETED " + dumpValue;
        }
    }

    public void ClaimCoins()
    {
        coinHolder.Play("CoinsToNumber");
        StartCoroutine(ScrambleNums());
        //coins.DOText("350", 0.55f, false, ScrambleMode.Numerals, "231443478195436").OnComplete(()=>HideClaimBtn()); DT 
    }

    private IEnumerator ScrambleNums()
    {
        float scr = 0.05f;
        while (scr > 0)
        {
            coins.text = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();

            scr -= 0.001f;
            yield return null;
        }
        GameManager.instance.coinsCollected += 500;

        
        int diff = GameManager.instance.coinsCollected.ToString().Length - basePlaces;
        if (diff > 0)
        {
            AdjustCoinTextPos(diff);
            basePlaces ++;
        }
        coins.text = GameManager.instance.coinsCollected.ToString();
        HideClaimBtn();
    }

   
    public void AdjustCoinTextPos(int zeroes)
    {
        coins.GetComponent<RectTransform>().anchoredPosition = new Vector2(coins.GetComponent<RectTransform>().anchoredPosition.x - 35 * zeroes, coins.GetComponent<RectTransform>().anchoredPosition.y);
    }

    private void HideClaimBtn()
    {
        claimBtn.SetActive(false);
    }

    public void RestartGameUI()
    {
        //coins.text = "000";
        progressBar.fillAmount = 0;
        ShowHideEndGameUI(false);

        GameManager.instance.boxAni.SetBool("Restart", true);
        GameManager.instance.trail.enabled = true;
        GameManager.instance.boxAni.speed = GameManager.instance.cachecSpeed;
        
        //GameManager.instance.boxAni.Play("levelBox", 0 ,0);


    }

    public void RestartState()
    {
        GameManager.instance.RestartGameState();
    }


}
