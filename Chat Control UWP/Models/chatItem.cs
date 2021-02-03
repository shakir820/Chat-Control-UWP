using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Chat_Control_UWP.Models
{
    public class chatItem: INotifyPropertyChanged
    {
        private chatItemType _itemType;
        private string text_msg;
        private BitmapImage image;

        public chatItemType ChatType
        {
            get { return _itemType; }
            set { _itemType = value; RaisePropertyChanged(); }
        }

        public string Id { get; set; }

        public string TextMsg
        {
            get { return text_msg; }
            set { text_msg = value; RaisePropertyChanged(); }
        }

        public BitmapImage Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }


    public enum chatItemType
    {
        text,
        image,
        file
    }

    

}
