using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Main;
using Interpreter.Eval;

public class Console : MonoBehaviour
{

    public TMP_InputField commandInput;
    public TMP_Text response;
    public Button submitButton;
    public GodGame GodGame;
    // Start is called before the first frame update
    void Start()
    {
        GodGame = GameObject.FindGameObjectWithTag("GodGame").GetComponent<GodGame>();

        submitButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (commandInput.text == "") return;
        Debug.Log("[CMD] Received: >: " + commandInput.text);

        string interpreterResponse = "Ok!";
        try
        {
            GodGame.Evaluator.ExecProgram(commandInput.text);
        }
        catch (EvalException e)
        {
            interpreterResponse = e.Message;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            interpreterResponse = "Ocorreu um erro inesperado";
        }

        // Call Code interpreter here inside a try/catch call... If catch print:
        response.text = "> " + commandInput.text + " \n" + interpreterResponse + "\n\n" + response.text;


        // clear command input
        commandInput.text = "";


    }
}