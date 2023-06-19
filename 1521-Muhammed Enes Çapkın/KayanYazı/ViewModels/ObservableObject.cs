using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows.Input;

public class ObservableObject : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
	{
		if (PropertyChanged != null)
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
	    NotifyCanExecuteChanged(propertyName);
	}
	protected void NotifyCanExecuteChanged([CallerMemberName] string propertyName = null)
	{
		this.GetType()
			.GetProperties()
			.Where(p => typeof(ICommand).IsAssignableFrom(p.PropertyType))
			.Select(p => (ActionCommand)p.GetValue(this))
			.ToList()
			.ForEach(cmd => cmd?.RaiseCanExecuteChanged());
	}
}
