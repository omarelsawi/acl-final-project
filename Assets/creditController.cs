using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class creditController : MonoBehaviour
{
    public Button skipButton;

   
    void Update()
    {
        // Check if the skip button is interactable
        if (skipButton.IsInteractable())
        {
            // Check if the skip button was clicked
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("in");
                // Get the current mouse position
                Vector2 mousePos = Input.mousePosition;

                // Check if the mouse is over the skip button
                if (RectTransformUtility.RectangleContainsScreenPoint(skipButton.transform as RectTransform, mousePos))
                {
                    // Return to the main menu
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }

 


}
