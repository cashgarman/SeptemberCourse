using System;
using System.Collections;
using UnityEngine;

public class SimonSaysGame : MonoBehaviour
{
    public PhysicalButton[] buttons;
    private int[] pattern = new int[10];
    private bool disableButtons;
    private int numRevealedTiles = 1;
    private int currentGuess;
    public AudioSource winSound;
    public AudioSource loseSound;

    void Start()
    {
        // Start the game
        StartGame();
    }

    // Games States:
    // - Revealing Tiles
    // - Awaiting Player Input

    private void StartGame()
    {
        Debug.Log("Starting game");

        StartCoroutine(FlashAllButtons(3, () =>
        {
            // Reset everything from the last game
            currentGuess = 0;
            numRevealedTiles = 1;

            // Randomly generate a pattern of tiles
            for (var i = 0; i < 10; ++i)
            {
                pattern[i] = UnityEngine.Random.Range(0, 4);
            }

            // Start revealing tiles (disable the buttons during this)
            disableButtons = true;
            StartCoroutine(RevealTiles(2f));
        }));
    }

    private IEnumerator RevealTiles(float delay)
    {
        yield return new WaitForSeconds(delay);

        for (var i = 0; i < numRevealedTiles; ++i)
        {
            ClearLitButtons();
            SetButtonLit(pattern[i], true);
            yield return new WaitForSeconds(1);
            ClearLitButtons();
            yield return new WaitForSeconds(.5f);
        }

        disableButtons = false;
    }

    private void SetButtonLit(int button, bool lit)
    {
        buttons[button].SetLit(lit);
    }

    private void ClearLitButtons()
    {
        for (var i = 0; i < 4; ++i)
        {
            SetButtonLit(i, false);
        }
    }

    void Update()
    {
        // CHEAT: Restart game on space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void OnGuess(int buttonIndex)
    {
        if(disableButtons)
        {
            Debug.Log("Pushed a button when they were disabled");
            return;
        }

        StartCoroutine(FlashButton(buttonIndex, () =>
        {
            // If the tile in the pattern at the current guess index matches
            if (pattern[currentGuess] == buttonIndex)
            {
                // Correct guess!
                ++currentGuess;

                // If the player won
                if (currentGuess >= numRevealedTiles && numRevealedTiles == 4)
                {
                    Debug.Log("You win!");
                    winSound.Play();
                    StartCoroutine(FlashAllButtons(3, () =>
                    {
                        StartGame();
                    }));
                    return;
                }

                // If the current guess was the last revealed tile
                if (currentGuess >= numRevealedTiles)
                {
                    // Reset the current guess
                    currentGuess = 0;

                    // Increase the number of revealed tiles
                    ++numRevealedTiles;
                    Debug.Log($"Revealing another tile, up to {numRevealedTiles}");

                    // Start revealing tiles (disable the buttons during this)
                    disableButtons = true;
                    StartCoroutine(RevealTiles(2f));
                }
            }
            // If the guess was incorrect
            else
            {
                // Game over
                loseSound.Play();
                Debug.Log("You lost!");
                StartCoroutine(FlashAllButtons(3, () =>
                {
                    StartGame();
                }));
            }
        }));
    }

    private IEnumerator FlashAllButtons(int numFlashes, Action callback)
    {
        for(var i = 0; i < numFlashes; ++i)
        {
            for(var k = 0; k < 4; ++k)
            {
                buttons[k].SetLit(true);
            }

            yield return new WaitForSeconds(0.333f);

            for (var k = 0; k < 4; ++k)
            {
                buttons[k].SetLit(false);
            }

            yield return new WaitForSeconds(0.333f);
        }

        callback();
    }

    private IEnumerator FlashButton(int buttonIndex, Action callback)
    {
        ClearLitButtons();
        SetButtonLit(buttonIndex, true);
        yield return new WaitForSeconds(1f);
        ClearLitButtons();

        callback();
    }
}
