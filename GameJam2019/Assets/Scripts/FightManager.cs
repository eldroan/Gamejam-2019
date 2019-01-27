using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FightManager : MonoBehaviour
{
    [SerializeField] private float fightDuration = 120;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private GameObject deathmatchText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private float deathmatchMessegeTime = 2;
    [SerializeField] private TextMeshProUGUI playerANameText;
    [SerializeField] private TextMeshProUGUI playerBNameText;
    [SerializeField] private bool isTesting;
    [SerializeField] private Transform playerASpawnPoint;
    [SerializeField] private Transform playerBSpawnPoint;

    public static FightManager Instance { get; private set; }


    private float RemaingTime;
    private bool OnDeathmatch;
    private float DeathmatchTime;

    private int playerAHits = 0;
    private int playerBHits = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = new FightManager();


        if (Session.Instance != null)
        {
            var playerA = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerA);
            var playerB = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerB);

            var playerAObject = Instantiate(playerA.Prefab, playerASpawnPoint.position, playerASpawnPoint.rotation);
            var playerBObject = Instantiate(playerB.Prefab, playerBSpawnPoint.position, playerBSpawnPoint.rotation);

            playerAObject.tag = Constants.PLAYER_1_TAG;
            playerBObject.tag = Constants.PLAYER_2_TAG;

            this.RemaingTime = fightDuration;
            this.UpdateRemaingTimeText(this.RemaingTime);
            this.OnDeathmatch = false;
            this.deathmatchText.SetActive(false);
            this.DeathmatchTime = 0;

            this.playerANameText.text = playerA.Name;
            this.playerBNameText.text = playerB.Name;

        }
    }

    private void Update()
    {
        if (!OnDeathmatch)
        {
            this.RemaingTime -= Time.deltaTime;
            this.UpdateRemaingTimeText(this.RemaingTime);

            if (this.RemaingTime <= 0)
            {
                this.StartDeathmatch();
            }
        }
        else
        {
            this.DeathmatchTime += Time.deltaTime;
            if(this.DeathmatchTime >= this.deathmatchMessegeTime && this.deathmatchText.activeInHierarchy)
            {
                this.deathmatchText.SetActive(false);
            }
        }
    }

    private void StartDeathmatch()
    {
        this.OnDeathmatch = true;
        this.deathmatchText.SetActive(true);
        this.playerAHits = 2;
        this.playerBHits = 2;
    }

    private void UpdateRemaingTimeText(float remaingTime)
    {
        if(remaingTime <= 0)
        {
            this.remainingTimeText.text = "00:00.00";
        }
        else
        {
            this.remainingTimeText.text = "0" + ((int)(remaingTime / 60)).ToString() + ":" + (((this.RemaingTime % 60) < 10) ? "0" : "") + (this.RemaingTime%60).ToString();
        }
    }

    public void OnPlayerHit(string playerID)
    {
        if (playerID == Constants.PLAYER_1_TAG)
        {
            this.playerAHits++;

            if (playerAHits == 3)
                Finish(playerID);

        }
        else if (playerID == Constants.PLAYER_2_TAG)
        {
            this.playerBHits++;

            if (playerAHits == 3)
                Finish(playerID);
        }
    }

    private void Finish(string playerID)
    {
        Time.timeScale = 0;
        //TODO fin del juego
    }
}
