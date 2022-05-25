namespace supermarket.Navigation.WindowVM
{
    public enum Main
    {
        ManagerEmployees,
        ManagerProducts,
        ManagerStoreProducts,
        ManagerCategories,
        ManagerCustomers,

        CashierChecks,
        CashierProducts,
        CashierStoreProducts,
        CashierCategories,
        CashierCustomers
    }

    public enum ManagerEmployees
    {
        AddEmployee,
        EditEmployee,
    }

    public enum ManagerCustomers
    {
        AddCustomer,
        EditCustomer,
    }

    public enum ManagerCategories
    {
        AddCategory,
        EditCategory,
    }

    public enum ManagerProducts
    {
        AddProduct,
        EditProduct,
    }

    public enum ManagerStoreProducts
    {
        AddStoreProduct,
        EditStoreProduct,
    }
}
