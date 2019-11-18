using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Speech;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Util;
using Fragment = Android.Support.V4.App.Fragment;

namespace IronMan_mobile2
{
    public class Editor : Fragment
    {
        private bool isRecording;
        private readonly int VOICE = 10;
        private EditText textBox;
        private ImageButton recButton;
        private EditText etIPaddress;
        private string serIpAddress;
        private Button saveScript;
        private Button connectButton;
        private Button editButton;
        public static List<string> scriptsList = MainActivity.scriptList;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.scripteditor, container, false);
            
            // set the isRecording flag to false (not recording)
            isRecording = false;

            // get the resources from the layout
            textBox = view.FindViewById<EditText>(Resource.Id.textYourText);
            
            //disable textBox editing
            textBox.Enabled = false;
            
            saveScript = view.FindViewById<Button>(Resource.Id.save_script);

            //find IP field
            etIPaddress = view.FindViewById<EditText>(Resource.Id.edIPaddress);
            recButton = view.FindViewById<ImageButton>(Resource.Id.btnRecord);
            connectButton = view.FindViewById<Button>(Resource.Id.connect);
            editButton = view.FindViewById<Button>(Resource.Id.btnEdit);
            
            //connect to computer
            connectButton.Click += delegate
            {
                GetScriptConnection.StartConnectionAsync(etIPaddress.Text);
                Toast.MakeText(this.Activity, "IP Address is empty", ToastLength.Long).Show();
            };
            
            textBox.EditorAction += delegate(object sender, TextView.EditorActionEventArgs args)
            {
                if (args.ActionId == ImeAction.Done)
                    textBox.Enabled = false;
            };

            //enable editing
            editButton.Click += delegate
            {
                textBox.RequestFocus();
                if (textBox.IsFocused)
                {
                    textBox.Enabled = true;
                    InputMethodManager imm =
                        Activity.GetSystemService(Context.InputMethodService) as InputMethodManager;  
                    imm?.ShowSoftInput(textBox, ShowFlags.Forced);
                }
                else textBox.Enabled = false;
            };
            

            // check to see if we can actually record - if we can, assign the event to the button
            string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                // no microphone, no recording. Disable the button and output an alert
                var alert = new AlertDialog.Builder(recButton.Context);
                alert.SetTitle("You don't seem to have a microphone to record with");
                alert.SetPositiveButton("OK", (sender, e) =>
                {
                    textBox.Text = "No microphone present";
                    recButton.Enabled = false;
                    return;
                });

                alert.Show();
            }
            else
                recButton.Click += delegate
                {
                    // change the text on the button
                    isRecording = !isRecording;
                    if (isRecording)
                    {
                        // create the intent and start the activity
                        var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel,
                            RecognizerIntent.LanguageModelFreeForm);

                        // put a message on the modal dialog
                        voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt,
                            Application.Context.GetString(Resource.String.messageSpeakNow));

                        // if there is more then 1.5s of silence, consider the speech over
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis,
                            1500);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                        voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

                        // you can specify other languages recognised here, for example
                        // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
                        // if you wish it to recognise the default Locale language and German
                        // if you do use another locale, regional dialects may not be recognised very well

                        voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                        StartActivityForResult(voiceIntent, VOICE);
                    }
                    isRecording = !isRecording;
                };
            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultCode == (int) Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = textBox.Text + matches[0] + '\n';

                        // limit the output to 500 characters
                        if (textInput.Length > 500)
                            textInput = textInput.Substring(0, 500);
                        textBox.Text = textInput;
                    }
                    else
                        textBox.Text = "No speech was recognised";
                    

                    saveScript.Click += delegate
                    {
                        serIpAddress = etIPaddress.Text;
                        if (serIpAddress.Length == 0)
                        {
                            Toast.MakeText(this.Activity, "IP Address is empty", ToastLength.Long).Show();
                        }
                        else
                        {
                            LayoutInflater layoutInflater = LayoutInflater.From(Context);
                            View view = layoutInflater.Inflate(Resource.Layout.file_name_dialog, null);
                            view.SetBackgroundResource(Resource.Drawable.ip_background);
                            AlertDialog.Builder builder = new AlertDialog.Builder(Context);
                            builder.SetTitle("Input file name");
                            EditText input = view.FindViewById<EditText>(Resource.Id.input);
                            builder.SetView(view);

                            builder.SetCancelable(false)
                                .SetPositiveButton("Save", delegate
                                {
                                    SendScriptConnection.StartConnectionAsync(serIpAddress, input.Text,
                                        textBox.Text);
                                    Toast.MakeText(Activity, "Script " + input.Text + " has been saved",
                                        ToastLength.Short).Show();
                                })
                                .SetNegativeButton("Cancel", delegate { builder.Dispose(); });
                            builder.Create().Show();
                        }
                    };
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}