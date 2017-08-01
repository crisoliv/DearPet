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

    public AudioClip appleAudio;
    public AudioClip bananaAudio;
    public AudioClip pizzaAudio;
    public AudioClip broccoliAudio;
    public AudioClip carrotAudio;
    public AudioClip chocolateAudio;
    public AudioClip eggAudio;
    public AudioClip watermelonAudio;
    public AudioClip icecreamAudio;

    //ADD BATHROOM OBJECTS AUDIO
    public AudioClip soapAudio;
    public AudioClip spongeAudio;

    public AudioSource audioSource;
    public AudioSource audioSourceEAT;
    public AudioSource audioSourceBATH;
    public AudioSource audioSourceCorrect;

    public Sprite appleSprite;
    public Sprite bananaSprite;
    public Sprite pizzaSprite;
    public Sprite broccoliSprite;
    public Sprite carrotSprite;
    public Sprite chocolateSprite;
    public Sprite eggSprite;
    public Sprite watermelonSprite;
    public Sprite icecreamSprite;
    public Sprite cakeSprite;
    public GameObject foodItem;

    public Sprite soapSprite;
    public Sprite spongeSprite;
    public GameObject bathObjItem;

    public GameObject happyWinPS;
    public Slider staminaBar;
    //Slider sliderStamina;
    public GameObject WaveListenUI;

    float timer;
    int word;
    int verb;
    string returnFood;
    string returnBathObj;
    Image foodImage;
    string[] foods = { "apple", "banana", "pizza", "broccoli", "carrot", "chocolat", "egg", "watermelon", "ice cream", "cake" };
    string[] bathroomObj = { "soap", "sponge" };
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
        if (Application.loadedLevelName == "Play")
        {
            returnFood = foods[index];
        }
        else
        {
            returnBathObj = bathroomObj[index];
            answerTextField.text = returnBathObj;
            audioSource.clip = soapAudio;
        }        

        answerTextField.color = Color.clear;
           
        timer = 0;
    }

    IEnumerator PlayAudioPart2()
    {        
        yield return new WaitForSeconds(1.5f);
        audioSource.Play();
    }

    void HandleOnResult(object sender, AIResponseEventArgs e)
    {
        WaveListenUI.SetActive(false);
        RunInMainThread(() => {
            var aiResponse = e.Response;
            if (aiResponse != null)
            {
                Debug.Log(aiResponse.Result.ResolvedQuery);
                var outText = JsonConvert.SerializeObject(aiResponse, jsonSettings);

                Debug.Log(outText);                

                string text = aiResponse.Result.ResolvedQuery.ToLower();

                // check what scene
                if (Application.loadedLevelName == "Play")
                {
                    if (text.Contains(returnFood))
                    {
                        word++;
                        PlayerPrefs.SetInt("PlayerWords", word);
                    }

                    if (text.Contains("eat"))
                    {
                        verb++;
                        PlayerPrefs.SetInt("PlayerVerbs", verb);
                    }
                    
                    StartCoroutine(CorrectAnswer(text));
                }
                else
                {

                    if (text.Contains(returnBathObj))
                    {
                        word++;
                        PlayerPrefs.SetInt("PlayerWords", word);
                    }

                    if (text.Contains("take a bath using"))
                    {
                        verb++;
                        PlayerPrefs.SetInt("PlayerVerbs", verb);
                    }
                    
                    StartCoroutine(CorrectAnswer2(text));

                }                             
                
            } else
            {
                Debug.LogError("Response is null");
            }
        });
    }

    IEnumerator CorrectAnswer2(string text)  // Called when user gives a correct answer.
    {
        if (text.Contains(returnBathObj) && text.Contains("take a bath using"))
        {
            audioSourceCorrect.Play();

            //yield return new WaitForSeconds(10);

            ///Character jump animation.

            StartCoroutine(StartHappyAnim());

            StartCoroutine(StaminaFeedbackUI());

            index++;
            if (index == 2)
            {
                index = 0;
            }

            switch (index)
            {
                case 0:
                    foodImage.sprite = soapSprite;
                    bathObjItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = soapAudio;
                    break;
                case 1:
                    foodImage.sprite = spongeSprite;
                    bathObjItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = spongeAudio;
                    break;
            }

        returnBathObj = bathroomObj[index];
        answerTextField.text = returnBathObj;
        //answerTextField.text = "ACERTOU";
        staminaBar.value += 0.25f;
        }
        else
        {
            answerTextField.color = Color.black;
            bathObjItem.SetActive(false);
            //audioSourceBATH.Play();

            timer = 0;

            //if(timer > 1)
            //{
                //audioSource.Play();
            //}
            
            //StartCoroutine(PlayAudioPart2());
            
            //audioSource.Play();                                        
            //answerTextField.text = "ERROU";
        }
        yield return null;
    }

    IEnumerator CorrectAnswer(string text)  // Called when user gives a correct answer.
    {
        if (text.Contains(returnFood) && text.Contains("eat"))
        {
            audioSourceCorrect.Play();

            //yield return new WaitForSeconds(10);

            ///Character jump animation.

            StartCoroutine(StartHappyAnim());

            StartCoroutine(StaminaFeedbackUI());

            index++;
            if (index == 10)
            {
                index = 0;
            }

            switch (index)
            {
                case 0:
                    foodImage.sprite = appleSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = appleAudio;
                    break;
                case 1:
                    foodImage.sprite = bananaSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = bananaAudio;
                    break;
                case 2:
                    foodImage.sprite = pizzaSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = pizzaAudio;
                    break;
                case 3:
                    foodImage.sprite = broccoliSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = broccoliAudio;
                    break;
                case 4:
                    foodImage.sprite = carrotSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = carrotAudio;
                    break;
                case 5:
                    foodImage.sprite = chocolateSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = chocolateAudio;
                    break;
                case 6:
                    foodImage.sprite = eggSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = eggAudio;
                    break;
                case 7:
                    foodImage.sprite = watermelonSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = watermelonAudio;
                    break;
                case 8:
                    foodImage.sprite = icecreamSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    audioSource.clip = icecreamAudio;
                    break;
                case 9:
                    foodImage.sprite = cakeSprite;
                    foodItem.SetActive(true);
                    answerTextField.color = Color.clear;
                    break;
            }

            returnFood = foods[index];
            answerTextField.text = returnFood;
            //answerTextField.text = "ACERTOU";
            staminaBar.value += 0.25f;
        }
        else
        {
            answerTextField.color = Color.black;
            foodItem.SetActive(false);
            audioSourceEAT.Play();

            timer = 0;

            StartCoroutine(PlayAudioPart2());
            //audioSource.Play();                                        
            //answerTextField.text = "ERROU";
        }
        yield return null;
    }

    IEnumerator StaminaFeedbackUI()
    {
        yield return new WaitForSeconds(1F);
        GameObject.Find("StaminaFeedbackPS").GetComponent<ParticleSystem>().Emit(100);
       // yield return new WaitForSeconds(2);

        yield return null;

    }

    void HandleOnError(object sender, AIErrorEventArgs e)
    {
        RunInMainThread(() => {
            Debug.LogException(e.Exception);
            Debug.Log(e.ToString());
            answerTextField.text = "Conexão Ruim"/*e.Exception.Message*/;
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (apiAiUnity != null)
        {
            apiAiUnity.Update();
        }

        // dispatch stuff on main thread
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
        

        staminaBar.value -= 0.009f * Time.deltaTime;
        
    }

    IEnumerator StartHappyAnim()
    {
        GameObject character = GameObject.Find("Pet");
        character.GetComponent<Animator>().SetBool("jump", true);

        int contTime=0;

        happyWinPS.SetActive(true);

        while (true)
        {

            contTime++;
            character.transform.position = new Vector2(0, - Mathf.PingPong(Time.time*3, .8F) /**Time.deltaTime */);
            yield return new WaitForSeconds(.02F);
            if (contTime > 100) break;
        }
        character.GetComponent<Animator>().SetBool("jump",false);

        happyWinPS.SetActive(false);

        yield return null;


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

        //WaveListenUI.SetActive(true);
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
        StartCoroutine(RunApi());
    }

    IEnumerator RunApi()
    {

        if (Application.loadedLevelName == "Play")                
        {
            yield return new WaitForSeconds(1.0F);
            audioSourceEAT.Play();
            yield return new WaitForSeconds(0.4F);
            audioSource.Play();
            yield return new WaitForSeconds(1.5F);
            //PlayerPrefs.SetInt("FirstApple", 1);
            //PlayerPrefs.Save();
        }
        else
        {
            yield return new WaitForSeconds(1.0F);
            audioSourceBATH.Play();
            yield return new WaitForSeconds(2.0F);
            if(index == 0)
            {
                audioSource.clip = soapAudio;
            }
            else
            {
                audioSource.clip = spongeAudio;
            }            
            audioSource.Play();
            yield return new WaitForSeconds(1.5F);
            //PlayerPrefs.SetInt("FirstApple", 1);
            //PlayerPrefs.Save();
        }

        try
        {

            StartCoroutine(StartWaveUI());
            apiAiUnity.StartNativeRecognition();
            
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        yield return null;
    }

    IEnumerator StartWaveUI()   //Starts Wave Listening Animation.
    {
        yield return new WaitForSeconds(.5F);
        WaveListenUI.SetActive(true);
        yield return new WaitForSeconds(6F);
        WaveListenUI.SetActive(false);
    }
}
