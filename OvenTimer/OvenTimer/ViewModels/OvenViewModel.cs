using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using OvenTimer.Models;
using Xamarin.Forms;

namespace OvenTimer.ViewModels
{
    public class OvenViewModel : INotifyPropertyChanged
    {
        readonly IList<Oven> source;
        
        public ObservableCollection<Oven> Ovens { get; private set; }
        public IList<Oven> EmptyMonkeys { get; private set; }

        public Oven PreviousMonkey { get; set; }
        public Oven CurrentMonkey { get; set; }
        public Oven CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand ItemChangedCommand => new Command<Oven>(ItemChanged);
        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        public ICommand DeleteCommand => new Command<Oven>(RemoveMonkey);
        public ICommand FavoriteCommand => new Command<Oven>(FavoriteMonkey);

        public OvenViewModel()
        {
            source = new List<Oven>();
            CreateMonkeyCollection();

            CurrentItem = Ovens.Skip(3).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 3;
            OnPropertyChanged("Position");
        }

        void CreateMonkeyCollection()
        {
            source.Add(new Oven
            {
                Name = "두꺼운 쿠키오븐",
                OvenNo = 1,
                ImageUrl = "dev1.jpg"
            });
            source.Add(new Oven
            {
                Name = "10단 쿠키오븐",
                OvenNo = 2,
                ImageUrl = "dev2.jpg"
            });
            source.Add(new Oven
            {
                Name = "경주빵 오븐",
                OvenNo = 3,
                ImageUrl = "dev3.jpg"
            });
            source.Add(new Oven
            {
                Name = "찰보리 오븐",
                OvenNo = 4,
                ImageUrl = "dev4.jpg"
            });

            Ovens = new ObservableCollection<Oven>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(monkey => monkey.Name.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var monkey in source)
            {
                if (!filteredItems.Contains(monkey))
                {
                    Ovens.Remove(monkey);
                }
                else
                {
                    if (!Ovens.Contains(monkey))
                    {
                        Ovens.Add(monkey);
                    }
                }
            }
        }

        void ItemChanged(Oven item)
        {
            PreviousMonkey = CurrentMonkey;
            CurrentMonkey = item;
            OnPropertyChanged("PreviousMonkey");
            OnPropertyChanged("CurrentMonkey");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveMonkey(Oven monkey)
        {
            if (Ovens.Contains(monkey))
            {
                Ovens.Remove(monkey);
            }
        }

        void FavoriteMonkey(Oven monkey)
        {
            monkey.IsFavorite = !monkey.IsFavorite;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
