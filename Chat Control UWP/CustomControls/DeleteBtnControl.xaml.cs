using Chat_Control_UWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Chat_Control_UWP.CustomControls
{
    public sealed partial class DeleteBtnControl : UserControl
    {
        public DeleteBtnControl()
        {
            this.InitializeComponent();
        }


        public event EventHandler<DeleteImageEventArg> DeleteImage;

        public string ChatItemId
        {
            get { return (string)GetValue(ChatItemIdProperty); }
            set { SetValue(ChatItemIdProperty, value); }
        }

      
        public static readonly DependencyProperty ChatItemIdProperty =
            DependencyProperty.Register("ChatItemId", typeof(string), typeof(DeleteBtnControl), new PropertyMetadata(null));

        private void onDeleteBtnClicked(object sender, RoutedEventArgs e)
        {
            DeleteImage?.Invoke(this, new DeleteImageEventArg { ImageId = ChatItemId });
        }
    }
}
