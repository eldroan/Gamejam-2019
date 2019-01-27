using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using TMPro.Examples;

public class FightManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float fightDuration = 120;
    [SerializeField] private TextMeshProUGUI remainingTimeText;
    [SerializeField] private GameObject deathmatchText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private float deathmatchMessegeTime = 2;
    [SerializeField] private TextMeshProUGUI player1NameText;
    [SerializeField] private TextMeshProUGUI player2NameText;
    [SerializeField] private bool isTesting;
    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;
    [SerializeField] private Image player1Avatar;
    [SerializeField] private Image player2Avatar;
    [SerializeField] public Image fadeImage;

    [SerializeField] private Image[] player1HitMarker;
    [SerializeField] private Image[] player2HitMarker;

    public static FightManager Instance { get; private set; }

    private float RemaingTime;
    private bool OnDeathmatch;
    private float DeathmatchTime;

    private int playerAHits = 0;
    private int playerBHits = 0;

    private void Awake()
    {
        Instance = this;

        if (Session.Instance != null)
        {
            //if (BackgroundBundle.Instance != null)
            //    background.sprite = BackgroundBundle.Instance.GetRandomBackground().Sprite;

                background.sprite = BackgroundBundle.Instance.GetBackground(1).Sprite;

            var playerA = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerA);
            var playerB = CharacterBundle.Instance.GetCharacter(Session.Instance.PlayerB);

            player1Avatar.sprite = playerA.Avatar;
            player2Avatar.sprite = playerB.Avatar;

            var playerAObject = Instantiate(playerA.Prefab, player1SpawnPoint.position, player1SpawnPoint.rotation);
            var playerBObject = Instantiate(playerB.Prefab, player2SpawnPoint.position, player2SpawnPoint.rotation);

            playerAObject.GetComponent<PlayerController>().PlayerID = Constants.PLAYER_1_TAG;
            playerBObject.GetComponent<PlayerController>().PlayerID = Constants.PLAYER_2_TAG;

            playerAObject.tag = Constants.PLAYER_1_TAG;
            playerBObject.tag = Constants.PLAYER_2_TAG;

            this.RemaingTime = fightDuration;
            this.UpdateRemaingTimeText(this.RemaingTime);
            this.OnDeathmatch = false;
            this.deathmatchText.SetActive(false);
            this.DeathmatchTime = 0;

            this.player1NameText.text = playerA.Name;
            this.player2NameText.text = playerB.Name;

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
    }

    private void StartDeathmatch()
    {
        this.OnDeathmatch = true;
        this.deathmatchText.SetActive(true);
        this.deathmatchText.GetComponent<VertexJitter>().ShakeText();
        this.GetComponent<Animator>().SetTrigger("Deathmatch");
        this.playerAHits = 2;
        this.playerBHits = 2;

        for (var i = 0; i<= 1; i++)
        {
            this.player1HitMarker[i].enabled = true;
            this.player2HitMarker[i].enabled = true;
        }
    }

    private void UpdateRemaingTimeText(float remaingTime)
    {
        if(remaingTime <= 0)
        {
            this.remainingTimeText.text = "00:00";
        }
        else
        {
            this.remainingTimeText.text = TimeSpan.FromSeconds(remaingTime).ToString(@"mm\:ss");
            //this.remainingTimeText.text = "0" + ((int)(remaingTime / 60)).ToString() + ":" + (((this.RemaingTime % 60) < 10) ? "0" : "") + (this.RemaingTime%60).ToString();
        }
    }

    public void OnPlayerHit(string playerID)
    {
        //if (this.playerBHits >= 3) return;

        if (playerID == Constants.PLAYER_1_TAG)
        {
            this.player2HitMarker[this.playerBHits].enabled = true;
            this.playerBHits++;

            if (playerBHits >= 3)
                Finish(playerID);

        }
        else if (playerID == Constants.PLAYER_2_TAG)
        {
            this.player1HitMarker[this.playerAHits].enabled = true;
            this.playerAHits++;
            if (playerAHits >= 3)
                Finish(playerID);
        }
    }

    private void Finish(string playerID)
    {
        var victory = playerID == Constants.PLAYER_1_TAG ? "2" : "1";
        this.victoryText.gameObject.SetActive(true);
        this.victoryText.text = $"victoria player {victory}";
        StartCoroutine(GenericFunctions.FadeInImage(2.5f, fadeImage, "MainMenu"));
    }
}
