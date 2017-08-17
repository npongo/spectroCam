using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace spectra.camera.Helpers
{
    public class ButtonFocusBehavior:Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
            AssociatedObject.PointerEntered += AssociatedObject_PointerEntered;
            AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;

            // Insert code that you want to run when the Behavior is attached to an object.
        }

        private void AssociatedObject_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Got focus");
        }

        private void AssociatedObject_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            AssociatedObject.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void AssociatedObject_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            AssociatedObject.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void AssociatedObject_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AssociatedObject.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }   
}
