//Use DoughouzChecker last version 4.2 to 
//build your own certifcate 
//For Full Documention 
//https://doughouzlight.com/?onepage-docs=wowonder-android
//CopyRight DoughouzLight
//For the accuracy of the icon and logo, please use this website " https://appicon.co/ " and add images according to size in folders " mipmap " 
 
using SocketIOClient.Transport;
using WoWonder.Helpers.Model;
using WoWonderClient;

namespace WoWonder
{
    internal static class AppSettings
    {
        public static readonly string TripleDesAppServiceProvider = "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";
    
        //Main Settings >>>>>
        //********************************************************* 
        public static string Version = "4.6";
        public static readonly string ApplicationName = "WoWonder";
        public static readonly string DatabaseName = "WoWonderMessenger";

        // Friend system = 0 , follow system = 1
        public static readonly int ConnectivitySystem = 1;
         
        public static readonly InitializeWoWonder.ConnectionType ConnectionTypeChat = InitializeWoWonder.ConnectionType.Socket; 
        public static readonly string PortSocketServer = "449"; 
        public static readonly TransportProtocol Transport = TransportProtocol.WebSocket;

        //Main Colors >>
        //*********************************************************
        public static readonly string MainColor = "#C83747";
        public static readonly string StoryReadColor = "#808080";

        //Language Settings >> http://www.lingoes.net/en/translator/langcode.htm
        //*********************************************************
        public static bool FlowDirectionRightToLeft = false;
        public static string Lang = ""; //Default language ar_AE

        //Set Language User on site from phone 
        public static readonly bool SetLangUser = true;  

        //Notification Settings >>
        //*********************************************************
        public static bool ShowNotification = true;
        public static string OneSignalAppId = "64974c58-9993-40ed-b782-0814edc401ea";

        //Error Report Mode
        //*********************************************************
        public static readonly bool SetApisReportMode = false;

        //Code Time Zone (true => Get from Internet , false => Get From #CodeTimeZone )
        //*********************************************************
        public static readonly bool AutoCodeTimeZone = true;
        public static readonly string CodeTimeZone = "UTC";

        public static readonly bool EnableRegisterSystem = true; 

        //Set Theme Full Screen App
        //*********************************************************
        public static readonly bool EnableFullScreenApp = false;
         
        public static readonly bool ShowSettingsUpdateManagerApp = false; 

        public static readonly bool ShowSettingsRateApp = true;  
        public static readonly int ShowRateAppCount = 5;

        //AdMob >> Please add the code ad in the Here and analytic.xml 
        //********************************************************* 
        public static readonly ShowAds ShowAds = ShowAds.AllUsers;

        //Three times after entering the ad is displayed
        public static readonly int ShowAdInterstitialCount = 5;
        public static readonly int ShowAdRewardedVideoCount = 5;
        public static int ShowAdNativeCount = 40;
        public static readonly int ShowAdAppOpenCount = 3;
         
        public static readonly bool ShowAdMobBanner = true;
        public static readonly bool ShowAdMobInterstitial = true;
        public static readonly bool ShowAdMobRewardVideo = true;
        public static readonly bool ShowAdMobNative = true;
        public static readonly bool ShowAdMobAppOpen = true; 
        public static readonly bool ShowAdMobRewardedInterstitial = true; 

        public static readonly string AdInterstitialKey = "ca-app-pub-5135691635931982/3442638218";
        public static readonly string AdRewardVideoKey = "ca-app-pub-5135691635931982/3814173301";
        public static readonly string AdAdMobNativeKey = "ca-app-pub-5135691635931982/9452678647";
        public static readonly string AdAdMobAppOpenKey = "ca-app-pub-5135691635931982/3836425196";  
        public static readonly string AdRewardedInterstitialKey = "ca-app-pub-5135691635931982/7476900652";
         
