using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VideoEditor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private bool IsMediaPlaying = false; // A field to track the playing state
    private int _clickCount = 0; // Track the number of clicks to detect double-click
    public MainWindow()
    {
        InitializeComponent();
        // Assuming 'videoPlayer' is the name of your MediaElement in XAML
        videoPlayer.LoadedBehavior = MediaState.Manual;
        videoPlayer.UnloadedBehavior = MediaState.Manual;

        // Set focus to the MediaElement to receive key events.
        videoPlayer.Focus();

        videoPlayer.MouseLeftButtonUp += VideoPlayer_MouseLeftButtonUp;

    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {

        IsMediaPlaying = false;
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Video Files (*.mp4;*.avi;*.wmv)|*.mp4;*.avi;*.wmv|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            videoPlayer.Source = new Uri(openFileDialog.FileName);
            videoPlayer.Position = TimeSpan.FromSeconds(0);
            
        }
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        videoPlayer.Play();
    }

    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        videoPlayer.Pause();
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        videoPlayer.Stop();
    }

    private void VideoPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TogglePlayPause();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            TogglePlayPause();
        }
    }

    private void TogglePlayPause()
    {
        if (videoPlayer.Source != null) // Check if a video is loaded
        {
            if (IsMediaPlaying)
            {
                videoPlayer.Pause();
                pauseOverlay.Visibility = Visibility.Visible; // Show the overlay
                IsMediaPlaying = false;
            }
            else
            {
                videoPlayer.Play();
                pauseOverlay.Visibility = Visibility.Hidden; // Hide the overlay
                IsMediaPlaying = true;
            }

        }
    }

    private void videoPlayer_MediaEnded(object sender, RoutedEventArgs e)
    {
        IsMediaPlaying = false;
        videoPlayer.Position = TimeSpan.Zero;
    }

    private void VideoPlayer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _clickCount++;
        if (_clickCount == 2)
        {
            _clickCount = 0; // Reset count after a double-click is detected
            ToggleFullScreen();
        }

        // Reset the click count after a short time interval
        var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) }; // 500 is a commonly used interval for double-click
        timer.Tick += (s, args) => { timer.Stop(); _clickCount = 0; };
        timer.Start();
    }

    private void FullScreenButton_Click(object sender, RoutedEventArgs e)
    {
        ToggleFullScreen();
    }

    private void ToggleFullScreen()
    {
        if (WindowState == WindowState.Maximized && WindowStyle == WindowStyle.None)
        {
            // Switch to normal mode
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;
            ResizeMode = ResizeMode.CanResize;
        }
        else
        {
            // Switch to full-screen mode
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
        }
    }
}
