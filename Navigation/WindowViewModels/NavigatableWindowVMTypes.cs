﻿namespace supermarket.Navigation.WindowViewModels
{
    public enum Main
    {
        ManagerEmployees,
        ManagerProducts,
        ManagerStoreProducts,
        ManagerCategories,
        ManagerCustomers,
        ManagerReceipts,
        ManagerSales,

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
        AddPromStoreProduct,
        AddNonPromStoreProduct,
        
        EditStoreProduct
    }
    
    public enum ManagerReceipts
    {
        DetailsReceipt
    }

    public enum CashierReceipts
    {
        DetailsReceipt,
        AddReceipt
    }
}
