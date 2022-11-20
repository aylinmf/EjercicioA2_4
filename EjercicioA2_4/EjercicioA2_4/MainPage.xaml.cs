using EjercicioA2_4.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace EjercicioA2_4
{
    public partial class MainPage : ContentPage
    {
        MediaFile FileVideo;
        Video video = null;

        public MainPage()
        {
            InitializeComponent();
            if (App.DBase != null) { }
        }

        
        private async void btnTomarVideo_Clicked(object sender, EventArgs e)
        {
            try
            {
               var name = DateTime.Now.ToString("yyyy'-'MM'-'dd'_'HHmmss") + ".mp4";

                FileVideo = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Name = name,
                    Directory = "Videos"
                });

                if (FileVideo == null)
                    return;


                mediaElement.Source = FileVideo.Path;

                await DisplayAlert("Video guardado en storage con exito", "Path: " + FileVideo.Path, "OK");

                video = new Video
                {
                    id = 0,
                    nombre = name,
                    Path = FileVideo.Path,
                    Date = DateTime.Now
                };

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

        }

        private async void btnGuardarVideo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (FileVideo == null)
                {
                    await DisplayAlert("Aviso", "Debe agregar un video", "OK");
                    return;
                }

                var result = await App.DBase.insertUpdateVideo(video);

                if (result > 0)
                {
                    await DisplayAlert("Aviso", "Video guardado en base de datos", "OK");

                    FileVideo = null;
                    video = null;
                    mediaElement.Source = null;
                }
                else
                    await DisplayAlert("Aviso", "El video no se pudo guardar en la base de datos", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }


        }

        private async void btnlista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new View.Listado());
        }
    }
}
