using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlantQuiz : MonoBehaviour
{
    public GameObject quizCanvas;
    public GameObject quizGame;
    public GameObject correctCanvas;

    public Button submitButton;
    public Button[] imageButtons;

    private int correctAnswerIndex = 1; // Index of the correct image button
    private int selectedAnswerIndex = -1; // Index of the selected image button

    void Start()
    {
        foreach (Button button in imageButtons)
        {
            button.onClick.AddListener(() => OnImageButtonClick(button));
        }

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    void OnImageButtonClick(Button clickedButton)
    {
        // Deselect all buttons
        foreach (Button button in imageButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        // Select the clicked button
        clickedButton.GetComponent<Image>().color = Color.green;
        selectedAnswerIndex = System.Array.IndexOf(imageButtons, clickedButton);
    }

    void OnSubmitButtonClick()
    {
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            StartCoroutine(ShowCorrectCanvas());
        }
        else
        {
            StartCoroutine(ShowIncorrectSelection());
        }
    }

    IEnumerator ShowCorrectCanvas()
    {
        quizGame.SetActive(false);
        correctCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        quizCanvas.SetActive(false);
        // Proceed to the next question or end the game
    }

    IEnumerator ShowIncorrectSelection()
    {
        imageButtons[selectedAnswerIndex].GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(2);
        ResetButtons();
        quizCanvas.SetActive(true);
    }

    void ResetButtons()
    {
        foreach (Button button in imageButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        selectedAnswerIndex = -1;
    }
}
