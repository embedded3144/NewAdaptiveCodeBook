using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chapter05._11_비동기데코레이터
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(IComponant component)
        {
            this.component = component;
        }

        public string Result
        {
            get
            {
                return result;
            }
            private set
            {
                if(result != value)
                {
                    result = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Result"));
                }
            }
        }

        public ICommand CalculateCommand
        {
            get
            {
                return calculateCommand;
            }
        }

        private void Calculate(object parameter)
        {
            Result = "처리 중...";
            component.Process();
            Result = "처리 완료!";
        }

        private string result;
        private IComponant component;
        private RelayCommand calculateCommand;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
