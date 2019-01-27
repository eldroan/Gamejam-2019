using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using TMPro.Examples;

public class CharacterSelectionMenuManager : MonoBehaviour
{
    // General Variable
    [SerializeField] private float maxTimeToSelect = 45f;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip clipSwitch = null;
    [SerializeField] private AudioClip clipSelect = null;

    [SerializeField] private GameObject[] characterOptionsP1;
    [SerializeField] private GameObject[] characterOptionsP2;

    private float timer;

    // UI objects
    private List<Image> selectorsPlayer1;
    private List<Image> selectorsPlayer2;
    private List<Image> characters;
    private Image fadeImage;
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI timerText;
    private GameObject player1CurrentSelection;
    private GameObject player2CurrentSelection;
    private bool player1selected = false;
    private bool player2selected = false;
    private bool fading = false;

    // Other objects
    private int player1SelectionIndex;
    private int player2SelectionIndex;
    
    
    private void Awake()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        selectorsPlayer1 = GameObject.Find("SelectorsPlayer1").GetComponentsInChildren<Image>().ToList();
        selectorsPlayer2 = GameObject.Find("SelectorsPlayer2").GetComponentsInChildren<Image>().ToList();
        characters = GameObject.Find("Characters").GetComponentsInChildren<Image>().ToList();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        titleText.GetComponent<VertexJitter>().ShakeText();
        player1SelectionIndex = 0;
        player2SelectionIndex = characters.Count - 1;  

        for (int i=0; i < selectorsPlayer1.Count; i++)
        {
            selectorsPlayer1[i].gameObject.SetActive(false);
            selectorsPlayer2[i].gameObject.SetActive(false);
        }

        selectorsPlayer1[player1SelectionIndex].gameObject.SetActive(true);
        selectorsPlayer2[player2SelectionIndex].gameObject.SetActive(true);

        this.characterOptionsP1[player1SelectionIndex].SetActive(true);
        this.characterOptionsP2[player2SelectionIndex].SetActive(true);

        timer = maxTimeToSelect;
    }

    // Update is called once per frame
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(GenericFunctions.FadeOutSound(1.5f, audioSource, 0f));
            StartCoroutine(GenericFunctions.FadeInImage(.75f, fadeImage, "MainMenu"));
        }

        if (player1selected && player2selected)
        {
            if (!fading)
            {
                StartCoroutine(GenericFunctions.FadeInImage(1.5f, fadeImage, "Stage1"));
                fading = true;
            }         
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SelectCharacterPlayer1();
            SelectCharacterPlayer2();
        }

        timerText.text = ((int)timer).ToString();

        if (Input.GetKeyDown(KeyCode.A) && !player1selected)
        {
            PreviousCharacterPlayer1();
        }

        if (Input.GetKeyDown(KeyCode.D) && !player1selected)
        {
            NextCharacterPlayer1();
        }

        if (Input.GetKeyDown(KeyCode.J) && !player2selected)
        {
            PreviousCharacterPlayer2();
        }

        if (Input.GetKeyDown(KeyCode.L) && !player2selected)
        {
            NextCharacterPlayer2();
        }

        if (Input.GetKeyDown(KeyCode.V) && !player1selected)
        {
            SelectCharacterPlayer1();
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && !player2selected)
        {
            SelectCharacterPlayer2();
        }


    }

    // -----------------------------------------------
    // Player 1 control functions
    private void NextCharacterPlayer1()
    {
        player1SelectionIndex += 1;
		if (player1SelectionIndex > characters.Count-1)
		{
			player1SelectionIndex = characters.Count - 1;
		}
		else
		{
			UpdatePlayer1Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void PreviousCharacterPlayer1()
    {
        player1SelectionIndex -= 1;
		if (player1SelectionIndex < 0)
		{
			player1SelectionIndex = 0;
		}
		else
		{
			UpdatePlayer1Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void UpdatePlayer1Selection()
    {
        for (int i=0; i < selectorsPlayer1.Count; i++)
        {
            if (i == player1SelectionIndex)
            {
                selectorsPlayer1[i].gameObject.SetActive(true);
                this.characterOptionsP1[i].SetActive(true);
            }
            else
            {
                selectorsPlayer1[i].gameObject.SetActive(false);
                this.characterOptionsP1[i].SetActive(false);
            }
        }

    }

    private void SelectCharacterPlayer1()
    {
        audioSource.PlayOneShot(clipSelect);
        selectorsPlayer1[player1SelectionIndex].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 230);
        selectorsPlayer1[player1SelectionIndex].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 230);
        player1selected = true;
        Session.Instance.PlayerA = (CharacterType)player1SelectionIndex;

    }

    // -----------------------------------------------
    // Player 2 control functions
    private void NextCharacterPlayer2()
    {
        player2SelectionIndex += 1;
		if (player2SelectionIndex > characters.Count-1)
		{
			player2SelectionIndex = characters.Count - 1;
		}
		else
		{
			UpdatePlayer2Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void PreviousCharacterPlayer2()
    {
        player2SelectionIndex -= 1;
		if (player2SelectionIndex < 0)
		{
			player2SelectionIndex = 0;
		}
		else
		{
			UpdatePlayer2Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void UpdatePlayer2Selection()
    {
        for (int i=0; i < selectorsPlayer2.Count; i++)
        {
            if (i == player2SelectionIndex)
            {
                selectorsPlayer2[i].gameObject.SetActive(true);
                this.characterOptionsP2[i].SetActive(true);
            }
            else
            {
                selectorsPlayer2[i].gameObject.SetActive(false);
                this.characterOptionsP2[i].SetActive(false);
            }
        }
    }

    private void SelectCharacterPlayer2()
    {
        audioSource.PlayOneShot(clipSelect);
        selectorsPlayer2[player2SelectionIndex].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 230);
        selectorsPlayer2[player2SelectionIndex].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 230);
        player2selected = true;
        Session.Instance.PlayerB = (CharacterType)player2SelectionIndex;
    }
}
