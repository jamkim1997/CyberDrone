using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
   
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] cards;

    public List<Sprite> gameCards = new List<Sprite>();
    public List<Button> btns = new List<Button>();
    private bool firstGuess, secondGuess;

    public GameObject miniGameUI;

    public GameObject imagePanel;

    private Player character;

    public GameObject[] minimapIcons;
    public GameObject minimapIcon;
    public Animator Red;
    public Animator blue;

    private int countCorrectguesses;
    private int gamegusses;

    private string firstGuseePuzzle, secondGuessPuzzle;
    private int firstGuessIndex, secondGuessIndex;


    void Awake()
    {
        cards = Resources.LoadAll<Sprite> ("Sprites2/minigame");
    }

    void Start()
    {
        GetButtons();
        AddGameCards();
        Shuffle(gameCards);
        gamegusses = gameCards.Count / 2;
    }
    void GetButtons()
    {
        for (int i = 0; i < btns.Count; i++) //objects.Length
        {
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGameCards()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if(index == looper / 2)
            {
                index = 0;

            }
            gameCards.Add(cards[index]);
            index++;
        }
    }


    public void PickCard()
    {
        if (!firstGuess)// !firstGuess
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuseePuzzle = gameCards[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gameCards[firstGuessIndex];
            btns[firstGuessIndex].interactable = false;
        }

        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gameCards[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gameCards[secondGuessIndex];
            btns[secondGuessIndex].interactable = false;

            StartCoroutine(CheckIfTheCardMatch());
             
        }
    }

    IEnumerator CheckIfTheCardMatch()
    {
        yield return new WaitForSeconds(0.4f);
        if(firstGuseePuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.2f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color (0,0,0,0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(.2f);

            CheckIfThegameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
            btns[firstGuessIndex].interactable = true;
            btns[secondGuessIndex].interactable = true;

        }
        yield return new WaitForSeconds(.2f);

        firstGuess = secondGuess = false;
        firstGuess = false;
        secondGuess = false;
    }

    void CheckIfThegameIsFinished() {
        countCorrectguesses++;
        if (countCorrectguesses == gamegusses)
        {
            Red.GetComponent<AudioSource>().Play();
            Destroy(miniGameUI);
            Destroy(imagePanel);
            Red.enabled = true;
            blue.enabled = true;
            Destroy(minimapIcon);
            character = FindObjectOfType<Player>();
            character.enabled = true;
            foreach(GameObject icon in minimapIcons)
            {
                icon.SetActive(true);
            }
        }

    }
    void Shuffle(List<Sprite> list)
    {

        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

