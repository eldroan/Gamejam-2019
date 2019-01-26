using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    [SerializeField] private float FightDuration = 120;
    [SerializeField] private Text RemainingTimeText;
    [SerializeField] private GameObject DeathmatchText;
    [SerializeField] private TextMeshProUGUI VictoryText;
    [SerializeField] private float DeathmatchMessegeTime = 2;
    [SerializeField] private TextMeshProUGUI PlayerANameText;
    [SerializeField] private TextMeshProUGUI PlayerBNameText;
    [SerializeField] private TextMeshProUGUI PlayerAHitsText;
    [SerializeField] private TextMeshProUGUI PlayerBHitsText;
    [SerializeField] private bool IsTesting;
    [SerializeField] private GameObject DefaultCharacterAPrefab;
    [SerializeField] private GameObject DefaultCharacterBPrefab;

    public static FightManager Instance;

    [HideInInspector] public CharacterScript PlayerACharacter;
    [HideInInspector] public CharacterScript PlayerBCharacter;

    private float RemaingTime;
    private bool OnDeathmatch;
    private float DeathmatchTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = new FightManager();

        if (IsTesting)
        {
            this.PlayerACharacter = Instantiate(DefaultCharacterAPrefab).GetComponent<CharacterScript>();
            this.PlayerBCharacter = Instantiate(DefaultCharacterBPrefab).GetComponent<CharacterScript>();
        }

        this.RemaingTime = FightDuration;
        this.UpdateRemaingTimeText(this.RemaingTime);
        this.OnDeathmatch = false;
        this.DeathmatchText.SetActive(false);
        this.DeathmatchTime = 0;

        this.PlayerANameText.text = this.PlayerACharacter.Name;
        this.PlayerBNameText.text = this.PlayerBCharacter.Name;

        this.PlayerACharacter.HitsText = this.PlayerAHitsText;
        this.PlayerBCharacter.HitsText = this.PlayerBHitsText;
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
            if(this.DeathmatchTime >= this.DeathmatchMessegeTime && this.DeathmatchText.activeInHierarchy)
            {
                this.DeathmatchText.SetActive(false);
            }
        }
    }

    private void StartDeathmatch()
    {
        this.OnDeathmatch = true;
        this.DeathmatchText.SetActive(true);
        this.PlayerACharacter.Hits = 2;
        this.PlayerBCharacter.Hits = 2;
    }

    private void UpdateRemaingTimeText(float remaingTime)
    {
        if(remaingTime <= 0)
        {
            this.RemainingTimeText.text = "00:00.00";
        }
        else
        {
            this.RemainingTimeText.text = "0" + ((int)(remaingTime / 60)).ToString() + ":" + (((this.RemaingTime % 60) < 10) ? "0" : "") + (this.RemaingTime%60).ToString();
        }
    }

    public void OnPlayerHit(CharacterScript character)
    {
        character.Hits += 1;
        if (character.Hits == 3)
        {
            this.VictoryText.text = character.Name;
            this.VictoryText.gameObject.SetActive(true);
            Debug.Log("Won " + character.Name);
        }
    }
}
