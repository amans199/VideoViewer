//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using VideoEditor.Models;

//namespace VideoEditor.ViewModels
//{
//    public class MainViewModel : INotifyPropertyChanged
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        private ObservableCollection<VideoItem> _videoItems;
//        public ObservableCollection<VideoItem> VideoItems
//        {
//            get => _videoItems;
//            set
//            {
//                _videoItems = value;
//                OnPropertyChanged(nameof(VideoItems));
//            }
//        }

//        private VideoItem _selectedVideo;
//        public VideoItem SelectedVideo
//        {
//            get => _selectedVideo;
//            set
//            {
//                _selectedVideo = value;
//                OnPropertyChanged(nameof(SelectedVideo));
//                // Here you can add logic that should happen when a video is selected
//            }
//        }

//        private bool _isMediaPlaying;
//        public bool IsMediaPlaying
//        {
//            get => _isMediaPlaying;
//            set
//            {
//                _isMediaPlaying = value;
//                OnPropertyChanged(nameof(IsMediaPlaying));
//                // Additional logic for when media play state changes
//            }
//        }

//        // Commands
//        public ICommand PlayCommand { get; }
//        public ICommand PauseCommand { get; }
//        public ICommand StopCommand { get; }
//        public ICommand LoadCommand { get; }
//        public ICommand BrowseFilesCommand { get; }

//        public MainViewModel()
//        {
//            VideoItems = new ObservableCollection<VideoItem>();
//            LoadVideoFilesAsync();

//            // Initialize commands
//            PlayCommand = new RelayCommand(Play);
//            PauseCommand = new RelayCommand(Pause);
//            StopCommand = new RelayCommand(Stop);
//            LoadCommand = new RelayCommand(Load);
//            BrowseFilesCommand = new RelayCommand(BrowseFiles);
//        }

//        private async void LoadVideoFilesAsync()
//        {
//            // Your asynchronous logic to load video files goes here
//        }

//        private void Play()
//        {
//            // Logic to play video
//            IsMediaPlaying = true;
//        }

//        private void Pause()
//        {
//            // Logic to pause video
//            IsMediaPlaying = false;
//        }

//        private void Stop()
//        {
//            // Logic to stop video
//            IsMediaPlaying = false;
//        }

//        private void Load()
//        {
//            // Logic to load a video file
//        }

//        private void BrowseFiles()
//        {
//            // Logic to browse files
//        }

//        protected virtual void OnPropertyChanged(string propertyName) =>
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

//        // RelayCommand implementation (or use any other ICommand implementation)
//        private class RelayCommand : ICommand
//        {
//            private Action _execute;
//            private Func<bool> _canExecute;

//            public RelayCommand(Action execute, Func<bool> canExecute = null)
//            {
//                _execute = execute;
//                _canExecute = canExecute;
//            }

//            public event EventHandler CanExecuteChanged;

//            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

//            public void Execute(object parameter) => _execute();

//            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}
