using TMPro;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] public string Name;

    [HideInInspector] public TextMeshProUGUI HitsText;

    private int hits;
    public int Hits {
        get {
            return this.hits;
        }
        set {
            this.hits = value;
            this.HitsText.text = value.ToString();
        }
    }
}
