using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using Yarn.Unity.Example;

public class DialogueUI : Yarn.Unity.DialogueUIBehaviour
{
    public GameObject dialogueContainer;
    public TextMeshProUGUI texto;

    public bool clickContinuar = false;

    private Yarn.OptionChooser SetSelectedOption;


    public GameObject continuePrompt;
    /// How quickly to show the text, in seconds per character
    [Tooltip("How quickly to show the text, in seconds per character")]
    public float textSpeed = 0.025f; 


    public List<TextMeshProUGUI> optionButtons;
    public override IEnumerator RunCommand(Command command)
    {
        // "Perform" the command
        Debug.Log("Command: " + command.text);

        yield break;
    }


    public void clickeadoContinuar()
    {
        clickContinuar = true;

    }


    public void iniciarConversacion(){
        var allParticipants = new List<NPC>(FindObjectsOfType<NPC>());
        var nodoInicio = allParticipants[0];
        for (int i = 0; i < allParticipants.Count; i++)
        {
            if (allParticipants[i].characterName.Equals("Shae"))
            {
                nodoInicio = allParticipants[i];
            }
        }
       

       // FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(target.talkToNode);
        FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(nodoInicio.talkToNode);
        //FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue();
        string variable = allParticipants[2].characterName;
            Debug.Log(variable);
    }
    public override IEnumerator RunLine(Line line)
    {
        texto.gameObject.SetActive(true);

        if (textSpeed > 0.0f)
        {
            // Display the line one character at a time
            var stringBuilder = new StringBuilder();

            foreach (char c in line.text)
            {
                stringBuilder.Append(c);
                texto.text = stringBuilder.ToString();
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            // Display the line immediately if textSpeed == 0
            texto.text = line.text;
        }

        if (continuePrompt != null)
            continuePrompt.SetActive(true);

        while (clickContinuar == false)
        {
            
            yield return null;
        }

        // Avoid skipping lines if textSpeed == 0
        yield return new WaitForEndOfFrame();

        // Hide the text and prompt
        texto.gameObject.SetActive(false);
        clickContinuar = false;





    }

    public override IEnumerator RunOptions(Options optionsCollection, OptionChooser optionChooser)
    {

        // Do a little bit of safety checking
        if (optionsCollection.options.Count > optionButtons.Count)
        {
            Debug.LogWarning("There are more options to present than there are" +
                             "buttons to present them in. This will cause problems.");
        }

        // Display each option in a button, and make it visible
        int i = 0;
        foreach (var optionString in optionsCollection.options)
        {
            optionButtons[i].gameObject.SetActive(true);
            optionButtons[i].text = optionString;
            i++;
        }

        // Record that we're using it
        SetSelectedOption = optionChooser;

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }

        // Hide all the buttons
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }



    }

    public void SetOption(int selectedOption)
    {

        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        // Start by hiding the container, line and option buttons
     

        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }

        // Hide the continue prompt if it exists
        if (continuePrompt != null)
            continuePrompt.SetActive(false);
    }
    void Update()
    {
        
    }
}
