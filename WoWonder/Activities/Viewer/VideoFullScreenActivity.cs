using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Drm;
using Com.Google.Android.Exoplayer2.Extractor.TS;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Source.Dash;
using Com.Google.Android.Exoplayer2.Source.Hls;
using Com.Google.Android.Exoplayer2.Source.Smoothstreaming;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.UI;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2.Util;
using WoWonder.Activities.Base;
using WoWonder.Activities.Tab;
using WoWonder.Helpers.Utils;
using Xamarin.Google.Android.DataTransport;
using Uri = Android.Net.Uri;

namespace WoWonder.Activities.Viewer
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.Locale | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.UiMode)]
    public class VideoFullScreenActivity : AppCompatActivity, IPlayerEventListener
    {
        #region Variables Basic

        private ProgressBar ProgressBar;
        private PlayerView PlayerView;
        private SimpleExoPlayer VideoPlayer;
        private string VideoUrl;

        #endregion

        #region General

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetTheme(WoWonderTools.IsTabDark() ? Resource.Style.MyTheme_Dark : Resource.Style.MyTheme);

                // Create your application here
                Methods.App.FullScreenApp(this, true);

                //ScreenOrientation.Portrait >>  Make to run your application only in portrait mode
                //ScreenOrientation.Landscape >> Make to run your application only in LANDSCAPE mode 
                //RequestedOrientation = ScreenOrientation.Landscape;

                SetContentView(Resource.Layout.VideoFullScreenLayout);

                VideoUrl = Intent?.GetStringExtra("videoUrl") ?? "";
                //var VideoDuration = Intent?.GetStringExtra("videoDuration") ?? "";

                //Get Value And Set Toolbar
                InitComponent();
                InitBackPressed();
                SetPlayer();

                // Uri
                Uri uri = Uri.Parse(VideoUrl);
                var videoSource = GetMediaSourceFromUrl(uri, uri?.Path?.Split('.').Last(), "normal");

                VideoPlayer.Prepare(videoSource);
                VideoPlayer.PlayWhenReady = true;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnPause()
        {
            try
            {
                StopVideo();
                base.OnPause();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        protected override void OnStop()
        {
            try
            {
                StopVideo();
                base.OnStop();
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
                base.OnLowMemory();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        protected override void OnDestroy()
        {
            try
            {
                ReleaseVideo();

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                base.OnDestroy();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        #endregion

        #region Functions

        private void InitComponent()
        {
            try
            {
                var media = new MediaController(this);
                media.Show(5000);

                ProgressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
                ProgressBar.Visibility = ViewStates.Visible;

                PlayerView = FindViewById<PlayerView>(Resource.Id.videoView);

                TabbedMainActivity.GetInstance()?.SetWakeLock();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private void InitBackPressed()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
                {
                    OnBackInvokedDispatcher.RegisterOnBackInvokedCallback((int)Priority.Default, new BackCallAppBase(this, "VideoFullScreenActivity", true));
                }
                else
                {
                    OnBackPressedDispatcher.AddCallback(new BackCallAppBase(this, "VideoFullScreenActivity", true));
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        #endregion

        #region Exo Player

        private void SetPlayer()
        {
            try
            {
                DefaultTrackSelector trackSelector = new DefaultTrackSelector(this);
                trackSelector.SetParameters(new DefaultTrackSelector.ParametersBuilder(this));

                VideoPlayer = new SimpleExoPlayer.Builder(this).SetTrackSelector(trackSelector).Build();

                PlayerView.UseController = true;
                PlayerView.Player = VideoPlayer;
                VideoPlayer.AddListener(this);

                var controlView = PlayerView.FindViewById<PlayerControlView>(Resource.Id.exo_controller);
                if (controlView != null)
                {
                    var mFullScreenIcon = controlView.FindViewById<ImageView>(Resource.Id.exo_fullscreen_icon);
                    var mFullScreenButton = controlView.FindViewById<FrameLayout>(Resource.Id.exo_fullscreen_button);

                    mFullScreenIcon.Visibility = ViewStates.Gone;
                    mFullScreenButton.Visibility = ViewStates.Gone;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        private IMediaSource GetMediaSourceFromUrl(Uri uri, string extension, string tag)
        {
            var bandwidthMeter = DefaultBandwidthMeter.GetSingletonInstance(this);
            var buildHttpDataSourceFactory = new DefaultDataSourceFactory(this, bandwidthMeter, new DefaultHttpDataSourceFactory(Util.GetUserAgent(this, AppSettings.ApplicationName)));
            var buildHttpDataSourceFactoryNull = new DefaultDataSourceFactory(this, bandwidthMeter, new DefaultHttpDataSourceFactory(Util.GetUserAgent(this, AppSettings.ApplicationName)));
            int type = Util.InferContentType(uri, extension);
            try
            {

                IMediaSource src;
                switch (type)
                {
                    case C.TypeSs:
                        src = new SsMediaSource.Factory(new DefaultSsChunkSource.Factory(buildHttpDataSourceFactory), buildHttpDataSourceFactoryNull).SetTag(tag).SetDrmSessionManager(IDrmSessionManager.DummyDrmSessionManager).CreateMediaSource(uri);
                        break;
                    case C.TypeDash:
                        src = new DashMediaSource.Factory(new DefaultDashChunkSource.Factory(buildHttpDataSourceFactory), buildHttpDataSourceFactoryNull).SetTag(tag).SetDrmSessionManager(IDrmSessionManager.DummyDrmSessionManager).CreateMediaSource(uri);
                        break;
                    case C.TypeHls:
                        DefaultHlsExtractorFactory defaultHlsExtractorFactory = new DefaultHlsExtractorFactory(DefaultTsPayloadReaderFactory.FlagAllowNonIdrKeyframes, true);
                        src = new HlsMediaSource.Factory(buildHttpDataSourceFactory).SetTag(tag).SetExtractorFactory(defaultHlsExtractorFactory).CreateMediaSource(uri);
                        break;
                    case C.TypeOther:
                        src = new ProgressiveMediaSource.Factory(buildHttpDataSourceFactory).SetTag(tag).CreateMediaSource(uri);
                        break;
                    default:
                        src = new ProgressiveMediaSource.Factory(buildHttpDataSourceFactory).SetTag(tag).CreateMediaSource(uri);
                        break;
                }

                return src;
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
                try
                {
                    return new ProgressiveMediaSource.Factory(buildHttpDataSourceFactory).SetTag(tag).CreateMediaSource(uri);
                }
                catch (Exception exception)
                {
                    Methods.DisplayReportResultTrack(exception);
                    return null!;
                }
            }
        }

        public void StopVideo()
        {
            try
            {
                if (PlayerView.Player != null && PlayerView.Player.PlaybackState == IPlayer.StateReady)
                    PlayerView.Player.PlayWhenReady = false;

                TabbedMainActivity.GetInstance()?.SetOffWakeLock();

                //GC Collect
                //GC.Collect();
            }
            catch (Exception exception)
            {
                Methods.DisplayReportResultTrack(exception);
            }
        }

        public void ReleaseVideo()
        {
            try
            {
                StopVideo();
                PlayerView?.Player?.Stop();

                if (VideoPlayer != null)
                {
                    VideoPlayer.Release();
                    VideoPlayer = null!;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnLoadingChanged(bool p0)
        {
        }

        public void OnPlaybackParametersChanged(PlaybackParameters p0)
        {
        }

        public void OnPlaybackSuppressionReasonChanged(int playbackSuppressionReason)
        {

        }

        public void OnPlayerError(ExoPlaybackException p0)
        {
        }

        public void OnPlayerStateChanged(bool playWhenReady, int playbackState)
        {
            try
            {
                switch (playbackState)
                {
                    case IPlayer.StateBuffering:
                        ProgressBar.Visibility = ViewStates.Visible;
                        break;
                    case IPlayer.StateReady:
                    case IPlayer.StateEnded:
                        ProgressBar.Visibility = ViewStates.Invisible;
                        break;
                }
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
            }
        }

        public void OnIsPlayingChanged(bool p0)
        {

        }

        public void OnPositionDiscontinuity(int p0)
        {
        }

        public void OnRepeatModeChanged(int p0)
        {
        }

        public void OnSeekProcessed()
        {
        }

        public void OnShuffleModeEnabledChanged(bool p0)
        {
        }

        public void OnTimelineChanged(Timeline p0, int p2)
        {
        }

        public void OnTracksChanged(TrackGroupArray p0, TrackSelectionArray p1)
        {
        }


        #endregion
         
        public void BackPressed()
        {
            try
            {
                ReleaseVideo();

                TabbedMainActivity.GetInstance()?.OffWakeLock();

                Finish();
            }
            catch (Exception e)
            {
                Methods.DisplayReportResultTrack(e);
                Finish();
            }
        }

    }
}