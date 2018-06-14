﻿using System;
using System.Collections.Generic;
using Sakuno.ING.Localization;
#if NET461
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
#elif WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
#endif

namespace Sakuno.ING.Shell.Layout
{
    public sealed class LayoutWindowList : List<LayoutWindow> { }

    public class LayoutRoot
    {
        public LayoutWindow MainWindow { get; set; }
        public LayoutWindowList SubWindows { get; } = new LayoutWindowList();
        public LayoutWindow this[string viewId]
            => SubWindows.Find(x => x.Id == viewId);
    }

#if NET461
    [ContentProperty(nameof(Content))]
#elif WINDOWS_UWP
    [ContentProperty(Name = nameof(Content))]
#endif
    public class LayoutWindow
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DataTemplate Content { get; set; }
    }

    public class ViewPresenter : ContentControl
    {
        public ViewPresenter()
        {
            DefaultStyleKey = typeof(ViewPresenter);
        }

        internal static readonly DependencyProperty ViewSourceProperty
            = DependencyProperty.Register(nameof(ViewSource), typeof(IReadOnlyDictionary<string, Type>), typeof(ViewPresenter), new PropertyMetadata(null, (d, _) => ((ViewPresenter)d).UpdateContent()));

        private IReadOnlyDictionary<string, Type> ViewSource => (IReadOnlyDictionary<string, Type>)GetValue(ViewSourceProperty);

        private string _viewId;
        public string ViewId
        {
            get => _viewId;
            set
            {
                _viewId = value;
                UpdateContent();
            }
        }

        private void UpdateContent()
        {
            if (ViewSource != null && ViewSource.TryGetValue(ViewId, out Type viewType))
                Content = Activator.CreateInstance(viewType);
            else
                Content = null;
        }
    }

    public class LocalizedTitleExtension : MarkupExtension
    {
        public LocalizedTitleExtension(string viewId) => ViewId = viewId;

#if NET461
        [ConstructorArgument("viewId")]
#endif
        public string ViewId { get; set; }

        internal static ILocalizationService LocalizationService;
        internal static string GetViewTitle(string viewId)
            => LocalizationService?.GetLocalized("ViewTitle", viewId) ?? viewId;

#if NET461
        public override object ProvideValue(IServiceProvider serviceProvider)
            => GetViewTitle(ViewId);
#elif WINDOWS_UWP
        protected override object ProvideValue()
            => Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView()
                .GetString("/ViewTitle/" + ViewId);
#endif
    }

    public class ViewSwitcher : Button
    {
        private string _viewId;
        public string ViewId
        {
            get => _viewId;
            set
            {
                _viewId = value;
                Content = LocalizedTitleExtension.GetViewTitle(ViewId);
            }
        }

        internal const string SwitchActionKey = "ViewSwitchAction";

#if NET461
        protected override void OnClick()
#elif WINDOWS_UWP
        protected override void OnTapped(TappedRoutedEventArgs e)
#endif
        {
            if (ViewId != null && Application.Current.Resources[SwitchActionKey] is Action<string> action)
                action(ViewId);
#if WINDOWS_UWP
            e.Handled = true;
#endif
        }
    }
}
