using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    [SerializeField] private float fightDuration = 120;
    [SerializeField] private Text remainingTimeText;
    [SerializeField] private GameObject deathmatchText;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private float deathmatchMessegeTime = 2;
    [SerializeField] private TextMeshProUGUI playerANameText;
    [SerializeField] private TextMeshProUGUI playerBNameText;
    [SerializeField] private TextMeshProUGUI playerAHitsText;
    [SerializeField] private TextMeshProUGUI playerBHitsText;
    [SerializeField] private bool isTesting;
    [SerializeField] private GameObject defaultCharacterAPrefab;
    [SerializeField] private GameObject defaultCharacterBPrefab;

    public static FightManager Instance { get; private set; }

    [HideInInspector] public CharacterScript PlayerACharacter;
    [HideInInspector] public CharacterScript PlayerBCharacter;

    private float RemaingTime;
    private bool OnDeathmatch;
    private float DeathmatchTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = new FightManager();

        if (isTesting)
        {
            this.PlayerACharacter = Instantiate(defaultCharacterAPrefab).GetComponent<CharacterScript>();
            this.PlayerBCharacter = Instantiate(defaultCharacterBPrefab).GetComponent<CharacterScript>();
        }

        this.RemaingTime = fightDuration;
        this.UpdateRemaingTimeText(this.RemaingTime);
        this.OnDeathmatch = false;
        this.deathmatchText.SetActive(false);
        this.DeathmatchTime = 0;

        this.playerANameText.text = this.PlayerACharacter.Name;
        this.playerBNameText.text = this.PlayerBCharacter.Name;

        this.PlayerACharacter.HitsText = this.playerAHitsText;
        this.PlayerBCharacter.HitsText = this.playerBHitsText;
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
        this.PlayerACharacter.Hits = 2;
        this.PlayerBCharacter.Hits = 2;
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

    public void OnPlayerHit(CharacterScript character)
    {
        character.Hits += 1;
        if (character.Hits == 3)
        {
            this.victoryText.text = character.Name;
            this.victoryText.gameObject.SetActive(true);
            Debug.Log("Won " + character.Name);
        }
    }
}
