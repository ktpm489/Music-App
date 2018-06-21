﻿using KalAcademyMusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KalAcademyMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Song> Songs;
        private static MediaPlayer mediaPlayer;
        DataAccess dataAccess;
        User user;
        public MainPage()
        {
            this.InitializeComponent();
            dataAccess = new DataAccess();
            Songs=dataAccess.GetSongsForUser(new User(0, null, null));
            mediaPlayer = new MediaPlayer();
            user=dataAccess.GetLoggedInUser();
        }
        /// <summary>
        /// On clicling on Play button this method will get called  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Song s = b.DataContext as Song;
            string mp3path = s.MusicMp3Path;
            if(b.Content.ToString()=="Play")
            {
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(mp3path));
                mediaPlayer.Play();
                b.Content = "Stop";
                
            }
            else
            {
                mediaPlayer.Pause();
                b.Content = "Play";
            }
            
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t=sender as TextBox;
            string searchcontent = t.Text;
            DataAccess dataAccess = new DataAccess();
            Songs=dataAccess.GetSongByName(searchcontent);

            //After calling an API we need to rebind GridView with new data(In this case its a collection of songs by name or artist)
            SongCollectionView.ItemsSource = Songs;
        }

        private void ButtonaAddtoFav_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Song s = b.DataContext as Song;
            dataAccess.AddSongToFavorite(user,s);
        }
    }
}