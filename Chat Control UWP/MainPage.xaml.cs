using Chat_Control_UWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Chat_Control_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();
        }



        private ObservableCollection<chatItem> chatItems = new ObservableCollection<chatItem>();

        public ObservableCollection<chatItem> ChatList
        {
            get { return chatItems; }
            set { chatItems = value; RaisePropertyChanged(); }
        }

        

        private void addMoreBtn_Click(object sender, RoutedEventArgs e)
        {
            var flyoutOption = new FlyoutShowOptions();
            flyoutOption.ShowMode = FlyoutShowMode.Transient;
            Flyout.ShowAt(addMoreBtn, flyoutOption);
        }

        private void sendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            if(chatTextBox.Visibility == Visibility.Visible)
            {
                var new_chat = new chatItem();
                new_chat.ChatType = chatItemType.text;
                new_chat.Id = Guid.NewGuid().ToString();
                new_chat.TextMsg = chatTextBox.Text;
                if (new_chat.TextMsg != null && new_chat.TextMsg.Length > 0)
                {
                    ChatList.Add(new_chat);
                }
                chatTextBox.Text = "";
            }
            else
            {
                var img_items = imageCollectionGridView.ItemsSource as ObservableCollection<chatItem>;
                
                foreach(var item in img_items)
                {
                    ChatList.Add(item);
                }
                img_items.Clear();
            }
            

            chatTextBox.Visibility = Visibility.Visible;
            imageListGrid.Visibility = Visibility.Collapsed;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private async void InsertImage_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
           
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                chatTextBox.Visibility = Visibility.Collapsed;
                imageListGrid.Visibility = Visibility.Visible;

                var image_collection = new ObservableCollection<chatItem>();

                foreach(var item in files)
                {
                    var thumbnail = await item.GetThumbnailAsync(ThumbnailMode.PicturesView, 200, ThumbnailOptions.ResizeThumbnail);
                    if (thumbnail != null)
                    {
                        var image = new BitmapImage();
                        image.SetSource(thumbnail);

                        var _chatItem = new chatItem();
                        _chatItem.Id = Guid.NewGuid().ToString();
                        _chatItem.ChatType = chatItemType.image;
                        _chatItem.Image = image;

                        image_collection.Add(_chatItem);
                    }
                }

                imageCollectionGridView.ItemsSource = image_collection;
            }
            
        }

        private void DeleteBtnControl_DeleteImage(object sender, DeleteImageEventArg e)
        {
            var img_collection = imageCollectionGridView.ItemsSource as ObservableCollection<chatItem>;
            var selected_img = img_collection.FirstOrDefault(a => a.Id == e.ImageId);
            img_collection.Remove(selected_img);
            if(img_collection.Count == 0)
            {
                chatTextBox.Visibility = Visibility.Visible;
                imageListGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
