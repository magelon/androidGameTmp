// <copyright file="SigninSampleScript.cs" company="Google Inc.">
// Copyright (C) 2017 Google Inc. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations

namespace SignInSample {
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Google;
  using UnityEngine;
  using UnityEngine.UI;
    using Firebase;
    using Firebase.Auth;
    using Firebase.Database;
    using Firebase.Unity.Editor;


    public class SigninSampleScript : MonoBehaviour {

        public Text statusText;
        public GameObject signButton;
        public GameObject start;

    public string webClientId = "481628943012-jrh4iq9m8k1ogrm4keg5jt601t2qr479.apps.googleusercontent.com";
        
    private GoogleSignInConfiguration configuration;
        DatabaseReference reference;

        // Defer the configuration creation until Awake so the web Client ID
        // Can be set via the property inspector in the Editor.
        void Awake() {
            configuration = new GoogleSignInConfiguration {
                WebClientId = webClientId,
                RequestEmail = true,
            RequestIdToken = true
      };
    }


        private void Start()
        {
            // Set these values before calling into the realtime database.
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://warsimulator-26663651.firebaseio.com/");
            // Get the root reference location of the database.
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            if (PlayerPrefs.HasKey("signed"))
            {
                start.SetActive(true);
                signButton.SetActive(false);
            }
            else
            {
               // OnSignIn();
            }

        }


        public void OnSignIn() {
      GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.RequestEmail = true;
            GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
            AddStatusText("Calling SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    public void OnSignOut() {
      AddStatusText("Calling SignOut");
      GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect() {
      AddStatusText("Calling Disconnect");
      GoogleSignIn.DefaultInstance.Disconnect();
    }


        private void writeNewUser(string userId, string name, string email,string time)
        {
            //t.text = name;
            User user = new User(name, email,time);
            string json = JsonUtility.ToJson(user);

            reference.Child("puzzle").Child("users").Child(userId).SetRawJsonValueAsync(json);
        }



        internal void OnAuthenticationFinished(Task<GoogleSignInUser> task) {
      if (task.IsFaulted) {
        using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator()) {
          if (enumerator.MoveNext()) {
            GoogleSignIn.SignInException error =
                    (GoogleSignIn.SignInException)enumerator.Current;
            AddStatusText("Got Error: " + error.Status + " " + error.Message);
          } else {
            AddStatusText("Got Unexpected Exception?!?" + task.Exception);
          }
        }
      } else if(task.IsCanceled) {
        AddStatusText("Canceled");
      } else  {
        AddStatusText("Welcome: " + task.Result.DisplayName + "!");
        writeNewUser(task.Result.UserId, task.Result.DisplayName, task.Result.Email,DateTime.Now.ToString());
                PlayerPrefs.SetInt("signed", 1);
                start.SetActive(true);
                signButton.SetActive(false);

            }
    }

    public void OnSignInSilently() {
      GoogleSignIn.Configuration = configuration;
      GoogleSignIn.Configuration.UseGameSignIn = false;
      GoogleSignIn.Configuration.RequestIdToken = true;
      AddStatusText("Calling SignIn Silently");

      GoogleSignIn.DefaultInstance.SignInSilently()
            .ContinueWith(OnAuthenticationFinished);
    }


    public void OnGamesSignIn() {
      GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.RequestEmail = true;
            GoogleSignIn.Configuration.UseGameSignIn = true;
      GoogleSignIn.Configuration.RequestIdToken = false;

      AddStatusText("Calling Games SignIn");

      GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
        OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    void AddStatusText(string text) {
      if (messages.Count == 1) {
        messages.RemoveAt(0);
      }
      messages.Add(text);
      string txt = "";
      foreach (string s in messages) {
        txt += "\n" + s;
      }
      statusText.text = txt;
    }
  }
}