        //FaceBook Ads >> Please add the code ad in the Here and analytic.xml 
        //*********************************************************
        public static readonly bool ShowFbBannerAds = false;
        public static readonly bool ShowFbInterstitialAds = false;
        public static readonly bool ShowFbRewardVideoAds = false;
        public static readonly bool ShowFbNativeAds = false;

        //YOUR_PLACEMENT_ID
        public static readonly string AdsFbBannerKey = "250485588986218_554026418632132";
        public static readonly string AdsFbInterstitialKey = "250485588986218_554026125298828";
        public static readonly string AdsFbRewardVideoKey = "250485588986218_554072818627492";
        public static readonly string AdsFbNativeKey = "250485588986218_554706301897477";

        //Colony Ads >> Please add the code ad in the Here 
        //*********************************************************  
        public static readonly bool ShowColonyBannerAds = true;  
        public static readonly bool ShowColonyInterstitialAds = true; 
        public static readonly bool ShowColonyRewardAds = true; 

        public static readonly string AdsColonyAppId = "appff22269a7a0a4be8aa"; 
        public static readonly string AdsColonyBannerId = "vz85ed7ae2d631414fbd";  
        public static readonly string AdsColonyInterstitialId = "vz39712462b8634df4a8"; 
        public static readonly string AdsColonyRewardedId = "vz32ceec7a84aa4d719a"; 
        //********************************************************* 

        //Social Logins >>
        //If you want login with facebook or google you should change id key in the analytic.xml file or AndroidManifest.xml
        //Facebook >> ../values/analytic.xml .. 
        //Google >> ../Properties/AndroidManifest.xml .. line 37
        //*********************************************************
        public static readonly bool EnableSmartLockForPasswords = false; 
         
        public static readonly bool ShowFacebookLogin = true;
        public static readonly bool ShowGoogleLogin = true;

        public static readonly string ClientId = "81603239249-i35mh67livs9gifrlv83e47dd3ohamsg.apps.googleusercontent.com";

        //Chat Window Activity >>
        //*********************************************************
        //if you want this feature enabled go to Properties -> AndroidManefist.xml and remove comments from below code
        //Just replace it with this 5 lines of code
        /*
         <uses-permission android:name="android.permission.READ_CONTACTS" />
         <uses-permission android:name="android.permission.READ_PHONE_NUMBERS" /> 
         */
        public static readonly bool ShowButtonContact = true;
        public static readonly bool InvitationSystem = true;  //Invite friends section
        /////////////////////////////////////
        
        public static readonly ChatTheme ChatTheme = ChatTheme.Default; //#new
         
        public static readonly bool ShowButtonCamera = true;
        public static readonly bool ShowButtonImage = true;
        public static readonly bool ShowButtonVideo = true;
        public static readonly bool ShowButtonAttachFile = true;
        public static readonly bool ShowButtonColor = true;
        public static readonly bool ShowButtonStickers = true;
        public static readonly bool ShowButtonMusic = true;
        public static readonly bool ShowButtonGif = true;
        public static readonly bool ShowButtonLocation = true;
         
        public static readonly bool OpenVideoFromApp = true;
        public static readonly bool OpenImageFromApp = true;
        
        //Record Sound Style & Text 
        public static readonly bool ShowButtonRecordSound = true;

        // Options List Message
        public static readonly bool EnableReplyMessageSystem = true; 
        public static readonly bool EnableForwardMessageSystem = true; 
        public static readonly bool EnableFavoriteMessageSystem = true; 
        public static readonly bool EnablePinMessageSystem = true; 
        public static readonly bool EnableReactionMessageSystem = true; 

        public static readonly bool ShowNotificationWithUpload = true;  

        public static readonly bool AllowDownloadMedia = true; 
        public static readonly bool EnableFitchOgLink = true; 

        public static readonly bool EnableSuggestionMessage = true; 

        /// <summary>
        /// https://dashboard.stipop.io/
        /// you can get api key from here https://prnt.sc/26ofmq9
        /// </summary>
        public static readonly string StickersApikey = "0a441b19287cad752e87f6072bb914c0"; 
         
