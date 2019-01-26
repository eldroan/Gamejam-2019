using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using TMPro.Examples;

public class CharacterSelectionMenuManager : MonoBehaviour
{
    [SerializeField] private float maxTimeToSelect = 45f;
    private float timer = 0f;

    // UI objects
    private List<Image> selectorsPlayer1;
    private List<Image> selectorsPlayer2;
    private List<Image> characters;
    private TextMeshProUGUI timeText;
    private GameObject player1CurrentSelection;
    private GameObject player2CurrentSelection;

    // Other objects
    private int player1Selection;
    private int player2Selection;
    
    
    void Awake()
    {
        selectorsPlayer1 = GameObject.Find("SelectorsPlayer1").GetComponentsInChildren<Image>().ToList();
        selectorsPlayer2 = GameObject.Find("SelectorsPlayer2").GetComponentsInChildren<Image>().ToList();
        characters = GameObject.Find("Characters").GetComponentsInChildren<Image>().ToList();

        
    }

    void Start()
    {
        player1Selection = 0;
        player2Selection = characters.Count - 1;  

        for (int i=0; i < selectorsPlayer1.Count; i++)
        {
            selectorsPlayer1[i].gameObject.SetActive(false);
            selectorsPlayer2[i].gameObject.SetActive(false);
        }

        selectorsPlayer1[player1Selection].gameObject.SetActive(true);
        selectorsPlayer2[player2Selection].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
