using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Views.Templates
{
    public partial class NotificationDetailItemTemplate
    {
        /// <summary>
        /// Команда удаления, св-во привязки
        /// </summary>
        public static readonly BindableProperty DeleteCommandProperty =
               BindableProperty.Create(
                   "DeleteCommand",
                   typeof(ICommand),
                   typeof(NotificationDetailItemTemplate),
                   default(ICommand));

        /// <summary>
        /// Команда удаления
        /// </summary>
        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public NotificationDetailItemTemplate()
        {
            InitializeComponent();

            var tapGesture = new TapGestureRecognizer
            {
                Command = new Command(OnDeleteTapped)
            };

            // По тапу на картинке удаления произвести удаление
            DeleteImage.GestureRecognizers.Add(tapGesture);
            InitializeCell();
        }

        /// <summary>
        /// Команда для исполнения удаления с анимацией
        /// </summary>
        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var isUwp = Device.RuntimePlatform == Device.UWP;
                    DeleteContainer.BackgroundColor = Color.FromHex("#EC0843");
                    DeleteImage.Source =  isUwp ? "Assets/ic_paperbin.png" : "ic_paperbin";

                    // Отыгрываем анимацию
                    await this.TranslateTo(-Width, 0, 500, Easing.SinIn);

                    // Выполняем удаление
                    DeleteCommand?.Execute(BindingContext);

                    // Инициализируем ячейку
                    InitializeCell();
                });
            }
        }

        /// <summary>
        /// При удалении (тап-жест)
        /// </summary>
        private void OnDeleteTapped()
        {
            TransitionCommand.Execute(null);
        }

        private void InitializeCell()
        {
            var isUwp = Device.RuntimePlatform == Device.UWP;
            // Координаты по умолчанию
            TranslationX = 0;
            // Картинка по умолчанию
            DeleteContainer.BackgroundColor = Color.FromHex("#F2F2F2");
            DeleteImage.Source = isUwp ? "Assets/ic_paperbin_red.png" : "ic_paperbin_red";
        }
    }
}