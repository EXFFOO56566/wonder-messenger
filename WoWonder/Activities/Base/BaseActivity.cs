using System;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Window;
using AndroidX.Activity;
using AndroidX.AppCompat.App;
using WoWonder.Activities.Call.Agora;
using WoWonder.Activities.Call.Twilio;
using WoWonder.Activities.Editor;
using WoWonder.Activities.SettingsPreferences;
using WoWonder.Activities.Story;
using WoWonder.Activities.Tab;
using WoWonder.Activities.Viewer;
using WoWonder.Helpers.Utils;
using Xamarin.Google.Android.DataTransport;

namespace WoWonder.Activities.Base
{
    [Activity]
    public class BaseActivity : AppCompatActivity
    {
        #region General

        public void InitBackPressed(string pageName = "")
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
                {
                    OnBackInvokedDispatcher.RegisterOnBackInvokedCallback((int)Priority.Default, new BackCallAppBase(this, pageName, true));
                }
                else
                {
                    OnBackPressedDispatcher.AddCallback(new BackCallAppBase(this, pageName, true));
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnTrimMemory(TrimMemory level)
        {
            try
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                //Glide.With(this).OnTrimMemory(level);
                base.OnTrimMemory(level);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnLowMemory()
        {
            try
            {
                GC.Collect(GC.MaxGeneration);
               // Glide.With(this).OnLowMemory();
                base.OnLowMemory();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            try
            {
                base.OnConfigurationChanged(newConfig);

                var currentNightMode = newConfig.UiMode & UiMode.NightMask;
                switch (currentNightMode)
                {
                    case UiMode.NightNo:
                        // Night mode is not active, we're using the light theme
                        MainSettings.ApplyTheme(MainSettings.LightMode);
                        break;
                    case UiMode.NightYes:
                        // Night mode is active, we're using dark theme
                        MainSettings.ApplyTheme(MainSettings.DarkMode);
                        break;
                }

                Delegate.SetLocalNightMode(WoWonderTools.IsTabDark() ? AppCompatDelegate.ModeNightYes : AppCompatDelegate.ModeNightNo);
                SetTheme(WoWonderTools.IsTabDark() ? Resource.Style.MyTheme_Dark : Resource.Style.MyTheme);
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        #endregion

        #region Menu

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        #endregion 
    }
     
    public class BackCallAppBase : OnBackPressedCallback, IOnBackInvokedCallback
    {
        private readonly Activity Activity;
        private readonly string PageName;
        public void OnBackInvoked()
        {
            try
            {
                // Back is pressed... Finishing the activity
                Activity?.Finish();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }
         
        public BackCallAppBase(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public BackCallAppBase(bool enabled) : base(enabled)
        {
        }
         
        public BackCallAppBase(Activity activity, string pageName, bool enabled) : base(enabled)
        {
            Activity = activity;
            PageName = pageName;
        }

        public override void HandleOnBackPressed()
        {
            try
            {
                if (string.IsNullOrEmpty(PageName))
                {
                    // Back is pressed... Finishing the activity
                    Activity?.Finish();
                } 
                else if (PageName == "TabbedMainActivity")
                {
                    var subActivity = Activity as TabbedMainActivity;
                    subActivity?.BackPressed();
                }
                else if (PageName == "AgoraAudioCallActivity")
                {
                    var subActivity  = Activity as AgoraAudioCallActivity;
                    subActivity?.FinishCall();
                } 
                else if (PageName == "AgoraVideoCallActivity")
                {
                    var subActivity  = Activity as AgoraVideoCallActivity;
                    subActivity?.FinishCall();
                } 
                else if (PageName == "TwilioAudioCallActivity")
                {
                    var subActivity  = Activity as TwilioAudioCallActivity;
                    subActivity?.FinishCall();
                } 
                else if (PageName == "TwilioVideoCallActivity")
                {
                    var subActivity  = Activity as TwilioVideoCallActivity;
                    subActivity?.FinishCall();
                } 
                else if (PageName == "StoryReplyActivity")
                {
                    var subActivity  = Activity as StoryReplyActivity;
                    subActivity?.BackPressed();
                } 
                else if (PageName == "VideoFullScreenActivity")
                {
                    var subActivity  = Activity as VideoFullScreenActivity;
                    subActivity?.BackPressed();
                } 
                else if (PageName == "EditColorActivity")
                {
                    var subActivity  = Activity as EditColorActivity;
                    subActivity?.BackPressed();
                } 
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            } 
        }
    }
}