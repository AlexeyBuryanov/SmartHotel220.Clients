using CarouselView.FormsPlugin.UWP;
using FFImageLoading.Forms;
using FFImageLoading.Forms.WinUWP;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SmartHotel220.Clients.UWP
{
    /// <summary>
    /// Предоставляет поведение приложения для дополнения класса приложения по умолчанию.
    /// </summary>
    public sealed partial class App
    {
        /// <summary>
        /// Инициализирует объект приложения singleton. Это первая строка исполняемого кода,
        /// и как таковой логический эквивалент main() или WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// Вызывается, когда приложение запускается обычно конечным пользователем.
        /// Другие точки входа будут использоваться, например, при запуске приложения
        /// для открытия определенного файла.
        /// </summary>
        /// <param name="e">Подробная информация о запросе и процессе запуска.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            #if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
            #endif

            // Не повторяйте инициализацию приложения, когда в Окне уже есть контент, просто убедитесь, что окно активно
            if (!(Window.Current.Content is Frame rootFrame))
            {
                // Создаём фрейм, чтобы действовать как контекст навигации и перейти к первой странице
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                var assembliesToInclude = new List<Assembly>
                {
                    typeof(CachedImage).GetTypeInfo().Assembly,
                    typeof(CachedImageRenderer).GetTypeInfo().Assembly,
                    typeof(CarouselViewRenderer).GetTypeInfo().Assembly
                };

                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Состояние загрузки из ранее приостановленного приложения
                }

                // Помещаем фрейм в текущее окно
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Когда стек навигации не восстанавливается, переходим на первую страницу,
                // настроив новую страницу, передав необходимую информацию в качестве параметра навигации
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            // Убедитесь, что текущее окно активно
            Window.Current.Activate();
        }

        /// <summary>
        /// Вызывается при ошибочной навигации
        /// </summary>
        /// <param name="sender">Frame, который не прошел навигацию</param>
        /// <param name="e">Подробная информация о сбое в навигации</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Не удалось загрузить страницу " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Вызывается, когда выполнение приложения приостанавливается. Состояние приложения сохраняется,
        /// не зная, будет ли приложение завершено или возобновлено с сохранением сохраненной памяти.
        /// </summary>
        /// <param name="sender">Источник запроса на приостановку.</param>
        /// <param name="e">Подробная информация о запросе на приостановку.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            // TODO: Сохранить состояние приложения и остановить любую фоновую активность
            deferral.Complete();
        }
    }
}
