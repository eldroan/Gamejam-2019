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
    private int player1Selection;
    private int player2Selection;
    
    
    void Awake()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        titleText = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        selectorsPlayer1 = GameObject.Find("SelectorsPlayer1").GetComponentsInChildren<Image>().ToList();
        selectorsPlayer2 = GameObject.Find("SelectorsPlayer2").GetComponentsInChildren<Image>().ToList();
        characters = GameObject.Find("Characters").GetComponentsInChildren<Image>().ToList();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        titleText.GetComponent<VertexJitter>().ShakeText();
        player1Selection = 0;
        player2Selection = characters.Count - 1;  

        for (int i=0; i < selectorsPlayer1.Count; i++)
        {
            selectorsPlayer1[i].gameObject.SetActive(false);
            selectorsPlayer2[i].gameObject.SetActive(false);
        }

        selectorsPlayer1[player1Selection].gameObject.SetActive(true);
        selectorsPlayer2[player2Selection].gameObject.SetActive(true);

        timer = maxTimeToSelect;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousCharacterPlayer1();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            NextCharacterPlayer1();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PreviousCharacterPlayer2();
        }

        if (Input.GetKeyDown(KeyCode.L))
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
        player1Selection += 1;
		if (player1Selection > characters.Count-1)
		{
			player1Selection = characters.Count - 1;
		}
		else
		{
			UpdatePlayer1Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void PreviousCharacterPlayer1()
    {
        player1Selection -= 1;
		if (player1Selection < 0)
		{
			player1Selection = 0;
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
            if (i == player1Selection)
            {
                selectorsPlayer1[i].gameObject.SetActive(true);
            }
            else
            {
                selectorsPlayer1[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectCharacterPlayer1()
    {
        audioSource.PlayOneShot(clipSelect);
        selectorsPlayer1[player1Selection].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 230);
        selectorsPlayer1[player1Selection].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 230);
        player1selected = true;
    }

    // -----------------------------------------------
    // Player 2 control functions
    private void NextCharacterPlayer2()
    {
        player2Selection += 1;
		if (player2Selection > characters.Count-1)
		{
			player2Selection = characters.Count - 1;
		}
		else
		{
			UpdatePlayer2Selection();
		}
		audioSource.PlayOneShot(clipSwitch);
    }

    private void PreviousCharacterPlayer2()
    {
        player2Selection -= 1;
		if (player2Selection < 0)
		{
			player2Selection = 0;
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
            if (i == player2Selection)
            {
                selectorsPlayer2[i].gameObject.SetActive(true);
            }
            else
            {
                selectorsPlayer2[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectCharacterPlayer2()
    {
        audioSource.PlayOneShot(clipSelect);
        selectorsPlayer2[player2Selection].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 230);
        selectorsPlayer2[player2Selection].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 230);
        player2selected = true;
    }
}
