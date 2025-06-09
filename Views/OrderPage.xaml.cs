using MedicineClient.ViewModels;
using MedicineClient.Models;
namespace MedicineClient.Views;

public partial class OrderPage : ContentPage
{
    public OrderPage(OrderPageViewModel vm, Medicine selectedMedicine)
    {
        InitializeComponent();
        vm.Initialize(selectedMedicine);
        BindingContext = vm;
    }
}