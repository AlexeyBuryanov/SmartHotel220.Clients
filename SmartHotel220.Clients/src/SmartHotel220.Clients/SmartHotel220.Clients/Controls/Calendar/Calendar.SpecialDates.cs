using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace SmartHotel220.Clients.Core.Controls
{
    // Календарь. Специальные даты.
    public partial class Calendar
    {
        #region Специальные даты

        public static readonly BindableProperty SpecialDatesProperty =
            BindableProperty.Create(nameof(SpecialDates), typeof(IEnumerable<SpecialDate>), typeof(Calendar), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (oldValue != null)
                    {
                        if (oldValue is INotifyCollectionChanged observableCollection)
                        {
                            observableCollection.CollectionChanged -= (s, e) =>
                            {
                                var newItems = e.NewItems;
                                if (newItems != null)
                                {
                                    foreach (SpecialDate sd in newItems)
                                    {
                                        var buttons = (bindable as Calendar)?._buttons;
                                        var button = buttons.FirstOrDefault(d => d.Date.Value.Date == sd.Date.Date);
                                        (bindable as Calendar)?.SetButtonSpecial(button, sd);
                                    }
                                }

                                var oldItems = e.OldItems;
                                if (oldItems != null)
                                {
                                    foreach (SpecialDate sd in newItems)
                                    {
                                        (bindable as Calendar)?.ResetButton(((Calendar)bindable)._buttons.FirstOrDefault(d => d.Date.Value == sd.Date));
                                    }
                                }
                            };
                        } // if
                    } // if

                    if (newValue != null)
                    {
                        if (newValue is INotifyCollectionChanged observableCollection)
                        {
                            observableCollection.CollectionChanged += (s, e) =>
                            {
                                var newItems = e.NewItems;
                                if (newItems != null)
                                {
                                    foreach (SpecialDate sd in newItems)
                                    {
                                        var buttons = (bindable as Calendar)?._buttons;
                                        var button = buttons.Where(d => d.Date.HasValue).FirstOrDefault(d => d.Date.Value.Date == sd.Date.Date);
                                        (bindable as Calendar)?.SetButtonSpecial(button, sd);
                                    }
                                }

                                var oldItems = e.OldItems;
                                if (oldItems != null)
                                {
                                    foreach (SpecialDate sd in oldItems)
                                    {
                                        (bindable as Calendar)?.ResetButton(((Calendar)bindable)._buttons.FirstOrDefault(d => d.Date.Value == sd.Date));
                                    }
                                } // if
                            };
                        } // if
                    } // if
                });
        
        public IEnumerable<SpecialDate> SpecialDates
		{
			get => (IEnumerable<SpecialDate>)GetValue(SpecialDatesProperty);
            set => SetValue(SpecialDatesProperty, value);
        }

		#endregion

        /// <summary>
        /// Изменить календарь
        /// </summary>
		public void RaiseSpecialDatesChanged()
		{
			ChangeCalendar(CalandarChanges.MaxMin);
		}

        /// <summary>
        /// Установить специальную кнопку
        /// </summary>
		protected void SetButtonSpecial(CalendarButton button, SpecialDate special)
		{
            if (button != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    button.BackgroundPattern = special.BackgroundPattern;
                    button.BackgroundImage = special.BackgroundImage;

                    if (special.FontSize.HasValue) button.FontSize = special.FontSize.Value;
                    if (special.BorderWidth.HasValue) button.BorderWidth = special.BorderWidth.Value;
                    if (special.BorderColor.HasValue) button.BorderColor = special.BorderColor.Value;
                    if (special.BackgroundColor.HasValue) button.BackgroundColor = special.BackgroundColor.Value;
                    if (special.TextColor.HasValue) button.TextColor = special.TextColor.Value;
                    if (special.FontAttributes.HasValue) button.FontAttributes = special.FontAttributes.Value;
                    if (!string.IsNullOrEmpty(special.FontFamily)) button.FontFamily = special.FontFamily;

                    button.IsEnabled = special.Selectable;
                });
            } // if
		}
	}
}