        //List Chat >>
        //*********************************************************
        public static readonly bool EnableChatPage = false; //>> Next update 
        public static readonly bool EnableChatGroup = true;
         
        // Options List Chat
        public static readonly bool EnableChatArchive = true; 
        public static readonly bool EnableChatPin = true;  
        public static readonly bool EnableChatMute = true; 
        public static readonly bool EnableChatMakeAsRead = true; 
         
        // Story >>
        //*********************************************************
        //Set a story duration >> Sec
        public static readonly long StoryImageDuration = 7;
        public static readonly long StoryVideoDuration = 30;  

        /// <summary>
        /// If it is false, it will appear only for the specified time in the value of the StoryVideoDuration
        /// </summary>
        public static readonly bool ShowFullVideo = false;  

        public static readonly bool EnableStorySeenList = true; 
        public static readonly bool EnableReplyStory  = true;

        /// <summary>
        /// you can edit video using FFMPEG 
        /// </summary>
        public static readonly bool EnableVideoEditor = true;  
        public static readonly bool EnableVideoCompress = false;

        //*********************************************************
        /// <summary>
        ///  Currency
        /// CurrencyStatic = true : get currency from app not api 
        /// CurrencyStatic = false : get currency from api (default)
        /// </summary>
        public static readonly bool CurrencyStatic = false;
        public static readonly string CurrencyIconStatic = "$";
        public static readonly string CurrencyCodeStatic = "USD";

        // Video/Audio Call Settings >>
        //*********************************************************
        public static readonly EnableCall EnableCall = EnableCall.AudioAndVideo;  //#new
        public static readonly SystemCall UseLibrary = SystemCall.Agora;  

        // Walkthrough Settings >>
        //*********************************************************
        public static readonly bool ShowWalkTroutPage = true;
         
        // Register Settings >>
        //*********************************************************
        public static readonly bool ShowGenderOnRegister = true;
         
        //Last Messages Page >>
        //*********************************************************
        public static readonly bool ShowOnlineOfflineMessage = true;

        public static readonly int RefreshAppAPiSeconds = 3500; // 3 Seconds
        public static readonly int MessageRequestSpeed = 4000; // 3 Seconds
         
        public static readonly ToastTheme ToastTheme = ToastTheme.Custom; 
        public static readonly ColorMessageTheme ColorMessageTheme = ColorMessageTheme.Default; 
        public static readonly PostButtonSystem ReactionTheme = PostButtonSystem.ReactionDefault;
          
        //Bypass Web Errors 
        //*********************************************************
        public static readonly bool TurnTrustFailureOnWebException = true;
        public static readonly bool TurnSecurityProtocolType3072On = true;

        public static readonly bool ShowTextWithSpace = false;

        public static TabTheme SetTabDarkTheme = TabTheme.Light;

        public static readonly bool ShowSuggestedUsersOnRegister = true; 

        //Settings Page >> General Account
        public static readonly bool ShowSettingsAccount = true;
        public static readonly bool ShowSettingsPassword = true;
        public static readonly bool ShowSettingsBlockedUsers = true;
        public static readonly bool ShowSettingsDeleteAccount = true;
        public static readonly bool ShowSettingsTwoFactor = true;
        public static readonly bool ShowSettingsManageSessions = true;
        public static readonly bool ShowSettingsWallpaper  = true; 
        public static readonly bool ShowSettingsFingerprintLock = true; 

        //Options chat heads (Bubbles) 
        //*********************************************************
        public static readonly bool ShowChatHeads = true; 

        //Always , Hide , FullScreen
        public static readonly string DisplayModeSettings = "Always";

        //Default , Left  , Right , Nearest , Fix , Thrown
        public static readonly string MoveDirectionSettings = "Right";

        //Circle , Rectangle
        public static readonly string ShapeSettings = "Circle";

        // Last position
        public static readonly bool IsUseLastPosition = true;

        public static readonly int AvatarPostSize = 60; 
        public static readonly int ImagePostSize = 200; 
    }
}