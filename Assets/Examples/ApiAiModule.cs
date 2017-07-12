//
// API.AI Unity SDK Sample
// =================================================
//
// Copyright (C) 2015 by Speaktoit, Inc. (https://www.speaktoit.com)
// https://www.api.ai
//
// ***********************************************************************************************************************
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// ***********************************************************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Reflection;
using ApiAiSDK;
using ApiAiSDK.Model;
using ApiAiSDK.Unity;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

public class ApiAiModule : MonoBehaviour
{

    public Text answerTextField;
    public Text inputTextField;
    private ApiAiUnity apiAiUnity;
    private AudioSource aud;
    public AudioClip listeningSound;

    public Sprite appleSprite;
    public Sprite bananaSprite;
    public Sprite orangeSprite;
    public Sprite broccoliSprite;
    public Sprite carrotSprite;
    public Sprite chocolateSprite;
    public Sprite eggSprite;
    public Sprite watermelonSprite;
    public Sprite icecreamSprite;
    public Sprite cakeSprite;
    public GameObject foodItem;

    string returnFood;
    Image foodImage;
    string[] foods = { "apple", "banana", "orange", "broccoli", "carrot", "chocolate", "egg", "watermelon", "icecream", "cake" };
    int index = 0;

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    { 
        NullValueHandling = NullValueHandling.Ignore,
    };

    private readonly Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    // Use this for initialization
    IEnumerator Start()
    {
        // check access to the Microphone
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            throw new NotSupportedException("Microphone using not authorized");
        }

        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) =>
        {
            return true;
        };
            
        const string ACCESS_TOKEN = "3485a96fb27744db83e78b8c4bc9e7b7";

        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.English);

        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);

        apiAiUnity.OnError += HandleOnError;
        apiAiUnity.OnResult += HandleOnResult;

        foodImage = foodItem.GetComponent<Image>();
        returnFood = foods[index];
    }

    void HandleOnResult(object sender, AIResponseEventArgs e)
    {
        RunInMainThread(() => {
            var aiResponse = e.Response;
            if (aiResponse != null)
            {
                Debug.Log(aiResponse.Result.ResolvedQuery);
                var outText = JsonConvert.SerializeObject(aiResponse, jsonSettings);

                Debug.Log(outText);                

                string text = aiResponse.Result.ResolvedQuery.ToLower();                

                if (/*aiResponse.Result.ResolvedQuery*/ text == returnFood)
                {                    
                    if (index == 10)
                    {
                        index = 0;
                    }
                    index++;
                    //foodImage.sprite = Resources.Load<Sprite>("Resources/" + returnFood);

                    switch (index)
                    {
                        case 0:
                            foodImage.sprite = appleSprite;
                            break;
                        case 1:
                            foodImage.sprite = bananaSprite;
                            break;
                        case 2:
                            foodImage.sprite = orangeSprite;
                            break;
                        case 3:
                            foodImage.sprite = broccoliSprite;
                            break;
                        case 4:
                            foodImage.sprite = carrotSprite;
                            break;
                        case 5:
                            foodImage.sprite = chocolateSprite;
                            break;
                        case 6:
                            foodImage.sprite = eggSprite;
                            break;
                        case 7:
                            foodImage.sprite = watermelonSprite;
                            break;
                        case 8:
                            foodImage.sprite = icecreamSprite;
                            break;
                        case 9:
                            foodImage.sprite = cakeSprite;
                            break;                        
                    }                    

                    returnFood = foods[index];
                    answerTextField.text = "ACERTOU";
                }
                else
                {
                    answerTextField.text = "ERROU";
                }

                answerTextField.text += text + returnFood/*aiResponse.Result.ResolvedQuery*/;
                
            } else
            {
                Debug.LogError("Response is null");
            }
        });
    }

    /*string LoadFood()
    {

        if (index == 10)
        {
            index = 0;
        }

        returnFood = foods[index];

        foodImage = foodItem.GetComponent<Image>();

        foodImage.sprite = Resources.Load<Sprite>(returnFood);

        if (returnFood == "apple")
        {
            apple.SetActive(true);
            banana.SetActive(false);
        }
        else if (returnFood == "banana") {
            apple.SetActive(false);
            banana.SetActive(true);
        }


        index++;
        return returnFood;
    }*/

    void HandleOnError(object sender, AIErrorEventArgs e)
    {
        RunInMainThread(() => {
            Debug.LogException(e.Exception);
            Debug.Log(e.ToString());
            answerTextField.text = e.Exception.Message;
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        if (apiAiUnity != null)
        {
            apiAiUnity.Update();
        }

        // dispatch stuff on main thread
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
    }

    private void RunInMainThread(Action action)
    {
        ExecuteOnMainThread.Enqueue(action);
    }

    public void PluginInit()
    {
        
    }
    
    public void StartListening()
    {
        Debug.Log("StartListening");
            
        if (answerTextField != null)
        {
            answerTextField.text = "Listening...";
        }
            
        aud = GetComponent<AudioSource>();
        apiAiUnity.StartListening(aud);

    }
    
    public void StopListening()
    {
        try
        {
            Debug.Log("StopListening");

            if (answerTextField != null)
            {
                answerTextField.text = "";
            }
            
            apiAiUnity.StopListening();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    
    public void SendText()
    {
        var text = inputTextField.text;

        Debug.Log(text);

        AIResponse response = apiAiUnity.TextRequest(text);

        if (response != null)
        {
            Debug.Log("Resolved query: " + response.Result.ResolvedQuery);
            var outText = JsonConvert.SerializeObject(response, jsonSettings);

            Debug.Log("Result: " + outText);

            answerTextField.text = outText;
        } else
        {
            Debug.LogError("Response is null");
        }

    }

    public void StartNativeRecognition()
    {
        try
        {
            apiAiUnity.StartNativeRecognition();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
