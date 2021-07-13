using HuaweiMobileServices.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Higgsman higgsman;

    public Transform pellets;

    public Text gameOverText;
    public Text scoreText;
    public Text BestscoreText;
    public Text livesText;

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnRewardAdClosed = OnRewardAdClosed;
        HMSAdsKitManager.Instance.OnRewardAdFailedToShow = OnRewardAdFailedToShow;
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        //Interstitial Ad will be shown when each round starts
        HMSAdsKitManager.Instance.ShowInterstitialAd();
        this.gameOverText.enabled = false;

        foreach (Transform pellets in this.pellets)
        {
            pellets.gameObject.SetActive(true); // when you eat them they will turn to fade,you need to reset pellets
        }

        ResetState();
    }

    private void GameOver()
    {
        //Banner Ad will be shown when you lose | game over
        HMSAdsKitManager.Instance.ShowBannerAd();
        this.gameOverText.enabled = true;
        
        SetScore(0);

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.higgsman.gameObject.SetActive(false);
    }
    private void SetBestScore(int score)
    {
        int intNewScore = int.Parse(this.BestscoreText.text);

        if (score > intNewScore)
        {
            this.BestscoreText.text = score.ToString().PadLeft(3, '0'); ;
            if (score > 999)
            {
                this.BestscoreText.text = score.ToString().PadRight(4, '1');
            }
        }
    }
    private void ResetState()  //when higgsman dies => ghost and higgsman reset but not pellets to continue where you left off
    {
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.higgsman.ResetState();
        SetBestScore(this.score);
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.scoreText.text = score.ToString().PadLeft(3, '0');
        if (score > 999)
        {
            this.scoreText.text = score.ToString().PadRight(4, '1');
        }
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        this.livesText.text = "x" + lives.ToString();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }
    public void HiggsmanEaten()
    {
        //this.higgsman.gameObject.SetActive(false);
        this.higgsman.DeathSequence();
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            //ResetState(); // call  reset state not new round ,since you want to continue with the remaining pellets 
            //but reseted ghosts and higgsman
            //comment out resetstate because we will wait 3 sec for higgman die (give little bit of grace period )
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            HMSAdsKitManager.Instance.ShowRewardedAd();
            if (!HMSAdsKitManager.Instance.IsRewardedAdLoaded)
            {
                GameOver();
            }
        }
    }

    public void OnRewarded(Reward reward)
    {
        SetLives(1);
        Debug.Log("[HMS] 1 Life rewarded due to watching rewarded Ad");
    }
    public void OnRewardAdClosed()
    {
        Debug.Log("[HMS] Rewarded ad closed! Current lives:" + this.lives);
        if (this.lives > 0)
        {
            Debug.Log("[HMS] Live gained. Restrating for New Round");
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            Debug.Log("[HMS] Ad closed without reward. No life remains. GameOver!");
            GameOver();
        }
    }
    public void OnRewardAdFailedToShow(int errorcode)
    {
        Debug.Log("[HMS] OnRewardAdFailedToShow. errorcode:" + errorcode);
    }

    


    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false); // deactive the eaten
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.higgsman.gameObject.SetActive(false); // ghost can not come and eat higgsman after you eat all pallet
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPellet powerpellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerpellet.duration);
        }
        PelletEaten(powerpellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerpellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
