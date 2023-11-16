using System.Windows.Media.Imaging;
using System;
using NAudio.Wave;
using System.ComponentModel;
using System.Diagnostics;

namespace VideoEditor.Models;
public class VideoItem
{
    public string Name { get; set; }
    public string Path { get; set; }
    public BitmapImage CoverImage { get; set; }

    public void LoadCoverImage()
    {
        //display simple placeholder image 
        CoverImage = new BitmapImage(new Uri("https://placehold.co/600x400", UriKind.Absolute));
    }

}
