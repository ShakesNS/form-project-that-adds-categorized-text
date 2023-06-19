using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

public class ViewModel : ObservableObject, IDataErrorInfo
{

    #region static Properties

    private static ControlTemplate errorTemplate;
    public static ControlTemplate ErrorTemplate
    {
        get
        {
            if (errorTemplate == null)
            {
                try
                {
                    ResourceDictionary dictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/ViewModels/ValidationResourceDictionary.xaml", UriKind.Relative));
                    errorTemplate = dictionary["ErrorTemplate"] as ControlTemplate;
                }
                catch { }
            }
            return errorTemplate;
        }
        private set { errorTemplate = value; }
    }

    #endregion


    #region static Methods

    public static void ClearValidationErrors(DependencyObject dependencyObject)
    {
        if (dependencyObject == null)
            return;

        foreach (var validationError in Validation.GetErrors(dependencyObject))
        {
            if (validationError.BindingInError is BindingExpression)
                Validation.ClearInvalid((BindingExpression)validationError.BindingInError);
            else if (validationError.BindingInError is MultiBindingExpression)
                Validation.ClearInvalid((MultiBindingExpression)validationError.BindingInError);
        }

        if (dependencyObject is Panel)
        {
            foreach (var child in ((Panel)dependencyObject).Children.Cast<DependencyObject>())
                ClearValidationErrors(child);
        }
        else if (dependencyObject is ContentControl)
        {
            ClearValidationErrors(((ContentControl)dependencyObject).Content as DependencyObject);
        }
        else if (dependencyObject is Decorator)
        {
            ClearValidationErrors(((Decorator)dependencyObject).Child as DependencyObject);
        }
    }

    public static void SetErrorTemplate(DependencyObject dependencyObject)
    {
        if (dependencyObject == null || ErrorTemplate == null)
            return;

        Validation.SetErrorTemplate(dependencyObject, ErrorTemplate);

        if (dependencyObject is Panel)
        {
            foreach (var child in ((Panel)dependencyObject).Children.Cast<DependencyObject>())
                SetErrorTemplate(child);
        }
        else if (dependencyObject is ContentControl)
        {
            SetErrorTemplate(((ContentControl)dependencyObject).Content as DependencyObject);
        }
        else if (dependencyObject is Decorator)
        {
            SetErrorTemplate(((Decorator)dependencyObject).Child as DependencyObject);
        }
    }

    #endregion


    #region Properties

    public string Error
    {
        get { return null; }
    }

    public string this[string columnName]
    {
        get { return OnValidate(columnName); }
    }

    public bool IsValid
    {
        get
        {
            return this.GetType().GetProperties().All(p => OnValidate(p.Name) == null);
        }
    }

    #endregion


    #region Methods

    protected virtual string OnValidate(string propertyName)
    {
        if (propertyName == "Item" || propertyName == "IsValid")
            return null;
        var context = new ValidationContext(this)
        {
            MemberName = propertyName
        };
        Collection<System.ComponentModel.DataAnnotations.ValidationResult> results = new Collection<System.ComponentModel.DataAnnotations.ValidationResult>();
        //bool isValid = Validator.TryValidateObject(this, context, results, true);
        var propertyInfo = this.GetType().GetProperty(propertyName);
        if (propertyInfo == null)
            return null;
        bool isValid = Validator.TryValidateProperty(propertyInfo.GetValue(this), context, results);
        if (!isValid)
        {
            //System.ComponentModel.DataAnnotations.ValidationResult result = results.SingleOrDefault(p => p.MemberNames.Any(memberName => memberName == propertyName));
            //return result?.ErrorMessage;
            return results.FirstOrDefault()?.ErrorMessage;
        }
        return null;
    }

    #endregion

}
