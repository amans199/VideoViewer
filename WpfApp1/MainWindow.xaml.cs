using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoEditor.Models;

namespace VideoEditor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BackgroundWorker fileBrowserWorker = new BackgroundWorker();
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


        // Configure the BackgroundWorker
        fileBrowserWorker.DoWork += FileBrowserWorker_DoWork;
        fileBrowserWorker.RunWorkerCompleted += FileBrowserWorker_RunWorkerCompleted;

        LoadVideoFilesAsync();

    }

    private void BrowseFilesButton_Click(object sender, RoutedEventArgs e)
    {
        videoPlayer.Stop();
        videoPlayer.Visibility = Visibility.Hidden; // Hide the video player
        videoFilesListView.Visibility = Visibility.Visible; // Show the video list
        pauseOverlay.Visibility = Visibility.Hidden;
    }


    private async void LoadVideoFilesAsync()
    {
        try
        {
            string directoryPath = "C:\\Users\\adm\\Downloads";

            var videoFiles = await Task.Run(() =>
                Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                         .Where(file => new string[] { ".mp4", ".avi", ".wmv", ".mov" }.Contains(Path.GetExtension(file).ToLower()))
                         .Select(file => new VideoItem { Path = file, Name = Path.GetFileName(file) })
                         .ToList());

            foreach (var videoItem in videoFiles)
            {
                videoItem.LoadCoverImage();
            }

            videoFilesListView.ItemsSource = videoFiles;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading video files: " + ex.Message);
        }
    }

    private void VideoFilesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (videoFilesListView.SelectedItem is VideoItem selectedVideo)
        {
            videoPlayer.Source = new Uri(selectedVideo.Path);
            videoPlayer.Play();
            videoPlayer.Visibility = Visibility.Visible; // Show the video player
            videoFilesListView.Visibility = Visibility.Collapsed; // Hide the video list
            pauseOverlay.Visibility = Visibility.Hidden;
        }
    }

    private void FileBrowserWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        string folderPath = e.Argument as string;
        string[] videoExtensions = { ".mp4", ".avi", ".wmv", ".mov" }; // Add more extensions as needed
        DirectoryInfo dirInfo = new DirectoryInfo(folderPath);

        var files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories)
                           .Where(file => videoExtensions.Contains(file.Extension.ToLower()))
                           .Select(file => file.FullName)
                           .ToList();

        e.Result = files;
    }

    private void FileBrowserWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            var videoFiles = e.Result as List<string>;
            // TODO: Update your UI with the list of video files
            // For example, you could populate a ListView with the video filenames
        }
        else
        {
            MessageBox.Show("Error occurred while browsing files: " + e.Error.Message);
        }
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
            pauseOverlay.Visibility = Visibility.Hidden;

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
        pauseOverlay.Visibility = Visibility.Hidden;
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
        pauseOverlay.Visibility = Visibility.Hidden;
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
